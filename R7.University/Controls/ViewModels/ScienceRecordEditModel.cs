﻿//
//  ScienceRecordEditModel.cs
//
//  Author:
//       Roman M. Yagodin <roman.yagodin@gmail.com>
//
//  Copyright (c) 2017 Roman M. Yagodin
//
//  This program is free software: you can redistribute it and/or modify
//  it under the terms of the GNU Affero General Public License as published by
//  the Free Software Foundation, either version 3 of the License, or
//  (at your option) any later version.
//
//  This program is distributed in the hope that it will be useful,
//  but WITHOUT ANY WARRANTY; without even the implied warranty of
//  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//  GNU Affero General Public License for more details.
//
//  You should have received a copy of the GNU Affero General Public License
//  along with this program.  If not, see <http://www.gnu.org/licenses/>.

using System.Web;
using Newtonsoft.Json;
using R7.Dnn.Extensions.Utilities;
using R7.Dnn.Extensions.ViewModels;
using R7.University.ModelExtensions;
using R7.University.Models;

namespace R7.University.Controls.ViewModels
{
    public class ScienceRecordEditModel : EditModelBase<ScienceRecordInfo>, IScienceRecordWritable
    {
        #region EditModelBase implementation

        [JsonIgnore]
        public override bool IsPublished => EduProgram.IsPublished (HttpContext.Current.Timestamp);

        public override IEditModel<ScienceRecordInfo> Create (ScienceRecordInfo model, ViewModelContext context)
        {
            return CopyCstor.New<ScienceRecordEditModel, IScienceRecordWritable> (model);
        }

        public override ScienceRecordInfo CreateModel ()
        {
            return CopyCstor.New<ScienceRecordInfo, IScienceRecordWritable> (this);
        }

        public override void SetTargetItemId (int targetItemId, string targetItemKey)
        {
            EduProgramId = targetItemId;
        }

        #endregion

        #region IScienceRecordWritable implementation

        public long ScienceRecordId { get; set; }

        public int EduProgramId { get; set; }

        [JsonIgnore]
        public EduProgramInfo EduProgram { get; set; }

        public int ScienceRecordTypeId { get; set; }

        [JsonIgnore]
        public ScienceRecordTypeInfo ScienceRecordType { get; set; }

        public string Description { get; set; }

        public decimal? Value1 { get; set; }

        public decimal? Value2 { get; set; }

        #endregion
    }
}
