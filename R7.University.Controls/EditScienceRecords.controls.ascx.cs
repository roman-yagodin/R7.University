﻿using System.Web.UI.WebControls;
using DotNetNuke.UI.UserControls;

namespace R7.University.Controls
{
    public partial class EditScienceRecords
    {
        protected DropDownList comboScienceRecordType;
        protected Label labelScienceRecordTypeHelp;
        protected LabelControl labelValue1;
        protected LabelControl labelValue2;
        protected TextEditor textDescription;
        protected TextBox textValue1;
        protected TextBox textValue2;
        protected Panel panelDescription;
        protected Panel panelValue1;
        protected Panel panelValue2;
        protected HiddenField hiddenScienceRecordTypeID;
        protected RequiredFieldValidator valValue1Required;
        protected RequiredFieldValidator valValue2Required;
        protected RangeValidator valValue1Range;
        protected RangeValidator valValue2Range;
        protected RequiredFieldValidator valDescriptionRequired;
    }
}