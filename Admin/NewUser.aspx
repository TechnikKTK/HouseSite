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

    <div class="form-group">
        <label for="tbxUserName">Логин для входа</label>
        <asp:TextBox ID="tbxUserName" runat="server" ClientIDMode="Static"  
            aria-describedby="loginHelp" placeholder="Введите логин"
            CssClass="form-control"/>
        <small id="loginHelp" class="form-text text-muted">Логин необходим для авторизации</small>
    </div>
    <div class="form-group">
        <label for="tbxFIO">Ф.И.О. (полностью)</label>
        <asp:TextBox ID="tbxFIO" ClientIDMode="Static" runat="server" 
            aria-describedby="fioHelp" placeholder="Введите ф.и.о."
            CssClass="form-control" /> 
        <small id="fioHelp" class="form-text text-muted">Ф.И.О. будет добавлено пользователю</small>
    </div>
    <div class="form-group">
        <label for="tbxEmail">Email</label>
        <asp:TextBox runat="server" ID="tbxEmail" ClientIDMode="Static"  
            aria-describedby="emailHelp" placeholder="Введите email"
            CssClass="form-control" />
        <small id="emailHelp" class="form-text text-muted">email должен соответствовать правилу: имя@домен.зона</small>
    </div>
    <div class="form-group">
        <label for="tbxPassword">Пароль</label>
        <asp:TextBox ID="tbxPassword" runat="server" ClientIDMode="Static" 
            aria-describedby="passwordHelp" placeholder="Введите пароль"
            CssClass="form-control" />
        <small id="passwordHelp" class="form-text text-muted">Запомните пароль.Он будет зашифрован после добавления пользователя</small>
    </div>
    <asp:Button ID="btnCreateUser" runat="server" CssClass="btn btn-primary"
        Text="Сохранить" OnClick="btnCreateUser_Click" />
    
</asp:Content>
