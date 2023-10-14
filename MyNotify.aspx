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
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:SqlDataSource ID="NotifySource" runat="server" ConnectionString="<%$ ConnectionStrings:migConnectionString %>"
        SelectCommand="SELECT ROW_NUMBER() OVER (Order by CreatedAt desc) AS RowNumber, * FROM [hs_UsersNotify]"></asp:SqlDataSource>


    <div class="card" style="margin: 75px auto">
        <div class="card-header d-flex justify-content-between align-items-center">
            <span>Сообщения от админимтратора для Вас</span>
            <asp:Button ID="btnUpdate" OnClick="btnUpdate_Click" runat="server" ClientIDMode="Static" Text="Обновить" CssClass="btn btn-primary" />
        </div>
        <ul class="list-group-flush">
            <li class="list-group-item">
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
                <asp:Repeater ID="NotifyRepeater" runat="server">
                    <ItemTemplate>
                        <tr <%# GetClass(Eval("Type"), Eval("IsRead")) %>>
                            <th scope="row">
                                <%# Eval("RowNumber") %>
                                <%--<asp:Button ID="btn_isRead" OnCommand="ChangeIsRead"
                                    runat="server"
                                    Visible="<%# getVisibility(Eval("IsRead")) %>" Text="Новое" CssClass="btn btn-outline-primary"
                                    CommandArgument="<%# Eval("ID").ToString() %>" />--%></th>
                            <td><%# Eval("CreatedAt", "{0:dd.MM.yyyy}") %></td>
                            <td><%# Eval("Message") %></td>
                            <td><i class="fa-solid <%# GetIcon(Eval("Type"), Eval("IsRead")) %>" style="<%# GetColor(Eval("Type"), Eval("IsRead")) %>"></i></td>
                        </tr>
                    </ItemTemplate>
                </asp:Repeater>
            </tbody>
        </table>

            </li>
        </ul>
    </div>
</asp:Content>

