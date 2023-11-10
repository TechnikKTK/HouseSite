using System;
using System.Web.UI.WebControls;
using System.Web.Security;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Threading.Tasks;


public partial class Admin_Users : System.Web.UI.Page
{
    private MembershipUserCollection allRegisteredUsers = Membership.GetAllUsers();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!this.IsPostBack)
        {
            lblOnlineUsers.Text = Membership.GetNumberOfUsersOnline().ToString();
            lblTotalUsers.Text = allRegisteredUsers.Count.ToString();
            string[] alph = "А;Б;В;Г;Д;Е;Ё;Ж;З;И;Й;К;Л;М;Н;О;П;Р;С;Т;У;Ф;Х;Ц;Ч;Ш;Щ;Э;Ю;Я;ВСЕ".Split(';');
            rptAlphabetBar.DataSource = alph;
            rptAlphabetBar.DataBind();
        }
    }

    protected void rptAlphabetBar_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        var alfa = e.CommandArgument.ToString();

        hiddenType.Value = "0";

        if (alfa == "ВСЕ")
        {
            hiddenAlfa.Value = "";
            this.BindAllUsers(0,reloadAllUsers: false);
        }
        else
        {
            hiddenAlfa.Value = alfa;
            this.BindAllUsers(0, alfabet: true, alfa: e.CommandArgument.ToString(), reloadAllUsers: false);
        }
    }

    protected void ChangeLockPost(object sender, CommandEventArgs e)
    {
        var admin = Membership.GetUser();
        string connect_str = ConfigurationManager.ConnectionStrings["migConnectionString"].ConnectionString;
        try
        {
            userID = Guid.Parse(e.CommandArgument.ToString());

            MembershipUser user = Membership.GetUser(userID);

            using (SqlConnection _connection = new SqlConnection(connect_str))
            {
                _connection.Open();

                bool barrier = false;

                SqlCommand cmd = new SqlCommand("SELECT Top(1) BrokeRules FROM hs_Users WHERE UserId=@UserId", _connection);

                cmd.Parameters.Add("@UserId", SqlDbType.UniqueIdentifier).Value = (Guid)user.ProviderUserKey;

                var reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    if (reader["BrokeRules"] != Convert.DBNull)
                    {
                        if ((bool)reader["BrokeRules"]) barrier = true;
                    }
                }

                reader.Close();

                cmd = new SqlCommand("Update hs_Users Set BrokeRules = @Broke WHERE UserId=@UserId", _connection);

                cmd.Parameters.Add("@UserId", SqlDbType.UniqueIdentifier).Value = (Guid)user.ProviderUserKey;
                cmd.Parameters.Add("@Broke", SqlDbType.Bit).Value = !barrier;

                cmd.ExecuteNonQuery();
                _connection.Close();

                Task.Run(()=> { HouseSiteService.SetLockBarrier(user, !barrier, connect_str); });
            }
        }
        catch (Exception ex)
        {
            if (admin != null)
            {
                HouseSiteService.SaveLogError((Guid)admin.ProviderUserKey, ex.Message, ex.StackTrace, connect_str, obj =>
                {
                    var href = "<script>setTimeout(function(){ window.location.href = '/home'; }, 500);</script>";
                    ClientScript.RegisterStartupScript(
                      this.GetType(),
                      Guid.NewGuid().ToString(), href);
                });
            }
            else
            {
                var href = "<script>setTimeout(function(){ window.location.href = '/home'; }, 500);</script>";
                ClientScript.RegisterStartupScript(
                  this.GetType(),
                  Guid.NewGuid().ToString(), href);
            }
        }

        
        bool isAlfabet = hiddenAlfa.Value != ""? true: false;
        string alfa = hiddenAlfa.Value;
        int type = int.Parse(hiddenType.Value);
       

        BindAllUsers(type, alfabet: isAlfabet, alfa: alfa, reloadAllUsers: true);
    }

    Guid userID = Guid.Empty;

    protected void ChangeBan(object sender, CommandEventArgs e)
    {
        var admin = Membership.GetUser();
        string connect_str = ConfigurationManager.ConnectionStrings["migConnectionString"].ConnectionString;
        try
        {
            userID = Guid.Parse(e.CommandArgument.ToString());

            MembershipUser user = Membership.GetUser(userID);
            user.IsApproved = !user.IsApproved;
            Membership.UpdateUser(user);

        }
        catch (Exception ex)
        {
            if (admin != null)
            {
                HouseSiteService.SaveLogError((Guid)admin.ProviderUserKey, ex.Message, ex.StackTrace, connect_str, obj =>
                {
                    var href = "<script>setTimeout(function(){ window.location.href = '/home'; }, 500);</script>";
                    ClientScript.RegisterStartupScript(
                      this.GetType(),
                      Guid.NewGuid().ToString(), href);
                });
            }
            else
            {
                var href = "<script>setTimeout(function(){ window.location.href = '/home'; }, 500);</script>";
                ClientScript.RegisterStartupScript(
                  this.GetType(),
                  Guid.NewGuid().ToString(), href);
            }
        }

        bool isAlfabet = hiddenAlfa.Value != "" ? true : false;
        string alfa = hiddenAlfa.Value;
        int type = int.Parse(hiddenType.Value);


        BindAllUsers(type, alfabet: isAlfabet, alfa: alfa, reloadAllUsers: true);
    }

    protected void DeleteUser(object sender, CommandEventArgs e)
    {
        var admin = Membership.GetUser();
        string connect_str = ConfigurationManager.ConnectionStrings["migConnectionString"].ConnectionString;
        try
        {
            userID = Guid.Parse(e.CommandArgument.ToString());

            MembershipUser user = Membership.GetUser(userID);

            using (SqlConnection _connection = new SqlConnection(connect_str))
            {
                _connection.Open();

                SqlCommand cmd = new SqlCommand("Delete FROM hs_Users WHERE UserId=@UserId", _connection);
                cmd.Parameters.Add("@UserId", SqlDbType.UniqueIdentifier).Value = (Guid)user.ProviderUserKey;
                cmd.ExecuteNonQuery();

                cmd = new SqlCommand("Delete FROM hs_Messages WHERE UserId=@UserId", _connection);
                cmd.Parameters.Add("@UserId", SqlDbType.UniqueIdentifier).Value = (Guid)user.ProviderUserKey;
                cmd.ExecuteNonQuery();

                cmd = new SqlCommand("Delete FROM hs_UsersNotify WHERE UserId=@UserId", _connection);
                cmd.Parameters.Add("@UserId", SqlDbType.UniqueIdentifier).Value = (Guid)user.ProviderUserKey;
                cmd.ExecuteNonQuery();

                cmd = new SqlCommand("Delete FROM hs_TokenDevices WHERE UserId=@UserId", _connection);
                cmd.Parameters.Add("@UserId", SqlDbType.UniqueIdentifier).Value = (Guid)user.ProviderUserKey;
                cmd.ExecuteNonQuery();

                _connection.Close();
            }

            Membership.DeleteUser(user.UserName, true);
        }
        catch (Exception ex)
        {
            if (admin != null)
            {
                HouseSiteService.SaveLogError((Guid)admin.ProviderUserKey, ex.Message, ex.StackTrace, connect_str, obj =>
                {
                    var href = "<script>setTimeout(function(){ window.location.href = '/home'; }, 500);</script>";
                    ClientScript.RegisterStartupScript(
                      this.GetType(),
                      Guid.NewGuid().ToString(), href);
                });
            }
            else
            {
                var href = "<script>setTimeout(function(){ window.location.href = '/home'; }, 500);</script>";
                ClientScript.RegisterStartupScript(
                  this.GetType(),
                  Guid.NewGuid().ToString(), href);
            }
        }

        bool isAlfabet = hiddenAlfa.Value != "" ? true : false;
        string alfa = hiddenAlfa.Value;
        int type = int.Parse(hiddenType.Value);


        BindAllUsers(type, alfabet: isAlfabet, alfa: alfa, reloadAllUsers: true);
    }


    protected string GetLockPost(string uid)
    {
        string barrier = "lock";
        if (GetLockValue(uid) == 1) barrier = "barrier";                

        return "/images/" + barrier + ".png";
    }

    protected string GetBan(string uid)
    {
        string barrier = "ban";
        if (GetBanValue(uid) == 1) barrier = "check";

        return "/images/" + barrier + ".svg";
    }

    protected int GetLockValue(string uid)
    {
        var admin = Membership.GetUser();
        string connect_str = ConfigurationManager.ConnectionStrings["migConnectionString"].ConnectionString;
        bool barrier = false;
        try
        {
            userID = Guid.Parse(uid);
            MembershipUser user = Membership.GetUser(userID);

            using (SqlConnection _connection = new SqlConnection(connect_str))
            {
                _connection.Open();
                SqlCommand cmd = new SqlCommand("SELECT Top(1) BrokeRules FROM hs_Users WHERE UserId=@UserId", _connection);

                cmd.Parameters.Add("@UserId", SqlDbType.UniqueIdentifier).Value = (Guid)user.ProviderUserKey;

                var reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    if (reader["BrokeRules"] != Convert.DBNull)
                    {
                        barrier = (bool)reader["BrokeRules"];
                    }
                }

                reader.Close();
            }
        }
        catch (Exception ex)
        {
            if (admin != null)
            {
                HouseSiteService.SaveLogError((Guid)admin.ProviderUserKey, ex.Message, ex.StackTrace, connect_str, obj =>
                {
                    var href = "<script>setTimeout(function(){ window.location.href = '/home'; }, 500);</script>";
                    ClientScript.RegisterStartupScript(
                      this.GetType(),
                      Guid.NewGuid().ToString(), href);
                });
            }
            else
            {
                var href = "<script>setTimeout(function(){ window.location.href = '/home'; }, 500);</script>";
                ClientScript.RegisterStartupScript(
                  this.GetType(),
                  Guid.NewGuid().ToString(), href);
            }
        }
        return barrier ? 1 : 0;
    }

    protected int GetBanValue(string uid)
    {
        var admin = Membership.GetUser();
        string connect_str = ConfigurationManager.ConnectionStrings["migConnectionString"].ConnectionString;
        bool barrier = false;
        try
        {
            userID = Guid.Parse(uid);
            MembershipUser user = Membership.GetUser(userID);

            if (user != null)
            {
                return user.IsApproved ? 1 : 0;
            }
            else return 0;
        }
        catch (Exception ex)
        {
            if (admin != null)
            {
                HouseSiteService.SaveLogError((Guid)admin.ProviderUserKey, ex.Message, ex.StackTrace, connect_str, obj =>
                {
                    var href = "<script>setTimeout(function(){ window.location.href = '/home'; }, 500);</script>";
                    ClientScript.RegisterStartupScript(
                      this.GetType(),
                      Guid.NewGuid().ToString(), href);
                });
            }
            else
            {
                var href = "<script>setTimeout(function(){ window.location.href = '/home'; }, 500);</script>";
                ClientScript.RegisterStartupScript(
                  this.GetType(),
                  Guid.NewGuid().ToString(), href);
            }
        }
        return barrier ? 1 : 0;
    }


    private void BindAllUsers(int typeSearch, bool alfabet = false, string alfa="", bool reloadAllUsers = false)
    {
        var admin = Membership.GetUser();

        var cmdText = "SELECT [UserName]," +
            "         (CHARINDEX('_',[UserName],4) - CHARINDEX('_',[UserName]))," +
                      " CONVERT(int,SUBSTRING([UserName],4, CAST(CASE WHEN " +
                      "(CHARINDEX('_',[UserName],4) - CHARINDEX('_',[UserName])) <= 0" +
                      " THEN CHARINDEX('_',[UserName])" +
                      " ELSE CHARINDEX('_',[UserName],4) - (CHARINDEX('_',[UserName]) +1) END as int)), 0)" +
                      " as FlatNum, [hs_Users].*" +
                      "FROM [aspnet_Users] " +
                      "INNER JOIN [hs_Users] ON [aspnet_Users].[UserId] = [hs_Users].[UserId]";

        var where = "";

        switch (typeSearch)
        {
            case 0:
                if (alfabet)
                {
                    where = string.Format(" Where [Name] like '{0}%' Or [LastName] like '{0}%' ",
                        alfa);
                }
                else
                {
                    where = string.Format(" Where [Name] like '%{0}%' OR [LastName] like '%{0}%'",
                        txtSearch.Text);
                }
                break;

            case 1:
                string ruAlfabet = "авкорнмсхует";
                string enAlfabet = "abkophmcxyet";

                string txt = txtSearch.Text.ToLower();
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
                eng_text = eng_text.Replace(" ", "");

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

                ru_text = ru_text.Replace(" ", "");

                where = string.Format(" Where [AutoNumber] like '%{0}%' OR [AutoNumber] like '%{0}%'",
                    ru_text, eng_text);
                break;

            case 2:
                where = string.Format(" Where [UserName] like '%{0}%'",
                        txtSearch.Text);
                break;
        }


        string connect_str = ConfigurationManager.ConnectionStrings["migConnectionString"].ConnectionString;

        try
        {
            using (SqlConnection _connection = new SqlConnection(connect_str))
            {
                var adapter = new SqlDataAdapter(_connection.CreateCommand());
                adapter.SelectCommand.CommandText = cmdText + where+ " Order by [FlatNum]";


                DataTable dataTable = new DataTable();
                adapter.Fill(dataTable);
                repeatUsers.DataSource = dataTable;
                repeatUsers.DataBind();
            }
        }
        catch (Exception ex)
        {
            if (admin != null)
            {
                HouseSiteService.SaveLogError((Guid)admin.ProviderUserKey, ex.Message, ex.StackTrace, connect_str, obj =>
                {
                    var href = "<script>setTimeout(function(){ window.location.href = '/home'; }, 500);</script>";
                    ClientScript.RegisterStartupScript(
                      this.GetType(),
                      Guid.NewGuid().ToString(), href);
                });
            }
            else
            {
                var href = "<script>setTimeout(function(){ window.location.href = '/home'; }, 500);</script>";
                ClientScript.RegisterStartupScript(
                  this.GetType(),
                  Guid.NewGuid().ToString(), href);
            }
        }
    }
   
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        hiddenType.Value = ddlUserSearchTypes.SelectedValue;
        int typeSearch = int.Parse(hiddenType.Value);
        
        BindAllUsers(typeSearch, reloadAllUsers: false);
    }


    protected void selectTypeSearch(object sender, EventArgs e)
    {
        hiddenType.Value = ddlUserSearchTypes.SelectedValue;
    }
}