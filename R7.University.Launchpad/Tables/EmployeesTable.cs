//
//  EmployeesTable.cs
//
//  Author:
//       Roman M. Yagodin <roman.yagodin@gmail.com>
//
//  Copyright (c) 2014-2016 Roman M. Yagodin
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

using System.Collections.Generic;
using System.Data;
using System.Linq;
using DotNetNuke.Entities.Modules;
using R7.University.Components;
using R7.University.Data;
using R7.University.Launchpad.ViewModels;

namespace R7.University.Launchpad
{
    public class EmployeesTable: LaunchpadTableBase
    {
        public EmployeesTable () : base ("Employees")
        {
        }

        public override DataTable GetDataTable (PortalModuleBase module, UniversityDbRepository repository, string search)
        {
            var employees = UniversityRepository.Instance.DataProvider.FindObjects<EmployeeInfo> (
                @"WHERE CONCAT([LastName], ' ', [FirstName], ' ', [OtherName], ' ',
                [Phone], ' ', [CellPhone], ' ', [Fax], ' ', 
                [Email], ' ', [SecondaryEmail], ' ', [WebSite], ' ',
                [WorkingHours]) LIKE N'%{0}%'", search, false);
            
            return DataTableConstructor.FromIEnumerable (employees.Select (e => new EmployeeViewModel (e)));
        }
    }
}
