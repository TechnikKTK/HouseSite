using System;
using System.Collections.Generic;
//using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;

public partial class Exit : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Session.Clear();
        Session.Abandon();
        FormsAuthentication.SignOut();
        
        var href = "<script>setTimeout(function(){ window.location.href = '/home'; }, 500);</script>";
        ClientScript.RegisterStartupScript(
          this.GetType(),
          Guid.NewGuid().ToString(), href);
    }
}