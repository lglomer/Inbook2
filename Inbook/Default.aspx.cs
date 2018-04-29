using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Web.UI.HtmlControls;

public partial class _Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Searcher search = new Searcher();

        if (!IsPostBack)
        {
            gradesDropdown.DataSource = search.GetGradesDropdown().Items;
            gradesDropdown.DataBind();

            DropDownList dropdownLocation = search.GetLocationDropdown();

            //locationDropdown.DataSource = dropdownLocation.Items;
            //locationDropdown.DataBind();

            locationDropdown2.DataSource = dropdownLocation.Items;
            locationDropdown2.DataBind();
        }
    }
    protected void searchBtn_Click(object sender, EventArgs e)
    {
        if (searchTb.Text == "") { return; }
            
        if (locationSearch.Text != "")
        {
            Response.Redirect("~/search.aspx?query=" + Server.UrlEncode(TrimAll(searchTb.Text)) + "&location=" + Server.UrlEncode(locationSearch.Text));
        }        
        else
        {
            Response.Redirect("~/search.aspx?query=" + Server.UrlEncode(TrimAll(searchTb.Text)));
        }

    }
    public string TrimAll(string str)
    {
        str = str.Trim();
        string[] arr = str.Split(' ');
        List<string> list = new List<string>();
        for (int i = 0; i < arr.Length; i++)
        {
            if (arr[i] != "")
            {
                list.Add(arr[i]);
            }
        }

        string newStr = "";
        for (int j = 0; j < list.Count; j++)
        {
            if (j == 0)
            {
                newStr = list[j];
            }
            else
            {
                newStr += " " + list[j];
            }
        }

        return newStr;
    }
}
