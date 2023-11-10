using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Web.Security;

public partial class AccepRules : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.Form["userName"] != null)
        {
            var result = UpdateUser(Request.Form["userName"]);
            Response.Clear();
            Response.ClearContent();
            Response.Write(result);
        }
    }

    private bool UpdateUser(string userName)
    {
        MembershipUser user = Membership.GetUser(userName);

        if (user != null)
        {
            string connect_str = ConfigurationManager.ConnectionStrings["migConnectionString"].ConnectionString;

            try
            {
                using (SqlConnection _connection = new SqlConnection(connect_str))
                {
                    _connection.Open();

                    SqlCommand cmd = new SqlCommand("Update [hs_Users] Set [ParkingRules] = 'True'," +
                        " [PersonalRules] = 'True' " +
                        " FROM [hs_Users] Where UserId = @UserID", _connection);
                    cmd.Parameters.Add("@UserID", SqlDbType.UniqueIdentifier).Value = (Guid)user.ProviderUserKey;

                    cmd.ExecuteNonQuery();

                    _connection.Close();
                }
                return true;
            }
            catch (Exception er)
            {
                return false;
            }
        }
        return false;
    }
}