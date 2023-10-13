<%@ Page Async="true" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="MyNotify.aspx.cs" Inherits="MyNotify" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <title>Мои уведомления
    </title>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentNav" runat="Server">

    <li class="nav-item">
        <a class="nav-link" href="/home">Дашборд</a>
    </li>
    <li class="nav-item">
        <a class="nav-link active" aria-current="page" href="#">Уведомления</a>
    </li>
    <li class="nav-item dropdown">
        <a class="nav-link dropdown-toggle" href="#" id="navbarDropdown" role="button" data-bs-toggle="dropdown" aria-expanded="false">Действия
        </a>
        <ul class="dropdown-menu" aria-labelledby="navbarDropdown">
            <li><a class="dropdown-item" href="/phone-by-auto">Получить номер по авто</a></li>
<%--            <li><a class="dropdown-item" href="/send-message">Отправить сообщение администратору</a></li>--%>
        </ul>
    </li>

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:SqlDataSource ID="NotifySource" runat="server" ConnectionString="<%$ ConnectionStrings:migConnectionString %>" 
        SelectCommand="SELECT ROW_NUMBER() OVER (Order by CreatedAt desc) AS RowNumber, * FROM [hs_UsersNotify]"></asp:SqlDataSource>

    <table class="table table-striped table-hover">
        <thead class="thead-dark">
            <tr>
                <th scope="col">#</th>
                <th scope="col">Дата</th>
                <th scope="col">Сообщение</th>
                <th scope="col"></th>
            </tr>
        </thead>
        <tbody>
            <asp:Repeater ID="NotifyRepeater" runat="server" DataSourceID="NotifySource" >
                <ItemTemplate>
                    <tr  <%# GetClass(Eval("Type")) %>>
                        <th scope="row"><%# Eval("RowNumber") %></th>
                        <td><%# Eval("CreatedAt", "{0:dd.MM.yyyy}") %></td>
                        <td><%# Eval("Message") %></td>
                        <td><i class="fa-solid <%# GetIcon(Eval("Type")) %>" style="<%# GetColor(Eval("Type")) %>"></i></td>
                    </tr>
                </ItemTemplate>
            </asp:Repeater>
        </tbody>
    </table>
</asp:Content>

