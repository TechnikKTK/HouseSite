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
        <label for="tbxLastName">Фамилия</label>
        <asp:TextBox ID="tbxLastName" ClientIDMode="Static" runat="server" 
            aria-describedby="lastnameHelp" placeholder="Введите фамилию"
            CssClass="form-control" /> 
        <small id="lastnameHelp" class="form-text text-muted">будет добавлено пользователю</small>
    </div>    
    <div class="form-group">
        <label for="tbxName">Имя</label>
        <asp:TextBox ID="tbxName" ClientIDMode="Static" runat="server" 
            aria-describedby="nameHelp" placeholder="Введите имя"
            CssClass="form-control" /> 
        <small id="nameHelp" class="form-text text-muted">будет добавлено пользователю</small>
    </div>    <div class="form-group">
        <label for="tbxPatronumic">Отчество</label>
        <asp:TextBox ID="tbxPatronumic" ClientIDMode="Static" runat="server" 
            aria-describedby="patronHelp" placeholder="Введите отчество"
            CssClass="form-control" /> 
        <small id="patronHelp" class="form-text text-muted">будет добавлено пользователю</small>
    </div>
    <div class="form-group">
        <label for="tbxAuto">Номер автомобиля (если есть)</label>
        <asp:TextBox runat="server" ID="tbxAuto" ClientIDMode="Static"  
            aria-describedby="phoneHelp" placeholder="Введите рег. знак авто"
            CssClass="form-control auto_num" />
        <small id="phoneHelp" class="form-text text-muted">номер рег.знака вводить криллицей</small>
    </div>
    <div class="form-group">
        <label for="tbxPhone">Номер телефона (если есть)</label>
        <asp:TextBox runat="server" ID="tbxPhone" ClientIDMode="Static"  
            aria-describedby="autoHelp" placeholder="Введите номер телефона"
            CssClass="form-control phone_num" />
        <small id="autoHelp" class="form-text text-muted">будет добавлено пользователю</small>
    </div>
    <div class="form-group">
        <label for="tbxEmail">Email (если есть)</label>
        <asp:TextBox runat="server" ID="tbxEmail" ClientIDMode="Static"  
            aria-describedby="emailHelp" placeholder="Введите email"
            CssClass="form-control email_text" />
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
