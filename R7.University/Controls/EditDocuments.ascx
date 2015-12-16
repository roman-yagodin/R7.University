﻿<%@ Control Language="C#" AutoEventWireup="false" CodeBehind="EditDocuments.ascx.cs" Inherits="R7.University.Controls.EditDocuments" %>
<%@ Register TagPrefix="dnn" TagName="Url" Src="~/controls/urlcontrol.ascx" %>
<%@ Register TagPrefix="dnn" TagName="Label" Src="~/controls/labelcontrol.ascx" %>
<%@ Register TagPrefix="dnn" Namespace="DotNetNuke.Web.UI.WebControls" Assembly="DotNetNuke.Web" %>

<div class="eduProgramDocuments">
    <fieldset>
        <div class="dnnFormItem">
            <asp:GridView id="gridDocuments" runat="server" AutoGenerateColumns="false" CssClass="dnnGrid"
                GridLines="None" OnRowDataBound="gridDocuments_RowDataBound" Style="margin-bottom:30px;width:775px">
                <HeaderStyle CssClass="dnnGridHeader" horizontalalign="Left" />
                <RowStyle CssClass="dnnGridItem" horizontalalign="Left" />
                <AlternatingRowStyle CssClass="dnnGridAltItem" />
                <SelectedRowStyle CssClass="dnnFormError" />
                <EditRowStyle CssClass="dnnFormInput" />
                <FooterStyle CssClass="dnnGridFooter" />
                <PagerStyle CssClass="dnnGridPager" />
                <Columns>
                    <asp:TemplateField>
                       <ItemTemplate>
                            <span style="white-space:nowrap">
                                <asp:LinkButton id="linkEdit" runat="server" OnCommand="linkEditDocument_Command" >
                                    <asp:Image runat="server" ImageUrl="<%# EditIconUrl %>" />
                                </asp:LinkButton>
                                <asp:LinkButton id="linkDelete" runat="server" OnCommand="linkDeleteDocument_Command" >
                                    <asp:Image runat="server" ImageUrl="<%# DeleteIconUrl %>" />
                                </asp:LinkButton>
                            </span>
                       </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="ViewItemID" />
                    <asp:BoundField DataField="LocalizedType" HeaderText="DocumentType" />
                    <asp:BoundField DataField="Title" HeaderText="DocumentTitle" />
                    <asp:BoundField DataField="SortIndex" HeaderText="DocumentSortIndex" />
                    <asp:BoundField DataField="StartDate" HeaderText="DocumentStartDate" />
                    <asp:BoundField DataField="EndDate" HeaderText="DocumentEndDate" />
                    <asp:BoundField DataField="FormattedUrl" HeaderText="DocumentUrl" HtmlEncode="false" />
                </Columns>
            </asp:GridView>
        </div>
        <div class="dnnFormItem">
            <dnn:Label id="labelDocumentType" runat="server" ControlName="comboDocumentType" />
            <asp:DropDownList id="comboDocumentType" runat="server" 
                DataTextField="LocalizedType"
                DataValueField="DocumentTypeID"
            />
        </div>
        <div class="dnnFormItem">
            <dnn:Label id="labelDocumentTitle" runat="server" ControlName="textDocumentTitle" />
            <asp:TextBox id="textDocumentTitle" runat="server" MaxLength="255" />
        </div>
        <div class="dnnFormItem">
            <dnn:Label id="labelDocumentUrl" runat="server" ControlName="urlDocumentUrl" />
            <dnn:Url id="urlDocumentUrl" runat="server" UrlType="N" 
                IncludeActiveTab="true"
                ShowFiles="true" ShowTabs="true"
                ShowUrls="true" ShowUsers="true"
                ShowLog="false" ShowTrack="false"
                ShowNone="true" ShowNewWindow="false" 
            />   
        </div>
        <div class="dnnFormItem">
            <dnn:Label id="labelDocumentSortIndex" runat="server" ControlName="textDocumentSortIndex" />
            <asp:TextBox id="textDocumentSortIndex" runat="server" Value="0" />
        </div>
        <div class="dnnFormItem">
            <dnn:Label ID="labelDocumentStartDate" runat="server" ControlName="datetimeDocumentStartDate" />
            <dnn:DnnDateTimePicker id="datetimeDocumentStartDate" runat="server" />
        </div>
        <div class="dnnFormItem">
            <dnn:Label ID="labelDocumentEndDate" runat="server" ControlName="datetimeDocumentEndDate" />
            <dnn:DnnDateTimePicker id="datetimeDocumentEndDate" runat="server" />
        </div>
        <div class="dnnFormItem">
            <div class="dnnLabel"></div>
            <asp:LinkButton id="buttonAddDocument" runat="server" resourcekey="buttonAddDocument" 
                CssClass="dnnPrimaryAction" OnCommand="buttonAddDocument_Command"  CommandArgument="Add" />
            <asp:LinkButton id="buttonUpdateDocument" runat="server" resourcekey="buttonUpdateDocument" 
                CssClass="dnnPrimaryAction" OnCommand="buttonAddDocument_Command" Visible="false" CommandArgument="Update" />
            <asp:LinkButton id="buttonCancelEditDocument" runat="server" resourcekey="buttonCancelEditDocument" 
                        CssClass="dnnSecondaryAction" OnClick="buttonCancelEditDocument_Click" />
        </div>
        <asp:HiddenField id="hiddenDocumentItemID" runat="server" />
    </fieldset>
</div>