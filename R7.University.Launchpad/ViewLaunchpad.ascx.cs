using System;
using System.Data;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using DotNetNuke.Entities.Modules;
using DotNetNuke.Entities.Modules.Actions;
using DotNetNuke.Services.Exceptions;
using R7.Dnn.Extensions.Modules;
using R7.University.Launchpad.Models;
using R7.University.Models;
using R7.University.Security;

namespace R7.University.Launchpad
{
    public partial class ViewLaunchpad : PortalModuleBase<LaunchpadSettings>, IActionable
    {
        #region Properties

        protected LaunchpadTables Tables = new LaunchpadTables ();

        #endregion

        #region Model context

        private UniversityModelContext modelContext;
        protected UniversityModelContext ModelContext
        {
            get { return modelContext ?? (modelContext = new UniversityModelContext ()); }
        }

        public override void Dispose ()
        {
            if (modelContext != null) {
                modelContext.Dispose ();
            }

            base.Dispose ();
        }

        #endregion

        private ISecurityContext securityContext;
        public ISecurityContext SecurityContext
        {
            get { return securityContext ?? (securityContext = new ModuleSecurityContext (UserInfo)); }
        }

        #region Multiview fixes

        /// <summary>
        /// Makes tab selected by its name.
        /// </summary>
        /// <param name="tabName">Tab name.</param>
        protected void SelectTab (string tabName)
        {
            // enumerate all repeater items
            foreach (RepeaterItem item in repeatTabs.Items) {
                // enumerate all child controls in a item
                foreach (var control in item.Controls)
                    if (control is HtmlControl) {
                        // this means <li>
                        var li = control as HtmlControl;

                        // set CSS class attribute to <li>,
                        // depending on linkbutton's (first child of <li>) commandname
                        li.Attributes ["class"] =
                            ((li.Controls [0] as LinkButton).CommandArgument == tabName) ? "ui-tabs-active" : "";
                    }
            }
        }

        /// <summary>
        /// Finds the view in a multiview by its name.
        /// </summary>
        /// <returns>The view.</returns>
        /// <param name="viewName">View name without prefix.</param>
        /// <param name="prefix">View name prefix.</param>
        protected View FindView (string viewName, string prefix = "view")
        {
            foreach (View view in multiView.Views)
                if (view.ID.ToLowerInvariant () == prefix + viewName)
                    return view;

            return null;
        }

        /// <summary>
        /// Mirrors multiview ActiveViewIndex changes in session variable
        /// </summary>
        /// <param name="sender">Sender (a MultiView)</param>
        /// <param name="e">Event arguments</param>
        protected void multiView_ActiveViewChanged (object sender, EventArgs e)
        {
            // set session variable to active view name without "view" prefix
            Session["Launchpad_ActiveView_" + TabModuleId] =
                multiView.GetActiveView ().ID.ToLowerInvariant ().Replace ("view", "");
        }

        string GetActiveTabNameFromSession () => (string) Session["Launchpad_ActiveView_" + TabModuleId];

        void SetActiveTabFix (string tabName)
        {
            multiView.SetActiveView (FindView (tabName));
            SelectTab (tabName);
        }

        #endregion

        #region Handlers

        /// <summary>
        /// Get DataTable stored in Session by GridView ID
        /// </summary>
        /// <returns>The data table.</returns>
        /// <param name="gridviewId">Gridview identifier.</param>
        private DataTable GetDataTable (string gridviewId)
        {
            var session = Session [gridviewId];
            if (session == null) {
                var tabName = GetActiveTabNameFromSession ();
                session = Tables.GetByGridId (gridviewId).GetDataTable (this, ModelContext, (string) Session [tabName + "_Search"]);
                Session [gridviewId] = session;
            }
            return (DataTable) session;
        }

        /// <summary>
        /// Handles Init event for a control
        /// </summary>
        /// <param name="e">Event args.</param>
        protected override void OnInit (EventArgs e)
        {
            base.OnInit (e);

            // read tab names
            var tables = Settings.Tables;
            if (tables == null || tables.Count == 0) {
                this.Message ("NotConfigured.Text", MessageType.Info, true);
                return;
            }

            if (tables.Count > 1) {
                // bind tabs
                repeatTabs.DataSource = tables;
                repeatTabs.DataBind ();
            }

            // wireup LoadComplete handler
            Page.LoadComplete += OnLoadComplete;

            // show first view if no session info available
            var tabName = GetActiveTabNameFromSession ();
            if (tabName == null) {
                // if no tabs set in settings, don't set active view
                if (tables != null && tables.Count > 0) {
                    SetActiveTabFix (tables[0]);
                }
            }

            // initialize Launchpad tables
            InitTables ();
        }

        void InitTables ()
        {
            foreach (var table in Tables.Tables) {
                table.Init (this, (GridView) FindControl ("grid" + table.Name), Settings.PageSize);
            }

            // can bind grid.ID => table now
            Tables.InitGridsDictionary ();
        }

        /// <summary>
        /// Fires when module load is complete (after all postback handlers)
        /// </summary>
        /// <param name="sender">Sender.</param>
        /// <param name="e">Event agruments.</param>
        protected void OnLoadComplete (object sender, EventArgs e)
        {
            try {
                var tabName = GetActiveTabNameFromSession ();
                if (!IsPostBack) {
                    // restore multiview state from session on first load
                    if (tabName != null) {
                        SetActiveTabFix (tabName);
                    }

                    // restore search phrase from session
                    if (Session [tabName + "_Search"] != null) {
                        textSearch.Text = (string) Session [tabName + "_Search"];
                    }

                    BindTab (tabName);
                }
                else {
                    // get postback initiator
                    var eventTarget = Request.Form ["__EVENTTARGET"];

                    // check if tab was switched
                    if (!string.IsNullOrEmpty (eventTarget) && eventTarget.Contains ("$linkTab")) {
                        BindTab (tabName);
                    }
                }
            }
            catch (Exception ex) {
                Exceptions.ProcessModuleLoadException (this, ex);
            }
        }

        protected void BindTab (string tabName)
        {
            // bind active table
            var table = Tables.GetByName (tabName);

            var searchText = textSearch.Text;
            if (!string.IsNullOrWhiteSpace (searchText)) {
                table.DataBind (this, ModelContext, searchText);
            }
            else {
                table.DataBind (this, ModelContext);
            }

            // setup link to add new item
            linkAddItem.NavigateUrl = table.GetAddUrl (this);
            linkAddItem.Visible = table.IsEditable && SecurityContext.CanAdd (table.EntityType);
        }

        /// <summary>
        /// Handles ItemDataBound event for tabs repeater
        /// </summary>
        /// <param name="sender">Sender.</param>
        /// <param name="e">Event arguments.</param>
        protected void repeatTabs_ItemDataBound (object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem) {
                var link = (LinkButton) e.Item.FindControl ("linkTab");
                var tabName = (string) e.Item.DataItem;
                var table = Tables.GetByName (tabName);
                if (table != null) {
                    link.Text = LocalizeString (table.ResourceKey);
                    link.CommandArgument = tabName;
                }
                else {
                    link.Visible = false;
                }
            }
        }

        #endregion

        #region IActionable implementation

        public ModuleActionCollection ModuleActions
        {
            get {
                var actions = new ModuleActionCollection ();
                foreach (var tableName in Settings.Tables) {
                    var table = Tables.GetByName (tableName);
                    if (table != null && table.IsEditable && SecurityContext.CanAdd (table.EntityType)) {
                        actions.Add (table.GetAction (this));
                    }
                }
                return actions;
            }
        }

        #endregion

        protected void gridView_Sorting (object sender, GridViewSortEventArgs e)
        {
            var gv = sender as GridView;

            // retrieve the table from the session object
            var dt = GetDataTable (gv.ID);

            if (dt != null) {
                // sort the data
                dt.DefaultView.Sort = e.SortExpression + " " + GetSortDirection (gv.ID, e.SortExpression);
                gv.DataSource = dt;
                gv.DataBind ();
            }
        }

        private string GetSortDirection (string controlID, string column)
        {
            // by default, set the sort direction to ascending
            var sortDirection = "ASC";

            // retrieve the last column that was sorted
            var sortExpression = ViewState [controlID + "SortExpression"] as string;

            if (sortExpression != null) {
                // check if the same column is being sorted
                // otherwise, the default value can be returned
                if (sortExpression == column) {
                    if (ViewState ["SortDirection"] != null) {
                        var lastDirection = (string) ViewState ["SortDirection"];
                        if (lastDirection == "ASC") {
                            sortDirection = "DESC";
                        }
                    }
                }
            }

            // Save new values in ViewState.
            // TODO: Test how this behave in case of multiple GridView's?
            ViewState ["SortDirection"] = sortDirection;
            ViewState [controlID + "SortExpression"] = column;

            return sortDirection;
        }

        protected void gridView_PageIndexChanging (object sender, GridViewPageEventArgs e)
        {
            var gv = sender as GridView;
            var dt = GetDataTable (gv.ID);

            if (dt != null) {
                gv.PageIndex = e.NewPageIndex;
                gv.DataSource = dt;
                gv.DataBind ();
            }

            // restore pager visibility, may also TopPagerRow
            gv.BottomPagerRow.Visible = true;
        }

        protected void gridView_RowDataBound (object sender, GridViewRowEventArgs e)
        {
            // exclude header?
            if (e.Row.RowType == DataControlRowType.DataRow) {
                // find edit hyperlink
                var linkEdit = (HyperLink) e.Row.Cells [0].FindControl ("linkEdit");
                var table = Tables.GetByGridId (((GridView) sender).ID);

                // assuming what e.Row.Cells[1] contains item ID
                linkEdit.NavigateUrl = table.GetEditUrl (this, e.Row.Cells [1].Text);
                linkEdit.Visible = table.IsEditable;
            }
        }

        /// <summary>
        /// Handles click on tab linkbutton
        /// </summary>
        /// <param name="sender">Sender.</param>
        /// <param name="e">Event arguments.</param>
        protected void linkTab_Clicked (object sender, EventArgs e)
        {
            var tabName = (sender as LinkButton).CommandArgument;
            SetActiveTabFix (tabName);

            multiView_ActiveViewChanged (multiView, EventArgs.Empty);

            // restore search phrase
            textSearch.Text = Session [tabName + "_Search"] != null ?
                (string) Session [tabName + "_Search"] : string.Empty;
        }

        protected void buttonSearch_Click (object sender, EventArgs e)
        {
            try {
                var tabName = GetActiveTabNameFromSession ();
                SetActiveTabFix (tabName);

                Session [tabName + "_Search"] = textSearch.Text.Trim ();

                Tables.GetByName (tabName).DataBind (this, ModelContext, textSearch.Text.Trim ());
            }
            catch (Exception ex) {
                Exceptions.ProcessModuleLoadException (this, ex);
            }
        }

        protected void buttonResetSearch_Click (object sender, EventArgs e)
        {
            try {
                var tabName = GetActiveTabNameFromSession ();
                SetActiveTabFix (tabName);

                textSearch.Text = string.Empty;
                Session [tabName + "_Search"] = null;

                Tables.GetByName (tabName).DataBind (this, ModelContext);
            }
            catch (Exception ex) {
                Exceptions.ProcessModuleLoadException (this, ex);
            }
        }
    }
}
