<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="MvcHelper.FluentViewPage<MvcHelper.Models.HomeViewModel>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

<%= Fluent.TextBox("Customer.LastName","") %>

</asp:Content>
