<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/AdminMasterPage.master" AutoEventWireup="true"
    CodeFile="Users.aspx.cs" Inherits="Admin_Users" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentNav" runat="Server">

    <li class="nav-item">
        <a class="nav-link active" href="#">Пользователи</a>
    </li>
    <li class="nav-item">
        <a class="nav-link" href="/admin/messages">Сообщения</a>
    </li>

</asp:Content>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="Server">
    <b>- Всего зарегистрировано пользователей:
        <asp:Literal runat="server" ID="lblTotalUsers" /><br />
        - Активно в настоящий момент:
        <asp:Literal runat="server" ID="lblOnlineUsers" /></b>
    <p></p>

    <asp:Repeater runat="server" ID="rptAlphabetBar"
        OnItemCommand="rptAlphabetBar_ItemCommand">
        <ItemTemplate>
            <asp:LinkButton ID="LinkButton1" runat="server" Text='<%# Container.DataItem %>'
                CommandArgument='<%# Container.DataItem %>' />&nbsp;&nbsp;
        </ItemTemplate>
    </asp:Repeater>
    <p></p>
    Поиск пользователя
    <p></p>
    Будем искать по:
    <asp:DropDownList runat="server" ID="ddlUserSearchTypes" ClientIDMode="Static" ToolTip="Выберите критерий поиска">
        <asp:ListItem Text="Имя или фамилия" Value="0" Selected="true" />
        <asp:ListItem Text="Номер авто" Value="1" />
        <asp:ListItem Text="Номер квартиры" Value="2" />
    </asp:DropDownList>
    <p></p>
    <asp:TextBox runat="server" ID="txtSearch" ClientIDMode="Static" Style="min-width: 300px;" />
    <asp:Button runat="server" ID="btnSearch" Text="Найти" Style="min-width: 100px;"
        OnClick="btnSearch_Click" />
    <p></p>
    <asp:HiddenField Value="" runat="server" ID="hiddenType" />
    <asp:HiddenField Value="" runat="server" ID="hiddenAlfa" />
    <asp:HiddenField Value="" runat="server" ID="hiddenText" />
    <div id="gvUserContent" style="overflow-x: auto;">
        <table class="table table-striped table-hover">
            <thead class="thead-dark">
                <tr>
                    <th scope="col">Логин</th>
                    <th scope="col">Фамилия и имя</th>
                    <th scope="col">Номер автомобиля</th>
                    <th scope="col">Телефон доступа</th>
                    <th scope="col">Действия</th>
                    <th scope="col">Шлагбаум</th>
                </tr>
            </thead>
            <tbody>
                <asp:Repeater runat="server" ID="repeatUsers">
                    <ItemTemplate>
                        <tr>
                            <th scope="row"><%# Eval("UserName") %>
                            </th>
                            <td><%# Eval("LastName") %> <%# Eval("Name") %></td>
                            <td><%# Eval("AutoNumber") %></td>
                            <td><%# Eval("Phone") %></td>
                            <td>
                                <div class="d-flex flex-row justify-content-between" style="width: 8rem;">
                                    <div title="Изменение данных пользователя">
                                        <asp:HyperLink runat="server" Text="<img src='/images/user-avatar.png'  border='0' />"
                                            NavigateUrl='<%# string.Format("/admin/users/edit?UserName={0}", Eval("UserName")) %>' />
                                    </div>
                                    <div title="Удалить пользователя из системы">
                                        <button type="button" style="border: 0; background: none; width: 40px; cursor: pointer" title="Удаление пользователя">
                                            <img src="/images/social-media.png"
                                                id="del_dialog" data-toggle="modal" data-target="#modalDel" onclick="delUser(this)" />
                                        </button>
                                        <asp:HiddenField ID="userKeyForDel" Value='<%# Eval("UserID") %>' runat="server" />
                                        <asp:Button ID="changeDel" CssClass="btnDel" Style="display: none" ClientIDMode="Predictable" runat="server" OnCommand="DeleteUser" CommandArgument='<%# Eval("UserID") %>' />
                                    </div>
                                    <div>
                                        <button type="button" style="border: 0; background: none; width: 40px; cursor: pointer" title="Вход в систему">
                                            <img src="<%# GetBan(Eval("UserID").ToString()) %>"
                                                id="ban_dialog" data-toggle="modal" data-target="#modalBan" onclick="changeBan(<%# GetBanValue(Eval("UserID").ToString()) %>, this)" />
                                        </button>
                                        <asp:HiddenField ID="userKeyForBan" Value='<%# Eval("UserID") %>' runat="server" />
                                        <asp:Button ID="changeBan" CssClass="btnChangeBan" Style="display: none" ClientIDMode="Predictable" runat="server" OnCommand="ChangeBan" CommandArgument='<%# Eval("UserID") %>' />
                                    </div>
                                </div>
                            </td>
                            <td>
                                <div class="d-flex flex-row justify-content-center">
                                    <button type="button" style="border: 0; background: none;">
                                        <img src="<%# GetLockPost(Eval("UserID").ToString()) %>"
                                            id="barrier_dialog" data-toggle="modal" data-target="#modalBarrier" onclick="changeBarrier(<%# GetLockValue(Eval("UserID").ToString()) %>, this)" />
                                    </button>
                                    <asp:HiddenField ID="userKey" Value='<%# Eval("UserID") %>' runat="server" />
                                    <asp:Button ID="changeBarrier" CssClass="btnChange" Style="display: none" ClientIDMode="Predictable" runat="server" OnCommand="ChangeLockPost" CommandArgument='<%# Eval("UserID") %>' />
                                </div>
                            </td>
                        </tr>
                    </ItemTemplate>
                </asp:Repeater>
            </tbody>
        </table>
    </div>
    <p></p>
    <asp:Button ID="btnNewUser" PostBackUrl="users/add" runat="server" Text="Создать нового пользователя" Style="min-width: 304px;" />

    <!-- Modal -->
    <div class="modal fade" id="modalBarrier" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabel" aria-hidden="true">
        <div class="modal-dialog" role="document">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title" id="exampleModalLabel">Блокировка шлагбаума</h5>
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">&times;</span>
                    </button>
                </div>
                <div class="modal-body">
                </div>
                <div class="modal-footer">
                    <button id="btn_ok" type="button" class="btn btn-primary" style="min-width: 75px;">OK</button>
                    <button type="button" class="btn btn-secondary" data-dismiss="modal">Отмена</button>
                </div>
            </div>
        </div>
    </div>

    <!-- Modal -->
    <div class="modal fade" id="modalBan" tabindex="-1" role="dialog" aria-labelledby="modalBanLabel" aria-hidden="true">
        <div class="modal-dialog" role="document">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title" id="modalBanLabel">Блокировка входа в кабинет</h5>
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">&times;</span>
                    </button>
                </div>
                <div class="modal-body">
                </div>
                <div class="modal-footer">
                    <button id="btnBan_ok" type="button" class="btn btn-primary" style="min-width: 75px;">OK</button>
                    <button type="button" class="btn btn-secondary" data-dismiss="modal">Отмена</button>
                </div>
            </div>
        </div>
    </div>

    <!-- Modal -->
    <div class="modal fade" id="modalDel" tabindex="-1" role="dialog" aria-labelledby="modalDelLabel" aria-hidden="true">
        <div class="modal-dialog" role="document">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title" id="modalDelLabel">Удаление пользователя</h5>
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">&times;</span>
                    </button>
                </div>
                <div class="modal-body">
                    Вы действительно хотите удалить этого пользовтеля из базы данных?
                    <span>Все данные об этом пользователе будут стерты</span>
                </div>
                <div class="modal-footer">
                    <button id="btnDel_ok" type="button" class="btn btn-primary" style="min-width: 75px;">OK</button>
                    <button type="button" class="btn btn-secondary" data-dismiss="modal">Отмена</button>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
