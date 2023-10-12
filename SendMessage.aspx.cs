using System;
using System.Web.Security;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.IO;

public partial class SendMessage : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (this.Page.User.Identity.IsAuthenticated)
        {
            if (this.Page.User.IsInRole("admin"))
            {
                Response.Redirect("~/admin/users");
            }
        }
        else    Response.Redirect("/home");
    }

    protected void btnSendMessages_Click(object sender, EventArgs e)
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
                        SqlCommand cmd = new SqlCommand("INSERT INTO hs_Messages (UserId, _Date, TypeMessage, BodyMessage) VALUES (@UserId, @Date, @TypeMessage, @BodyMessage)", _connection);
                        cmd.Parameters.Add("@UserId", SqlDbType.UniqueIdentifier).Value = (Guid)user.ProviderUserKey;
                        cmd.Parameters.Add("@Date", SqlDbType.DateTime).Value = DateTime.Now;
                        cmd.Parameters.Add("@TypeMessage", SqlDbType.VarChar).Value = ddlTypeMessage.Text;
                        cmd.Parameters.Add("@BodyMessage", SqlDbType.VarChar).Value = tbxBodyMessage.Text;

                        cmd.ExecuteNonQuery();
                        _connection.Close();
                    }

                    Response.Redirect("/message-ok");
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
}