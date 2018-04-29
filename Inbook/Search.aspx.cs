using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Data;

public partial class Search : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Request.QueryString["query"] != null)
            {
                Searcher search = new Searcher(Server.UrlDecode(Request.QueryString["query"].ToString()));
                searchTb.Text = search.GetQuery();

                if (Request.QueryString["grade"] != null)
                {
                    search.SetGrade(Server.UrlDecode(Request.QueryString["grade"].ToString()));
                }

                if (Request.QueryString["catagory"] != null)
                {
                    search.SetCatagory(Server.UrlDecode(Request.QueryString["catagory"].ToString()));
                }

                if (Request.QueryString["location"] != null)
                {
                    search.SetLocation(Server.UrlDecode(Request.QueryString["location"].ToString()));
                }

                if (Request.QueryString["writers"] != null)
                {
                    search.SetWriters(Server.UrlDecode(Request.QueryString["writers"].ToString()));
                }

                if (Request.QueryString["page"] != null)
                {
                    search.SetPage(Convert.ToInt32(Request.QueryString["page"].ToString()));
                }

                ShowData(search); //show data
            }
            else
            {
                Response.Redirect("~/");
            }
        }
    }
    protected void searchBtn_Click(object sender, EventArgs e)
    {
        if (searchTb.Text.Trim() == "") { return; }
        Response.Redirect("~/search.aspx?query=" + Server.UrlEncode(searchTb.Text));
    }

    public void ShowData(Searcher search)
    {
        DataTable dt = search.SearchPosts();

        HtmlGenericControl filtersCtrl = search.GetFilters(dt);
        filtersMain.Controls.Add(filtersCtrl);

        searchResults.InnerHtml = ""; // empty the results div

        if (dt.Rows.Count == 0)
        {
            searchResults.InnerHtml = "<div style='padding-top: 15px'>לא נמצאו תוצאות.</div>";
        }
        else
        {
            HtmlGenericControl postsDiv = search.GetResultsControlByTable(dt);
            searchResults.Controls.Add(postsDiv);
        }
    }
}