﻿<%@ Control Language="C#" AutoEventWireup="false" CodeBehind="EditScienceRecords.ascx.cs" Inherits="R7.University.Controls.EditScienceRecords" %>
<%@ Register TagPrefix="dnn" TagName="Label" Src="~/controls/labelcontrol.ascx" %>
<%@ Register TagPrefix="dnn" Namespace="DotNetNuke.Web.UI.WebControls" Assembly="DotNetNuke.Web" %>
<%@ Register TagPrefix="dnn" Namespace="DotNetNuke.Web.Client.ClientResourceManagement" Assembly="DotNetNuke.Web.Client" %>

<dnn:DnnCssInclude runat="server" FilePath="~/DesktopModules/MVC/R7.University/R7.University/css/admin.css" Priority="200" />
<dnn:DnnJsInclude runat="server" FilePath="~/DesktopModules/MVC/R7.University/R7.University/Controls/js/gridAndForm.js" ForceProvider="DnnFormBottomProvider" />
<div class="dnnForm dnnClear u8y-edit-sciencerecords">
    <fieldset>
        <div class="dnnFormItem">
            <asp:GridView id="gridItems" runat="server" AutoGenerateColumns="false" CssClass="dnnGrid u8y-gaf-grid" GridLines="None">
                <HeaderStyle CssClass="dnnGridHeader" HorizontalAlign="Left" />
                <RowStyle CssClass="dnnGridItem" HorizontalAlign="Left" />
                <AlternatingRowStyle CssClass="dnnGridAltItem" />
                <SelectedRowStyle CssClass="dnnFormError" />
                <EditRowStyle CssClass="dnnFormInput" />
                <FooterStyle CssClass="dnnGridFooter" />
                <PagerStyle CssClass="dnnGridPager" />
                <Columns>
                    <asp:TemplateField>
                       <ItemTemplate>
                            <span class="u8y-gaf-actions">
                                <asp:LinkButton id="linkEdit" runat="server" OnCommand="OnEditItemCommand" ToolTip='<%# LocalizeString ("EditItem.Title") %>'>
                                    <asp:Image runat="server" ImageUrl="<%# EditIconUrl %>" />
                                </asp:LinkButton>
                                <asp:LinkButton id="linkDelete" runat="server" OnCommand="OnDeleteItemCommand" ToolTip='<%# LocalizeString ("DeleteItem.Title") %>'>
                                    <asp:Image runat="server" ImageUrl="<%# DeleteIconUrl %>" />
                                </asp:LinkButton>
                                <asp:LinkButton id="linkUndelete" runat="server" OnCommand="OnUndeleteItemCommand" ToolTip='<%# LocalizeString ("UndeleteItem.Title") %>'>
                                    <asp:Image runat="server" ImageUrl="<%# UndeleteIconUrl %>" />
                                </asp:LinkButton>
                                <asp:Label id="labelEditMarker" runat="server" />
                            </span>
                       </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="ViewItemID" />
                </Columns>
            </asp:GridView>
        </div>
        <div class="dnnFormItem">
            <div class="dnnLabel"></div>
			<ul class="dnnActions">
				<li>
                    <asp:LinkButton id="buttonAddItem" runat="server" resourcekey="buttonAddScienceRecord" 
                        CssClass="dnnPrimaryAction" CommandArgument="Add" 
                        CausesValidation="true" ValidationGroup="ScienceRecords" />
				</li>	
                <li>
				    <asp:LinkButton id="buttonUpdateItem" runat="server" resourcekey="buttonUpdateScienceRecord" 
                        CssClass="dnnPrimaryAction" CommandArgument="Update" 
                        CausesValidation="true" ValidationGroup="ScienceRecords" />
				</li>
                <li>
				    <asp:LinkButton id="buttonCancelEditItem" runat="server" resourcekey="CancelEdit" 
                        CssClass="dnnSecondaryAction" />
				</li>
				<li>&nbsp;</li>
    			<li>
					<asp:LinkButton id="buttonResetForm" runat="server" resourcekey="ResetForm" 
                        CssClass="dnnSecondaryAction" />
				</li>
			</ul>	
        </div>
		<asp:HiddenField id="hiddenViewItemID" runat="server" />
		<asp:HiddenField id="hiddenScienceRecordID" runat="server" />
    </fieldset>
</div>
