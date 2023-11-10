using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Web;
using System.Web.Security;
using System.Net.Mail;
using System.ServiceModel.MsmqIntegration;
using System.Threading.Tasks;
using System.Windows.Controls;
using MailKit.Net.Smtp;
using MimeKit;
using Newtonsoft.Json.Linq;

/// <summary>
/// Сводное описание для HouseSiteService
/// </summary>
public class HouseSiteService
{
    static List<ITask> Tasks = new List<ITask>();

    public static void SetLockBarrier(MembershipUser user, bool isLocked, string conString)
    {
        var message = "";
        var body = "";
        var notify_body = "";
        if (isLocked)
        {
            message = "Доступ к шлагбауму ограничен. ";
            body = "Обратитесь к <a href=\"mailto:admin@podm8.ru\">admin@podm8.ru</a>";
            notify_body = "Обратитесь к вашему администратору admin@podm8.ru";
        }
        else
        {
            message = "Доступ к шлагбауму восстановлен";
            body = notify_body = string.Format("{0: dd.MM.yyyy HH:mm}", DateTime.Now);
        }

        using (SqlConnection _connection = new SqlConnection(conString))
        {
            _connection.Open();
            var cmd = new SqlCommand("Insert Into hs_UsersNotify VALUES(@UserId,@CreatedAt,@Message,@Type,@IsRead)", _connection);

            cmd.Parameters.Add("@UserId", SqlDbType.UniqueIdentifier).Value = (Guid)user.ProviderUserKey;
            cmd.Parameters.Add("@CreatedAt", SqlDbType.DateTime).Value = DateTime.Now;
            cmd.Parameters.Add("@Message", SqlDbType.NVarChar).Value = message + body;
            cmd.Parameters.Add("@Type", SqlDbType.Int).Value = isLocked ? 2 : 1;
            cmd.Parameters.Add("@IsRead", SqlDbType.Bit).Value = false;

            cmd.ExecuteNonQuery();
            _connection.Close();
        }

        foreach (var item in GetUserTokens((Guid)user.ProviderUserKey, conString))
        {
            FcmService.SendMessage((Guid)user.ProviderUserKey, item, message, notify_body, conString);
        }
    }

    public static void SaveFcmMessages(Guid userKey, string messageID, string token, string conString)
    {
        using (SqlConnection _connection = new SqlConnection(conString))
        {
            _connection.Open();
            var cmd = new SqlCommand("Insert Into hs_FcmMessagess VALUES(@MessageID," +
                "@UserID,@DeviceToken,@SendAt)", _connection);

            cmd.Parameters.Add("@UserId", SqlDbType.UniqueIdentifier).Value = userKey;
            cmd.Parameters.Add("@SendAt", SqlDbType.DateTime).Value = DateTime.Now;
            cmd.Parameters.Add("@DeviceToken", SqlDbType.VarChar).Value = token;
            cmd.Parameters.Add("@MessageID", SqlDbType.VarChar).Value = messageID;

            cmd.ExecuteNonQuery();
            _connection.Close();
        }
    }

    public static List<string> GetUserTokens(Guid userKey, string conString)
    {
        List<string> tokens = new List<string>();
        using (SqlConnection _connection = new SqlConnection(conString))
        {
            _connection.Open();

            var cmd = new SqlCommand("Select DeviceToken From hs_TokenDevices Where UserId = @UserId", _connection);

            cmd.Parameters.Add("@UserId", SqlDbType.UniqueIdentifier).Value = userKey;

            var reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                tokens.Add(reader["DeviceToken"].ToString());
            }

            reader.Close();
            _connection.Close();
        }

        return tokens;
    }

    protected static string SendToMail(EmailData email)
    {
        try
        {
            var message = new MimeMessage();
            message.From.Add(new MailboxAddress("Funsiki", "noreply@funsiki.com"));
            message.To.Add(new MailboxAddress("To", email.EmailAddress));
            message.Subject = "Сообщение из \"Мой Дом\"";

            var builder = new BodyBuilder();
            builder.TextBody = string.Format("{0}\r\n", email.Data);
            builder.HtmlBody = string.Format("<html><head><title>Сообщение от администратора</title></head><body><p><strong>{0}</strong></p></body></html>", email.Data);

            message.Body = builder.ToMessageBody();
            using (var client = new MailKit.Net.Smtp.SmtpClient())
            {
                // For demo-purposes, accept all SSL certificates (in case the server supports STARTTLS)  
                client.ServerCertificateValidationCallback = (s, c, h, e) => true;
                client.Connect("mail.privateemail.com", 587);
                // Note: only needed if the SMTP server requires authentication  
                client.Authenticate("noreply@funsiki.com", "kW2022Anton#");
                client.Send(message);
                client.Disconnect(true);
            }

            return "OK";
        }
        catch (Exception er)
        {
            return string.Format("Error:{0}", er.Message);
        }
    }

    public static void SaveLogError(Guid userKey, string message, string stackTrace, string conString, ContextCallback method)
    {
        List<object> args = new List<object>();
        args.Add(userKey);
        args.Add(message);  
        args.Add(stackTrace);
        args.Add(conString);

        Task.Run(async() => 
        {
            Tasks.Add(new MyTask(LogError, args));
            
            while (Tasks.Count > 0)
            {
                while (Tasks[0].IsBusy) { await Task.Delay(1000); }
                if (!Tasks[0].IsBusy)
                {
                    await Tasks[0].StartTask();
                    Tasks.RemoveAt(0);
                    if (method != null)
                        method.Invoke(null);
                }
            }
        });        
    }

    static void LogError(Guid userKey, string message, string stackTrace, string conString)
    {
        using (SqlConnection _connection = new SqlConnection(conString))
        {
            try
            {
                _connection.Open();

                var cmd = new SqlCommand("Select DeviceToken From hs_TokenDevices Where UserId = @UserId", _connection);

                cmd.Parameters.AddWithValue("@Message", message);
                cmd.Parameters.AddWithValue("@CreatedAt", DateTime.Now);
                cmd.Parameters.AddWithValue("@StackTrace", stackTrace);
                cmd.Parameters.AddWithValue("@UserId", userKey);

                cmd.ExecuteNonQuery();
            }
            catch(Exception ex) { }
            finally { _connection.Close(); }
        }
    }
}

public class EmailData
{
    public string EmailAddress { get; set; }
    public string Title { get; set; }
    public string Data { get; set; }
}