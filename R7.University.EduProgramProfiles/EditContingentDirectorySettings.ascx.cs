//
//  EditContingentDirectorySettings.ascx.cs
//
//  Author:
//       Roman M. Yagodin <roman.yagodin@gmail.com>
//
//  Copyright (c) 2017-2018 Roman M. Yagodin
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

using System;
using R7.Dnn.Extensions.Controls;
using R7.Dnn.Extensions.ViewModels;
using R7.University.EduProgramProfiles.Models;
using R7.University.EduProgramProfiles.Modules;

namespace R7.University.EduProgramProfiles
{
    public partial class EditContingentDirectorySettings: EditDirectorySettingsBase<ContingentDirectorySettings>
    {
        protected override void OnInit (EventArgs e)
        {
            base.OnInit (e);

            comboMode.DataSource = EnumViewModel<ContingentDirectoryMode>.GetValues (ViewModelContext, false);
            comboMode.DataBind ();
        }

        protected override void OnLoadSettings ()
        {
            comboMode.SelectByValue (Settings.Mode);
        }

        protected override void OnUpdateSettings ()
        {
            Settings.Mode = (ContingentDirectoryMode) Enum.Parse (typeof (ContingentDirectoryMode), comboMode.SelectedValue);
        }
    }
}

