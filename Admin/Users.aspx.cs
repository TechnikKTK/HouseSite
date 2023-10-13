using System;
using System.Web.UI.WebControls;
using System.Web.Security;
using System.Web.Profile;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.IO;
using System.Security.Cryptography;
using System.Threading;
using System.Linq;
using System.Threading.Tasks;

public partial class Admin_Users : System.Web.UI.Page
{
    private string SearchText = "";

    private MembershipUserCollection allRegisteredUsers = Membership.GetAllUsers();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!this.IsPostBack)
        {
            lblOnlineUsers.Text = Membership.GetNumberOfUsersOnline().ToString();
            lblTotalUsers.Text = allRegisteredUsers.Count.ToString();
            //string[] alph = "A;B;C;D;E;F;G;J;K;L;M;N;O;P;Q;R;S;T;U;V;W;X;Y;Z;All".Split(';');
            string[] alph = "А;Б;В;Г;Д;Е;Ё;Ж;З;И;Й;К;Л;М;О;П;Р;С;Т;У;Ф;Х;Ц;Ш;Щ;Э;Ю;Я;ВСЕ".Split(';');
            rptAlphabetBar.DataSource = alph;
            rptAlphabetBar.DataBind();
        }
    }

    protected void rptAlphabetBar_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        gvUsers.Attributes.Add("SearchByEmail", false.ToString());
        gvUsers.Attributes.Add("SearchByAuto", false.ToString());
        if (e.CommandArgument.ToString().Length == 1)
        {
            gvUsers.Attributes.Add("SearchText", e.CommandArgument.ToString() + "%");
            this.BindAllUsers(false);
        }
        else
        {
            gvUsers.Attributes.Add("SearchText", "");
            this.BindAllUsers(false);
        }
    }

    protected void ChangeLockPost(object sender, CommandEventArgs e)
    {
        int row = -1;
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
            string base_ = System.Web.HttpContext.Current.Server.MapPath("~\\Catch");
            File.AppendAllText(base_ + "\\_exc.txt", DateTime.Now + "Page_Load \n" + ex + "\n");
        }

        BindAllUsers(true);
    }

    Guid userID = Guid.Empty;

    protected string GetLockPost(string uid)
    {
        string barrier = "lock";
        if (GetLockValue(uid) == 1) barrier = "barrier";                

        return "/images/" + barrier + ".png";
    }

    protected int GetLockValue(string uid)
    {
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
            string base_ = System.Web.HttpContext.Current.Server.MapPath("~\\Catch");
            File.AppendAllText(base_ + "\\_exc.txt", DateTime.Now + "Page_Load \n" + ex + "\n");
        }

        return barrier ? 1 : 0;
    }


    private void BindAllUsers(bool reloadAllUsers)
    {
        MembershipUserCollection allUsers = null;
        if (reloadAllUsers)
            allUsers = Membership.GetAllUsers();
        
        string searchText = "";
        if (!string.IsNullOrEmpty(gvUsers.Attributes["SearchText"]))
            searchText = gvUsers.Attributes["SearchText"];
        bool searchByEmail = false;
        bool searchByAuto = false;
        if (!string.IsNullOrEmpty(gvUsers.Attributes["SearchByEmail"]))
        {
            searchByEmail = bool.Parse(gvUsers.Attributes["SearchByEmail"]);
            searchByAuto = bool.Parse(gvUsers.Attributes["SearchByAuto"]);
        }
        if (searchText.Length > 0)
        {
            if (searchByEmail)
                allUsers = Membership.FindUsersByEmail(searchText);
            else if(searchByAuto)
                allUsers = Membership.FindUsersByEmail(FindUserByAuto(searchText));
            else
                allUsers = Membership.FindUsersByName(searchText);
        }
        else
        {
            allUsers = allRegisteredUsers;
        }
        gvUsers.DataSource = allUsers;
        gvUsers.DataBind();
    }

    private string FindUserByAuto(string searchText)
    {
        string ruAlfabet = "авкорнмсхует";
        string enAlfabet = "abkophmcxyet";

        string txt = searchText.ToLower();
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

        string result = "";
        string connect_str = ConfigurationManager.ConnectionStrings["migConnectionString"].ConnectionString;
        try
        {
            using (SqlConnection _connection = new SqlConnection(connect_str))
            {
                _connection.Open();
                SqlCommand cmd = new SqlCommand("Select Email From hs_Users  Where LOWER(AutoNumber) lIKE @auto OR LOWER(AutoNumber) lIKE @auto_en", _connection);
                cmd.Parameters.AddWithValue("@auto", ru_text);
                cmd.Parameters.AddWithValue("@auto_en", eng_text);

                var email = cmd.ExecuteScalar();
                _connection.Close();

                if (email != null)
                {
                    result = email.ToString();
                }
            }
        }
        catch (Exception ex)
        {
            string base_ = System.Web.HttpContext.Current.Server.MapPath("~\\Catch");
            File.AppendAllText(base_ + "\\_exc.txt", DateTime.Now + "btnCreateUser_Click \n" + ex + "\n");
        }

        return result;
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        bool searchByEmail = (ddlUserSearchTypes.SelectedValue == "0");
        bool searchByAuto = (ddlUserSearchTypes.SelectedValue == "2");

        gvUsers.Attributes.Add("SearchText", "%" + txtSearchText.Text + "%");
        gvUsers.Attributes.Add("SearchByEmail", searchByEmail.ToString());
        gvUsers.Attributes.Add("SearchByAuto", searchByAuto.ToString());
        BindAllUsers(false);
    }

    protected void gvUsers_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        string userName = gvUsers.DataKeys[e.RowIndex].Value.ToString();
        ProfileManager.DeleteProfile(userName);
        Membership.DeleteUser(userName);
        BindAllUsers(true);
        lblTotalUsers.Text = allRegisteredUsers.Count.ToString();
    }

    protected void gvUsers_RowCreated(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
           ImageButton btn = e.Row.Cells[7].Controls[0] as ImageButton;
           btn.OnClientClick = "if (confirm('Выдействительно хотите удалить пользователя?') == false) return false;";
        }
    }

    protected void selectTypeSearch(object sender, EventArgs e)
    {
        SearchText = ddlUserSearchTypes.SelectedValue;
    }

    protected string GetPlaceHolder()
    {
        return SearchText == "E-mail" ? "Например: vasya@ya.ru" : "Например: Василий";
    }


    protected void changeBarrier_Click(object sender, EventArgs e)
    {

    }
}