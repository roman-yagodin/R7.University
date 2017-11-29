﻿using System.Web.UI.WebControls;
using DotNetNuke.Web.UI.WebControls;

namespace R7.University.Controls
{
    public partial class EditEduFormYears
    {
        protected DropDownList comboYear;
        protected RadioButtonList radioEduForm;
        protected TextBox textTimeToLearnYears;
        protected TextBox textTimeToLearnMonths;
        protected TextBox textTimeToLearnHours;
        protected HiddenField hiddenEduFormID;
        protected DnnDateTimePicker datetimeStartDate;
        protected DnnDateTimePicker datetimeEndDate;
    }
}
