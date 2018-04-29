using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using Microsoft.ApplicationBlocks.Data;


/// <summary>
/// Summary description for usersService
/// </summary>
public class UsersService
{
	public UsersService()
	{
	}

    public DataTable GetAllUsers()
    {
        var conn = MyAdoHelper.ConnectToDb("Database.mdf");
        try
        {
            DataSet ds = new DataSet();
            ds = (SqlHelper.ExecuteDataset(conn, CommandType.StoredProcedure, "GetAllUsers"));
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

    public int IsUserEmailExist(string email)
    {
        var conn = MyAdoHelper.ConnectToDb("Database.mdf");
        try
        {
            var param = new System.Data.SqlClient.SqlParameter[1];
            param[0] = new System.Data.SqlClient.SqlParameter("email", SqlDbType.NVarChar);
            param[0].Value = NormalizeCase(email);

            int userId = Convert.ToInt32(SqlHelper.ExecuteScalar(conn, CommandType.StoredProcedure, "IsUserEmailExist", param));
            return userId;
            
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

    public DataTable IsUserExistByEmailAndPass(string email, string password)
    {
        var conn = MyAdoHelper.ConnectToDb("Database.mdf");
        try
        {
            var param = new System.Data.SqlClient.SqlParameter[2];
            param[0] = new System.Data.SqlClient.SqlParameter("email", SqlDbType.NVarChar);
            param[0].Value = NormalizeCase(email);
            param[1] = new System.Data.SqlClient.SqlParameter("password", SqlDbType.NVarChar);
            param[1].Value = NormalizeCase(password);

            DataSet ds = new DataSet();
            ds = SqlHelper.ExecuteDataset(conn, CommandType.StoredProcedure, "IsUserExistByEmailAndPass", param);
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
    public string GetFullNameById (int userId)
    {
        var conn = MyAdoHelper.ConnectToDb("Database.mdf");
        try
        {
            var param = new System.Data.SqlClient.SqlParameter[1];
            param[0] = new System.Data.SqlClient.SqlParameter("userId", SqlDbType.Int);
            param[0].Value = userId;

            string fullName = Convert.ToString(SqlHelper.ExecuteScalar(conn, CommandType.StoredProcedure, "GetFullNameById", param));
            return fullName;
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

    public void RegisterUser(string fullName, string email, string password, int isAdmin)
    {
        var conn = MyAdoHelper.ConnectToDb("Database.mdf");
        try
        {
            var param = new System.Data.SqlClient.SqlParameter[4];

            param[0] = new System.Data.SqlClient.SqlParameter("fullName", SqlDbType.NVarChar);
            param[0].Value = NormalizeCase(fullName);

            param[1] = new System.Data.SqlClient.SqlParameter("email", SqlDbType.NVarChar);
            param[1].Value = NormalizeCase(email);

            param[2] = new System.Data.SqlClient.SqlParameter("password", SqlDbType.NVarChar);
            param[2].Value = NormalizeCase(password);

            param[3] = new System.Data.SqlClient.SqlParameter("isAdmin", SqlDbType.Int);
            param[3].Value = isAdmin;

            SqlHelper.ExecuteNonQuery(conn, CommandType.StoredProcedure, "RegisterUser", param);
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

    public int GetUserIdByEmail(string email)
    {
        var conn = MyAdoHelper.ConnectToDb("Database.mdf");
        try
        {
            var param = new System.Data.SqlClient.SqlParameter[1];
            param[0] = new System.Data.SqlClient.SqlParameter("email", SqlDbType.NVarChar);
            param[0].Value = NormalizeCase(email);
            int userId = Convert.ToInt32(SqlHelper.ExecuteScalar(conn, CommandType.StoredProcedure, "GetUserIdByEmail", param));
            return userId;
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
    public string NormalizeCase (string str)
    {
        if (String.IsNullOrEmpty(str))
            return String.Empty;
        return Char.ToUpper(str[0]) + str.Substring(1).ToLower();
    }

    public void UpdateUser(int userId, string fullName, string email, string password, int isAdmin)
    {
        var conn = MyAdoHelper.ConnectToDb("Database.mdf");
        try
        {
            var param = new System.Data.SqlClient.SqlParameter[5];

            param[0] = new System.Data.SqlClient.SqlParameter("userId", SqlDbType.Int);
            param[0].Value = userId;

            param[1] = new System.Data.SqlClient.SqlParameter("fullName", SqlDbType.NVarChar);
            param[1].Value = NormalizeCase(fullName);

            param[2] = new System.Data.SqlClient.SqlParameter("email", SqlDbType.NVarChar);
            param[2].Value = NormalizeCase(email);

            param[3] = new System.Data.SqlClient.SqlParameter("password", SqlDbType.NVarChar);
            param[3].Value = NormalizeCase(password);

            param[4] = new System.Data.SqlClient.SqlParameter("isAdmin", SqlDbType.Int);
            param[4].Value = isAdmin;

            SqlHelper.ExecuteNonQuery(conn, CommandType.StoredProcedure, "UpdateUser", param);
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
        public void DeleteUser(int userId)
    {
        var conn = MyAdoHelper.ConnectToDb("Database.mdf");
        try
        {
            var param = new System.Data.SqlClient.SqlParameter[1];

            param[0] = new System.Data.SqlClient.SqlParameter("userId", SqlDbType.Int);
            param[0].Value = userId;

            SqlHelper.ExecuteNonQuery(conn, CommandType.StoredProcedure, "DeleteUser", param);
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

    public DataTable GetUserById(int userId)
    {
        var conn = MyAdoHelper.ConnectToDb("Database.mdf");
        try
        {
            var param = new System.Data.SqlClient.SqlParameter[1];

            param[0] = new System.Data.SqlClient.SqlParameter("userId", SqlDbType.Int);
            param[0].Value = userId;

            DataSet ds = new DataSet();
            ds = SqlHelper.ExecuteDataset(conn, CommandType.StoredProcedure, "GetUserById", param);
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
    public bool IsPassCorrect(int userId, string password)
    {
        var conn = MyAdoHelper.ConnectToDb("Database.mdf");
        try
        {
            var param = new System.Data.SqlClient.SqlParameter[2];
            param[0] = new System.Data.SqlClient.SqlParameter("userId", SqlDbType.Int);
            param[0].Value = userId;
            param[1] = new System.Data.SqlClient.SqlParameter("password", SqlDbType.NVarChar);
            param[1].Value = NormalizeCase(password);

            int userIdReturn = Convert.ToInt32(SqlHelper.ExecuteScalar(conn, CommandType.StoredProcedure, "IsPassCorrect", param));
            //if password incorrect
            if (userIdReturn == -1)
            {
                return false;
            }
            else
            {
                return true;
            }
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