<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Search.aspx.cs" Inherits="Search" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <script type="text/javascript">
        function CallIsSearchValid() {
            return isSearchValid('<%=searchTb.ClientID%>');
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Main" Runat="Server">
    <div class="main mb-main mainStrip">
        <div class="searchBar">
            <asp:TextBox ID="searchTb" CssClass="ol-textbox ol-textbox-aside" AutoCompleteType="None" autocomplete="off" placeholder="חפש ספר" runat="server"></asp:TextBox>
            <asp:Button ID="searchBtn" CssClass="ol-button ol-button-aside " Text="חפש" runat="server" OnClientClick="return CallIsSearchValid();" OnClick="searchBtn_Click" />
        </div>
    </div>
<%--    <div class="filtersListContainer"><div id="filtersList" class="filtersList" runat="server"></div></div>--%>
    <div id="filtersMain" runat="server"></div>
    <div class="main mb-main postsContainer">
        <div id="searchResults" runat="server"></div>
    </div>
</asp:Content>

