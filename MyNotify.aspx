<%@ Page Async="true" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="MyNotify.aspx.cs" Inherits="MyNotify" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <title>
        Мои уведомления
    </title>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentNav" Runat="Server">

    <li class="nav-item">
        <a class="nav-link" href="/home">Дашборд</a>
    </li>
    <li class="nav-item">
        <a class="nav-link active" aria-current="page" href="#">Уведомления</a>
    </li>
    <li class="nav-item dropdown">
        <a class="nav-link dropdown-toggle" href="#" id="navbarDropdown" role="button" data-bs-toggle="dropdown" aria-expanded="false">
            Действия
        </a>
        <ul class="dropdown-menu" aria-labelledby="navbarDropdown">
            <li><a class="dropdown-item" href="/phone-by-auto">Получить номер по авто</a></li>
            <li><a class="dropdown-item" href="/send-message">Отправить сообщение администратору</a></li>
        </ul>
    </li>
                
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <%--<asp:Button ID="send_fcm" runat="server" OnClick="send_fcm_Click" style="position:absolute; top: 50%; left:50%;" />--%>
    <div style="margin: 100px 0 0 0; background-color: lightgray; padding: 10px; border-radius: 12px; overflow: hidden">
        <table class="table table-dark" style="border-radius: 12px; overflow: hidden">
            <thead class="thead-dark">
                <tr>
                    <th scope="col">#</th>
                    <th scope="col">First</th>
                    <th scope="col">Last</th>
                    <th scope="col">Handle</th>
                </tr>
            </thead>
            <tbody>
                <tr>
                    <th scope="row">1</th>
                    <td>Mark</td>
                    <td>Otto</td>
                    <td>@mdo</td>
                </tr>
                <tr>
                    <th scope="row">2</th>
                    <td>Jacob</td>
                    <td>Thornton</td>
                    <td>@fat</td>
                </tr>
                <tr>
                    <th scope="row">3</th>
                    <td>Larry</td>
                    <td>the Bird</td>
                    <td>@twitter</td>
                </tr>
            </tbody>
        </table>

        <table class="table table-dark" style="margin: 0; border-radius: 12px; overflow: hidden">
            <thead class="thead-light">
                <tr>
                    <th scope="col">#</th>
                    <th scope="col">First</th>
                    <th scope="col">Last</th>
                    <th scope="col">Handle</th>
                </tr>
            </thead>
            <tbody>
                <tr>
                    <th scope="row">1</th>
                    <td>Mark</td>
                    <td>Otto</td>
                    <td>@mdo</td>
                </tr>
                <tr>
                    <th scope="row">2</th>
                    <td>Jacob</td>
                    <td>Thornton</td>
                    <td>@fat</td>
                </tr>
                <tr>
                    <th scope="row">3</th>
                    <td>Larry</td>
                    <td>the Bird</td>
                    <td>@twitter</td>
                </tr>
            </tbody>
        </table>
    </div>
</asp:Content>

