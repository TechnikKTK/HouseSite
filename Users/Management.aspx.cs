using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Web.Security;

public partial class Management : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!this.IsPostBack)
        {
            MembershipUser user = Membership.GetUser();
            
            string connect_str = ConfigurationManager.ConnectionStrings["migConnectionString"].ConnectionString;
            try
            {
                using (SqlConnection _connection = new SqlConnection(connect_str))
                {
                    _connection.Open();
                    SqlCommand cmd = new SqlCommand("SELECT * FROM hs_Users WHERE UserId=@UserId", _connection);

                    cmd.Parameters.Add("@UserId", SqlDbType.UniqueIdentifier).Value = (Guid)user.ProviderUserKey;

                    SqlDataReader reader = cmd.ExecuteReader();

                    if (reader.Read())
                    {
                        lblFlatNumber.Text = GetString(reader["FlatNumber"], "Не указано"); 
                        lblEntrance.Text = GetString(reader["Entrance"], "Не указано");
                        lblFloor.Text = GetString(reader["Floor"], "Не указано");
                        lblFio.Text = string.Format("{0} {1} {2}", reader["LastName"], reader["Name"], reader["Patronumic"]);
                        lblPhone.Text = GetString(reader["Phone"], "Не указано"); 
                        lblPhoneAdv.Text = GetString(reader["PhoneAdv"], "Не указано");
                        lblEmail.Text = GetString(reader["Email"], "Не указано");
                        lblEmail.NavigateUrl = GetNavString(reader["Email"], "#");
                        lblAutoNumber.Text = GetString(reader["AutoNumber"], " номер не указан");
                        lblStatus.Text = GetString(reader["Status"], "Не указано");
                        lblAutoMark.Text = GetString(reader["AutoMark"], "Марка не указана,"); 
                        lblAutoMarkAdv.Text = GetString(reader["AutoMarkAdv"], "Марка не указана,"); 
                        lblAutoNumberAdv.Text = GetString(reader["AutoNumberAdv"], " номер не указан"); 
                        lblRemoteCamera.Text = GetString(reader["RemoteCamera"], "Нет доступа к камере");
                        lblSOS.Text = GetString(reader["SOS"], "Не cостоит", "Состоит"); 
                        lblBarrierLocked.Text = GetString(reader["BrokeRules"], "открыт доступ", "заблокирован доступ");
                    }

                    _connection.Close();
                }
            }
            catch (Exception ex)
            {
                if (user != null)
                {
                    HouseSiteService.SaveLogError((Guid)user.ProviderUserKey, ex.Message, ex.StackTrace, connect_str, obj =>
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

    private string GetNavString(object data, string empty)
    {
        if (data == DBNull.Value)
        {
            return empty;
        }
        else if (data.ToString().Length < 1)
        {
            return empty;
        }
        else
        {
            return "mailto:" + data.ToString();
        }

    }

    private string GetString(object data, string empty, string full="")
    {
        if(data == DBNull.Value)
        {
            return empty;
        }
        else if(data.ToString().Length <1)
        {
            return empty;
        }
        else
        {
            if (data is bool)
            {
                return (bool)data ? full : empty;
            }
            else
            {
                var d = full.Length > 0 ? full : data.ToString();
                return d;
            }
        }
    }
}