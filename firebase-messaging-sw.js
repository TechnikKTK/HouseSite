importScripts('https://www.gstatic.com/firebasejs/10.4.0/firebase-app-compat.js');
importScripts('https://www.gstatic.com/firebasejs/10.4.0/firebase-messaging-compat.js');

function initInSw() {
    // [START messaging_init_in_sw]
    firebase.initializeApp({
        apiKey: "AIzaSyD2hUr8YSxU_-SEGbsfFnjBME_etlTZfEE",
        authDomain: "housesite-35175.firebaseapp.com",
        projectId: "housesite-35175",
        storageBucket: "housesite-35175.appspot.com",
        messagingSenderId: "325743467907",
        appId: "1:325743467907:web:cdce7d2eb115a20df52ab6",
        measurementId: "G-BM86Q96RSP",
    });

    // Retrieve an instance of Firebase Messaging so that it can handle background
    // messages.
    const messaging = firebase.messaging();
    // [END messaging_init_in_sw]

    onBackgroundMessage();
}

function onBackgroundMessage() {
    const messaging = firebase.messaging();

    // [START messaging_on_background_message]
    messaging.onBackgroundMessage((payload) => {
        console.log(
            '[firebase-messaging-sw.js] Received background message ',
            payload
        );

        if ("setAppBadge" in navigator) {
            navigator.setAppBadge(payload.apns.payload.aps.notification_count);
        }

        // Customize notification here
        var notification = {
            body: payload.notification.body,
            click_action: '/notify',
            icon: '/favicon.ico',
            image: payload.notification.image ? payload.notification.image: '',
            title: payload.notification.title
        };

        self.registration.showNotification(notification.title, notification);
    });
    // [END messaging_on_background_message]
}

initInSw();