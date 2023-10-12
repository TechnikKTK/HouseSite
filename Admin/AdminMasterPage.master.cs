using System;
using System.Collections.Generic;

using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Admin_AdminMasterPage : System.Web.UI.MasterPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!this.IsPostBack)
        {
		    if (!this.Page.User.Identity.IsAuthenticated)
                    {
                        Response.Redirect("~/default.aspx");
                    }
                    string userName = HttpContext.Current.User.Identity.Name;

            	if (string.IsNullOrEmpty(userName))
            	{
                  
			        Response.Redirect("~/default.aspx");
		        }

		if (!this.Page.User.IsInRole("admin"))
        	{
        	   	Response.Redirect("~/default.aspx");
	        }
        }
    }
}


         
