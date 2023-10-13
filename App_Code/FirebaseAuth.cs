using FirebaseAdmin;
using FirebaseAdmin.Auth;
using FirebaseAdmin.Messaging;
using Google.Apis.Auth.OAuth2;
using System;
using System.Threading.Tasks;

/// <summary>
/// Сводное описание для FirebaseAuth
/// </summary>
public static class FcmService
{
    public static readonly string path = System.Web.Hosting.HostingEnvironment.MapPath("~/json/housesite-35175-firebase-adminsdk-av4l6-e66943f3b8.json");
    static FirebaseApp app;
    public static Task<bool> Init()
    {
        if (app == null)
        {
            app = FirebaseApp.Create(new AppOptions()
            {
                Credential = GoogleCredential.FromFile(path),
            });
        }
        return Task.FromResult(true);
    }

    public static async Task AddOrCheckUser()
    {
        await Init();

        var user = await FirebaseAuth.GetAuth(app).GetUserByEmailAsync("user@example.com");
        if (user == null)
        {
            UserRecordArgs args = new UserRecordArgs()
            {
                Email = "user@example.com",
                EmailVerified = false,
                PhoneNumber = "+11234567890",
                Password = "secretPassword",
                DisplayName = "John Doe",
                PhotoUrl = "http://www.example.com/12345678/photo.png",
                Disabled = false,
            };
            UserRecord userRecord = await FirebaseAuth.GetAuth(app).CreateUserAsync(args);
        }
    }

    public static async Task<string> SendFcmMessageAsync(string token, string title, string body, string image = "")
    {
        await Init();

        var messaging = FirebaseMessaging.GetMessaging(app);

        var message = new FirebaseAdmin.Messaging.Message()
        {
            Token = token,
            Webpush = new WebpushConfig
            {
                Notification = new WebpushNotification()
                {
                    Title = title,
                    Body = body,
                    Icon = "https://prodm8.bsite.net/favicon.ico",
                    Image = image,                    
                },  
            },
            Android = new AndroidConfig
            {
                Notification = new AndroidNotification()
                {
                    ClickAction = "https://prodm8.bsite.net/notify",
                    Body = body,
                    Title = title,
                    NotificationCount = 1,
                    Priority = NotificationPriority.MAX,
                    Visibility = NotificationVisibility.PUBLIC,
                    Sticky = true,
                    Icon = "https://prodm8.bsite.net/favicon.ico",
                },
                Priority = Priority.High,
                CollapseKey = "houseSite"
            },
            Apns = new ApnsConfig
            {
                Aps = new Aps
                {
                    Badge = 1,
                    Alert = new ApsAlert
                    {
                        Body = body,
                        Title = title,
                        Subtitle = "Мой Дом",
                    }
                }
            }
        };

        var result = await messaging.SendAsync(message).ConfigureAwait(false);
        return result;
    }

    public static void SendMessage(Guid userKey,string token, string title, string body, string strConn)
    {
        Task.Run(async () =>
        {
            var result = await SendFcmMessageAsync(token, title, body).ConfigureAwait(false);

            if (result != null)
            {
                HouseSiteService.SaveFcmMessages(userKey,result,token,strConn);
            }
        });
    }
}