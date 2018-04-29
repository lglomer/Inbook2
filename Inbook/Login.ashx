<%@ WebHandler Language="C#" Class="Login" %>

using System;
using System.Web;
using System.Data;


public class Login : IHttpHandler, System.Web.SessionState.IRequiresSessionState
{

    public void ProcessRequest(HttpContext context)
    {
        context.Response.ContentType = "text/plain";

        if (context.Request.Form["email"] != null && context.Request.Form["password"] != null)
        {
            string email = Uri.UnescapeDataString(context.Request.Form["email"].ToString());
            string password = Uri.UnescapeDataString(context.Request.Form["password"].ToString());

            Users user = new Users();

            DataTable dt = user.IsUserExistByEmailAndPass(email, password);

            int isAdmin = Convert.ToInt32(dt.Rows[0]["isAdmin"]);
            int userId = Convert.ToInt32(dt.Rows[0]["userId"]);


            //If user exists
            if (userId >= 0)
            {

                //visit counter
                context.Application.Lock();
                if (context.Application["visitCount"] == null)
                {
                    context.Application["visitCount"] = 1;
                }
                else
                {
                    context.Application["visitCount"] = Convert.ToInt32(context.Application["visitCount"]) + 1;
                }
                context.Application.UnLock();


                context.Session["userId"] = userId;
                if (isAdmin != 0)
                {
                    context.Session["isAdmin"] = isAdmin;
                }
                context.Session.Timeout = 30;
                context.Response.Write("redirect");
            }
            else
            {
                context.Response.Write("* אימייל או סיסמא לא נכונים.");
            }
        }
    }
 
    public bool IsReusable {
        get {
            return false;
        }
    }

}