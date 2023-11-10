using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web.Security;

public partial class CheckRules : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.Form["userName"] != null)
        {
            var result = AccessGranted(Request.Form["userName"]);
            Response.Clear();
            Response.ClearContent();
            Response.Write(result);
        }
    }

    private bool AccessGranted(string name)
    {
        MembershipUser user = Membership.GetUser(name);
        string connect_str = ConfigurationManager.ConnectionStrings["migConnectionString"].ConnectionString;
        List<bool> rules = new List<bool>();

        var role =  Roles.GetRolesForUser(name);
        if (role!=null)
        {
            if (role.Contains("admin"))
                return true;
        }

        if (user != null)
        {
            using (SqlConnection _connection = new SqlConnection(connect_str))
            {
                _connection.Open();

                SqlCommand cmd = new SqlCommand("SELECT Top(1) [ParkingRules], [PersonalRules] From [hs_Users] " +
                    " Where UserId = @UserID", _connection);
                cmd.Parameters.Add("@UserID", SqlDbType.UniqueIdentifier).Value = (Guid)user.ProviderUserKey;

                var reader = cmd.ExecuteReader();


                while (reader.Read())
                {
                    rules.Add((bool)reader[0]);
                    rules.Add((bool)reader[1]);
                }
                _connection.Close();
            }

            return rules.Count(r => r == true) == rules.Count;
        }

        return false;
    }
}