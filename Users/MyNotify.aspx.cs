using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Windows.Markup;
using System.Data.Common;
using System.Threading;

public partial class MyNotify : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        GetMyNotify();
    }

    private void GetMyNotify()
    {
        try
        {
            MembershipUser user = Membership.GetUser();

            if (user != null)
            {
                string connect_str = ConfigurationManager.ConnectionStrings["migConnectionString"].ConnectionString;
                try
                {
                    using (SqlConnection _connection = new SqlConnection(connect_str))
                    {
                        _connection.Open();
                        SqlCommand cmd = new SqlCommand("SELECT ROW_NUMBER() OVER (Order by CreatedAt desc) AS RowNumber, * FROM [hs_UsersNotify] Where UserID = @UserId", _connection);
                        cmd.Parameters.Add("@UserId", SqlDbType.UniqueIdentifier).Value = (Guid)user.ProviderUserKey;
                        
                        SqlDataAdapter da = new SqlDataAdapter(cmd);
                        DataTable notify = new DataTable();
                        da.Fill(notify);

                        NotifyRepeater.DataSource= notify;
                        NotifyRepeater.DataBind();

                        //_connection.Close();
                    }
                }
                catch (Exception ex)
                {
                    string base_ = System.Web.HttpContext.Current.Server.MapPath("~\\Catch");
                    File.AppendAllText(base_ + "\\_exc.txt", DateTime.Now + "btnCreateUser_Click \n" + ex + "\n");
                }
            }
        }
        catch
        {

        }
    }

    protected void send_fcm_Click(object sender, EventArgs e)
    {       
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
        try
        {
            MembershipUser user = Membership.GetUser();

            if (user != null)
            {
                string connect_str = ConfigurationManager.ConnectionStrings["migConnectionString"].ConnectionString;
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
                    string base_ = System.Web.HttpContext.Current.Server.MapPath("~\\Catch");
                    File.AppendAllText(base_ + "\\_exc.txt", DateTime.Now + "btnCreateUser_Click \n" + ex + "\n");
                }
            }
        }
        catch (Exception ex)
        {
            string base_ = System.Web.HttpContext.Current.Server.MapPath("~\\Catch");
            File.AppendAllText(base_ + "\\_exc.txt", DateTime.Now + "btnCreateUser_Click \n" + ex + "\n");
        }
    }

    public bool getVisibility(object data)
    {
        if(data == null) return true;
        bool isRead = (bool)data;

        return !isRead;
    }

    //protected void rptr_ItemDataBound(object sender, RepeaterItemEventArgs e)
    //{
    //    if (e.Item.ItemType == ListItemType.AlternatingItem || e.Item.ItemType == ListItemType.Item)
    //    {
    //        // Stuff to databind
    //        Button myButton = (Button)e.Item.FindControl("btn_isRead");

    //        myButton.CommandName = "ChangeIsRead";
    //        myButton.CommandArgument = (((System.Data.DataRowView)e.Item.DataItem).Row.ItemArray[6]).ToString();

    //        try
    //        {
    //            this.Page.ClientScript.RegisterForEventValidation(new PostBackOptions(myButton));
    //        }
    //        catch { }
    //    }
    //}

    //protected void rptr_ItemCommand(object source, RepeaterCommandEventArgs e)
    //{
    //    if (e.CommandName == "ChangeIsRead")
    //    {
    //        UpdateRow(e.CommandArgument);
    //        GetMyNotify();
    //    }
    //}

    //protected void Page_Init(object sender, EventArgs e)
    //{
    //    // rptr is your repeater's name
    //    NotifyRepeater.ItemCommand += new RepeaterCommandEventHandler(rptr_ItemCommand);
    //}
}