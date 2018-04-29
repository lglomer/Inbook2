using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class MasterPage : System.Web.UI.MasterPage
{
    int userId;
    string path;
    public string firstName = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        path = HttpContext.Current.Request.Url.AbsolutePath.ToLower();

        Users user = new Users();

        if (Session["userId"] == null)
        {
            logoutBtn.Visible = false;
            navLoggedIn.Visible = false;
        }
        else
        {
            userId = Convert.ToInt32(Session["userId"].ToString());
            firstName = user.GetFullNameById(userId).Split(' ')[0];

            showSignup.Visible = false;
            navLoggedIn.Visible = true;
        }

        if (!IsPostBack)
        {
            if (path == "/default.aspx" || path == "default.aspx" || path == "" || path == "/") //not working everytime (/#
            {
                logo.Style.Add("margin-top", "11%");
            }
        }
    }


    protected void logout_Click(object sender, EventArgs e)
    {
        Session.Abandon();
        Response.Redirect("/");
    }

    protected void signupBtn_Click(object sender, EventArgs e)
    {
        Users user = new Users();

        if (Page.IsValid)
        {
            if (fullName.Text.Length > 100)
            {
                signupInvalidMsg.InnerHtml = "* שם ארוך מדי";
                return;
            }

            if (fullName.Text.Length < 4)
            {
                signupInvalidMsg.InnerHtml = "* שם קצר מדי";
                return;
            }

            if (emailAddress.Text.Length > 254)
            {
                signupInvalidMsg.InnerHtml = "* כתובת אימייל ארוכה מדי";
                return;
                //כתובת אימייל ארוכה מדי
            }

            if (password.Text.Length < 6)
            {
                signupInvalidMsg.InnerHtml = "* סיסמא קצרה מדי";
                fullName.CssClass = "ol-textbox-invalid";
                return;
                //כתובת אימייל ארוכה מדי
            }

            //if email dosen't exist
            if (user.IsUserEmailExist(emailAddress.Text) == -1)
            {

                //Register user
                user.RegisterUser(fullName.Text, emailAddress.Text, password.Text, 0);
                int userId = user.GetUserIdByEmail(emailAddress.Text);

                // Visit counter
                Application.Lock();
                if (Application["visitCount"] == null)
                {

                    Application["visitCount"] = 1;
                }
                else
                {
                    Application["visitCount"] = Convert.ToInt32(Application["visitCount"]) + 1;
                }
                Application.UnLock();


                Session["firstLogin"] = "true"; //To show welcome message on home page
                Session["userId"] = userId;
                Session.Timeout = 30;
                Response.Redirect("Default.aspx");

            }
            else
            {
                emailAddress.CssClass = "ol-textbox-invalid"; 
                return;
                //אימייל קיים
            }
        }
    }
}
