﻿using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Web;
using System.Web.Security;

/// <summary>
/// Сводное описание для HouseSiteService
/// </summary>
public class HouseSiteService
{
    public HouseSiteService()
    {
        //
        // TODO: добавьте логику конструктора
        //
    }

    public static HouseSiteService Instance { get; set; }

    public static void SetLockBarrier(MembershipUser user, bool isLocked, string conString)
    {
        var message = "";
        var body = "";
        if(isLocked)
        {
            message = "Доступ к шлагбауму ограничен. ";
            body = "Обратитесь <a href=\"mailto:admin@podm8.ru\">admin@podm8.ru</a>";
        }
        else
        {
            message = "Доступ к шлакбауму восстановлен";
            body = string.Format("{0: dd.MM.yyyy HH:mm}", DateTime.Now);
        }

        using (SqlConnection _connection = new SqlConnection(conString))
        {
            _connection.Open();
            var cmd = new SqlCommand("Insert Into hs_UsersNotify VALUES(@UserId,@CreatedAt,@Message,@Type,@IsRead)", _connection);

            cmd.Parameters.Add("@UserId", SqlDbType.UniqueIdentifier).Value = (Guid)user.ProviderUserKey;
            cmd.Parameters.Add("@CreatedAt", SqlDbType.DateTime2).Value = DateTime.Now;
            cmd.Parameters.Add("@Message", SqlDbType.NVarChar).Value = message + body;
            cmd.Parameters.Add("@Type", SqlDbType.Int).Value = isLocked? 2:1;
            cmd.Parameters.Add("@IsRead", SqlDbType.Bit).Value = false;

            cmd.ExecuteNonQuery();
            _connection.Close();
        }

        foreach (var item in GetUserTokens((Guid)user.ProviderUserKey, conString))
        {
            FcmService.SendMessage(item, message, body);
        }
    }


    public static List<string> GetUserTokens(Guid userKey, string conString)
    {
        List<string> tokens = new List<string>();
        using (SqlConnection _connection = new SqlConnection(conString))
        {
            _connection.Open();

            var cmd = new SqlCommand("Select DeviceToken From hs_TokenDevices Where UserId = @UserId", _connection);

            cmd.Parameters.Add("@UserId", SqlDbType.UniqueIdentifier).Value = userKey;

            var reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                tokens.Add(reader["DeviceToken"].ToString());
            }

            reader.Close();
            _connection.Close();
        }

        return tokens;
    }
}