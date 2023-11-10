<%@ Page Async="true" Language="C#" MasterPageFile="~/Users/MasterPage.master" EnableEventValidation="false"
    AutoEventWireup="true" CodeFile="MyNotify.aspx.cs" Inherits="MyNotify" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <title>Мои уведомления</title>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentNav" runat="Server">

    <li class="nav-item">
        <a class="nav-link" href="/home">Главная</a>
    </li>
    <li class="nav-item">
        <a class="nav-link active" aria-current="page" href="#">Уведомления</a>
    </li>
    <li class="nav-item">
        <a class="nav-link" href="/messages">Мои обращения</a>
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
    <asp:SqlDataSource ID="NotifySource" runat="server" ConnectionString="<%$ ConnectionStrings:migConnectionString %>"
        SelectCommand="SELECT ROW_NUMBER() OVER (Order by CreatedAt desc) AS RowNumber, * FROM [hs_UsersNotify]"></asp:SqlDataSource>

    <div class="card" style="margin: 75px auto;">
        <div class="card-header card-header d-flex justify-content-between align-items-center">
            <span>Сообщения от админитратора для Вас</span>
            <asp:Button ID="btnUpdate" OnClick="btnUpdate_Click" runat="server" ClientIDMode="Static" Text="Обновить" CssClass="btn btn-primary" />
        </div>
        <ul class="list-group list-group-flush">
            <li class="list-group-item">
                <asp:Repeater ID="NotifyRepeater" runat="server">
                    <HeaderTemplate>
                    <% if (NotifyRepeater.Items.Count > 0) { %>  
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
                    <% } %>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <tr <%# GetClass(Eval("Type"),Eval("IsRead")) %>>
                            <th scope="row">
                                <asp:ImageButton ID="btn_isRead" OnCommand="ChangeIsRead"
                                    runat="server" ImageUrl='<%# GetImage(Eval("IsRead")) %>'
                                    Style="max-width: 20px;"
                                    CommandArgument='<%# Eval("ID").ToString() %>' /></th>
                            <td><%# Eval("CreatedAt", "{0:dd.MM.yyyy HH:mm}") %></td>
                            <td>
                                <%# Eval("Message") %>
                                <%# GetLink(Eval("AnswerID")) %>
                            </td>
                            <td><i class="fa-solid <%# GetIcon(Eval("Type"),Eval("IsRead")) %>" style="<%# GetColor(Eval("Type"),Eval("IsRead")) %>"></i></td>
                        </tr>
                    </ItemTemplate>
                    <FooterTemplate>                        
                    <% if (NotifyRepeater.Items.Count == 0) { %>  
                        <span>Сообщений пока нет.</span>
                    <% } else {%>  
                            </tbody>
                        </table> 
                    <% } %>
                    </FooterTemplate>
                </asp:Repeater>
            </li>
        </ul>
    </div>

    <!-- Modal -->
    <div class="modal fade" id="modalQuest" tabindex="-1" role="dialog" aria-labelledby="baseText" aria-hidden="true">
        <div class="modal-dialog" role="document">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title" id="baseText">Исходящий вопрос</h5>
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">&times;</span>
                    </button>
                </div>
                <div class="modal-body">

                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-secondary" data-dismiss="modal">Закрыть</button>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
