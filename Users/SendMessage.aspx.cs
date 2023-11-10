using System;
using System.Web.Security;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;

public partial class SendMessage : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

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
                    object lastID = 0;
                    using (SqlConnection _connection = new SqlConnection(connect_str))
                    {
                        _connection.Open();
                        SqlCommand cmd = new SqlCommand("INSERT INTO hs_Messages (UserId, _Date, TypeMessage, BodyMessage) VALUES (@UserId, @Date, @TypeMessage, @BodyMessage)", _connection);
                        cmd.Parameters.Add("@UserId", SqlDbType.UniqueIdentifier).Value = (Guid)user.ProviderUserKey;
                        cmd.Parameters.Add("@Date", SqlDbType.DateTime).Value = DateTime.Now;
                        cmd.Parameters.Add("@TypeMessage", SqlDbType.VarChar).Value = ddlTypeMessage.Text;
                        cmd.Parameters.Add("@BodyMessage", SqlDbType.VarChar).Value = tbxBodyMessage.Text;

                        cmd.ExecuteNonQuery();

                        cmd = new SqlCommand("Select Max(ID) From [hs_Messages] Where UserId=@UserId", _connection);
                        cmd.Parameters.Add("@UserId", SqlDbType.UniqueIdentifier).Value = (Guid)user.ProviderUserKey;

                        lastID = cmd.ExecuteScalar();

                       _connection.Close();
                    }

                    tbxBodyMessage.Text=string.Empty;

                    var text = string.Format("Ваше обращение зарегистрировано! Номер:{0}", lastID);

                    DisplayAlert(text, false, url: "/messages");
                }
                catch (Exception ex)
                {
                    DisplayAlert(ex.Message, true);
                }                
            }
        }
        catch(Exception ex)
        {
            DisplayAlert(ex.Message, true);
        }
    }

    protected virtual void DisplayAlert(string message, bool error, string url = "")
    {
        string begin = @"setTimeout(function(){";
        string end = @";}, 1000);";


        ClientScript.RegisterStartupScript(
          this.GetType(),
          Guid.NewGuid().ToString(),
          string.Format("{2}alertMsg('{0}','{1}', '{4}'){3}",
            message.Replace("'", @"\'").Replace("\n", "\\n").Replace("\r", "\\r"),
            error ? "warning" : "info", begin, end, url),
            true);
    }
}