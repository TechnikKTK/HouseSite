using System;
using System.Collections.Generic;
//using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;

public partial class SendOk : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (this.Page.User.Identity.IsAuthenticated)
        {
            if (this.Page.User.IsInRole("admin"))
            {
                Response.Redirect("~/admin/users.aspx");
            }
        }
        else    Response.Redirect("Default.aspx");
    }


    protected void lbBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("SendMessage.aspx");
    }
}