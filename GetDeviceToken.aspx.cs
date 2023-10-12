using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Web.Security;
using System.IO;

public partial class GetDeviceToken : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            var value = Request.Form["device"];
            if(value != null)
            {
                SetTokenByUser(value);
                Response.Write("token успешно принят");
            }
        }        
    }

    private void SetTokenByUser(string value)
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

                    SqlCommand cmd = new SqlCommand("Select Count(DeviceToken) From hs_TokenDevices Where UserId = @UserId",_connection);

                    cmd.Parameters.Add("@UserId", SqlDbType.UniqueIdentifier).Value = (Guid)user.ProviderUserKey;

                    var count = cmd.ExecuteScalar();
                    if (count != null)
                    {
                        if ((int)count == 0)
                        {
                            cmd = new SqlCommand("INSERT INTO hs_TokenDevices (UserId, DeviceToken, RegisterDate) " +
                            "VALUES (@UserId, @Token, @Date)", _connection);

                            cmd.Parameters.Add("@UserId", SqlDbType.UniqueIdentifier).Value = (Guid)user.ProviderUserKey;
                            cmd.Parameters.Add("@Date", SqlDbType.DateTime).Value = DateTime.Now;
                            cmd.Parameters.Add("@Token", SqlDbType.VarChar).Value = value;

                            cmd.ExecuteNonQuery();
                            _connection.Close();
                        }
                    } 
                }
            }
            catch (Exception ex)
            {
                string base_ = System.Web.HttpContext.Current.Server.MapPath("~\\Catch");
                File.AppendAllText(base_ + "\\_exc.txt", DateTime.Now + "btnCreateUser_Click \n" + ex + "\n");
            }
        }
    }
}