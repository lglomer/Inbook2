using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using Microsoft.ApplicationBlocks.Data;

/// <summary>
/// Summary description for SearcherService
/// </summary>
public class SearcherService
{
	public SearcherService()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    public DataTable GetAllYeshuvim()
    {
        var conn = MyAdoHelper.ConnectToDb("Database.mdf");
        try
        {
            DataSet ds = new DataSet();
            ds = (SqlHelper.ExecuteDataset(conn, CommandType.StoredProcedure, "GetAllYeshuvim"));
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