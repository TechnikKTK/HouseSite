using System;


public partial class _Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (this.Page.User.Identity.IsAuthenticated)
        {
            if (this.Page.User.IsInRole("admin"))
            {
                Response.Redirect("~/admin/users");
            }
            Response.Redirect("/dashboard");
        }
    }
}