using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

/// <summary>
/// Summary description for Posts
/// </summary>
public class Posts
{
	public Posts()
	{
		//
		// TODO: Add constructor logic here
		//
	}
    public DataTable GetAllValidPosts()
    {
        // get all posts where isAvailable = true and isDisabled = false
        PostsService ps = new PostsService();
        return (ps.GetAllValidPosts());
    }

    public void AddPost(string postTitle, int price, string phone, string bookImg, int userId)
    {
        PostsService ps = new PostsService();
        ps.AddPost(postTitle, price, phone, bookImg, userId);
    }
}