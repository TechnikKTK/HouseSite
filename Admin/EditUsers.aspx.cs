using System;
using System.Web.Security;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;

public partial class EditUsers : System.Web.UI.Page
{
    protected string lockedPrichina = "много раз ввел пароль не правильно.";
    string userName = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        userName = this.Request.QueryString["UserName"];
        var admin = Membership.GetUser();

        if (userName == null) return;

        if (!this.IsPostBack)
        {
            MembershipUser user = Membership.GetUser(userName);
            lblUserName.Text = user.UserName;
            lnkEmailAddress.Text = user.Email;
            lblRegistered.Text = user.CreationDate.ToString();
            lblLastLogin.Text = user.LastLoginDate.ToString();
            lblLastActivity.Text = user.LastActivityDate.ToString();
            chkIsOnlineNow.Text = user.IsOnline ? " Да":" Нет";

            string connect_str = ConfigurationManager.ConnectionStrings["migConnectionString"].ConnectionString;
            try
            {
                using (SqlConnection _connection = new SqlConnection(connect_str))
                {
                    _connection.Open();
                    SqlCommand cmd = new SqlCommand("SELECT * FROM hs_Users WHERE UserId=@UserId", _connection);
                  
                    cmd.Parameters.Add("@UserId", SqlDbType.UniqueIdentifier).Value = (Guid)user.ProviderUserKey;
         
                    SqlDataReader reader =  cmd.ExecuteReader();

                    if (reader.Read())
                    {
                        tbxFlatNumber.Text =  reader["FlatNumber"] == DBNull.Value ? "" : reader["FlatNumber"].ToString();
                        tbxEntrance.Text = reader["Entrance"] == DBNull.Value ? "" : reader["Entrance"].ToString(); 
                        tbxFloor.Text = reader["Floor"] == DBNull.Value ? "" : reader["Floor"].ToString();
                        tbxName.Text = reader["Name"] == DBNull.Value ? "" : reader["Name"].ToString();
                        tbxPatronumic.Text = reader["Patronumic"] == DBNull.Value ? "" : reader["Patronumic"].ToString();
                        tbxLastName.Text = reader["LastName"] == DBNull.Value ? "" : reader["LastName"].ToString();
                        ddlStatus.Text = reader["Status"] == DBNull.Value ? "" : reader["Status"].ToString();
                        tbxPhone.Text = reader["Phone"] == DBNull.Value ? "" : reader["Phone"].ToString();
                        tbxPhoneAdv.Text = reader["PhoneAdv"] == DBNull.Value ? "" : reader["PhoneAdv"].ToString();
                        tbxEmail.Text = reader["Email"] == DBNull.Value ? "" : reader["Email"].ToString();
                        tbxAutoNumber.Text = reader["AutoNumber"] == DBNull.Value ? "" : reader["AutoNumber"].ToString();
                        tbxAutoMark.Text = reader["AutoMark"] == DBNull.Value ? "" : reader["AutoMark"].ToString();
                        tbxAutoMarkAdv.Text = reader["AutoMarkAdv"] == DBNull.Value ? "" : reader["AutoMarkAdv"].ToString();
                        tbxAutoNumberAdv.Text = reader["AutoNumberAdv"] == DBNull.Value ? "" : reader["AutoNumberAdv"].ToString();
                        tbxRemoteCamera.Text = reader["RemoteCamera"] == DBNull.Value ? "" : reader["RemoteCamera"].ToString();
                        chkSOS.Checked = reader["SOS"] == DBNull.Value ? false : (bool)reader["SOS"];
                        chkRulesParking.Checked = reader["ParkingRules"] == DBNull.Value ? false : (bool)reader["ParkingRules"];
                        chkRulesPersonal.Checked = reader["PersonalRules"] == DBNull.Value ? false : (bool)reader["PersonalRules"];
                        tbxComments.Text = reader["Comments"] == DBNull.Value ? "" : reader["Comments"].ToString();
                    }

                    _connection.Close();
                }
            }
            catch (Exception ex)
            {
                if (admin != null)
                {
                    HouseSiteService.SaveLogError((Guid)admin.ProviderUserKey, ex.Message, ex.StackTrace, connect_str, obj =>
                    {
                        var href = "<script>setTimeout(function(){ window.location.href = '/home'; }, 500);</script>";
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

    protected void btnGenPass_Click(object sender, EventArgs e)
    {
        tbxPassword.Text = GetAlphaNumericRandomString(6);
    }

    protected string GetLockedUser()
    {
        MembershipUser user = Membership.GetUser(userName);

        if (user == null)
        {
            lockedPrichina = "администратор сменил логин пользователя.";
            return "hidden";  
        }
        else
        {
            if (!user.IsApproved)
                lockedPrichina = "заблокирован администратором.";
        }
        return user.IsLockedOut ? "" : "hidden";
    }

    protected void btnChangePass_Click(object sender, EventArgs e)
    {
        MembershipUser user = Membership.GetUser(userName);
        if(user.IsLockedOut)
        {
            if (user.IsApproved)
                user.UnlockUser();
            else
            {
                DisplayAlert("Ошибка: Для заблокированного пользователя нельзя менять пароль.", true);
                return;
            }
        }

        var old_pass = user.ResetPassword();
        var new_pass = tbxPassword.Text.Length > 0 ? tbxPassword.Text : user.ResetPassword();
        
        if(user.ChangePassword(old_pass,new_pass))
        {
            DisplayAlert("Успех: Пароль пользователя успешно обновлен.", false);
        }
        else
        {
            DisplayAlert("Ошибка: Пароль пользователя не обновлен.", true);
        }
    }
        
    protected void btnSaveUserInfo_Click(object sender, EventArgs e)
    {
        var admin = Membership.GetUser();

        string connect_str = ConfigurationManager.ConnectionStrings["migConnectionString"].ConnectionString;
        try
        {
            using (SqlConnection _connection = new SqlConnection(connect_str))
            {
                MembershipUser user = Membership.GetUser(userName);

                _connection.Open();
                SqlCommand cmd = new SqlCommand("UPDATE hs_Users SET " +
                                                "FlatNumber=@FlatNumber, Entrance=@Entrance, " +
                                                "Floor=@Floor, Name=@Name, Status=@Status, " +
                                                "Phone=@Phone, PhoneAdv = @PhoneAdv, " +
                                                "Email=@Email, AutoNumber=@AutoNumber, " +
                                                "AutoMark=@AutoMark, AutoMarkAdv=@AutoMarkAdv, " +
                                                "AutoNumberAdv = @AutoNumberAdv, " +
                                                "RemoteCamera=@RemoteCamera, SOS=@SOS, " +
                                                "Comments=@Comments, " +
                                                "LastName = @LastName, Patronumic = @PatronName," +
                                                "ParkingRules =@ParkingRules, PersonalRules = @PersonalRules " +
                                                " WHERE UserId=@UserId", _connection);

                cmd.Parameters.Add("@UserId", SqlDbType.UniqueIdentifier).Value = (Guid)user.ProviderUserKey;
                cmd.Parameters.Add("@FlatNumber", SqlDbType.VarChar).Value = tbxFlatNumber.Text;
                cmd.Parameters.Add("@Entrance", SqlDbType.VarChar).Value = tbxEntrance.Text;
                cmd.Parameters.Add("@Floor", SqlDbType.VarChar).Value = tbxFloor.Text;
                cmd.Parameters.Add("@Name", SqlDbType.VarChar).Value = tbxName.Text;
                cmd.Parameters.Add("@LastName", SqlDbType.VarChar).Value = tbxLastName.Text;
                cmd.Parameters.Add("@PatronName", SqlDbType.VarChar).Value = tbxPatronumic.Text;
                cmd.Parameters.Add("@Status", SqlDbType.VarChar).Value = ddlStatus.Text;
                cmd.Parameters.Add("@Phone", SqlDbType.VarChar).Value = tbxPhone.Text;
                cmd.Parameters.Add("@PhoneAdv", SqlDbType.VarChar).Value = tbxPhoneAdv.Text;
                cmd.Parameters.Add("@Email", SqlDbType.VarChar).Value = tbxEmail.Text;
                var data = tbxAutoNumber.Text.Replace(" ", "");
                cmd.Parameters.Add("@AutoNumber", SqlDbType.VarChar).Value = data;
                cmd.Parameters.Add("@AutoMark", SqlDbType.VarChar).Value = tbxAutoMark.Text;
                var data2 = tbxAutoNumberAdv.Text.Replace(" ", "");
                cmd.Parameters.Add("@AutoNumberAdv", SqlDbType.VarChar).Value = data2;
                cmd.Parameters.Add("@AutoMarkAdv", SqlDbType.VarChar).Value = tbxAutoMarkAdv.Text;
                cmd.Parameters.Add("@RemoteCamera", SqlDbType.VarChar).Value = tbxRemoteCamera.Text;
                cmd.Parameters.Add("@SOS", SqlDbType.Bit).Value = chkSOS.Checked;
                cmd.Parameters.Add("@ParkingRules", SqlDbType.Bit).Value = chkRulesParking.Checked;
                cmd.Parameters.Add("@PersonalRules", SqlDbType.Bit).Value = chkRulesPersonal.Checked;
                cmd.Parameters.Add("@Comments", SqlDbType.VarChar).Value = tbxComments.Text;

                cmd.ExecuteNonQuery();
                _connection.Close();

                DisplayAlert("Успех: Данные пользователя успешно сохранены.", false, "/admin/users");
            }
        }
        catch (Exception ex)
        {
            if (admin != null)
            {
                DisplayAlert(string.Format("Ошибка: {0}", ex.Message), true);

                HouseSiteService.SaveLogError((Guid)admin.ProviderUserKey, ex.Message, ex.StackTrace, connect_str, obj =>
                {
                    var href = "<script>setTimeout(function(){ window.location.href = '/home'; }, 500);</script>";
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

    private bool CheckLogin(MembershipUser user, string newUserName, string connstring)
    {
        if(user.UserName == newUserName) { return true; }

        var new_user = Membership.GetUser(newUserName);
        if (new_user != null)
        {
            return false;
        }

        using (SqlConnection connection = new SqlConnection(connstring))
        {
            connection.Open();

            using (SqlCommand command = connection.CreateCommand())
            {
                command.CommandText = "UPDATE aspnet_Users SET UserName=@NewUsername,LoweredUserName=@LoweredNewUsername " +
                    "WHERE UserName=@OldUsername";

                SqlParameter parameter = new SqlParameter("@OldUsername", SqlDbType.VarChar);
                parameter.Value = user.UserName;
                command.Parameters.Add(parameter);

                parameter = new SqlParameter("@NewUsername", SqlDbType.VarChar);
                parameter.Value = newUserName;
                command.Parameters.Add(parameter);

                parameter = new SqlParameter("@LoweredNewUsername", SqlDbType.VarChar);
                parameter.Value = newUserName.ToLower();
                command.Parameters.Add(parameter);

                return command.ExecuteNonQuery() > 0;
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

    protected void btnUnlock_Click(object sender, EventArgs e)
    {
        MembershipUser user = Membership.GetUser(userName);
        if (user.IsLockedOut)
        {
            if (user.IsApproved)
            {
                user.UnlockUser();
                DisplayAlert("Успех: Пользователь успешно разблокирован.", false);
            }
            else
            {
                DisplayAlert("Ошибка: Вы ограничили вход, разрешите вход пользователю.", true);
                return;
            }
        }
    }

    protected void btnChangeLogin_Click(object sender, EventArgs e)
    {
        MembershipUser user = Membership.GetUser(userName);
        string connect_str = ConfigurationManager.ConnectionStrings["migConnectionString"].ConnectionString;

        if (!CheckLogin(user, lblUserName.Text, connect_str))
        {
            DisplayAlert("Ошибка: Пользователь с таким логином уже зарегистрирован. Выберите другой логин.", true);
        }
        else
        {
            DisplayAlert("Успех: Логин пользователя успешно изменен.", false, "/admin/users/edit?UserName=" + lblUserName.Text);
        }
    }
}