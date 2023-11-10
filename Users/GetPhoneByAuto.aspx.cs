using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web.Security;

public partial class GetPhoneByAuto : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void GetPhone_Click(object sender, EventArgs e)
    {
        MembershipUser user = Membership.GetUser();
        string connect_str = ConfigurationManager.ConnectionStrings["migConnectionString"].ConnectionString;

        try
        {
            string ruAlfabet = "авкорнмсхует";
            string enAlfabet = "abkophmcxyet";

            string txt = txt_auto.Text.ToLower();
            string ru_text = "";
            string eng_text = "";

            foreach (var symbol in txt)
            {
                eng_text += symbol;
                foreach (var item in ruAlfabet)
                {                   
                    if (eng_text.Contains(item))
                    {
                        eng_text = eng_text.Replace(item, enAlfabet[ruAlfabet.IndexOf(item)]);
                    }
                }
            }
            foreach (var symbol in txt)
            {
                ru_text += symbol;

                foreach (var item in enAlfabet)
                {
                    if (ru_text.Contains(item))
                    {
                        ru_text = ru_text.Replace(item, ruAlfabet[enAlfabet.IndexOf(item)]);
                    }
                }
            }

            if (user != null)
            {
                try
                {
                    using (SqlConnection _connection = new SqlConnection(connect_str))
                    {
                        _connection.Open();
                        SqlCommand cmd = new SqlCommand("Select [Phone],[PhoneAdv] From [hs_Users] Where LOWER(AutoNumber) = @auto OR LOWER(AutoNumber) = @auto_en", _connection);
                        cmd.Parameters.AddWithValue("@auto", ru_text);
                        cmd.Parameters.AddWithValue("@auto_en", eng_text);

                        var reader = cmd.ExecuteReader();
                       
                        List<string> list = new List<string>();
                        while (reader.Read())
                        {
                            if(!reader.IsDBNull(0))
                                list.Add(reader.GetString(0));
                            if (!reader.IsDBNull(1))
                                list.Add(reader.GetString(1));
                        }

                        _connection.Close();
                        
                        if (list.Count > 0)
                        {
                            foreach (var item in list)
                            {
                                text_result.Text += string.Format("<li class=\"list-group-item\"><a href=\"tel:{0}\"> {0}</a></li>", item);
                            }
                        }
                        else
                        {
                            text_result.Text = "<li class=\"list-group-item\">Ничего не найдено</li>";
                        }
                    }
                }
                catch (Exception ex)
                {
                    HouseSiteService.SaveLogError((Guid)user.ProviderUserKey, ex.Message, ex.StackTrace, connect_str, obj =>
                    {
                        var href = "<script>setTimeout(function(){ window.location.href = '/home'; }, 500);</script>";
                        ClientScript.RegisterStartupScript(
                          this.GetType(),
                          Guid.NewGuid().ToString(), href);
                    });
                }
            }
        }
        catch (Exception ex)
        {
            HouseSiteService.SaveLogError((Guid)user.ProviderUserKey, ex.Message, ex.StackTrace, connect_str, obj =>
            {
                var href = "<script>setTimeout(function(){ window.location.href = '/home'; }, 500);</script>";
                ClientScript.RegisterStartupScript(
                  this.GetType(),
                  Guid.NewGuid().ToString(), href);
            });
        }
    }
}