<%@ Page Title="" Language="C#" MasterPageFile="~/Users/MasterPage.master" AutoEventWireup="true" CodeFile="Management.aspx.cs" Inherits="Management" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <title>панель управления
    </title>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentNav" runat="Server">

    <li class="nav-item">
        <a class="nav-link active" aria-current="page" href="#">Главная</a>
    </li>
    <li class="nav-item">
        <a class="nav-link" href="/notify">Уведомления</a>
    </li>
    <li class="nav-item dropdown">
        <a class="nav-link dropdown-toggle" href="#" id="navbarDropdown" role="button" data-bs-toggle="dropdown" aria-expanded="false">Действия
        </a>
        <ul class="dropdown-menu" aria-labelledby="navbarDropdown">
            <li><a class="dropdown-item" href="/phone-by-auto">Получить номер по авто</a></li>
            <li><a class="dropdown-item" href="/send-message">Отправить сообщение администратору</a></li>
        </ul>
    </li>

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
<div class="card" style="max-width: 29rem; margin: 75px auto;">
        <div class="card-header text-center">
            <strong>Добро пожаловать в "Мой Дом"</strong>
        </div>
        <ul class="list-group list-group-flush">
            <li class="list-group-item list-group-item-action active  text-center">Данный ресур предназначен для жильцов дома</li>
            <li class="list-group-item list-group-item-action text-center">Здесь вы можете общаться с администратором</li>
            <li class="list-group-item list-group-item-action text-center">А так же получать уведомления от администратора</li>
            <li class="list-group-item list-group-item-action text-center list-group-item-warning" id="menu_mobile">
                <a href="#">Для начала работы воспользуйтесь меню</a></li>
        </ul>
        <div class="card-header text-center">
            <strong>Желаем Вам приятного дня!</strong>
        </div>
    </div>


</asp:Content>
