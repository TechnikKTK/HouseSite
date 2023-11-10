using System;
using System.Data.SqlClient;
using System.Web.Security;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Data;

public partial class _Default : System.Web.UI.Page
{
    protected string lockedPrichina = "много раз ввел пароль не правильно.";

    protected void Page_Load(object sender, EventArgs e)
    {
        if (this.Page.User.Identity.IsAuthenticated)
        {
            GotoLK();
        }
    }

    private void GotoLK()
    {
        if (this.Page.User.IsInRole("admin"))
        {
            Response.Redirect("/admin/users");
        }
        else
        {
            Response.Redirect("/dashboard");
        }
    }


    protected string CheckUser()
    {
        var login = LoginView1.FindControl("Login") as Login;
        if (login != null)
        {
            TextBox userName = login.FindControl("UserName") as TextBox;
            TextBox password = login.FindControl("Password") as TextBox;

            if (userName != null)
            {
                if (userName.Text.Length > 0 && password.Text.Length > 0)
                {
                    MembershipUser user = Membership.GetUser(userName.Text);

                    if (user == null)
                    {
                        lockedPrichina = "Логин и/или пароль не верный.";
                        return "";
                    }
                    else
                    {
                        if (!user.IsApproved)
                        {
                            lockedPrichina = "Ваш аккаунт заблокирован администратором.";
                            return "";

                        }
                        else
                        {
                            lockedPrichina = "Ваш аккунт заблокирован на 15 минут.\r\n Причина: Слишком часто вводили неверно пароль.";
                            return user.IsLockedOut ? "" : "hidden";
                        }
                    }
                }
            }
    
            lockedPrichina = "";
            return "hidden";
        }
        return "hidden";
    }

    protected void btnConfirm_Click(object sender, EventArgs e)
    {
        var need = rulesCheckBox.Items.Count;
        var count = 0;
        foreach (var item in rulesCheckBox.Items)
        {
            if ((item as ListItem).Selected)
                count++;
        }

        if (count == need)
        {
            Login login = (Login)LoginView1.FindControl("Login");
            if (login != null)
            {
                var success = Membership.ValidateUser(login.UserName, login.Password);
                if (success)
                {
                    MembershipUser user = Membership.GetUser(login.UserName);
                    UpdateUser(user);
                }
            } 
        }
    }

    private void UpdateUser(MembershipUser user)
    {
        string connect_str = ConfigurationManager.ConnectionStrings["migConnectionString"].ConnectionString;

        if (user != null)
        {
            try
            {
                using (SqlConnection _connection = new SqlConnection(connect_str))
                {
                    _connection.Open();

                    SqlCommand cmd = new SqlCommand("Update [hs_Users] Set [ParkingRules] = 'True'," +
                        " [PersonalRules] = 'True' " +
                        " FROM [hs_Users] Where UserId = @UserID", _connection);
                    cmd.Parameters.Add("@UserID", SqlDbType.UniqueIdentifier).Value = (Guid)user.ProviderUserKey;

                    cmd.ExecuteNonQuery();
                    
                    _connection.Close();
                }
                //GotoLK();
            }
            catch(Exception er)
            {

            }
        }
    }
}