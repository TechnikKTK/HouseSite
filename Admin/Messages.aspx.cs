using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;
using System.Web.Security;
using System.Web.UI.WebControls;

public partial class Messages : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if(!IsPostBack)
        {
            GetMessages();
        }
    }

    protected string GetClass(object data)
    {
        bool isRead = (bool)data;

        if (!isRead)
        {
            return "class='table-bold'";
        }
        else
        {
            return "class='table-read'";
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

    protected void ChangeIsRead(object sender, CommandEventArgs e)
    {
        UpdateRow(e.CommandArgument);
        GetMessages();
    }

    protected void DeleteMessage(object sender, CommandEventArgs e)
    {
        var admin = Membership.GetUser();
        string connect_str = ConfigurationManager.ConnectionStrings["migConnectionString"].ConnectionString;
        try
        {
            var messageID = int.Parse(e.CommandArgument.ToString());

            using (SqlConnection _connection = new SqlConnection(connect_str))
            {
                _connection.Open();

                SqlCommand cmd = new SqlCommand("Delete FROM [hs_Messages] WHERE [ID]=@Id", _connection);

                cmd.Parameters.Add("@Id", SqlDbType.Int).Value = messageID;

                cmd.ExecuteNonQuery();
                _connection.Close();
            }

            GetMessages();
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

    protected void GoAnswer(object sender, CommandEventArgs e)
    {
        
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

                        SqlCommand cmd = new SqlCommand("Update [hs_Messages] Set IsRead = 'True', State= 1 Where ID = @Row", _connection);
                        cmd.Parameters.Add("@Row", SqlDbType.Int).Value = int.Parse(row.ToString());
                        cmd.ExecuteNonQuery();

                        _connection.Close();
                    }
                }
                catch (Exception ex)
                {
                    HouseSiteService.SaveLogError((Guid)user.ProviderUserKey, ex.Message, ex.StackTrace, connect_str, obj =>
                    {
                        var href = "<script>setTimeout(function(){ window.location.href = '/admin/messages'; }, 500);</script>";
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

    protected string EncodeMessage(object messageBody)
    {
        if (messageBody == null) return "";

        var message = messageBody.ToString().Replace("\r\n", " ").Replace("\n", "").Trim().Trim().Trim();

        return message;
    }

    private void GetMessages()
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
                        SqlCommand cmd = new SqlCommand("SELECT [aspnet_Users].UserName as " +
                            "UserName,[hs_Messages].UserId, [ID], [BodyMessage], [TypeMessage]," +
                            " [_Date] AS CreatedAt, [IsRead] FROM [hs_Messages] Inner Join " +
                            "[aspnet_Users] On [aspnet_Users].UserId = [hs_Messages].UserId" +
                            " Order by CreatedAt desc", _connection);


                        SqlDataAdapter da = new SqlDataAdapter(cmd);
                        DataTable notify = new DataTable();
                        da.Fill(notify);

                        NotifyRepeater.DataSource = notify;
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