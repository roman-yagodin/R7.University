﻿//
//  EduProgramStandardObrnadzorViewModel.cs
//
//  Author:
//       Roman M. Yagodin <roman.yagodin@gmail.com>
//
//  Copyright (c) 2015-2016 Roman M. Yagodin
//
//  This program is free software: you can redistribute it and/or modify
//  it under the terms of the GNU General Public License as published by
//  the Free Software Foundation, either version 3 of the License, or
//  (at your option) any later version.
//
//  This program is distributed in the hope that it will be useful,
//  but WITHOUT ANY WARRANTY; without even the implied warranty of
//  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//  GNU General Public License for more details.
//
//  You should have received a copy of the GNU General Public License
//  along with this program.  If not, see <http://www.gnu.org/licenses/>.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DotNetNuke.Common;
using DotNetNuke.Services.Localization;
using R7.DotNetNuke.Extensions.ViewModels;
using R7.University.ModelExtensions;
using R7.University.Models;
using R7.University.ViewModels;

namespace R7.University.EduProgramDirectory
{
    public class EduProgramStandardObrnadzorViewModel: EduProgramViewModelBase
    {
        public IIndexer Indexer { get; protected set; }

        public ViewModelContext Context { get; protected set; }

        public EduProgramStandardObrnadzorViewModel (IEduProgram model, ViewModelContext context, IIndexer indexer)
            : base (model)
        {
            Context = context;
            Indexer = indexer;
        }

        [Obsolete ("Use FormatHelper.FormatDocumentLinks instead")]
        protected string FormatDocumentLinks (IEnumerable<IDocument> documents, string microdata, DocumentGroupPlacement groupPlacement, GetDocumentTitle getDocumentTitle = null)
        {
            var markupBuilder = new StringBuilder ();
            var count = 0;
            foreach (var document in documents) {
                var linkMarkup = document.FormatDocumentLink_WithMicrodata (
                    (getDocumentTitle == null)? document.Title : getDocumentTitle (document),
                    Localization.GetString ("LinkOpen.Text", Context.LocalResourceFile),
                    true,
                    groupPlacement,
                    Context.Module.TabId,
                    Context.Module.ModuleId,
                    microdata
                );

                if (!string.IsNullOrEmpty (linkMarkup)) {
                    markupBuilder.Append ("<li>" + linkMarkup + "</li>");
                    count++;
                }
            }

            var markup = markupBuilder.ToString ();
            if (!string.IsNullOrEmpty (markup)) {
                return ((count == 1)? "<ul class=\"list-inline\">" : "<ul>") + markup + "</ul>";
            }

            return string.Empty;
        }

        protected IEnumerable<IDocument> GetDocuments (IEnumerable<IDocument> documents)
        {
            return documents
                .Where (d => Context.Module.IsEditable || d.IsPublished ())
                .OrderBy (d => d.Group)
                .ThenBy (d => d.SortIndex);
        }

        public int Order 
        {
            get { return Indexer.GetNextIndex (); }
        }

        public string Title_Link
        {
            get {
                if (!string.IsNullOrWhiteSpace (Model.HomePage)) {
                    return string.Format ("<a href=\"{0}\" target=\"_blank\">{1}</a>",
                        Globals.NavigateURL (int.Parse (Model.HomePage)),
                        Model.Title);
                }

                return Model.Title;
            }
        }

        public string EduLevel_String
        {
            get { return FormatHelper.FormatShortTitle (EduLevel.ShortTitle, EduLevel.Title); }
        }

        public string EduStandard_Links
        {
            get { 
                return FormatDocumentLinks (
                    GetDocuments (Model.GetDocumentsOfType (SystemDocumentType.EduStandard)),
                    "itemprop=\"EduStandartDoc\"",
                    DocumentGroupPlacement.InTitle
                );
            }
        }

        public string ProfStandard_Links
        {
            get { 
                return FormatDocumentLinks (
                    GetDocuments (Model.GetDocumentsOfType (SystemDocumentType.ProfStandard)),
                    string.Empty,
                    DocumentGroupPlacement.InTitle
                );
            }
        }
    }
}

