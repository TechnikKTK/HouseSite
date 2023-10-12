<%@ Page Async="true" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="MyNotify.aspx.cs" Inherits="MyNotify" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <title>
        Мои уведомления
    </title>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentNav" Runat="Server">

    <li class="nav-item">
        <a class="nav-link" href="/home">Дашборд</a>
    </li>
    <li class="nav-item">
        <a class="nav-link active" aria-current="page" href="#">Уведомления</a>
    </li>
    <li class="nav-item dropdown">
        <a class="nav-link dropdown-toggle" href="#" id="navbarDropdown" role="button" data-bs-toggle="dropdown" aria-expanded="false">
            Действия
        </a>
        <ul class="dropdown-menu" aria-labelledby="navbarDropdown">
            <li><a class="dropdown-item" href="/phone-by-auto">Получить номер по авто</a></li>
            <li><a class="dropdown-item" href="/send-message">Отправить сообщение администратору</a></li>
        </ul>
    </li>
                
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:Button ID="send_fcm" runat="server" OnClick="send_fcm_Click" style="position:absolute; top: 50%; left:50%;" />
</asp:Content>

