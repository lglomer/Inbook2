<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default" %>

<asp:Content ID="HeadContent" ContentPlaceHolderID="head" Runat="Server">
    <script type="text/javascript">

        function switchSearchMethod() {
            if ($("#<%=searchByClass.ClientID%>").css('display') == "none") {
                $("#<%=searchBooks.ClientID%>").hide();
                $("#<%=searchByClass.ClientID%>").fadeIn();

                $("#<%=searchMethod.ClientID%>").text("חיפוש לפי ספר");

            } else {
                $("#<%=searchByClass.ClientID%>").hide();
                $("#<%=searchBooks.ClientID%>").fadeIn();

                $("#<%=searchMethod.ClientID%>").text("חיפוש לפי כיתה / קורס");

            }
        }   

        $(document).ready(function () {

            $(document).tooltip();

            $('#<%=locationSearch.ClientID%>').ready(function () {
                var xmlhttp = new XMLHttpRequest();
                xmlhttp.onreadystatechange = function () {
                    if (xmlhttp.readyState == 4 && xmlhttp.status == 200) {
                        var src = [];
                        src = xmlhttp.responseText.split("/o");

                        $('#<%=locationSearch.ClientID%>').autocomplete({
                            source: function (request, response) {
                                var results = $.ui.autocomplete.filter(src, request.term);

                                response(results.slice(0, 10));
                            },
                            position: {
                                my: "right top",
                                at: "right bottom"
                            }
                        });
                    }
                };

                xmlhttp.open("GET", "autocomplete.ashx?query=" + encodeURIComponent(document.getElementById('<%=locationSearch.ClientID%>').value).toString() + "&method=2", true);
                xmlhttp.send();

            });
        });
    </script>
</asp:Content>


<asp:Content ID="MainContent" ContentPlaceHolderID="Main" Runat="Server">
    <div style="text-align:center; margin-top: 40px;">


        <div id="searchBooks" runat="server">
            <asp:TextBox ID="searchTb" CssClass="ol-textbox" placeholder="חפש ספר" runat="server"></asp:TextBox>
            <div style="display: inline-block; margin-top:15px;">
                <div class="ui-widget">
                    <asp:TextBox ID="locationSearch" CssClass="ol-textbox ol-textbox-wide" AutoCompleteType="None" autocomplete="off" placeholder="ישוב / עיר" runat="server"></asp:TextBox>
                </div>  
            </div>
            <div>
                <div style="margin: 0 auto; display: inline-block;">
                    <asp:Button ID="searchBtn" CssClass="ol-button" runat="server" OnClick="searchBtn_Click" Text="חיפוש" />
                </div>
            </div>
        </div>

        <div id="searchByClass" runat="server" style="display: none">
                <div style="display: inline-block; margin-top:15px;">
                    <asp:DropDownList ID="gradesDropdown" CssClass="searchDropdown" runat="server"></asp:DropDownList>
                    <asp:DropDownList ID="locationDropdown2" CssClass="searchDropdown" runat="server"></asp:DropDownList>
                </div>
            <div>
                <div style="margin: 0 auto; display: inline-block;">
                    <asp:Button ID="Button1" CssClass="ol-button" runat="server" OnClick="searchBtn_Click" Text="חיפוש ספר" />
                </div>
            </div>
        </div>




        <div class="links"> 
            <a href="javascript: switchSearchMethod();" id="searchMethod" runat="server">חיפוש לפי כיתה / קורס</a>         
            <a href="/AddPost.aspx"> הוסף ספר למכירה<b style="margin-right:5px;">+</b>
            </a>
        </div>
    </div>

</asp:Content>
