using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IdentityModel;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class MyNotify : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //if (!IsPostBack)
        //{
        //    Page.RegisterAsyncTask(new PageAsyncTask(SendData));
        //}

        GetMyNotifications();
    }

    private void GetMyNotifications()
    {
        try
        {
            MembershipUser user = Membership.GetUser();

            if(user != null)
            {
                string connectionString = ConfigurationManager.ConnectionStrings["migConnectionString"].ConnectionString;
                try
                {
                    using(SqlConnection _connection = new SqlConnection(connectionString))
                    {
                        SqlCommand cmd = new SqlCommand("SELECT ROW_NUMBER() OVER (Order by CreatedAt desc) AS RowNumber, * " + 
                            "FROM [hs_UsersNotify] Where UserID = @UserId", _connection);

                        cmd.Parameters.AddWithValue("@UserId", (Guid)user.ProviderUserKey);

                        SqlDataAdapter da = new SqlDataAdapter(cmd);
                        DataTable notify = new DataTable();
                        da.Fill(notify);

                        NotifyRepeater.DataSource = notify;
                        NotifyRepeater.DataBind();
                    }
                }
                catch (Exception ex)
                {
                    string base_ = HttpContext.Current.Server.MapPath("~\\Catch");
                    File.AppendAllText(base_ + "\\exc.txt", DateTime.Now + "btnCreateUser_Click \n" + ex + "\n");
                }
            }
        }
        catch(Exception ex)
        {
            string base_ = HttpContext.Current.Server.MapPath("~\\Catch");
            File.AppendAllText(base_ + "\\exc.txt", DateTime.Now + "btnCreateUser_Click \n" + ex + "\n");
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
            return "class=''";
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
            return "color:#ffffff";
    }

    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        GetMyNotifications();
    }
    protected void ChangeIsRead(object sender, CommandEventArgs e)
    {
        UpdateRow(e.CommandArgument);
        GetMyNotifications();
    }
    public bool getVisibility(object data)
    {
        if (data == null) return true;
        bool isRead = (bool)data;

        return !isRead;
    }
    private void UpdateRow(object row)
    {
        try
        {
            MembershipUser user = Membership.GetUser();

            if (user != null)
            {
                string connectionString = ConfigurationManager.ConnectionStrings["migConnectionString"].ConnectionString;
                try
                {
                    using (SqlConnection _connection = new SqlConnection(connectionString))
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
                    string base_ = HttpContext.Current.Server.MapPath("~\\Catch");
                    File.AppendAllText(base_ + "\\exc.txt", DateTime.Now + "btnCreateUser_Click \n" + ex + "\n");
                }
            }
        }
        catch (Exception ex)
        {
            string base_ = HttpContext.Current.Server.MapPath("~\\Catch");
            File.AppendAllText(base_ + "\\exc.txt", DateTime.Now + "btnCreateUser_Click \n" + ex + "\n");
        }
    }
    protected void send_fcm_Click(object sender, EventArgs e)
    {       
        Page.ExecuteRegisteredAsyncTasks();
    }

    public async Task SendData()
    {
        await Fcm.SendFcmMessageAsync(
            "fs3IhPqPxArBXTS603thCC:APA91bF4ZuLgNCT-b18g-QspppKri1aTDFTPJDSnNPwy2eEWV5n1kL25g20Vfd4kQ6X-jmsxiu05H5m8WpmbAUBkeyGesSGCPqwP-rUz8wFTXtMNaWgYwRuOb36cDcI4Pnx96cKJz5QP",
            "Hello",
            "XA-XA-XA!!!");
    }
}