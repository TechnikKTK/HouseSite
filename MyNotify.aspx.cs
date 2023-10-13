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
        }
    }

    protected void send_fcm_Click(object sender, EventArgs e)
    {       
    }

    protected string GetIcon(object message_type)
    {
        int type = (int)message_type;

        switch (type)
        {
            case 0:
                return "fa-circle-info";
            case 1:
                return "fa-file";
            case 2:
                return "fa-triangle-exclamation";
            case 3:
                return "fa-passport";
            default:
                return "";
        }
    }

    protected string GetClass(object message_type)
    {
        int type = (int)message_type;

        switch (type)
        {
            case 0:
                return "class='table-info'";
            case 1:
                return "";
            case 2:
                return "class='table-danger'";
            case 3:
                return "class='table-light'";
            default:
                return "";
        }
    }

    protected string GetColor(object message_type)
    {
        int type = (int)message_type;

        switch (type)
        {
            case 0:
                return "color:#6362c7";
            case 1:
                return "color:#39c057";
            case 2:
                return "color:#aa0000";
            case 3:
                return "color:#ffffff";
            default:
                return "color:#000000";
        }
    }
}