using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;

/// <summary>
/// Сводное описание для User
/// </summary>
public class User:MembershipUser
{
    public bool BrokeRules { get; set; }
    public bool SOS { get; set; }
    public string RemoteCamera { get; set; }
    public string AutoMark { get; set; }
    public string AutoNumber { get; set; }
    public string Comments { get; set; }

    public User()
    {
        //
        // TODO: добавьте логику конструктора
        //
    }
}