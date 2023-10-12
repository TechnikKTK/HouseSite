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
            string[] alph = "A;B;C;D;E;F;G;J;K;L;M;N;O;P;Q;R;S;T;U;V;W;X;Y;Z;All".Split(';');
            rptAlphabetBar.DataSource = alph;
            rptAlphabetBar.DataBind();
        }
    }

    protected void rptAlphabetBar_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        gvUsers.Attributes.Add("SearchByEmail", false.ToString());
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
        string connect_str = ConfigurationManager.ConnectionStrings["migConnectionString"].ConnectionString;
        string barrier = "lock";
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
                        if ((bool)reader["BrokeRules"]) barrier = "barrier";
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

        return "~/images/" + barrier + ".png";
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
        if (!string.IsNullOrEmpty(gvUsers.Attributes["SearchByEmail"]))
            searchByEmail = bool.Parse(gvUsers.Attributes["SearchByEmail"]);
        if (searchText.Length > 0)
        {
            if (searchByEmail)
                allUsers = Membership.FindUsersByEmail(searchText);
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

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        bool searchByEmail = (ddlUserSearchTypes.SelectedValue == "E-mail");
        gvUsers.Attributes.Add("SearchText", "%" + txtSearchText.Text + "%");
        gvUsers.Attributes.Add("SearchByEmail", searchByEmail.ToString());
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

}