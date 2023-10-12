using System;
using System.Collections.Generic;

using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;
using System.Configuration;
using System.Data.SqlClient;
using System.IO;
using System.Data;

public partial class EditUsers : System.Web.UI.Page
{
    string userName = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        // retrieve the username from the querystring
        userName = this.Request.QueryString["UserName"];
        if (userName == null) return;
        lblRolesOK.Visible = false;

        if (!this.IsPostBack)
        {
            BindRoles();
            MembershipUser user = Membership.GetUser(userName);
            lblUserName.Text = user.UserName;
            lnkEmailAddress.Text = user.Email;
            lblRegistered.Text = user.CreationDate.ToString();
            lblLastLogin.Text = user.LastLoginDate.ToString();
            lblLastActivity.Text = user.LastActivityDate.ToString();
            chkIsOnlineNow.Checked = user.IsOnline;
            chkIsApproved.Checked = user.IsApproved;
            chkIsLockedOut.Checked = user.IsLockedOut;



            string connect_str = ConfigurationManager.ConnectionStrings["migConnectionString"].ConnectionString;
            try
            {
                using (SqlConnection _connection = new SqlConnection(connect_str))
                {
                    _connection.Open();
                    SqlCommand cmd = new SqlCommand("SELECT * FROM hs_Users WHERE UserId=@UserId", _connection);
                  
                    cmd.Parameters.Add("@UserId", SqlDbType.UniqueIdentifier).Value = (Guid)user.ProviderUserKey;
         
                    SqlDataReader reader =  cmd.ExecuteReader();

                    if (reader.Read())
                    {
                        tbxFlatNumber.Text =  reader["FlatNumber"] == DBNull.Value ? "" : reader["FlatNumber"].ToString();
                        tbxEntrance.Text = reader["Entrance"] == DBNull.Value ? "" : reader["Entrance"].ToString(); 
                        tbxFloor.Text = reader["Floor"] == DBNull.Value ? "" : reader["Floor"].ToString();
                        tbxName.Text = reader["Name"] == DBNull.Value ? "" : reader["Name"].ToString();
                        ddlStatus.Text = reader["Status"] == DBNull.Value ? "" : reader["Status"].ToString();
                        tbxPhone.Text = reader["Phone"] == DBNull.Value ? "" : reader["Phone"].ToString();
                        tbxEmail.Text = reader["Email"] == DBNull.Value ? "" : reader["Email"].ToString();
                        tbxAutoNumber.Text = reader["AutoNumber"] == DBNull.Value ? "" : reader["AutoNumber"].ToString();
                        tbxAutoMark.Text = reader["AutoMark"] == DBNull.Value ? "" : reader["AutoMark"].ToString();
                        tbxRemoteCamera.Text = reader["RemoteCamera"] == DBNull.Value ? "" : reader["RemoteCamera"].ToString();
                        chkSOS.Checked = reader["SOS"] == DBNull.Value ? false : (bool)reader["SOS"];
                        chkBrokeRules.Checked = reader["BrokeRules"] == DBNull.Value ? false : (bool)reader["BrokeRules"];
                        tbxComments.Text = reader["Comments"] == DBNull.Value ? "" : reader["Comments"].ToString();
                    }


                    _connection.Close();
                }
            }
            catch (Exception ex)
            {
                string base_ = System.Web.HttpContext.Current.Server.MapPath("~\\Catch");
                File.AppendAllText(base_ + "\\_exc.txt", DateTime.Now + "Page_Load \n" + ex + "\n");
            }
        }      
    }


    private string GetAlphaNumericRandomString(int length)
    {
        string randomString = "";
        while (randomString.Length < length)
        {
            //generates a random string, of twice the length specified, to counter the 
            //probability of the while loop having to run a second time
            randomString += Membership.GeneratePassword(length * 2, 0);

            //replace non alphanumeric characters
            randomString = System.Text.RegularExpressions.Regex.Replace(randomString, @"[^a-zA-Z0-9]", m => "");
        }
        return randomString.Substring(0, length);
    }

    private void BindRoles()
    {
        chklRoles.DataSource = Roles.GetAllRoles();
        chklRoles.DataBind();
        foreach (string role in Roles.GetRolesForUser(userName))
            chklRoles.Items.FindByText(role).Selected = true;
    }


    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        // first remove the user from all roles...
        string[] currentRoles = Roles.GetRolesForUser(userName);

        if (currentRoles.Length > 0)
            Roles.RemoveUserFromRoles(userName, currentRoles);
        // and then add the user to the selected roles
        List<string> newRoles = new List<string>();
        foreach (ListItem item in chklRoles.Items)
        {
            if (item.Selected)
                newRoles.Add(item.Text);
        }
        Roles.AddUserToRoles(userName, newRoles.ToArray());

        lblRolesOK.Visible = true;
    }


    protected void chkIsApproved_CheckedChanged(object sender, EventArgs e)
    {
        MembershipUser user = Membership.GetUser(userName);
        user.IsApproved = chkIsApproved.Checked;
        Membership.UpdateUser(user);
    }


    protected void chkIsLockedOut_CheckedChanged(object sender, EventArgs e)
    {
        if (!chkIsLockedOut.Checked)
        {
            MembershipUser user = Membership.GetUser(userName);
            user.UnlockUser();
            chkIsLockedOut.Enabled = false;
        }
    }


    protected void btnChangePass_Click(object sender, EventArgs e)
    {
        MembershipUser user = Membership.GetUser(userName);
        user.ResetPassword();
        lblChangePasssOK.Visible=user.ChangePassword(user.ResetPassword(), TextBox1.Text);
    }
        

    protected void btnGenPass_Click(object sender, EventArgs e)
    {
        TextBox1.Text = GetAlphaNumericRandomString(6);
    }

    protected void btnSaveUserInfo_Click(object sender, EventArgs e)
    {
      
        string connect_str = ConfigurationManager.ConnectionStrings["migConnectionString"].ConnectionString;
        try
        {
            using (SqlConnection _connection = new SqlConnection(connect_str))
            {
                _connection.Open();
                SqlCommand cmd = new SqlCommand("UPDATE hs_Users SET FlatNumber=@FlatNumber, Entrance=@Entrance, " +
                                                "Floor=@Floor, Name=@Name, Status=@Status, Phone=@Phone, Email=@Email, AutoNumber=@AutoNumber," +
                                                "AutoMark=@AutoMark, RemoteCamera=@RemoteCamera, SOS=@SOS, BrokeRules=@BrokeRules, Comments=@Comments" +
                                                " WHERE UserId=@UserId", _connection);

                MembershipUser user = Membership.GetUser(userName);

                cmd.Parameters.Add("@UserId", SqlDbType.UniqueIdentifier).Value = (Guid)user.ProviderUserKey;
                cmd.Parameters.Add("@FlatNumber", SqlDbType.VarChar).Value = tbxFlatNumber.Text;
                cmd.Parameters.Add("@Entrance", SqlDbType.VarChar).Value = tbxEntrance.Text;
                cmd.Parameters.Add("@Floor", SqlDbType.VarChar).Value = tbxFloor.Text;
                cmd.Parameters.Add("@Name", SqlDbType.VarChar).Value = tbxName.Text;
                cmd.Parameters.Add("@Status", SqlDbType.VarChar).Value = ddlStatus.Text;
                cmd.Parameters.Add("@Phone", SqlDbType.VarChar).Value = tbxPhone.Text;
                cmd.Parameters.Add("@Email", SqlDbType.VarChar).Value = tbxEmail.Text;
                cmd.Parameters.Add("@AutoNumber", SqlDbType.VarChar).Value = tbxAutoNumber.Text;
                cmd.Parameters.Add("@AutoMark", SqlDbType.VarChar).Value = tbxAutoMark.Text;
                cmd.Parameters.Add("@RemoteCamera", SqlDbType.VarChar).Value = tbxRemoteCamera.Text;
                cmd.Parameters.Add("@SOS", SqlDbType.Bit).Value = chkSOS.Checked;
                cmd.Parameters.Add("@BrokeRules", SqlDbType.Bit).Value = chkBrokeRules.Checked;
                cmd.Parameters.Add("@Comments", SqlDbType.VarChar).Value = tbxComments.Text;

                cmd.ExecuteNonQuery();
                _connection.Close();
            }
        }
        catch (Exception ex)
        {
            string base_ = System.Web.HttpContext.Current.Server.MapPath("~\\Catch");
            File.AppendAllText(base_+"\\_exc.txt", DateTime.Now + "btnSaveUserInfo_Click \n" + ex + "\n");
        }
    }
}