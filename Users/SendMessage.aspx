<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SendMessage.aspx.cs" MasterPageFile="~/Users/MasterPage.master" Inherits="SendMessage" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <title>
        Отправить заявку
    </title>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentNav" Runat="Server">

    <li class="nav-item">
        <a class="nav-link" href="/home">Дашборд</a>
    </li>
    <li class="nav-item">
        <a class="nav-link" href="#">Уведомления</a>
    </li>
    <li class="nav-item dropdown">
        <a class="nav-link dropdown-toggle active" aria-current="page" href="#" id="navbarDropdown" role="button" data-bs-toggle="dropdown" aria-expanded="false">
            Действия
        </a>
        <ul class="dropdown-menu" aria-labelledby="navbarDropdown">
            <li><a class="dropdown-item" href="/phone-by-auto">Получить номер по авто</a></li>
            <li><a class="dropdown-item" href="#">Отправить сообщение администратору</a></li>
        </ul>
    </li>
                
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<div align="center" style="padding: 5px;    margin-top: 55px;">
    <table border="0" cellpadding="0" cellspacing="0" style="padding: 5px">
        <tr>
            <td> 
                <asp:DropDownList ID="ddlTypeMessage" runat="server">
                    <asp:ListItem>Передать информацию об автомобиле</asp:ListItem>
                    <asp:ListItem>Изменить ФИО</asp:ListItem>
                    <asp:ListItem>Изменить телефон</asp:ListItem>
                    <asp:ListItem>Изменить E-mail</asp:ListItem>
                    <asp:ListItem>Подать заявку на решение проблемы</asp:ListItem>
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td style="height:20px;">
                      
            </td>
        </tr>
        <tr>
            <td> 
                <asp:TextBox TextMode="MultiLine"  ID="tbxBodyMessage" runat="server" style="width: 100%; height: 180px;"></asp:TextBox>
            </td>
        </tr>
         <tr>
            <td style="height:20px;">
                      
            </td>
        </tr>
        <tr>
            <td>
                <asp:Button ID="btnSendMessages" runat="server" CssClass="btn btn-primary"  Text="Отправить" OnClick="btnSendMessages_Click" />
            </td>
        </tr>
    </table>    
    </div>
</asp:Content>