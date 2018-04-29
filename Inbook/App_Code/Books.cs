using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

/// <summary>
/// Summary description for Books
/// </summary>
public class Books
{
    const string defBook = "img/uploads/covers/bookDefault.jpg";
	public Books()
	{
		//
		// TODO: Add constructor logic here
		//
	}
    public DataTable GetAllBooks()
    {
        BooksService bs = new BooksService();
        return (bs.GetAllBooks());
    }
    public DataTable SearchBooks(string query)
    {
        if (query.Trim() == "") 
        { 
            return new DataTable(); // return an empty datatable
        }

        BooksService bs = new BooksService();
        return bs.SearchBooks(query);
    }
    public DataTable AutocompleteBooks(string query)
    {
        if (query.Trim() == "")
        {
            return new DataTable(); // return an empty datatable
        }

        BooksService bs = new BooksService();
        return bs.AutocompleteBooks(query);
    }

    public DataTable GetBookByTitle(string query)
    {
        BooksService bs = new BooksService();
        return bs.GetBookByTitle(query);
    }
}