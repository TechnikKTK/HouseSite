<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SendOk.aspx.cs" Inherits="SendOk" %>

<!doctype html>
<html lang="ru">
  <head>
    <title>Заявка отправлена</title>
    <meta charset="utf-8">
    <meta name="viewport" content="width=device-width, initial-scale=1">
    <meta name="keywords" content="" />
    <meta name="description" content="" />

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
    <form id="form1" runat="server">
        <div class="alert alert-info d-flex align-items-center" role="alert" id="alert" style="justify-content: space-between;
        align-items: center!important;
        flex-wrap: nowrap;
        flex-direction: column;">
            <svg class="bi flex-shrink-0 me-2" width="24" height="24" role="img" aria-label="Warning:">
                <use xlink:href="info-fill" />
            </svg>
            <div>
                <span id="alert_text">Ваше обращение зарегистрировано! Номер:
                    <%if (Page.Request.QueryString["messID"] != null)
                      {  %>
                      <%=Page.Request.QueryString["messID"] %>
                    <%} %>
                </span>
            </div>
           <div style="margin-top: 10px; text-align: center">
                <span id="alert_timer">Вы будете перенаправлены на главную через 5...</span>
           </div>
        </div>
    </form>

      <!-- Scripts -->
    <!-- Bootstrap core JavaScript -->
    <script src="/js/jquery.min.js"></script>
    <script src="/js/popper.min.js"></script>
    <script src="/js/bootstrap.min.js"></script>

    <script src="/js/owl-carousel.js"></script>

    <script src="/js/tabs.js"></script>
    <script src="/js/popup.js"></script>

    <script type="text/javascript">
        $(document).ready(function () {
            let time = 4;
            setInterval(function () {
                $('#alert_timer').html(`Вы будете перенаправлены на главную через ${time}...`);
                time--;
                if (time < 0) location.href = "/home";
            }, 1000);
        });
    </script>
</body>
</html>
