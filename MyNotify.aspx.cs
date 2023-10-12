using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class MyNotify : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Page.RegisterAsyncTask(new PageAsyncTask(SendData));
        }
    }

    protected void send_fcm_Click(object sender, EventArgs e)
    {       
        Page.ExecuteRegisteredAsyncTasks();
    }

    public async Task SendData()
    {
        await Fcm.SendFcmMessageAsync(
            "fs3IhPqPxArBXTS603thCC:APA91bF4ZuLgNCT-b18g-QspppKri1aTDFTPJDSnNPwy2eEWV5n1kL25g20Vfd4kQ6X-jmsxiu05H5m8WpmbAUBkeyGesSGCPqwP-rUz8wFTXtMNaWgYwRuOb36cDcI4Pnx96cKJz5QP",
            "Hello",
            "XA-XA-XA!!!");
    }
}