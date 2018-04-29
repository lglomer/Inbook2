using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

/// <summary>
/// Summary description for Users
/// </summary>
public class Users
{
    private string fullName, email, password, phoneNumber;
    private int userId;
    public Users(string fullName, string email, string password)
    {
        this.fullName = fullName;
        this.email = email;
        this.password = password;
    }
    public Users(string email, string password)
    {

    }
    public Users()
    {

    }
    public DataTable GetAllUsers()
    {
        UsersService us = new UsersService();
        return (us.GetAllUsers());
    }
    public int IsUserEmailExist(string email)
    {
        UsersService us = new UsersService();
        return (us.IsUserEmailExist(email));
    }

    public DataTable IsUserExistByEmailAndPass(string email, string password)
    {
        UsersService us = new UsersService();
        return (us.IsUserExistByEmailAndPass(email, password));
    }
    public string GetFullNameById(int userId)
    {
        UsersService us = new UsersService();
        return (us.GetFullNameById(userId));
    }
    public void RegisterUser(string fullName, string email, string password, int isAdmin)
    {
        UsersService us = new UsersService();
        us.RegisterUser(fullName, email, password, isAdmin);
    }

    public int GetUserIdByEmail(string email)
    {
        UsersService us = new UsersService();
        return (us.GetUserIdByEmail(email));
    }
    public void UpdateUser(int userId, string fullName, string email, string password, int isAdmin)
    {
        UsersService us = new UsersService();
        us.UpdateUser(userId, fullName, email, password, isAdmin);
    }
    public void DeleteUser(int userId)
    {
        UsersService us = new UsersService();
        us.DeleteUser(userId);
    }
    public DataTable GetUserById(int userId)
    {
        UsersService us = new UsersService();
        return us.GetUserById(userId);
    }

    public bool IsPassCorrect(int userId, string password)
    {
        UsersService us = new UsersService();
        return us.IsPassCorrect(userId, password);
    }
}