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
        <asp:ListItem Text="Имя пользователя" Selected="true" Value="0" />
        <asp:ListItem Text="E-mail" Value="1" />
        <asp:ListItem Text="Номер авто" Value="2" />
    </asp:DropDownList>
    <p></p>
    <asp:TextBox runat="server" ID="txtSearchText" ClientIDMode="Static" Style="min-width: 200px;" />
    <asp:Button runat="server" ID="btnSearch" Text="Найти" Style="min-width: 100px;"
        OnClick="btnSearch_Click" />
    <p></p>
    <asp:GridView ID="gvUsers" runat="server" AutoGenerateColumns="false"
        DataKeyNames="UserName" ClientIDMode="Static"
        OnRowCreated="gvUsers_RowCreated" OnRowDeleting="gvUsers_RowDeleting" Width="1030">
        <HeaderStyle BackColor="#97aac6" />
        <Columns>
            <asp:BoundField HeaderText="Имя" DataField="UserName" />
            <asp:HyperLinkField HeaderText="E-mail" DataTextField="Email" DataNavigateUrlFormatString="mailto:{0}" DataNavigateUrlFields="Email" />
            <asp:BoundField HeaderText="Дата создания" DataField="CreationDate"
                DataFormatString="{0:dd.MM.yyyy HH:mm}" />
            <asp:BoundField HeaderText="Последняя активность" DataField="LastActivityDate"
                DataFormatString="{0:dd.MM.yyyy HH:mm}" />
            <asp:BoundField HeaderText="Код пользовтаеля" DataField="ProviderUserKey" />
            <asp:CheckBoxField HeaderText="Разрешен вход" DataField="IsApproved"
                HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" />
            <asp:HyperLinkField Text="<img src='/images/user-avatar.png'  border='0' />"
                DataNavigateUrlFormatString="/admin/users/edit?UserName={0}" DataNavigateUrlFields="UserName" />
            <asp:ButtonField CommandName="Delete" ButtonType="Image"
                ImageUrl="/images/social-media.png" />
            <asp:TemplateField HeaderText="Шлагбаум">
                <ItemTemplate>
                    <div>
                        <button type="button" style="border: 0; background: none;">
                            <img src="<%# GetLockPost(Eval("ProviderUserKey").ToString()) %>"
                                id="barrier_dialog" data-toggle="modal" data-target="#exampleModal" onclick="changeBarrier(<%# GetLockValue(Eval("ProviderUserKey").ToString()) %>, this)" />
                        </button>
                        <asp:HiddenField ID="userKey" Value='<%# Eval("ProviderUserKey") %>' runat="server" />
                        <asp:Button ID="changeBarrier" CssClass="btnChange" style="display:none" ClientIDMode="Predictable" runat="server" OnCommand="ChangeLockPost" CommandArgument='<%# Eval("ProviderUserKey") %>' />
                    </div>
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
        <EmptyDataTemplate>Не найдено пользователей.</EmptyDataTemplate>
    </asp:GridView>
    <p></p>
    <asp:Button ID="btnNewUser" PostBackUrl="users/add" runat="server" Text="Создать нового пользователя" Style="min-width: 304px;" />

    <!-- Modal -->
    <div class="modal fade" id="exampleModal" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabel" aria-hidden="true">
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
</asp:Content>
