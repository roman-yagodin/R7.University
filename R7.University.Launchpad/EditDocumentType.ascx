﻿<%@ Control Language="C#" AutoEventWireup="false" CodeBehind="EditDocumentType.ascx.cs" Inherits="R7.University.Launchpad.EditDocumentType" %>
<%@ Register TagPrefix="dnn" TagName="Label" Src="~/controls/labelcontrol.ascx" %>
<%@ Register TagPrefix="dnn" Namespace="DotNetNuke.Web.Client.ClientResourceManagement" Assembly="DotNetNuke.Web.Client" %>
<%@ Register TagPrefix="controls" TagName="AgplSignature" Src="~/DesktopModules/MVC/R7.University/R7.University.Controls/AgplSignature.ascx" %>

<dnn:DnnCssInclude runat="server" FilePath="~/DesktopModules/MVC/R7.University/R7.University/assets/css/module.css" />
<dnn:DnnJsInclude runat="server" FilePath="~/DesktopModules/MVC/R7.University/R7.University.Launchpad/js/editDocumentType.js" ForceProvider="DnnFormBottomProvider" />

<div class="dnnForm dnnClear">
	<fieldset>
		<div class="dnnFormItem">
			<dnn:Label id="labelType" runat="server" ControlName="textType" />
			<asp:TextBox id="textType" runat="server" MaxLength="64" />
            <asp:RequiredFieldValidator runat="server" ControlToValidate="textType" Display="Dynamic"
                CssClass="dnnFormMessage dnnFormError" resourcekey="Type.Required" />
		</div>
		<div class="dnnFormItem">
			<dnn:Label id="labelDescription" runat="server" ControlName="textDescription" />
			<asp:TextBox id="textDescription" runat="server" TextMode="MultiLine" Rows="3" />
            <asp:RegularExpressionValidator runat="server"
                CssClass="dnnFormMessage dnnFormError" resourcekey="Description.MaxLength"
                ControlToValidate="textDescription" Display="Dynamic"
                ValidationExpression="[\s\S]{0,255}">
            </asp:RegularExpressionValidator>
		</div>
		<div class="dnnFormItem">
            <dnn:Label id="labelFilenameFormat" runat="server" ControlName="textFilenameFormat" />
            <asp:TextBox id="textFilenameFormat" runat="server" MaxLength="255" />
			<asp:CustomValidator runat="server" ControlToValidate="textFilenameFormat"
                Display="Dynamic" CssClass="dnnFormMessage dnnFormError" resourcekey="FilenameFormat.Invalid"
			    EnableClientScript="true" ClientValidationFunction="validateFilenameFormat" />
        </div>
        <div class="dnnFormItem">
            <dnn:Label id="labelIsSystem" runat="server" ControlName="checkIsSystem" />
            <asp:CheckBox id="checkIsSystem" runat="server" Enabled="false" />
        </div>
	</fieldset>
	<ul class="dnnActions dnnClear">
		<li><asp:LinkButton id="buttonUpdate" runat="server" CssClass="btn btn-primary" ResourceKey="cmdUpdate" CausesValidation="true" /></li>
		<li>&nbsp;</li>
		<li><asp:LinkButton id="buttonDelete" runat="server" CssClass="btn btn-danger" ResourceKey="cmdDelete" /></li>
		<li>&nbsp;</li>
		<li><asp:HyperLink id="linkCancel" runat="server" CssClass="btn btn-outline-secondary" ResourceKey="cmdCancel" /></li>
	</ul>
    <controls:AgplSignature runat="server" ShowRule="false" />
</div>
