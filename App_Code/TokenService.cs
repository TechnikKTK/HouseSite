using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;

/// <summary>
/// Сводное описание для TokenService
/// </summary>
public class TokenService
{
    private static readonly HttpClient client = new HttpClient();
    
    public TokenService()
    {
        //
        // TODO: добавьте логику конструктора
        //
    }

    public static async Task<string> SendTokenToServer()
    {
        var values = new Dictionary<string, string>
        {
            { "thing1", "hello" },
            { "thing2", "world" }
        };

        var content = new FormUrlEncodedContent(values);

        var response = await client.PostAsync("http://www.example.com/recepticle.aspx", content);

        var responseString = await response.Content.ReadAsStringAsync();
        return responseString;
    }
}