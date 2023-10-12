<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CheckFcm.aspx.cs" Inherits="CheckFcm" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
     <div id="myalert"></div>   
    
    <script src="/js/jquery.min.js"></script>
    <script type="module">
        import { initializeApp } from "https://www.gstatic.com/firebasejs/10.4.0/firebase-app.js";
        import { getMessaging, getToken, onMessage } from "https://www.gstatic.com/firebasejs/10.4.0/firebase-messaging.js";

        // Your web app's Firebase configuration
        const firebaseConfig = {
        apiKey: "AIzaSyD2hUr8YSxU_-SEGbsfFnjBME_etlTZfEE",
        authDomain: "housesite-35175.firebaseapp.com",
        projectId: "housesite-35175",
        storageBucket: "housesite-35175.appspot.com",
        messagingSenderId: "325743467907",
        appId: "1:325743467907:web:cdce7d2eb115a20df52ab6",
        measurementId: "G-BM86Q96RSP"
        };

        // Initialize Firebase
        export const app = initializeApp(firebaseConfig);

        //self.FIREBASE_APPCHECK_DEBUG_TOKEN = true;
        //const debugToken = 'aa045770-13ba-4074-9386-94a22b76b84d';
        const messaging = getMessaging();

        export async function requestPermission()
        {
            console.log('Requesting permission...');

            if (Notification.permission !== "granted") {
                Notification.requestPermission().then(async (permission) => {
                    if (permission === 'granted') {
                        console.log('Notification permission granted.');
                    }
                    else {
                        console.log("Включите поддержку уведомлений в вашем браузере для этого сайта");
                    }

                    location.href = "/dashboard";
                });
            }
        }

        export function registerServiceWorker() {
            const matchValue = navigator.serviceWorker;
            if (matchValue != null) {
                const serviceWorker = matchValue;
                const options = { "type": "module" };
                return serviceWorker.register("./firebase-messaging-sw.js", options);
            }
            else {
                return Promise.reject(new Error("Service worker not available"));
            }
        }

        export const onMessageListener = async () =>
            new Promise((resolve) =>
                (async () => {
                    const messagingResolve = await messaging;
                    onMessage(messagingResolve, (payload) => {
                        // console.log('On message: ', messaging, payload);
                        resolve(payload);
                    });
                })()
            );



        export async function getFCMToken() {
            try {

                registerServiceWorker();

                const token = await getToken(messaging,
                {
                    vapidKey: "BOolhDIKwbxYGInYx2V_IcA1fEyjy-o3jCbtv4pL89d7Ri9XcslDY6pu910PZNygkD6AlBNN6LX5Aroc-HObNEs"
                });

                
                $.ajax({
                    url: '/token',         /* Куда пойдет запрос */
                    method: 'post',             /* Метод передачи (post или get) */
                    dataType: 'html',          /* Тип данных в ответе (xml, json, script, html). */
                    data: { device: token },     /* Параметры передаваемые в запросе. */
                    success: function (data) {   /* функция которая будет выполнена после успешного запроса.  */
                        /*setTimeout(function () {*/
                            location.href = "/dashboard";
                        //}, 3000);
                    }
                });
                
                return token;
            } catch (e) {
                console.log('getFCMToken error', e);
                location.href = "/dashboard";
                return undefined;
            }
        }

        var alertPlaceholder = document.getElementById('myalert');

        function alert(message, type) {
            var wrapper = document.createElement('div')
            wrapper.innerHTML =
                `<div class="alert alert-${type} d-flex align-items-center" role="alert" id="alert" style="margin-top: 60px;display:none;justify-content: space-between;">
                    <svg class="bi flex-shrink-0 me-2" width="24" height="24" role="img" aria-label="Warning:">
                        <use xlink:href="#${type}-fill" />
                    </svg>
                    <div>
                        <span id="alert_text">${message}</span>
                    </div>
                    <button type="button" id="btn_alert_close" class="btn-close" data-bs-dismiss="alert" aria-label="Close"></button>
                </div>`;
            alertPlaceholder.append(wrapper)
            }

        requestPermission()
            .then(function () {
                getFCMToken()
                    .then(function (result) {
                        console.log("The token is: ", result);
                    })
            })            
            .catch(function (err) {
                console.log('Unable to get permission to notify.', err);
            });

    </script>
</body>
</html>
