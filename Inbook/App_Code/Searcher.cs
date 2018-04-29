using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

/// <summary>
/// Summary description for Search
/// </summary>
public class Searcher
{
    private string query, grade, catagory, location, writers;
    private int page;

    const string defImgLocation = "img/uploads/covers/";
    const string defBookImg = defImgLocation + "bookDefault.jpg";
    public Searcher(string query)
	{
        this.query = TrimAll(query);
        this.grade = "";
        this.catagory = "";
        this.location = "";
        this.writers = "";
        this.page = 1; //0 = no paging
	}
    public Searcher()
    {
        this.query = "";
        this.grade = "";
        this.catagory = "";
        this.location = "";
        this.writers = "";
        this.page = 1;
    }

    public void SetQuery(string query)
    {
        this.query = TrimAll(query); //total trim free text
    }
    public void SetGrade(string grade)
    {
        this.grade = grade;
    }
    public void SetCatagory(string catagory)
    {
        this.catagory = catagory;
    }
    public void SetLocation(string location)
    {
        this.location = location;
    }
    public void SetWriters(string writers)
    {
        this.writers = writers;
    }
    public void SetPage(int page)
    {
        this.page = page;
    }


    public string GetQuery()
    {
        return this.query;
    }
    public string GetGrade()
    {
        return this.grade;
    }
    public string GetCatagory()
    {
        return this.catagory;
    }
    public string GetLocation()
    {
        return this.location;
    }
    public string GetWriters()
    {
        return this.writers;
    }
    public int GetPage()
    {
        return this.page;
    }


    public DataTable SearchPosts()
    {
        if (query == "")
        {
            return new DataTable(); // return an empty datatable
        }
        else
        {
            PostsService ps = new PostsService();
            return ps.SearchPosts(query, grade, catagory, location, writers);
        }
    }

    public HtmlGenericControl GetResultsControlByTable(DataTable posts)
    {
        HtmlGenericControl booksContainer = new HtmlGenericControl("div"); //div to return
        HtmlGenericControl postDiv, postImgHref, postImg, postContent, postTitle, postTitleOverImg, postDescription;
        string postPicLocation;
        int limit, min;

        if (page == 0)
        {
            limit = posts.Rows.Count;
            min = 0;
        }
        else
        {
            limit = page * 12;
            min = (page - 1) * 12;
        }

        for (int s = min; s < posts.Rows.Count && s < limit; s++)
        {
            postDiv = new HtmlGenericControl("div");
            postDiv.Attributes["class"] = "post clearfix";

            postImgHref = new HtmlGenericControl("a");
            postImgHref.Attributes["href"] = "#"; // ON CLICK
            postImgHref.Attributes["style"] = "text-decoration: none;";

            postImg = new HtmlGenericControl("div");
            postImg.Attributes["class"] = "postImg valignParent";

            postContent = new HtmlGenericControl("div");
            postContent.Attributes["class"] = "postContent";

            postDescription = new HtmlGenericControl("span");

            postTitle = new HtmlGenericControl("p");
            postTitle.InnerHtml = "<a href='#' class='postTitle'>" + posts.Rows[s]["postTitle"].ToString() + "</a>";

            postTitleOverImg = new HtmlGenericControl("h1");
            postTitleOverImg.InnerHtml = posts.Rows[s]["postTitle"].ToString();

            postPicLocation = posts.Rows[s]["postPicture"].ToString().Trim();

            if (postPicLocation.Trim() == "" || postPicLocation == defBookImg)
            {
                postTitleOverImg.Attributes["class"] = "bookTitle valignChild postTitleOverImg";
                postImg.Style.Add("background-image", "url('" + defBookImg + "')");
            }
            else
            {
                postTitleOverImg.Attributes["class"] = "bookTitle bookTitleOnImg valignChild postTitleOverImg";
                postImg.Style.Add("background-image", "url('" + defImgLocation + postPicLocation + "')");
            }

            if (posts.Rows[s]["writers"].ToString().Trim() != "")
            {
                postDescription.InnerHtml += "מחבר/י הספר: <span style='color: gray;'>" + posts.Rows[s]["writers"].ToString().Trim() + "</span><p></p>";
            }
            postDescription.InnerHtml += "<a href='#' class='a'>הצג פרטים</a>";

            postContent.Controls.Add(postTitle);
            postContent.Controls.Add(postDescription);

            postImg.Controls.Add(postTitleOverImg);
            postImgHref.Controls.Add(postImg);

            postDiv.Controls.Add(postImgHref);
            postDiv.Controls.Add(postContent);

            booksContainer.Controls.Add(postDiv);
        }

        int totalPages = 0;

        if (posts.Rows.Count % 12 != 0)
        {
            totalPages = (posts.Rows.Count / 12) + 1;
        }
        else
        {
            totalPages = posts.Rows.Count / 12;
        }

        if (posts.Rows.Count > 12 && page > 0)
        {
            HtmlGenericControl pages = new HtmlGenericControl("div");
            pages.Attributes["style"] = "text-align: center; margin-top: 10px;";
            pages.InnerHtml = "עמוד ";
            for (int f = totalPages; f > 0; f--)
            {
                pages.InnerHtml += "<a href='" + GetUrl(this.query, this.grade, this.location, this.catagory, this.writers, f) + "' class='a' style='margin-right: 5px;'>" + f + "</a>";
            }

            booksContainer.Controls.Add(pages);
        }

        return booksContainer;
    }

    public DropDownList GetGradesDropdown()
    {
        DropDownList gradesDropdown = new DropDownList();
        //gradesDropdown.Attributes.Add("class", "searchDropdown");
        Users user = new Users();

        ListItem li = new ListItem();
        li.Text = "לכל כיתה";
        gradesDropdown.Items.Add(li);

        li = new ListItem();
        li.Text = "א";
        gradesDropdown.Items.Add(li);

        li = new ListItem();
        li.Text = "ב";
        gradesDropdown.Items.Add(li);

        li = new ListItem();
        li.Text = "ג";
        gradesDropdown.Items.Add(li);

        li = new ListItem();
        li.Text = "ד";
        gradesDropdown.Items.Add(li);

        li = new ListItem();
        li.Text = "ה";
        gradesDropdown.Items.Add(li);

        li = new ListItem();
        li.Text = "ו";
        gradesDropdown.Items.Add(li);

        li = new ListItem();
        li.Text = "ז";
        gradesDropdown.Items.Add(li);

        li = new ListItem();
        li.Text = "ח";
        gradesDropdown.Items.Add(li);

        li = new ListItem();
        li.Text = "ט";
        gradesDropdown.Items.Add(li);

        li = new ListItem();
        li.Text = "י";
        gradesDropdown.Items.Add(li);

        li = new ListItem();
        li.Text = "י'א";
        gradesDropdown.Items.Add(li);

        li = new ListItem();
        li.Text = "י'ב";
        gradesDropdown.Items.Add(li);

        li = new ListItem();
        li.Text = "אחר";
        gradesDropdown.Items.Add(li);

        return gradesDropdown;
    }
    public DataTable GetAllYeshuvim()
    {
        SearcherService ss = new SearcherService();
        return ss.GetAllYeshuvim();
    }
    public DropDownList GetLocationDropdown()
    {
        DropDownList locationsDropdown = new DropDownList();
        //locationsDropdown.Attributes.Add("class", "searchDropdown");

        DataTable dt = GetAllYeshuvim();

        ListItem li = new ListItem();
        li.Text = "כל האזורים";
        locationsDropdown.Items.Add(li);

        for (int i = 0; i < dt.Rows.Count; i++)
        {
            li = new ListItem();
            li.Text = dt.Rows[i]["yeshuvName"].ToString();
            locationsDropdown.Items.Add(li);
        }

        li = new ListItem();
        li.Text = "אחר";
        locationsDropdown.Items.Add(li);

        return locationsDropdown;
    }

    public HtmlGenericControl GetFilteredList()
    {
        HtmlGenericControl main = new HtmlGenericControl("div");
        main.Attributes["class"] = "main mainStrip mb-main";
        HtmlGenericControl filteredList = new HtmlGenericControl("div");
        filteredList.Attributes["class"] = "filtersList";
        HtmlGenericControl filtersListItem;

        if (this.grade != "")
        {
            filtersListItem = new HtmlGenericControl("div");
            filtersListItem.Attributes["class"] = "filtersListItem";
            filtersListItem.InnerHtml = "<b>שכבת גיל: </b>" + this.grade + "<a href='" + GetUrl(this.query, "", this.location, this.catagory, this.writers, 1) + "' class='filtersListItemRemove'></a>";
            filteredList.Controls.Add(filtersListItem);
        }

        if (this.location != "")
        {
            filtersListItem = new HtmlGenericControl("div");
            filtersListItem.Attributes["class"] = "filtersListItem";
            filtersListItem.InnerHtml = "<b>אזור: </b>" + this.location + "<a href='" + GetUrl(this.query, this.grade, "", this.catagory, this.writers, 1) + "' class='filtersListItemRemove'></a>";
            filteredList.Controls.Add(filtersListItem);
        }

        if (this.catagory != "")
        {
            filtersListItem = new HtmlGenericControl("div");
            filtersListItem.Attributes["class"] = "filtersListItem";
            filtersListItem.InnerHtml = "<b>קטגוריה: </b>" + this.catagory + "<a href='" + GetUrl(this.query, this.grade, this.location, "", this.writers, 1) + "' class='filtersListItemRemove'></a>";
            filteredList.Controls.Add(filtersListItem);
        }

        if (this.writers != "")
        {
            filtersListItem = new HtmlGenericControl("div");
            filtersListItem.Attributes["class"] = "filtersListItem";
            filtersListItem.InnerHtml = "<b>מחבר: </b>" + this.writers + "<a href='" + GetUrl(this.query, this.grade, this.location, this.catagory, "", 1) + "' class='filtersListItemRemove'></a>";
            filteredList.Controls.Add(filtersListItem);
        }

        main.Controls.Add(filteredList);
        return main;
    }
    public HtmlGenericControl GetFilters(DataTable posts)
    {
        HtmlGenericControl returnDiv = new HtmlGenericControl("div"); //div to return
        HtmlGenericControl filtersContainer = new HtmlGenericControl("div"); 
        filtersContainer.Attributes["class"] = "filtersMain main mb-main";

        HtmlGenericControl filterDiv, filterTitle, filterOption;
        bool isInList = false, isDivEmpty = true;
        List<string>
            gradeList = new List<string>(),
            catagoryList = new List<string>(),
            locationList = new List<string>(),
            writersList = new List<string>();

        //go through the table
        for (int i = 0; i < posts.Rows.Count; i++)
        {
            filterDiv = new HtmlGenericControl();
            filterDiv.Attributes["class"] = "filterDiv";

            if (i == 0)
            {
                if (posts.Rows[i]["grade"].ToString().Trim() != "")
                    gradeList.Add(posts.Rows[i]["grade"].ToString());

                if (posts.Rows[i]["catagory"].ToString().Trim() != "")
                    catagoryList.Add(posts.Rows[i]["catagory"].ToString());

                if (posts.Rows[i]["location"].ToString().Trim() != "")
                    locationList.Add(posts.Rows[i]["location"].ToString());

                if (posts.Rows[i]["writers"].ToString().Trim() != "")
                    writersList.Add(posts.Rows[i]["writers"].ToString());
            }
            else
            {
                for (int j = 0; j < gradeList.Count; j++)
                {
                    if (posts.Rows[i]["grade"].ToString() == gradeList[j])
                    {
                        isInList = true;
                        break;
                    }
                }

                if (isInList == false && posts.Rows[i]["grade"].ToString().Trim() != "")
                {
                    gradeList.Add(posts.Rows[i]["grade"].ToString());
                }

                isInList = false;

                for (int s = 0; s < catagoryList.Count; s++)
                {
                    if (posts.Rows[i]["catagory"].ToString() == catagoryList[s])
                    {
                        isInList = true;
                        break;
                    }
                }

                if (isInList == false && posts.Rows[i]["catagory"].ToString().Trim() != "")
                {
                    catagoryList.Add(posts.Rows[i]["catagory"].ToString());
                }

                isInList = false;

                for (int a = 0; a < locationList.Count; a++)
                {
                    if (posts.Rows[i]["location"].ToString() == locationList[a])
                    {
                        isInList = true;
                        break;
                    }
                }

                if (isInList == false && posts.Rows[i]["location"].ToString().Trim() != "")
                {
                    locationList.Add(posts.Rows[i]["location"].ToString());
                }

                isInList = false;

                for (int a = 0; a < writersList.Count; a++)
                {
                    if (posts.Rows[i]["writers"].ToString() == writersList[a])
                    {
                        isInList = true;
                        break;
                    }
                }

                if (isInList == false && posts.Rows[i]["writers"].ToString().Trim() != "")
                {
                    writersList.Add(posts.Rows[i]["writers"].ToString());
                }

                isInList = false;
            }
        }

        if (gradeList.Count > 1)
        {
            filterDiv = new HtmlGenericControl("div");
            filterDiv.Attributes["class"] = "filterDiv";

            filterTitle = new HtmlGenericControl("b");
            filterTitle.Attributes["style"] = "margin-bottom: 7px; display: block;";
            filterTitle.InnerHtml = "שכבת גיל";
            filterDiv.Controls.Add(filterTitle);

            for (int w = 0; w < gradeList.Count; w++)
            {
                filterOption = new HtmlGenericControl("a");
                filterOption.Attributes["class"] = "a br";

                if (HttpContext.Current != null)
                {
                    filterOption.Attributes["href"] = GetUrl(this.query,HttpContext.Current.Server.UrlEncode(gradeList[w]),this.location,this.catagory,this.writers,1);
                }
                else
                {
                    filterOption.Attributes["href"] = "#";
                }

                filterOption.InnerHtml = gradeList[w].Trim();
                filterDiv.Controls.Add(filterOption);
            }

            filtersContainer.Controls.Add(filterDiv);
            isDivEmpty = false;
        }

        if (catagoryList.Count > 1)
        {
            filterDiv = new HtmlGenericControl("div");
            filterDiv.Attributes["class"] = "filterDiv";

            filterTitle = new HtmlGenericControl("b");
            filterTitle.Attributes["style"] = "margin-bottom: 7px; display: block;";
            filterTitle.InnerHtml = "קטגוריה";
            filterDiv.Controls.Add(filterTitle);

            for (int w = 0; w < catagoryList.Count; w++)
            {
                filterOption = new HtmlGenericControl("a");
                filterOption.Attributes["class"] = "a br";
                if (HttpContext.Current != null)
                {

                    filterOption.Attributes["href"] = GetUrl(this.query, this.grade, this.location, HttpContext.Current.Server.UrlEncode(catagoryList[w]), this.writers, 1);
                }
                else
                {
                    filterOption.Attributes["href"] = "#";
                }

                filterOption.InnerHtml = catagoryList[w].Trim();
                filterDiv.Controls.Add(filterOption);
                isDivEmpty = false;
            }

            filtersContainer.Controls.Add(filterDiv);
        }

        if (locationList.Count > 1)
        {
            filterDiv = new HtmlGenericControl("div");
            filterDiv.Attributes["class"] = "filterDiv";

            filterTitle = new HtmlGenericControl("b");
            filterTitle.Attributes["style"] = "margin-bottom: 7px; display: block;";
            filterTitle.InnerHtml = "אזור";

            filterDiv.Controls.Add(filterTitle);

            for (int w = 0; w < locationList.Count; w++)
            {
                filterOption = new HtmlGenericControl("a");
                filterOption.Attributes["class"] = "a br";
                if (HttpContext.Current != null)
                {
                    filterOption.Attributes["href"] = GetUrl(this.query, this.grade, HttpContext.Current.Server.UrlEncode(locationList[w]), this.catagory, this.writers, 1);
                }
                else
                {
                    filterOption.Attributes["href"] = "#";
                }

                filterOption.InnerHtml = locationList[w].Trim();
                filterDiv.Controls.Add(filterOption);
            }

            filtersContainer.Controls.Add(filterDiv);
            isDivEmpty = false;
        }

        if (writersList.Count > 1)
        {
            filterDiv = new HtmlGenericControl("div");
            filterDiv.Attributes["class"] = "filterDiv";

            filterTitle = new HtmlGenericControl("b");
            filterTitle.Attributes["style"] = "margin-bottom: 7px; display: block;";
            filterTitle.InnerHtml = "כותבים";

            filterDiv.Controls.Add(filterTitle);

            for (int w = 0; w < writersList.Count; w++)
            {
                filterOption = new HtmlGenericControl("a");
                filterOption.Attributes["class"] = "a br";
                if (HttpContext.Current != null)
                {
                    filterOption.Attributes["href"] = GetUrl(this.query, this.grade, this.location, this.catagory, HttpContext.Current.Server.UrlEncode(writersList[w]), 1);
                }
                else
                {
                    filterOption.Attributes["href"] = "#";
                }

                filterOption.InnerHtml = writersList[w].Trim();
                filterDiv.Controls.Add(filterOption);
            }

            filtersContainer.Controls.Add(filterDiv);
            isDivEmpty = false;
        }

        HtmlGenericControl filteredList = GetFilteredList();
        returnDiv.Controls.Add(filteredList);

        if (isDivEmpty == true)
        {
            return returnDiv;
        }
        else
        {
            returnDiv.Controls.Add(filtersContainer);
            return returnDiv;
        }
    }
    public string GetUrl(string query, string grade, string location, string catagory, string writers, int page)
    {
        string url = HttpContext.Current.Request.Url.ToString().Split('?')[0];
        char ch = '?'; //? or & (makes the url look clean)

        if (query != "")
        {
            url += ch+"query=" + query;
            ch = '&';
        }

        if (grade != "")
        {
            url += ch+"grade=" + grade;
            ch = '&';
        }

        if (location != "")
        {
            url += ch+"location=" + location;
            ch = '&';
        }
            
        if (catagory != "")
        {
            url += ch+"catagory=" + catagory;
            ch = '&';
        }

        if (writers != "")
        {
            url += ch+"writers=" + writers;
            ch = '&';
        }

        if (page > 1) // or != 0?
        {
            url += ch+"page=" + page;
            ch = '&';
        }

        return url;
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