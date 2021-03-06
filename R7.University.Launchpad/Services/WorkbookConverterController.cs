using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using DotNetNuke.Services.Exceptions;
using DotNetNuke.Web.Api;
using R7.University.Templates;
using R7.University.Launchpad.ViewModels;
using R7.University.Models;
using R7.University.Employees.Queries;

namespace R7.University.Launchpad.Services
{
    public class WorkbookConverterController: DnnApiController
    {
        [HttpPost]
        [DnnAuthorize]
        [ValidateAntiForgeryToken]
        public async Task<HttpResponseMessage> Upload ()
        {
            try {
                var provider = new MultipartMemoryStreamProvider ();
                await Request.Content.ReadAsMultipartAsync (provider);

                var results = new List<WorkbookConverterUploadResult> ();
                foreach (var contents in provider.Contents) {
                    var fileBytes = await contents.ReadAsByteArrayAsync ();

                    var guid = StoreUploadedFile (fileBytes);

                    // extract filename
                    var fileNameParam = contents.Headers.ContentDisposition.Parameters
                        .FirstOrDefault (p => p.Name.ToLower () == "filename");

                    var fileName = (fileNameParam == null) ? "" : fileNameParam.Value.Trim ('"');

                    results.Add (new WorkbookConverterUploadResult {
                        FileName = fileName,
                        Guid = guid
                    });
                }

                return Request.CreateResponse (HttpStatusCode.OK, results);
            }
            catch (Exception ex) {
                Exceptions.LogException (ex);
                return Request.CreateErrorResponse (HttpStatusCode.InternalServerError, ex);
            }
        }

        string StoreUploadedFile (byte [] fileBytes)
        {
            var guid = Guid.NewGuid ().ToString ();
            var tempFilePath = Path.Combine (Path.GetTempPath (), guid);
            File.WriteAllBytes (tempFilePath, fileBytes);

            return guid;
        }

        [HttpGet]
        [DnnAuthorize]
        public HttpResponseMessage Convert (string fileName, string guid, string format)
        {
            try {
                if (!format.Contains ("CSV")) {
                    return Request.CreateResponse (HttpStatusCode.BadRequest);
                }

                var text = GetWorkbookText (Path.Combine (Path.GetTempPath (), guid), format);

                var result = Request.CreateResponse (HttpStatusCode.OK);
                result.Content = new StringContent (text, Encoding.UTF8, "text/plain");
                result.Content.Headers.ContentType = new MediaTypeHeaderValue ("text/plain");
                result.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue ("attachment") {
                    FileName = Path.GetFileNameWithoutExtension (fileName) + ".txt"
                };

                return result;
            }
            catch (Exception ex) {
                Exceptions.LogException (ex);
                return Request.CreateErrorResponse (HttpStatusCode.InternalServerError, ex);
            }
        }
        [HttpGet]
        [DnnAuthorize]
        public HttpResponseMessage ConvertOriginal (string fileName, string guid, string format)
        {
            try {
                if (!format.Contains ("CSV")) {
                    return Request.CreateResponse (HttpStatusCode.BadRequest);
                }

                var filePath = Path.Combine (Path.GetTempPath (), guid);

                var text = FindAndConvertOriginal (filePath, format);

                if (string.IsNullOrEmpty (text)) {
                    return Request.CreateResponse (HttpStatusCode.NotFound);
                }

                var result = Request.CreateResponse (HttpStatusCode.OK);
                result.Content = new StringContent (text, Encoding.UTF8, "text/plain");
                result.Content.Headers.ContentType = new MediaTypeHeaderValue ("text/plain");
                result.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue ("attachment") {
                    FileName = Path.GetFileNameWithoutExtension (fileName) + "_0.txt"
                };

                return result;
            }
            catch (Exception ex) {
                Exceptions.LogException (ex);
                return Request.CreateErrorResponse (HttpStatusCode.InternalServerError, ex);
            }
        }

        string FindAndConvertOriginal (string filePath, string format)
        {
            var workbookManager = new WorkbookManager ();
            var serializer = workbookManager.GetWorkbookSerializer (format);

            var bookInfo = workbookManager.ReadWorkbookInfo (filePath);
            if (bookInfo.EntityId != null && !string.IsNullOrEmpty (bookInfo.EntityType)) {
                var employee = GetEmployee (bookInfo.EntityId.Value);
                if (employee != null) {
                    return GetEmployeeCsvText (employee, serializer);
                }
            }
            return null;
        }

        string GetWorkbookText (string tempFilePath, string format)
        {
            var workbookManager = new WorkbookManager ();
            return workbookManager.SerializeWorkbook (tempFilePath, workbookManager.GetWorkbookSerializer (format));
        }

        string GetEmployeeCsvText (IEmployee employee, IWorkbookSerializer serializer)
        {
            var templateEngine = new UniversityTemplateEngineManager ()
                .GetEmployeeTemplateEngine (employee, PortalSettings);

            return templateEngine.ApplyAndSerialize (UniversityTemplateHelper.GetLocalizedEmployeeTemplatePath (), serializer).ToString ();
        }

        // TODO: Don't reference EmployeeQuery from R7.University.Employees assembly
        IEmployee GetEmployee (int employeeId)
        {
            using (var modelContext = new UniversityModelContext ()) {
                return new EmployeeQuery (modelContext).SingleOrDefault (employeeId);
            }
        }
    }
}
