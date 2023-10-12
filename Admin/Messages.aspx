<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/AdminMasterPage.master" AutoEventWireup="true"
    CodeFile="Messages.aspx.cs" Inherits="Messages" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentNav" runat="Server">

    <li class="nav-item">
        <a class="nav-link" href="/admin/users">Пользователи</a>
    </li>
    <li class="nav-item">
        <a class="nav-link active" href="#">Сообщения</a>
    </li>

</asp:Content>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="Server">
    <div>Заявки пользователей</div>
    <p>
        <asp:GridView ID="gvMessages" ClientIDMode="Static" runat="server" AutoGenerateColumns="False" DataSourceID="SqlDataSource1" Width="836px">
            <HeaderStyle BackColor="#767676" />
            <Columns>
                <asp:BoundField DataField="BodyMessage" HeaderText="BodyMessage" SortExpression="BodyMessage" />
                <asp:BoundField DataField="TypeMessage" HeaderText="TypeMessage" SortExpression="TypeMessage" />
                <asp:BoundField DataField="column1" HeaderText="column1" SortExpression="column1" />
                <asp:BoundField DataField="UserId" HeaderText="UserId" SortExpression="UserId" />
            </Columns>
        </asp:GridView>
    </p>
    <p>
        <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:migConnectionString %>" SelectCommand="SELECT [BodyMessage], [TypeMessage], [_Date] AS column1, [UserId] FROM [hs_Messages]"></asp:SqlDataSource>
    </p>
</asp:Content>
