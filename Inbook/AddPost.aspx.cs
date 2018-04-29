using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Web.UI.HtmlControls;
using System.IO;

public partial class AddPost : System.Web.UI.Page
{
    string postTitle;
    protected void Page_Load(object sender, EventArgs e)
    {
        Searcher search = new Searcher();

        if (!IsPostBack)
        {
            gradesDropdown.DataSource = search.GetGradesDropdown().Items;
            DataBind();

            locationDropdown.DataSource = search.GetLocationDropdown().Items;
            DataBind();
        }
    }

    public bool isFormValid()
    {
        if (Session["userId"] != null)
        {
            int n;

            if (int.TryParse(priceTb.Text, out n))
            {
                if (Convert.ToInt32(priceTb.Text) < 0)
                {
                    Page.Header.Controls.Add(new LiteralControl("<script type='text/javascript'>alert('הכנס מחיר תקין')</script>"));
                    return false;
                }
            }
            else
            {
                Page.Header.Controls.Add(new LiteralControl("<script type='text/javascript'>alert('הכנס מחיר תקין')</script>"));
                return false;
            }

            if (phoneTb.Text == "")
            {
                Page.Header.Controls.Add(new LiteralControl("<script type='text/javascript'>alert('הכנס מספר טלפון')</script>"));
                return false;
            }

            if (!int.TryParse(phoneTb.Text, out n))
            {
                Page.Header.Controls.Add(new LiteralControl("<script type='text/javascript'>alert('הכנס טלפון תקין (ספרות בלבד)')</script>"));
                return false;

            }
            else
            {
                if (phoneTb.Text.Length < 6)
                {
                    Page.Header.Controls.Add(new LiteralControl("<script type='text/javascript'>alert('הכנס טלפון תקין')</script>"));
                    return false;
                }
            }
        }
        else
        {
            return false;
        }

        return true;

    }

    protected void finishBtn_Click(object sender, EventArgs e)
    {
        if (isFormValid())
        {
            if (FileUpload1.HasFile)
            {
                Upload upload = new Upload(FileUpload1);

                if (upload.IsExtensionAllowed())
                {
                    upload.AddCoverPicture();
                }
                else
                {
                    //alert extension not allowed.
                    Page.Header.Controls.Add(new LiteralControl("<script type='text/javascript'>alert('סוג קובץ לא מורשה')</script>"));
                }
            }


            Posts post = new Posts();
            //post.AddPost(searchTb.Text, Convert.ToInt32(priceTb.Text), phoneTb.Text);
        }
    }
}