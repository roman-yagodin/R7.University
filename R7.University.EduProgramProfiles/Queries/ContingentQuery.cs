//
//  ContingentQuery.cs
//
//  Author:
//       Roman M. Yagodin <roman.yagodin@gmail.com>
//
//  Copyright (c) 2017-2019 Roman M. Yagodin
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

using System.Collections.Generic;
using System.Linq;
using R7.Dnn.Extensions.Models;
using R7.University.Models;
using R7.University.Queries;

namespace R7.University.EduProgramProfiles.Queries
{
    public class ContingentQuery
    {
        protected readonly IModelContext ModelContext;

        public ContingentQuery (IModelContext modelContext)
        {
            ModelContext = modelContext;
        }

        public IEnumerable<EduProgramProfileFormYearInfo> ListByDivisionAndEduLevels (IEnumerable<int> eduLevelIds,
                                                                                      int? divisionId,
                                                                                      DivisionLevel divisionLevel)
        {
            return ModelContext.Query<EduProgramProfileFormYearInfo> ()
                               .IncludeEduProgramProfileWithEduProgramAndDivisions ()
                               .Include2 (eppfy => eppfy.EduForm)
                               .Include2 (eppfy => eppfy.Contingent)
                               .Include2 (eppfy => eppfy.Year)
                               .WhereEduLevelsOrAll (eduLevelIds)
                               .WhereDivisionOrAll (divisionId, divisionLevel)
                               .DefaultOrder ()
                               .ThenByDescending (ev => ev.Year != null ? ev.Year.Year : int.MaxValue)
                               .ToList ();
        }
    }
}
