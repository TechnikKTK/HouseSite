using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.IdentityModel.Protocols.WSTrust;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Users_GetMessage : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.Form["messageID"] != null)
        {
            var result = GetMessage(Request.Form["messageID"]);
            Response.Clear();
            Response.ClearContent();
            Response.Write(result);
        }
    }

    private string GetMessage(string messageID)
    {
        string result = "success";

        try
        {
            string connect_str = ConfigurationManager.ConnectionStrings["migConnectionString"].ConnectionString;
            using (SqlConnection _connection = new SqlConnection(connect_str))
            {
                _connection.Open();

                SqlCommand cmd = new SqlCommand("Select Top(1) [BodyMessage] From [hs_Messages] WHERE [ID]=@Id", _connection);
                cmd.Parameters.Add("@Id", SqlDbType.Int).Value = int.Parse(messageID);

                result = cmd.ExecuteScalar().ToString();

                _connection.Close();
            }
        }
        catch (Exception er)
        {
            result = "error: " + er.Message;
        }

        return result;
    }
}