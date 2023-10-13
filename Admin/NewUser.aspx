<%@ Page Language="C#" MasterPageFile="~/Admin/AdminMasterPage.master" AutoEventWireup="true" CodeFile="NewUser.aspx.cs" Inherits="Admin_NewUser" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentNav" runat="Server">

    <li class="nav-item">
        <a class="nav-link active" href="/admin/users">Пользователи</a>
    </li>
    <li class="nav-item">
        <a class="nav-link" href="/admin/messages">Сообщения</a>
    </li>

</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="Server">
    <div>
        <table width='528' border='0' bordercolor='red' cellpadding='0' cellspacing='0'>
            <tr>
                <td height="10" colspan="2">Новый пользователь:</td>
            </tr>
            <tr>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td class='my2'>Логин для входа:</td>
                <td>
                    <asp:TextBox ID="UserName" runat="server" CssClass="my21" />
                </td>
            </tr>
            <tr>
                <td height="10" colspan="2"></td>
            </tr>
            <tr>
                <td class='my2'>E-mail</td>
                <td>
                    <asp:TextBox runat="server" ID="tbxEmail" CssClass="my21" />
                </td>
            </tr>
            <tr>
                <td height="10" colspan="2"></td>
            </tr>
            <tr>
                <td class='my2'>Пароль</td>
                <td>
                    <asp:TextBox ID="Password" runat="server" CssClass="my21" />
                </td>
            </tr>

            <tr>
                <td height="10" colspan="2"></td>
            </tr>
            <tr>
                <td colspan="2">
                    <asp:Button ID="btnCreateUser" runat="server"
                        Text="Сохранить" OnClick="btnCreateUser_Click" /></td>
            </tr>
        </table>
    </div>

</asp:Content>
