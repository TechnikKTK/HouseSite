using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;

public partial class Admin_AnswerToMessage : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.Form["userKey"] != null)
            if (Request.Form["messageID"] != null)
                if (Request.Form["answer"] != null)
                {
                    var result = AnswerMessage(Request.Form["userKey"], Request.Form["messageID"], Request.Form["answer"]);
                    Response.Clear();
                    Response.ClearContent();
                    Response.Write(result);
                }
    }

    private string AnswerMessage(string userkey, string messageID, string answer)
    {
        string result = "success";

        try
        {
            string connect_str = ConfigurationManager.ConnectionStrings["migConnectionString"].ConnectionString;
            using (SqlConnection _connection = new SqlConnection(connect_str))
            {
                _connection.Open();

                SqlCommand cmd = new SqlCommand("Update [hs_Messages] Set [IsRead] = 'True', State = 2 WHERE [ID]=@Id", _connection);
                cmd.Parameters.Add("@Id", SqlDbType.Int).Value = int.Parse(messageID);

                cmd.ExecuteNonQuery();

                cmd = new SqlCommand("INSERT INTO hs_UsersNotify (UserId, CreatedAt, Type, Message, AnswerID) VALUES (@UserId, @Date, @TypeMessage, @BodyMessage, @messageID)", _connection);
                cmd.Parameters.Add("@UserId", SqlDbType.UniqueIdentifier).Value = Guid.Parse(userkey);
                cmd.Parameters.Add("@Date", SqlDbType.DateTime).Value = DateTime.Now;
                cmd.Parameters.Add("@TypeMessage", SqlDbType.Int).Value = 0;
                cmd.Parameters.Add("@BodyMessage", SqlDbType.VarChar).Value = string.Format("Ответ администратора: {0}", answer);
                cmd.Parameters.Add("@messageID", SqlDbType.Int).Value = int.Parse(messageID);

                cmd.ExecuteNonQuery();

                _connection.Close();
            }
        }
        catch(Exception er) 
        {
            result = er.Message;
        }

        return result;
    }
}