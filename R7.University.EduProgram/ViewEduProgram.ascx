﻿<%@ Control Language="C#" AutoEventWireup="false" CodeBehind="ViewEduProgram.ascx.cs" Inherits="R7.University.EduProgram.ViewEduProgram" %>
<%@ Register TagPrefix="dnn" Namespace="DotNetNuke.Web.Client.ClientResourceManagement" Assembly="DotNetNuke.Web.Client" %>

<dnn:DnnCssInclude runat="server" FilePath="~/DesktopModules/MVC/R7.University/R7.University/css/module.css" />

<asp:FormView id="formEduProgram" runat="server" ItemType="R7.University.EduProgram.ViewModels.EduProgramViewModel" RenderOuterTable="false">
    <ItemTemplate>
        <div class="u8y-eduprogram">
            <div class="u8y-eduprogram-info">
                <p>
                    <label runat="server"><%# LocalizeString ("EduLevel.Text") %></label>
                    <%# Eval ("EduLevel_Title") %>
                </p>
                <div runat="server" Visible='<%# Eval ("DivisionsVisible") %>'>
                    <label><%# LocalizeString ("Divisions.Text") %></label>
                    <ul class="u8y-eduprogram-divisions">
                    <asp:ListView runat="server" DataSource="<%# Item.DivisionViewModels %>" ItemType="R7.University.EduProgram.ViewModels.EduProgramDivisionViewModel" >
                        <LayoutTemplate>
                            <div runat="server" id="itemPlaceholder"></div>
                        </LayoutTemplate>
                        <ItemTemplate>
                            <li><%# Item.DivisionLink %></li>
                        </ItemTemplate>                 
                    </asp:ListView>
                    </ul>
                </div>
                <div runat="server" Visible='<%# Eval ("EduStandard_Visible") %>' class="u8y-para">
                    <label runat="server"><%# LocalizeString ("EduStandard.Text") %></label>
                    <%# HttpUtility.HtmlDecode ((string) Eval ("EduStandard_Links")) %>
                </div>
            </div>
            <div runat="server" Visible='<%# Eval ("EduProgramProfiles_Visible") %>'>
                <asp:ListView runat="server" DataSource="<%# Item.EduProgramProfileViewModels %>" ItemType="R7.University.EduProgram.ViewModels.EduProgramProfileViewModel">
                    <LayoutTemplate>
                        <div runat="server" class="u8y-eduprogram-profiles">
                            <div runat="server" id="itemPlaceholder"></div>
                        </div>
                    </LayoutTemplate>
                    <ItemTemplate>
                        <div>
                            <h3 runat="server" class='<%# Eval ("CssClass") %>'>
                                <asp:HyperLink runat="server" Visible='<%# IsEditable %>' NavigateUrl='<%# Eval ("Edit_Url") %>' IconKey="Edit" />
                                <%# Eval ("Title_String") %>
                            </h3>
                            <p>
                                <label runat="server"><%# LocalizeString ("EduLevel.Text") %></label>
                                <%# Eval ("EduLevel_Title") %>
                            </p>
							<div runat="server" Visible='<%# Item.DivisionsVisible %>'>
                                <label><%# LocalizeString ("Divisions.Text") %></label>
                                <ul class="u8y-eduprogram-profiles-divisions">
                                <asp:ListView runat="server" DataSource="<%# Item.DivisionViewModels %>" ItemType="R7.University.EduProgram.ViewModels.EduProgramDivisionViewModel" >
                                    <LayoutTemplate>
                                        <div runat="server" id="itemPlaceholder"></div>
                                    </LayoutTemplate>
                                    <ItemTemplate>
                                        <li><%# Item.DivisionLink %></li>
                                    </ItemTemplate>                 
                                </asp:ListView>
                                </ul>
                            </div>
							<div runat="server" Visible='<%# (bool) Eval ("AccreditedToDate_Visible") || (bool) Eval ("CommunityAccreditedToDate_Visible") %>' class="u8y-para">
                                <div runat="server" Visible='<%# Eval ("AccreditedToDate_Visible") %>'>
                                    <label runat="server"><%# LocalizeString ("AccreditedToDate.Text") %></label>
                                    <%# Eval ("AccreditedToDate_String") %>
                                </div>
                                <div runat="server" Visible='<%# Eval ("CommunityAccreditedToDate_Visible") %>'>
                                    <label runat="server"><%# LocalizeString ("CommunityAccreditedToDate.Text") %></label>
                                    <%# Eval ("CommunityAccreditedToDate_String") %>
                                </div>
                            </div>
                            <div runat="server" Visible='<%# Eval ("EduForms_Visible") %>' class="u8y-para-end">
                                <label runat="server"><%# LocalizeString ("EduForms.Text") %></label>
                                <%# HttpUtility.HtmlDecode ((string) Eval ("EduForms_String")) %>
                            </div>
                        </div>
                    </ItemTemplate>
                </asp:ListView>
            </div>
        </div>
    </ItemTemplate>
</asp:FormView>