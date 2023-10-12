<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default" %>

<!DOCTYPE html>

<html>
<head runat="server">
    <meta http-equiv="X-UA-Compatible" content="IE=Edge" />
    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <meta name="keywords" content="" />
    <meta name="description" content="" />

    <title>Авторизация</title>

    <link rel="stylesheet" href="css/bootstrap.min.css" />
    <link rel="stylesheet" href="css/animate.css" />
    <link rel="stylesheet" href="css/fontawesome.min.css" />
    <link rel="stylesheet" href="css/owl.theme.css" />
    <link rel="stylesheet" href="css/owl.carousel.css" />
    <link rel="stylesheet" href="css/style.css" />
    <link href='https://fonts.googleapis.com/css?family=Open+Sans:400,700' rel='stylesheet' type='text/css' />
    <link href="css/app.css" rel="stylesheet" />
    <link href="css/all.min.css" rel="stylesheet" />
</head>
<body>
     <svg xmlns="http://www.w3.org/2000/svg" style="display: none;">
      <symbol id="primary-fill" fill="currentColor" viewBox="0 0 16 16">
        <path d="M16 8A8 8 0 1 1 0 8a8 8 0 0 1 16 0zm-3.97-3.03a.75.75 0 0 0-1.08.022L7.477 9.417 5.384 7.323a.75.75 0 0 0-1.06 1.06L6.97 11.03a.75.75 0 0 0 1.079-.02l3.992-4.99a.75.75 0 0 0-.01-1.05z"/>
      </symbol>
      <symbol id="info-fill" fill="currentColor" viewBox="0 0 16 16">
        <path d="M8 16A8 8 0 1 0 8 0a8 8 0 0 0 0 16zm.93-9.412-1 4.705c-.07.34.029.533.304.533.194 0 .487-.07.686-.246l-.088.416c-.287.346-.92.598-1.465.598-.703 0-1.002-.422-.808-1.319l.738-3.468c.064-.293.006-.399-.287-.47l-.451-.081.082-.381 2.29-.287zM8 5.5a1 1 0 1 1 0-2 1 1 0 0 1 0 2z"/>
      </symbol>
      <symbol id="warning-fill" fill="currentColor" viewBox="0 0 16 16">
        <path d="M8.982 1.566a1.13 1.13 0 0 0-1.96 0L.165 13.233c-.457.778.091 1.767.98 1.767h13.713c.889 0 1.438-.99.98-1.767L8.982 1.566zM8 5c.535 0 .954.462.9.995l-.35 3.507a.552.552 0 0 1-1.1 0L7.1 5.995A.905.905 0 0 1 8 5zm.002 6a1 1 0 1 1 0 2 1 1 0 0 1 0-2z"/>
      </symbol>
    </svg>
    <div id="myalert"></div>
    <form id="form1" runat="server">
        <div align="center" style="padding: 5px">
            <asp:LoginView ID="LoginView1" runat="server">
                <AnonymousTemplate>
                    <asp:Login ID="Login" runat="server" FailureAction="Refresh"
                        DestinationPageUrl="home">
                        <LayoutTemplate>
                            <div class="form">
                                <div class="form-group">
                                    <label for="UserName">Имя пользователя</label>
                                    <asp:TextBox ID="UserName" ClientIDMode="Static"  autocomplete="on" runat="server" CssClass="form-control" Text="" />
                                </div>
                                <div class="form-group">
                                    <label for="Password">Пароль</label>
                                    <asp:TextBox ID="Password" ClientIDMode="Static"  runat="server" CssClass="form-control" TextMode="Password" />
                                </div>
                                <div class="form-group">
                                    <asp:CheckBox ID="RememberMe" runat="server" CssClass="loginCheck" Text=" Запомнить меня" Style="white-space: break-spaces" />
                                </div>
                                <div class="form-group">
                                    <asp:Button ID="Submit" runat="server" CommandName="Login" Text="Войти" ValidationGroup="Login" CssClass="btn btn-primary w-100" />
                                </div>
                            </div>
                            <asp:Label Visible="false" ID="lblBadPass" runat="server" ForeColor="Red" Text="Login or password failed!"></asp:Label>
                        </LayoutTemplate>
                    </asp:Login>
                </AnonymousTemplate>
                <RoleGroups>
                    <asp:RoleGroup Roles="admin">
                        <ContentTemplate>
                            <asp:LoginName ID="logNmUser" runat="server" FormatString="Здравствуйте {0}" class="registerLable" />
                            <br />
                            <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl="Posts.aspx">Список постов</asp:HyperLink><br />
                            <asp:LoginStatus ID="LoginStatus1" runat="server" LoginText="Выход" LogoutText="Выход" LogoutPageUrl="Default.aspx" LogoutAction="Redirect" />
                        </ContentTemplate>
                    </asp:RoleGroup>
                    <asp:RoleGroup Roles="user">
                        <ContentTemplate>
                            <asp:LoginName ID="logNmUser" runat="server" FormatString="Здравствуйте {0}" class="registerLable" />
                            <br />
                            <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl="Posts.aspx">Список постов</asp:HyperLink><br />
                            <asp:LoginStatus ID="LoginStatus2" runat="server" LoginText="Выход" LogoutText="Выход" LogoutPageUrl="Default.aspx" LogoutAction="Redirect" />
                        </ContentTemplate>
                    </asp:RoleGroup>
                </RoleGroups>
            </asp:LoginView>
        </div>
    </form>
    <!-- Scripts -->
    <script src="/js/jquery.min.js"></script>
    <!-- Bootstrap core JavaScript -->
    <script src="/js/popper.min.js"></script>
    <script src="/js/bootstrap.min.js"></script>
    <script src="/js/owl-carousel.js"></script>
    <script src="/js/tabs.js"></script>
    <script src="/js/popup.js"></script>

    <script src="https://www.gstatic.com/firebasejs/10.4.0/firebase-messaging-compat.js"></script>
    <script src="app.js" type="module"></script>
    <script type="module">
        import { getMessaging, onMessage, getToken } from "https://www.gstatic.com/firebasejs/10.4.0/firebase-messaging.js";

        const messaging = getMessaging();   
        var registration = null;

        if ('serviceWorker' in navigator) {
            window.addEventListener('load', () => {
                navigator.serviceWorker.register('/firebase-messaging-sw.js').then(function (reg) {
                    registration = reg;
                });
            });
        }

        getToken(messaging, {
            vapidKey: "BOolhDIKwbxYGInYx2V_IcA1fEyjy-o3jCbtv4pL89d7Ri9XcslDY6pu910PZNygkD6AlBNN6LX5Aroc-HObNEs"
        }).then((currentToken) => {
            if (currentToken) {
                console.log(currentToken);

                onMessage(messaging, (payload) => {
                    console.log('Message received. ', payload);

                    var notification = {
                        body: "It's found today at 9:39",
                        click_action: "https://www.nasa.gov/feature/goddard/2016/hubble-sees-a-star-inflating-a-giant-bubble",
                        icon: "https://peter-gribanov.github.io/serviceworker/Bubble-Nebula.jpg",
                        image: "https://peter-gribanov.github.io/serviceworker/Bubble-Nebula_big.jpg",
                        title: "Bubble Nebula"
                    };

                    registration.showNotification(
                        payload.notification.title,
                        notification);

                    if ("setAppBadge" in navigator) {
                        navigator.setAppBadge(1);
                    }
                });
            }
        }).catch((err) => {
            Myalert("Включите поддержку уведомлений в вашем браузере для этого сайта", 'warning', 5);
        });

        export function Myalert(message, type, time) {
            let alertContainer = $("#myalert");
            let button = "";
            if (type == "warning")
                button = `<button id='request' class='btn btn-primary'>Разрешить</button>`;
            let wrapper = document.createElement('div')
            wrapper.innerHTML =
                `<div class="alert alert-${type} d-flex align-items-center" role="alert" id="alert" style="margin-top: 3px;display:none;justify-content: space-between; width:99%; margin-left:3px">
                    <svg class="bi flex-shrink-0 me-2" width="24" height="24" role="img" aria-label="Warning:">
                        <use xlink:href="#${type}-fill" />
                    </svg>
                    <div class="flex-column" style="justify-content: space-between;">
                        <span id="alert_text">${message}</span>
                        ${button}
                    </div>
                    <button type="button" id="btn_alert_close" class="btn-close" data-bs-dismiss="alert" aria-label="Close"></button>
                </div>`;
            alertContainer.append(wrapper);

            $('#request').click(function () { RequestPermission(); });

            setTimeout(function () {
                $("#myalert").html('');
            }, time * 1000);
        }

        function removeJS(filename, href) {
            var tags = document.getElementsByTagName('script');

            for (var i = tags.length; i >= 0; i--) { //search backwards within nodelist for matching elements to remove
                if (tags[i] && tags[i].getAttribute('src') != null && tags[i].getAttribute('src').indexOf(filename) != -1)
                    tags[i].parentNode.removeChild(tags[i]); //remove element by calling parentNode.removeChild()
            }

            tags = document.getElementsByTagName('a');

            for (var i = tags.length; i >= 0; i--) { //search backwards within nodelist for matching elements to remove
                if (tags[i] && tags[i].getAttribute('href') != null && tags[i].getAttribute('href').indexOf(href) != -1) {
                    let elem = tags[i].parentNode;
                    elem.innerHTML = '';
                    elem.parentNode.removeChild(elem);
                }
            }
        }

        $(document).ready(function () {
            setTimeout(function () {
                removeJS('https://a.bsite.net/footer.js','https://freeasphosting.net/');
            }, 500);
        });

        function CheckPermission() {
            if (!("Notification" in window)) {
                // Check if the browser supports notifications
                alert("This browser does not support desktop notification");
            } else if (Notification.permission === "granted") {
                // Check whether notification permissions have already been granted;
                // if so, create a notification
                //const notification = new Notification("Hi there!");
                console.log("Уведомления включены!");
                // …
            } else if (Notification.permission !== "denied") {
                // We need to ask the user for permission
                Myalert("Включите поддержку уведомлений в вашем браузере для этого сайта", 'warning', 5);
            }

            // At last, if the user has denied notifications, and you
            // want to be respectful there is no need to bother them anymore.
        }

        function RequestPermission() {
            Notification.requestPermission().then((permission) => {
                // If the user accepts, let's create a notification
                if (permission === "granted") {
                        //var notification = {
                        //    body: "Мой Дом",
                        //    click_action: "/notify",
                        //    icon: "/favicon.ico",
                        //    title: "Поздравляем. Вы включили уведомления!"
                        //};
                        //const grant_notification = new Notification(notification.title, notification);
                        // …
                    $("#myalert").html('');
                    Myalert("Уведомления включены!", 'info', 3);
                }
            });
        }

        CheckPermission();

    </script>
</body>
</html>
