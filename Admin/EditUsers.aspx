<%@ Page Language="C#" MasterPageFile="~/Admin/AdminMasterPage.master" AutoEventWireup="true" CodeFile="EditUsers.aspx.cs" Inherits="EditUsers" %>
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
            <td>Логин:</td>
            <td>
                <asp:Literal runat="server" ID="lblUserName" />
            </td>
        </tr>
        <tr>
            <td>E-mail:</td>
            <td>
                <asp:HyperLink ID="lnkEmailAddress" runat="server" /></td>
        </tr>
        <tr>
            <td>Зарегистрирован:</td>
            <td>
                <asp:Literal ID="lblRegistered" runat="server" /></td>
        </tr>
        <tr>
            <td>Последний логин:</td>
            <td>
                <asp:Literal ID="lblLastLogin" runat="server" /></td>
        </tr>
        <tr>
            <td>Последний вход:</td>
            <td>
                <asp:Literal ID="lblLastActivity" runat="server" /></td>
        </tr>
        <tr>
            <td>Пользователь Online:</td>
            <td>
                <asp:CheckBox ID="chkIsOnlineNow" Enabled="false" runat="server" /></td>
        </tr>
        <tr>
            <td>Разблокирован:</td>
            <td>
                <asp:CheckBox ID="chkIsApproved" AutoPostBack="true" OnCheckedChanged="chkIsApproved_CheckedChanged" runat="server" /></td>
        </tr>
        <tr>
            <td>Заблокирован:</td>
            <td>
                <asp:CheckBox ID="chkIsLockedOut" AutoPostBack="true" OnCheckedChanged="chkIsLockedOut_CheckedChanged" runat="server" /></td>
        </tr>
    </table>
    <p></p>
    <div>Роли пользователя</div>
    <p></p>
    
    <table cellpadding="2" width="450">
        <tr>
            <td> 
                <asp:CheckBoxList runat="server" ID="chklRoles" RepeatColumns="5" CellSpacing="10" />
                <asp:Button ID="btnUpdate" Text="Применить роли"
                    OnClick="btnUpdate_Click" runat="server" />
                <asp:Label runat="server" ID="lblRolesOK"
                    Text="Роль пользователя успешно изменена" Visible="false" />
            </td>
        </tr>
    </table>
    
    <table>
        <tr>
            <td>
                <asp:TextBox ID="TextBox1" runat="server" style="width:100%"></asp:TextBox>
                </td>
                <td>
                    <asp:Button ID="btnChangePass" Text="Сменить пароль" OnClick="btnChangePass_Click" runat="server" />
                </td>
            </tr>
        <tr>
           <td colspan="2">
            <asp:Button ID="btnGenPass" Text="Сгенерировать пароль"
            OnClick="btnGenPass_Click" runat="server" style="width:100%" />
                       <asp:Label runat="server" ID="lblChangePasssOK"
            Text="Пароль успешно изменен" Visible="False" />
           </td>
        </tr>
        <tr>
            <td colspan="2">Номер квартиры:</td></tr><tr>
            <td colspan="2">
                <asp:TextBox ID="tbxFlatNumber" runat="server" Width="100%"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td colspan="2">Подъезд:</td></tr><tr>
            <td colspan="2">
                <asp:TextBox ID="tbxEntrance" runat="server" Width="100%"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td colspan="2">Этаж:</td></tr><tr>
            <td colspan="2">
                <asp:TextBox ID="tbxFloor" runat="server" Width="100%"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td colspan="2">ФИО:</td></tr><tr>
            <td colspan="2">
                <asp:TextBox ID="tbxName" runat="server" Width="100%"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td colspan="2" class="auto-style1">Статус:</td></tr><tr>
            <td colspan="2" class="auto-style1">
                <asp:DropDownList ID="ddlStatus" runat="server" Width="100%">
                    <asp:ListItem>Собственник</asp:ListItem>
                    <asp:ListItem>Член семьи собственника</asp:ListItem>
                    <asp:ListItem>Арендатор</asp:ListItem>
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td colspan="2">Телефон заявителья:</td></tr><tr>
            <td colspan="2">
                <asp:TextBox ID="tbxPhone" runat="server" Width="100%"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td colspan="2">E-mail:</td></tr><tr>
            <td colspan="2">
                <asp:TextBox ID="tbxEmail" runat="server" Width="100%"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td colspan="2">Номер автомобиля:</td></tr><tr>
            <td colspan="2">
                <asp:TextBox ID="tbxAutoNumber" runat="server" Width="100%"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td colspan="2">Марка автомобиля:</td></tr><tr>
            <td colspan="2">
                <asp:TextBox ID="tbxAutoMark" runat="server" Width="100%"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td colspan="2">Улалённый доступ к камерам видеонаблюдения (логин пароль):</td></tr><tr>
            <td colspan="2">
                <asp:TextBox ID="tbxRemoteCamera" runat="server" Width="100%"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>Участвует ли в общедомовом чате "SOS":</td>
            <td>
                <asp:CheckBox ID="chkSOS" runat="server" />
            </td>
        </tr>
        <tr>
            <td>Нарушил ли правила парковки (отключен доступ):</td>
            <td>
                <asp:CheckBox ID="chkBrokeRules" runat="server" />
            </td>
        </tr>
        <tr>
            <td colspan="2">Примечание:</td></tr><tr>
            <td colspan="2">
                <asp:TextBox ID="tbxComments" runat="server" Height="62px" Width="100%" TextMode="MultiLine"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td colspan="2">&nbsp;</td></tr><tr>
            <td colspan="2">
                <asp:Button ID="btnSaveUserInfo" runat="server" OnClick="btnSaveUserInfo_Click" Text="Сохранить изменения" Width="217px" />
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

