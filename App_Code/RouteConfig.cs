using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Routing;

/// <summary>
/// Сводное описание для RouteConfig
/// </summary>
public class RouteConfig
{
    public static void RegisterRoutes(RouteCollection routes)
    {
        routes.MapPageRoute("home", "home", "~/Default.aspx");
        routes.MapPageRoute("dashboard", "dashboard", "~/Users/Management.aspx");
        routes.MapPageRoute("notify", "notify", "~/Users/MyNotify.aspx");
        routes.MapPageRoute("phone-by-auto", "phone-by-auto", "~/Users/GetPhoneByAuto.aspx");
        routes.MapPageRoute("fcm", "save-token", "~/GetDeviceToken.aspx");
        routes.MapPageRoute("sndMess", "send-message", "~/Users/SendMessage.aspx");
        routes.MapPageRoute("OkMess", "message-ok", "~/Users/SendOk.aspx");
        routes.MapPageRoute("check_fcm", "fcm", "~/CheckFcm.aspx");
        routes.MapPageRoute("users", "admin/users", "~/Admin/Users.aspx");
        routes.MapPageRoute("addusers", "admin/users/add", "~/Admin/NewUser.aspx");
        routes.MapPageRoute("editusers", "admin/users/edit", "~/Admin/EditUsers.aspx");
        routes.MapPageRoute("messages", "admin/messages", "~/Admin/Messages.aspx");
    }
}