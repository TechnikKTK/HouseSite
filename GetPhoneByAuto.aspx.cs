using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;

public partial class GetPhoneByAuto : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void GetPhone_Click(object sender, EventArgs e)
    {
        try
        {
            MembershipUser user = Membership.GetUser();

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
                string connect_str = ConfigurationManager.ConnectionStrings["migConnectionString"].ConnectionString;
                try
                {
                    using (SqlConnection _connection = new SqlConnection(connect_str))
                    {
                        _connection.Open();
                        SqlCommand cmd = new SqlCommand("Select Phone From hs_Users  Where LOWER(AutoNumber) = @auto OR LOWER(AutoNumber) = @auto_en", _connection);
                        cmd.Parameters.AddWithValue("@auto", ru_text);
                        cmd.Parameters.AddWithValue("@auto_en", eng_text);

                        var phone = cmd.ExecuteScalar();
                        _connection.Close();

                        if (phone != null)
                        {
                            text_result.Text = string.Format("<li class=\"list-group-item\">Номер: <a href=\"tel:{0}\"> {0}</a></li>", phone);
                        }   
                        else
                        {
                            text_result.Text = string.Format("<li class=\"list-group-item\">Ничего не найдено</li>", phone);
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
        catch (Exception ex)
        {
            string base_ = System.Web.HttpContext.Current.Server.MapPath("~\\Catch");
            File.AppendAllText(base_ + "\\_exc.txt", DateTime.Now + "btnCreateUser_Click \n" + ex + "\n");
        }
    }
}