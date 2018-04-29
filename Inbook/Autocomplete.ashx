<%@ WebHandler Language="C#" Class="Autocomplete" %>

using System;
using System.Web;
using System.Collections.Generic;
using System.Data;

public class Autocomplete : IHttpHandler {

    string query = "";
    int method;
    
    public void ProcessRequest (HttpContext context) {
        context.Response.ContentType = "text/plain";
        
        if (context.Request.QueryString["query"] != null && context.Request.QueryString["method"] != null)
        {
            method = Convert.ToInt32(context.Request.QueryString["method"].ToString());

            if (method == 1) //return autocomplete options
            {
                query = TrimAll(Uri.UnescapeDataString(context.Request.QueryString["query"].ToString()));

                Books book = new Books();
                DataTable dt = book.AutocompleteBooks(query);
                string autocomplete = "", title;

                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count && i < 5; i++)
                    {
                        title = dt.Rows[i]["title"].ToString();
                        if (autocomplete == "")
                        {
                            autocomplete += title;
                        }
                        else
                        {
                            autocomplete += "/o" + title;
                        }

                    }

                    context.Response.Write(autocomplete);
                }
                else
                {
                    context.Response.Write("");
                }
            }
            else
            {
                if (method == 2)
                {
                    Searcher search = new Searcher();
                    DataTable yeshuvimDt = search.GetAllYeshuvim();
                    string autocomplete = "", yeshuv;
                    
                    
                    if (yeshuvimDt.Rows.Count > 0)
                    {
                        for (int i = 0; i < yeshuvimDt.Rows.Count; i++)
                        {
                            yeshuv = yeshuvimDt.Rows[i]["yeshuvName"].ToString();
                            if (autocomplete == "")
                            {
                                autocomplete += yeshuv;
                            }
                            else
                            {
                                autocomplete += "/o" + yeshuv;
                            }
                        }

                        context.Response.Write(autocomplete);
                    }
                    else
                    {
                        context.Response.Write("");
                    }
                    
                }
            }
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
 
    public bool IsReusable {
        get {
            return false;
        }
    }

}