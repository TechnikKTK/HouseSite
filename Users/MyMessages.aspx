<%@ Page Title="" Language="C#" MasterPageFile="~/Users/MasterPage.master" AutoEventWireup="true" CodeFile="MyMessages.aspx.cs" Inherits="Users_MyMessages" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <title>Мои обращения</title>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentNav" runat="Server">

    <li class="nav-item">
        <a class="nav-link" href="/home">Главная</a>
    </li>
    <li class="nav-item">
        <a class="nav-link" href="/notify">Уведомления</a>
    </li>
    <li class="nav-item">
        <a class="nav-link active" aria-current="page" href="#">Мои обращения</a>
    </li>
    <li class="nav-item dropdown">
        <a class="nav-link dropdown-toggle" href="#" id="navbarDropdown" role="button" data-bs-toggle="dropdown" aria-expanded="false">Действия
        </a>
        <ul class="dropdown-menu" aria-labelledby="navbarDropdown">
            <li><a class="dropdown-item" href="/phone-by-auto">Получить телефон владельца авто</a></li>
            <li><a class="dropdown-item" href="/send-message">Отправить сообщение администратору</a></li>
        </ul>
    </li>

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="card" style="margin: 75px auto;">
        <div class="card-header card-header d-flex justify-content-between align-items-center">
            <span>Список обращений к администратору</span>
            <asp:Button ID="btnUpdate" OnClick="btnUpdate_Click" runat="server" ClientIDMode="Static" Text="Обновить" CssClass="btn btn-primary" />
        </div>
        <ul class="list-group list-group-flush">
            <li class="list-group-item">
                <div>
                <asp:Repeater ID="MessageRepeater" runat="server">
                    <HeaderTemplate>
                        <% if (MessageRepeater.Items.Count > 0)
                            { %>
                        <table class="table table-striped table-hover" id="tblMessages">
                            <thead class="thead-dark">
                                <tr>
                                    <th scope="col">#</th>
                                    <th scope="col">Дата</th>
                                    <th scope="col">Тема</th>
                                    <th scope="col">Сообщение</th>
                                    <th scope="col" style="min-width:105px">Статус</th>
                                </tr>
                            </thead>
                            <tbody>
                                <% } %>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <tr <%# GetClass(Eval("State"),Eval("IsRead")) %>>
                            <th scope="row">
                               <%# Eval("ID") %>
                            </th>
                            <td><%# Eval("_Date", "{0:dd.MM.yyyy HH:mm}") %></td>
                            <td>
                                <%# Eval("TypeMessage") %>
                            </td>
                            <td>
                                <%# Eval("BodyMessage") %>
                            </td>
                            <td>
                                <%# GetStatus(Eval("State")) %>
                            </td>
                        </tr>
                    </ItemTemplate>
                    <FooterTemplate>
                        <% if (MessageRepeater.Items.Count == 0)
                            { %>
                        <span>Сообщений пока нет.</span>
                        <% }
                            else
                            {%>  
                            </tbody>
                        </table> 
                    <% } %>
                    </FooterTemplate>
                </asp:Repeater>
                    </div>
            </li>
        </ul>
    </div>
</asp:Content>

