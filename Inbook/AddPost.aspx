<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="AddPost.aspx.cs" Inherits="AddPost" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <asp:PlaceHolder ID="PlaceHolder1" runat="server">
        <script type="text/javascript">


            function tryPopupHere() {
                if ("<%=Session["userId"]%>" == "") {
                    $('#<%=showSignup.ClientID%>').click();
                    return false;
                }
            }

            $(document).ready(function () {

                $("#<%=showSignup.ClientID%>").leanModal({ top: 200, closeButton: ".popup_close" });

            setTimeout(tryPopupHere, 600);

            $(document).tooltip();

            $('#<%=searchTb.ClientID%>').on("keyup", function () {
                var xmlhttp = new XMLHttpRequest();
                xmlhttp.onreadystatechange = function () {
                    if (xmlhttp.readyState == 4 && xmlhttp.status == 200) {
                        var src = [];
                        if (xmlhttp.responseText.indexOf("/o") > -1) {
                            src = xmlhttp.responseText.split("/o");
                        } else {
                            src = [xmlhttp.responseText];
                        }

                        $('#<%=searchTb.ClientID%>').autocomplete({
                            source: function (request, response) {
                                if (src[0] != "") {
                                    response(src)
                                }
                                else {
                                    response(request);
                                }
                            },
                            position: {
                                my: "right top",
                                at: "right bottom"
                            }
                        });
                    }
                };

                xmlhttp.open("GET", "autocomplete.ashx?query=" + encodeURIComponent(document.getElementById('<%=searchTb.ClientID%>').value).toString() + "&method=1", true);
                xmlhttp.send();

            });


            $('#<%=priceTb.ClientID%>').ready(function () {
                $('#<%=priceTb.ClientID%>').on('input propertychange paste', function () {
                    if (this.value.length > 4) {
                        this.value = this.value.slice(0, 4);
                    }
                    if (this.value < 0) {
                        this.value = 0;
                    }
                });
            });
        });

    </script>

    </asp:PlaceHolder>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="Main" Runat="Server">

    <div class="main small-main" style="margin-bottom: 8%">
        <asp:Image ID="bookImg" CssClass="bookImg" ImageUrl="/img/uploads/covers/bookDefault.jpg" runat="server" />
        <div class="fileUpload">
            <span>העלה תמונה של הספר</span>
            <asp:FileUpload ID="FileUpload1" CssClass="upload" runat="server" />
        </div>

        <div class="ui-widget">
            <asp:TextBox ID="searchTb" CssClass="ol-textbox ol-textbox-wide" AutoCompleteType="None" autocomplete="off" placeholder="שם הספר" runat="server"></asp:TextBox>
        </div>
        <asp:TextBox ID="priceTb" type="number" CssClass="ol-textbox ol-textbox-wide" AutoCompleteType="None" autocomplete="off" placeholder='מחיר בש"ח' runat="server"></asp:TextBox>
        <asp:TextBox ID="phoneTb" CssClass="ol-textbox ol-textbox-wide" AutoCompleteType="None" autocomplete="off" MaxLength="12" placeholder='מספר טלפון' runat="server"></asp:TextBox>
        <div id="gradesDiv" style="display: inline-block; margin-top:15px;" runat="server">
            <asp:DropDownList ID="gradesDropdown" CssClass="addpostDropdown searchDropdown" runat="server"></asp:DropDownList>
        </div>
        <div id="locationDiv" style="display: inline-block; margin-top:15px;" runat="server">
            <asp:DropDownList ID="locationDropdown" CssClass="addpostDropdown searchDropdown" runat="server"></asp:DropDownList>
        </div>
        <p></p>
        <asp:Button ID="finishBtn" OnClientClick="return tryPopupHere();" OnClick="finishBtn_Click"  CssClass="navBtn" runat="server" Text="הוסף" />
        <h2>לתקן - אחרי התחברות הטופס לא נשלח והנתונים נמחקים</h2>
        <a href="#signup" id="showSignup" rel="leanModal" name="signup" runat="server"></a>
    </div>
</asp:Content>