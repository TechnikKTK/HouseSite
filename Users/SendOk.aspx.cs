using System;

public partial class SendOk : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }


    protected void lbBack_Click(object sender, EventArgs e)
    {
        var href = "<script>setTimeout(function(){ window.location.href = '/send-message'; }, 500);</script>";
        ClientScript.RegisterStartupScript(
          this.GetType(),
          Guid.NewGuid().ToString(), href);
    }
}