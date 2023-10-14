<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SendMessage.aspx.cs" MasterPageFile="~/Users/MasterPage.master" Inherits="SendMessage" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <title>Отправить заявку
    </title>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentNav" runat="Server">

    <li class="nav-item">
        <a class="nav-link" href="/home">Главная</a>
    </li>
    <li class="nav-item">
        <a class="nav-link" href="/notify">Уведомления</a>
    </li>
    <li class="nav-item dropdown">
        <a class="nav-link dropdown-toggle active" aria-current="page" href="#" id="navbarDropdown" role="button" data-bs-toggle="dropdown" aria-expanded="false">Действия
        </a>
        <ul class="dropdown-menu" aria-labelledby="navbarDropdown">
            <li><a class="dropdown-item" href="/phone-by-auto">Получить номер по авто</a></li>
            <li><a class="dropdown-item" href="#">Отправить сообщение администратору</a></li>
        </ul>
    </li>

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="card" style="width: 23rem; margin: 75px auto;">
        <div class="card-header">
            Выберите вопрос
        </div>
        <ul class="list-group list-group-flush">
            <li class="list-group-item">
                <asp:DropDownList ID="ddlTypeMessage" runat="server">
                    <asp:ListItem>Передать информацию об автомобиле</asp:ListItem>
                    <asp:ListItem>Изменить ФИО</asp:ListItem>
                    <asp:ListItem>Изменить телефон</asp:ListItem>
                    <asp:ListItem>Изменить E-mail</asp:ListItem>
                    <asp:ListItem>Подать заявку на решение проблемы</asp:ListItem>
                </asp:DropDownList>
            </li>
            <li class="list-group-item">Введите ваше сообщение
            </li>
            <li class="list-group-item">
                <asp:TextBox TextMode="MultiLine" ID="tbxBodyMessage" runat="server" Style="width: 100%; height: 180px;"></asp:TextBox>
            </li>
            <li class="list-group-item">
                <asp:Button ID="btnSendMessages" runat="server" CssClass="btn btn-primary" Text="Отправить" OnClick="btnSendMessages_Click" />
            </li>
        </ul>
    </div>
</asp:Content>
