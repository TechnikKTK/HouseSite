using System;
using System.Collections.Generic;

using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Xml.Linq;
using System.IO;

public partial class Admin_NewUser : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        int i = 8;

        if (!IsPostBack)
        {
            tbxPassword.Text = GetAlphaNumericRandomString(6);
        }
    }

    private string GetAlphaNumericRandomString(int length)
    {
        string randomString = "";
        while (randomString.Length < length)
        {
            //generates a random string, of twice the length specified, to counter the 
            //probability of the while loop having to run a second time
            randomString += Membership.GeneratePassword(length * 2, 0);

            //replace non alphanumeric characters
            randomString = System.Text.RegularExpressions.Regex.Replace(randomString, @"[^a-zA-Z0-9]", m => "");
        }
        return randomString.Substring(0, length);
    }

    protected void btnCreateUser_Click(object sender, EventArgs e)
    {
        int i = 0;
        try
        {
            MembershipUser newUser = null;
            newUser = Membership.CreateUser(tbxUserName.Text, tbxPassword.Text, tbxEmail.Text);

            if (newUser != null)
            {
                string connect_str = ConfigurationManager.ConnectionStrings["migConnectionString"].ConnectionString;
                try
                {
                    using (SqlConnection _connection = new SqlConnection(connect_str))
                    {
                        _connection.Open();
                        SqlCommand cmd = new SqlCommand("INSERT INTO hs_Users (UserId, Name, Email) VALUES (@UserId, @FIO, @Email)", _connection);
                        cmd.Parameters.Add("@UserId", SqlDbType.UniqueIdentifier).Value = (Guid) newUser.ProviderUserKey;
                        cmd.Parameters.Add("@Email", SqlDbType.VarChar).Value = newUser.Email;
                        cmd.Parameters.Add("@FIO", SqlDbType.VarChar).Value = tbxFIO.Text;

                        cmd.ExecuteNonQuery();
                        _connection.Close();
                    }
                }
                catch (Exception ex)
                {
                    string base_ = System.Web.HttpContext.Current.Server.MapPath("~\\Catch");
                    File.AppendAllText(base_+"\\_exc.txt", DateTime.Now + "btnCreateUser_Click \n" + ex + "\n");
                }

                Response.Redirect("/admin/users");
            }
        }
        catch
        {

        }
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        int i = 0;
        try
        {
            MembershipUser newUser = null;
            newUser = Membership.CreateUser(tbxUserName.Text, tbxPassword.Text, tbxEmail.Text);

            if (newUser != null)
            {
                Response.Redirect("/admin/users");
            }
        }
        catch
        {

        }
    }
}