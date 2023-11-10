using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Web.Security;
using System.Web.UI.WebControls;

public partial class MyNotify : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        GetMyNotify();
    }

    private void GetMyNotify()
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
                        SqlCommand cmd = new SqlCommand("SELECT * FROM [hs_UsersNotify] Where [UserId] = @UserId Order by [CreatedAt] desc", _connection);

                        cmd.Parameters.AddWithValue("@UserId", (Guid)user.ProviderUserKey);

                        SqlDataAdapter da = new SqlDataAdapter(cmd);
                        DataTable notify = new DataTable();
                        da.Fill(notify);

                        NotifyRepeater.DataSource= notify;
                        NotifyRepeater.DataBind();
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
        catch(Exception ex)
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

    protected void send_fcm_Click(object sender, EventArgs e)
    {       
    }

    protected string GetLink(object ansID)
    {
        if (ansID != Convert.DBNull)
        {
            return string.Format("<span data-toggle='modal' data-target='#modalQuest' " +
                "style='cursor:pointer;text-decoration: underline; font-weight: bold;'" +
                " onclick='getMessage({0})'> " +
                " на ваше обращение № {0}</span>", ansID);
        }
        else
        {
            return "";
        }
    }

    protected string GetIcon(object message_type, object data)
    {
        bool isRead = (bool)data;
        int type = (int)message_type;

        if (!isRead)
        {
            switch (type)
            {
                case 0:
                    return "fa-circle-info";
                case 1:
                    return "fa-file";
                case 2:
                    return "fa-triangle-exclamation";
                case 3:
                    return "fa-passport";
                default:
                    return "";
            }
        }
        else
            return "";
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
        GetMyNotify();
    }

    protected void ChangeIsRead(object sender, CommandEventArgs e)
    {
        UpdateRow(e.CommandArgument); 
        GetMyNotify();
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

    public bool getVisibility(object data)
    {
        if(data == null) return true;
        bool isRead = (bool)data;

        return !isRead;
    }

}