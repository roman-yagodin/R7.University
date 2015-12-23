﻿//
// EduProgramProfileExtensions.cs
//
// Author:
//       Roman M. Yagodin <roman.yagodin@gmail.com>
//
// Copyright (c) 2015 Roman M. Yagodin
//
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
//
// The above copyright notice and this permission notice shall be included in
// all copies or substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
// THE SOFTWARE.

using System;
using System.Linq;
using System.Collections.Generic;
using DotNetNuke.R7;

namespace R7.University.ModelExtensions
{
    public static class EduProgramProfileExtensions
    {
        public static EduProgramProfileInfo WithEduProgram (
            this EduProgramProfileInfo eduProfile, ControllerBase controller)
        {
            eduProfile.EduProgram = controller.Get<EduProgramInfo> (eduProfile.EduProgramID);

            return eduProfile;
        }

        public static IEnumerable<EduProgramProfileInfo> WithEduPrograms (
            this IEnumerable<EduProgramProfileInfo> eduProgramProfiles, ControllerBase controller)
        {
            var eduPrograms = controller.GetObjects<EduProgramInfo> ();

            return eduProgramProfiles.Join (eduPrograms, epp => epp.EduProgramID, ep => ep.EduProgramID, 
                delegate (EduProgramProfileInfo epp, EduProgramInfo ep) {
                    epp.EduProgram = ep;
                    return epp;
                }
            );
        }

        public static IEnumerable<EduProgramProfileInfo> WithEduLevel (
            this IEnumerable<EduProgramProfileInfo> eduProgramProfiles, ControllerBase controller)
        {
            foreach (var eduProgramProfile in eduProgramProfiles) {
                eduProgramProfile.EduProgram.WithEduLevel (controller);
                yield return eduProgramProfile;
            }
        }

        public static EduProgramProfileInfo WithEduProgramProfileForms (
            this EduProgramProfileInfo eduProfile, ControllerBase controller)
        {
            eduProfile.EduProgramProfileForms = controller.GetObjects<EduProgramProfileFormInfo> (
                "WHERE [EduProgramProfileID] = @0", eduProfile.EduProgramProfileID)
                .WithEduForms (controller)
                .Cast<IEduProgramProfileForm> ()
                .ToList ();

            return eduProfile;
        }

        public static IEnumerable<EduProgramProfileInfo> WithEduProgramProfileForms (
            this IEnumerable<EduProgramProfileInfo> eduProgramProfiles, ControllerBase controller)
        {
            foreach (var eduProgramProfile in eduProgramProfiles) {
                yield return eduProgramProfile.WithEduProgramProfileForms (controller);
            }
        }

        public static IEduProgramProfile WithDocuments (
            this IEduProgramProfile eduProgramProfile, ControllerBase controller)
        {
            eduProgramProfile.Documents = controller.GetObjects<DocumentInfo> (
                "WHERE [ItemID] = @0", "EduProgramProfileID=" + eduProgramProfile.EduProgramProfileID)
                .Cast<IDocument> ()
                .ToList ();
            
            eduProgramProfile.Documents.WithDocumentType (controller);

            return eduProgramProfile;
        }

        public static IEnumerable<IEduProgramProfile> WithDocuments (
            this IEnumerable<IEduProgramProfile> eduProgramProfiles, ControllerBase controller)
        {
            foreach (var eduProgramProfile in eduProgramProfiles) {
                yield return eduProgramProfile.WithDocuments (controller);
            }
        }
    }
}
