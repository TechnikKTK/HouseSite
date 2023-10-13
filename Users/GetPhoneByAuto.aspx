<%@ Page Title="" Language="C#" MasterPageFile="~/Users/MasterPage.master" AutoEventWireup="true" CodeFile="GetPhoneByAuto.aspx.cs" Inherits="GetPhoneByAuto" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <title>Узнать номер владельца по авто
    </title>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentNav" runat="Server">

    <li class="nav-item">
        <a class="nav-link" href="/home">Дашборд</a>
    </li>
    <li class="nav-item">
        <a class="nav-link" href="/notify">Уведомления</a>
    </li>
    <li class="nav-item dropdown">
        <a class="nav-link dropdown-toggle active" aria-current="page" href="#" id="navbarDropdown" role="button" data-bs-toggle="dropdown" aria-expanded="false">Действия
        </a>
        <ul class="dropdown-menu" aria-labelledby="navbarDropdown">
            <li><a class="dropdown-item" href="#">Получить номер по авто</a></li>
            <li><a class="dropdown-item" href="/send-message">Отправить сообщение администратору</a></li>
        </ul>
    </li>

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <div class="card" style="width: 18rem;margin: 75px auto;">
        <div class="card-header">
            Получение номера владельца автомобиля
        </div>
        <ul class="list-group list-group-flush">
            <li class="list-group-item">введите номер автомобиля</li>
            <li class="list-group-item"><asp:TextBox ID="txt_auto" placeholder="a000aa99" runat="server" 
                Text="" ToolTip="Рег. номер с регионом"
                CssClass="w-100" /></li>
            <li class="list-group-item"><asp:Button Text="Номер владельца" runat="server" CssClass="btn btn-primary w-100" ID="btn_getPhone" OnClick="GetPhone_Click" /></li>
            <asp:Literal ID="text_result" runat="server" Text=""></asp:Literal>
        </ul>
    </div>

</asp:Content>

