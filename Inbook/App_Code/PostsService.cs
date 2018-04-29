using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using Microsoft.ApplicationBlocks.Data;

/// <summary>
/// Summary description for PostsService
/// </summary>
public class PostsService
{
	public PostsService()
	{
		//
		// TODO: Add constructor logic here
		//
	}
    public DataTable GetAllValidPosts()
    {
        var conn = MyAdoHelper.ConnectToDb("Database.mdf");
        try
        {
            DataSet ds = new DataSet();
            ds = (SqlHelper.ExecuteDataset(conn, CommandType.StoredProcedure, "GetAllValidPosts"));
            DataTable dt = ds.Tables[0];
            return dt;
        }
        catch (Exception ex)
        {
            throw ex;
        }
        finally
        {
            conn.Close();
        }
    }

    public DataTable SearchPosts(string query, string grade, string catagory, string location, string writers)
    {
        var conn = MyAdoHelper.ConnectToDb("Database.mdf");
        try
        {

            if (query == null) { query = ""; }
            if (catagory == null) { catagory = ""; }
            if (location == null) { location = ""; }
            if (writers == null) { writers = ""; }

            query = query.Trim();

            var param = new System.Data.SqlClient.SqlParameter[5];
            param[0] = new System.Data.SqlClient.SqlParameter("query", SqlDbType.NVarChar);
            if (query == "")
            {
                param[0].Value = null; //check this
            }
            else
            {
                param[0].Value = query;
            }
            
            param[1] = new System.Data.SqlClient.SqlParameter("grade", SqlDbType.NVarChar);
            if (grade == "")
            {
                param[1].Value = "null";
            }
            else
            {
                param[1].Value = grade;
            }

            param[2] = new System.Data.SqlClient.SqlParameter("catagory", SqlDbType.NVarChar);
            if (catagory == "")
            {
                param[2].Value = "null";
            }
            else
            {
                param[2].Value = catagory;
            }

            param[3] = new System.Data.SqlClient.SqlParameter("location", SqlDbType.NVarChar);
            if (location == "")
            {
                param[3].Value = "null";
            }
            else
            {
                param[3].Value = location;
            }

            param[4] = new System.Data.SqlClient.SqlParameter("writers", SqlDbType.NVarChar);
            if (writers == "")
            {
                param[4].Value = "null";
            }
            else
            {
                param[4].Value = writers;
            }

            DataSet ds = new DataSet();
            ds = (SqlHelper.ExecuteDataset(conn, CommandType.StoredProcedure, "SearchPosts", param));
            DataTable dt = ds.Tables[0];
            return dt;
        }
        catch (Exception ex)
        {
            throw ex;
        }
        finally
        {
            conn.Close();
        }
    }

    public void AddPost(string postTitle, int price, string phone, string bookImg, int userId)
    {
        var conn = MyAdoHelper.ConnectToDb("Database.mdf");
        try
        {
            var param = new System.Data.SqlClient.SqlParameter[5];
            param[0] = new System.Data.SqlClient.SqlParameter("postTitle", SqlDbType.NVarChar);
            param[0].Value = postTitle;

            param[1] = new System.Data.SqlClient.SqlParameter("price", SqlDbType.SmallMoney);
            param[1].Value = price;

            param[2] = new System.Data.SqlClient.SqlParameter("contactPhone", SqlDbType.NVarChar);
            param[2].Value = phone;

            param[3] = new System.Data.SqlClient.SqlParameter("bookImg", SqlDbType.NVarChar);
            param[3].Value = bookImg;

            param[4] = new System.Data.SqlClient.SqlParameter("userId", SqlDbType.Int);
            param[4].Value = userId;

            SqlHelper.ExecuteNonQuery(conn, CommandType.StoredProcedure, "AddPost", param);
        }
        catch (Exception ex)
        {
            throw ex;
        }
        finally
        {
            conn.Close();
        }
    }
}