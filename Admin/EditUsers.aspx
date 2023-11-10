<%@ Page Async="true" Language="C#" MasterPageFile="~/Admin/AdminMasterPage.master" AutoEventWireup="true" CodeFile="EditUsers.aspx.cs" Inherits="EditUsers" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentNav" runat="Server">

    <li class="nav-item">
        <a class="nav-link active" href="/admin/users">Пользователи</a>
    </li>
    <li class="nav-item">
        <a class="nav-link" href="/admin/messages">Сообщения</a>
    </li>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <div>Настройки пользователя</div>
    <p></p>
    <table cellpadding="2">
        <tr>
            <td class="d-flex flex-row">
                Логин:
                <asp:TextBox runat="server" ID="lblUserName" Style="width: 100%; margin:0 10px"
                    placeholder="Логин жильца" />
            </td>
            <td>
                <asp:Button runat="server" ID="btnChangeLogin" Text="Применить" OnClick="btnChangeLogin_Click" />
            </td>
        </tr>
        <tr>
            <td>E-mail:</td>
            <td>
                <asp:HyperLink ID="lnkEmailAddress" runat="server" />
            </td>
        </tr>
        <tr>
            <td>Зарегистрирован:</td>
            <td>
                <asp:Literal ID="lblRegistered" runat="server" />
            </td>
        </tr>
        <tr>
            <td>Последний логин:</td>
            <td>
                <asp:Literal ID="lblLastLogin" runat="server" />
            </td>
        </tr>
        <tr>
            <td>Последний вход:</td>
            <td>
                <asp:Literal ID="lblLastActivity" runat="server" />
            </td>
        </tr>
        <tr>
            <td>Пользователь сейчас на сайте?</td>
            <td>
                <asp:Label ID="chkIsOnlineNow" runat="server" /></td>
        </tr>
        <tr>
            <td colspan="2">
                <div class="alert alert-warning <%= GetLockedUser() %>" role="alert" style="max-width: 656px;">
                  Пользователь заблокирован. Причина: <%=lockedPrichina %>
                    <asp:Button ID="btnUnlock" CssClass="btn btn-primary" runat="server" OnClick="btnUnlock_Click" Text="Разблокировать" />
                </div>
            </td>
        </tr>
    </table>
    <p></p>
    <table>
        <tr>
            <td>
                <asp:TextBox ID="tbxPassword" runat="server" Style="width: 100%"
                    placeholder="Новый пароль"></asp:TextBox>
            </td>
            <td>
                <asp:Button ID="btnChangePass" Text="Сменить пароль" OnClick="btnChangePass_Click" runat="server" />
            </td>
        </tr>
        <tr>
            <td colspan="2">
                <asp:Button ID="btnGenPass" Text="Сгенерировать пароль"
                    OnClick="btnGenPass_Click" runat="server" Style="width: 100%" />
                <asp:Label runat="server" ID="lblChangePasssOK"
                    Text="Пароль успешно изменен" Visible="False" />
            </td>
        </tr>
        <tr>
            <td colspan="2">Номер квартиры:</td>
        </tr>
        <tr>
            <td colspan="2">
                <asp:TextBox ID="tbxFlatNumber" runat="server" Width="100%"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td colspan="2">Подъезд:</td>
        </tr>
        <tr>
            <td colspan="2">
                <asp:TextBox ID="tbxEntrance" runat="server" Width="100%"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td colspan="2">Этаж:</td>
        </tr>
        <tr>
            <td colspan="2">
                <asp:TextBox ID="tbxFloor" runat="server" Width="100%"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td colspan="2">Фамилия:</td>
        </tr>
        <tr>
            <td colspan="2">
                <asp:TextBox ID="tbxLastName" ClientIDMode="Static" runat="server"
                    placeholder="Введите фамилию" Width="100%" />
            </td>
        </tr>
        <tr>
            <td colspan="2">Имя:</td>
        </tr>
        <tr>
            <td colspan="2">
                <asp:TextBox ID="tbxName" ClientIDMode="Static" runat="server"
                    placeholder="Введите имя" Width="100%" />
            </td>
        </tr>
        <tr>
            <td colspan="2">Отчество:</td>
        </tr>
        <tr>
            <td colspan="2">
                <asp:TextBox ID="tbxPatronumic" ClientIDMode="Static" runat="server"
                    placeholder="Введите отчество" Width="100%" />
            </td>
        </tr>
        <tr>
            <td colspan="2" class="auto-style1">Статус:</td>
        </tr>
        <tr>
            <td colspan="2" class="auto-style1">
                <asp:DropDownList ID="ddlStatus" runat="server" Width="100%">
                    <asp:ListItem>Собственник</asp:ListItem>
                    <asp:ListItem>Член семьи собственника</asp:ListItem>
                    <asp:ListItem>Арендатор</asp:ListItem>
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td colspan="2">Телефон жильца:</td>
        </tr>
        <tr>
            <td colspan="2">
                <asp:TextBox ID="tbxPhone" runat="server" Width="100%" CssClass="phone_num"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td colspan="2">Телефон жильца (дополнительный):</td>
        </tr>
        <tr>
            <td colspan="2">
                <asp:TextBox ID="tbxPhoneAdv" runat="server" Width="100%" CssClass="phone_num"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td colspan="2">E-mail:</td>
        </tr>
        <tr>
            <td colspan="2">
                <asp:TextBox ID="tbxEmail" runat="server" Width="100%" CssClass="email_text"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td colspan="2">Номер автомобиля:</td>
        </tr>
        <tr>
            <td colspan="2">
                <asp:TextBox ID="tbxAutoNumber" ClientIDMode="Static" runat="server" Width="100%" CssClass="auto_num"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td colspan="2">Марка автомобиля:</td>
        </tr>
        <tr>
            <td colspan="2">
                <asp:TextBox ID="tbxAutoMark" runat="server" Width="100%"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td colspan="2">Регистрация второго автомобиля
                <input type="checkbox" id="dop_auto" />
            </td>
        </tr>
        <tr class="trAdv">
            <td colspan="2">Номер автомобиля (дополнительно):</td>
        </tr>
        <tr class="trAdv">
            <td colspan="2">
                <asp:TextBox ID="tbxAutoNumberAdv" runat="server" Width="100%" ClientIDMode="Static" CssClass="auto_num"></asp:TextBox>
            </td>
        </tr>
        <tr class="trAdv">
            <td colspan="2">Марка автомобиля (дополнительно):</td>
        </tr>
        <tr class="trAdv">
            <td colspan="2">
                <asp:TextBox ID="tbxAutoMarkAdv" runat="server" Width="100%"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td colspan="2">Удалённый доступ к камерам видеонаблюдения (логин пароль)</td>
        </tr>
        <tr>
            <td colspan="2">
                <asp:TextBox ID="tbxRemoteCamera" runat="server" Width="100%"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>Участник чата SOS</td>
            <td>
                <asp:CheckBox ID="chkSOS" runat="server" />
            </td>
        </tr>
        <tr>
            <td>Ознакомлен и согласен с Положением о пользовании парковкой</td>
            <td>
                <asp:CheckBox ID="chkRulesParking" runat="server" />
            </td>
        </tr>
        <tr>
            <td>С обработкой своих персональных данных согласен</td>
            <td>
                <asp:CheckBox ID="chkRulesPersonal" runat="server" />
            </td>
        </tr>

        <tr>
            <td colspan="2">Примечание:</td>
        </tr>
        <tr>
            <td colspan="2">
                <asp:TextBox ID="tbxComments" runat="server" Height="62px" Width="100%" TextMode="MultiLine"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td colspan="2">&nbsp;</td>
        </tr>
        <tr>
            <td colspan="2">
                <asp:Button ID="btnSaveUserInfo" runat="server" OnClick="btnSaveUserInfo_Click" Text="Сохранить изменения" Width="250px" />
            </td>
        </tr>
    </table>

</asp:Content>
<asp:Content ID="Content3" runat="server" ContentPlaceHolderID="head">
    <style type="text/css">
        .auto-style1 {
            height: 25px;
        }
    </style>
</asp:Content>

