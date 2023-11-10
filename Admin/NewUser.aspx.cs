using System;
using System.Web.Security;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Threading.Tasks;

public partial class Admin_NewUser : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
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
        var admin = Membership.GetUser();
        string connect_str = ConfigurationManager.ConnectionStrings["migConnectionString"].ConnectionString;

        try
        {
            MembershipUser newUser = null;
            newUser = Membership.GetUser(tbxUserName.Text);
            if(newUser != null)
            {
                DisplayAlert("Ошибка: Пользователь с таким логином уже зарегистрирован.", true);
                return;
            }

            newUser = Membership.CreateUser(tbxUserName.Text, tbxPassword.Text);

            if (newUser != null)
            {
                try
                {
                    using (SqlConnection _connection = new SqlConnection(connect_str))
                    {
                        _connection.Open();
                        SqlCommand cmd = new SqlCommand("INSERT INTO hs_Users " +
                            "(UserId, Name, LastName, Patronumic, Email,Phone,AutoNumber) " +
                            "VALUES (@UserId, @Name,  @LastName, @Patronumic,  @Email,@Phone,@Auto)", _connection);
                        cmd.Parameters.Add("@UserId", SqlDbType.UniqueIdentifier).Value = (Guid) newUser.ProviderUserKey;
                        cmd.Parameters.Add("@Email", SqlDbType.VarChar).Value = tbxEmail.Text;
                        cmd.Parameters.Add("@Name", SqlDbType.VarChar).Value = tbxName.Text;
                        cmd.Parameters.Add("@LastName", SqlDbType.VarChar).Value = tbxLastName.Text;
                        cmd.Parameters.Add("@Patronumic", SqlDbType.VarChar).Value = tbxPatronumic.Text;
                        var data = tbxAuto.Text.Replace(" ", "");
                        cmd.Parameters.Add("@Auto", SqlDbType.VarChar).Value = data;
                        cmd.Parameters.Add("@Phone", SqlDbType.VarChar).Value = tbxPhone.Text;

                        cmd.ExecuteNonQuery();
                        _connection.Close();

                        DisplayAlert("Пользователь успешно добавлен.", false, "/admin/users");
                    }
                }
                catch (Exception ex)
                {
                    DisplayAlert(string.Format("Ошибка: {0}",ex.Message), true);
                    if (admin != null)
                    {
                        HouseSiteService.SaveLogError((Guid)admin.ProviderUserKey, ex.Message, ex.StackTrace, connect_str, obj =>
                        {
                            var href = "<script>setTimeout(function(){ window.location.href = '/admin/users'; }, 500);</script>";
                            ClientScript.RegisterStartupScript(
                              this.GetType(),
                              Guid.NewGuid().ToString(), href);
                        });
                    }
                    else
                    {
                        var href = "<script>setTimeout(function(){ window.location.href = '/home'; }, 500);</script>";
                        ClientScript.RegisterStartupScript(
                          this.GetType(),
                          Guid.NewGuid().ToString(), href);
                    }
                }
            }
        }
        catch (Exception ex)
        {
            DisplayAlert(string.Format("Ошибка: {0}", ex.Message), true);
            if (admin != null)
            {
                HouseSiteService.SaveLogError((Guid)admin.ProviderUserKey, ex.Message, ex.StackTrace, connect_str, obj =>
                {
                    var href = "<script>setTimeout(function(){ window.location.href = '/admin/users'; }, 500);</script>";
                    ClientScript.RegisterStartupScript(
                      this.GetType(),
                      Guid.NewGuid().ToString(), href);
                });
            }
            else
            {
                var href = "<script>setTimeout(function(){ window.location.href = '/home'; }, 500);</script>";
                ClientScript.RegisterStartupScript(
                  this.GetType(),
                  Guid.NewGuid().ToString(), href);
            }
        }
    }

    protected virtual void DisplayAlert(string message, bool error, string url = "")
    {
        string begin = @"setTimeout(function(){";
        string end = @";}, 1000);";


        ClientScript.RegisterStartupScript(
          this.GetType(),
          Guid.NewGuid().ToString(),
          string.Format("{2}alertMsg('{0}','{1}', '{4}'){3}",
            message.Replace("'", @"\'").Replace("\n", "\\n").Replace("\r", "\\r"),
            error ? "warning" : "info", begin, end, url),
            true);
    }
}