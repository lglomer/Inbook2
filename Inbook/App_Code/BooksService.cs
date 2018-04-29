using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using Microsoft.ApplicationBlocks.Data;

/// <summary>
/// Summary description for BooksService
/// </summary>
public class BooksService
{
	public BooksService()
	{
		//
		// TODO: Add constructor logic here
		//
	}
    public DataTable GetAllBooks()
    {
        var conn = MyAdoHelper.ConnectToDb("Database.mdf");
        try
        {
            DataSet ds = new DataSet();
            ds = (SqlHelper.ExecuteDataset(conn, CommandType.StoredProcedure, "GetAllBooks"));
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

    public DataTable SearchBooks(string query)
    {
        var conn = MyAdoHelper.ConnectToDb("Database.mdf");
        try
        {
            query = query.Trim();

            var param = new System.Data.SqlClient.SqlParameter[1];
            param[0] = new System.Data.SqlClient.SqlParameter("query", SqlDbType.NVarChar);
            param[0].Value = query;

            DataSet ds = new DataSet();
            ds = (SqlHelper.ExecuteDataset(conn, CommandType.StoredProcedure, "SearchBooks", param));
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
    public DataTable AutocompleteBooks(string query)
    {
        var conn = MyAdoHelper.ConnectToDb("Database.mdf");
        try
        {
            query = query.Trim();

            var param = new System.Data.SqlClient.SqlParameter[1];
            param[0] = new System.Data.SqlClient.SqlParameter("query", SqlDbType.NVarChar);
            param[0].Value = query;

            DataSet ds = new DataSet();
            ds = (SqlHelper.ExecuteDataset(conn, CommandType.StoredProcedure, "AutocompleteBooks", param));
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

    public DataTable GetBookByTitle(string title)
    {
        var conn = MyAdoHelper.ConnectToDb("Database.mdf");
        try
        {
            var param = new System.Data.SqlClient.SqlParameter[1];
            param[0] = new System.Data.SqlClient.SqlParameter("title", SqlDbType.NVarChar);
            param[0].Value = title;

            DataSet ds = new DataSet();
            ds = (SqlHelper.ExecuteDataset(conn, CommandType.StoredProcedure, "GetBookByTitle", param));
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
}