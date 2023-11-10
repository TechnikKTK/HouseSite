using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Users_MyMessages : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        GetMyMessages();
    }

    private void GetMyMessages()
    {
        string connect_str = ConfigurationManager.ConnectionStrings["migConnectionString"].ConnectionString;
        MembershipUser user = Membership.GetUser();

        try
        {
            user = Membership.GetUser();

            if (user != null)
            {
                try
                {
                    using (SqlConnection _connection = new SqlConnection(connect_str))
                    {
                        SqlCommand cmd = new SqlCommand("SELECT hs_Messages.* FROM hs_Messages WHERE (UserId = @UserId) Order by [_Date] desc", _connection);

                        cmd.Parameters.AddWithValue("@UserId", (Guid)user.ProviderUserKey);

                        SqlDataAdapter da = new SqlDataAdapter(cmd);
                        DataTable notify = new DataTable();
                        da.Fill(notify);

                        MessageRepeater.DataSource = notify;
                        MessageRepeater.DataBind();
                    }
                }
                catch (Exception ex)
                {
                    HouseSiteService.SaveLogError((Guid)user.ProviderUserKey, ex.Message, ex.StackTrace, connect_str, obj =>
                    {
                        var href = "<script>setTimeout(function(){ window.location.href = '/home'; }, 500);</script>";
                        ClientScript.RegisterStartupScript(
                          this.GetType(),
                          Guid.NewGuid().ToString(), href);
                    });
                }
            }
        }
        catch (Exception ex)
        {
            HouseSiteService.SaveLogError((Guid)user.ProviderUserKey, ex.Message, ex.StackTrace, connect_str, obj =>
            {
                var href = "<script>setTimeout(function(){ window.location.href = '/home'; }, 500);</script>";
                ClientScript.RegisterStartupScript(
                  this.GetType(),
                  Guid.NewGuid().ToString(), href);
            });
        }
    }


    protected string GetClass(object message_type, object data)
    {
        bool isRead = (bool)data;
        int type = (int)message_type;

        if (!isRead)
        {
            switch (type)
            {
                case 0:
                    return "class='table-info'";
                case 1:
                    return "";
                case 2:
                    return "class='table-danger'";
                case 3:
                    return "class='table-light'";
                default:
                    return "";
            }
        }
        else
        {
            return "class=''";
        }
    }

    protected string GetStatus(object status)
    {
        int type = (int)status;

        switch (type)
        {
            case 1:
                return "Ждет ответа";
            case 2:
                return "<a href='/notify'>Есть ответ</a>";
            default:
                return "Новое";
        }
    }

    protected string GetImage(object data)
    {
        bool isRead = (bool)data;

        if (isRead)
        {
            return "/images/envelope-open-text-solid.svg";
        }
        else
        {
            return "/images/envelope-solid.svg";
        }
    }
    protected string GetColor(object message_type, object data)
    {
        bool isRead = (bool)data;
        int type = (int)message_type;

        if (!isRead)
        {
            switch (type)
            {
                case 0:
                    return "color:#6362c7";
                case 1:
                    return "color:#39c057";
                case 2:
                    return "color:#aa0000";
                case 3:
                    return "color:#ffffff";
                default:
                    return "color:#000000";
            }
        }
        else
        {
            return "color:#ffffff";
        }
    }

    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        GetMyMessages();
    }

    protected void ChangeIsRead(object sender, CommandEventArgs e)
    {
        var data = e.CommandArgument as object[];
        if (!(bool)data[1])
        {
            UpdateRow(data[0]);
            GetMyMessages();
        }
    }

    private void UpdateRow(object row)
    {
        MembershipUser user = Membership.GetUser();
        string connect_str = ConfigurationManager.ConnectionStrings["migConnectionString"].ConnectionString;

        try
        {
            if (user != null)
            {
                try
                {
                    using (SqlConnection _connection = new SqlConnection(connect_str))
                    {
                        _connection.Open();

                        SqlCommand cmd = new SqlCommand("Update [hs_UsersNotify] Set IsRead = 'True' Where ID = @Row", _connection);
                        cmd.Parameters.Add("@Row", SqlDbType.Int).Value = int.Parse(row.ToString());
                        cmd.ExecuteNonQuery();

                        _connection.Close();
                    }
                }
                catch (Exception ex)
                {
                    HouseSiteService.SaveLogError((Guid)user.ProviderUserKey, ex.Message, ex.StackTrace, connect_str, obj =>
                    {
                        var href = "<script>setTimeout(function(){ window.location.href = '/home'; }, 500);</script>";
                        ClientScript.RegisterStartupScript(
                          this.GetType(),
                          Guid.NewGuid().ToString(), href);
                    });
                }
            }
        }
        catch (Exception ex)
        {
            HouseSiteService.SaveLogError((Guid)user.ProviderUserKey, ex.Message, ex.StackTrace, connect_str, obj =>
            {
                var href = "<script>setTimeout(function(){ window.location.href = '/home'; }, 500);</script>";
                ClientScript.RegisterStartupScript(
                  this.GetType(),
                  Guid.NewGuid().ToString(), href);
            });
        }
    }

}