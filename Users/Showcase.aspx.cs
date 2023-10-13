using System;
using System.Collections.Generic;
//using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;

public partial class _Showcase : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (this.Page.User.Identity.IsAuthenticated)
        {
            if (this.Page.User.IsInRole("admin"))
            {
                Response.Redirect("~/admin/users.aspx");
            }
            Response.Redirect("Posts2.aspx");
        }
    }

}