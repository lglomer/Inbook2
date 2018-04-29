using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using Microsoft.ApplicationBlocks.Data;

/// <summary>
/// Summary description for UploadService
/// </summary>
public class UploadService
{
	public UploadService()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    public string AddPicture(string fileName, string fileExtension, string fileCatagory)
    {
        var conn = MyAdoHelper.ConnectToDb("Database.mdf");
        try
        {
            var param = new System.Data.SqlClient.SqlParameter[3];
            param[0] = new System.Data.SqlClient.SqlParameter("fileName", SqlDbType.NVarChar);
            param[0].Value = fixString(fileName);

            param[1] = new System.Data.SqlClient.SqlParameter("fileExtension", SqlDbType.NVarChar);
            param[1].Value = fileExtension;

            param[2] = new System.Data.SqlClient.SqlParameter("fileCatagory", SqlDbType.NVarChar);
            param[2].Value = fileCatagory;

            string newName = SqlHelper.ExecuteScalar(conn, CommandType.StoredProcedure, "AddPicture", param).ToString();
            return newName;
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

    public string fixString(string str)
    {
        str = str.Trim().ToLower();

        string[] arr = str.Split(new Char[] { ' ', ':' }, StringSplitOptions.RemoveEmptyEntries);

        string newStr = "";
        for (int j = 0; j < arr.Length; j++)
        {
            if (j == 0)
            {
                newStr = arr[j];
            }
            else
            {
                newStr += "_" + arr[j];
            }
        }

        //newStr.Replace("'", "");

        return newStr;
    }
}