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
    <asp:TextBox runat="server" ID="txtSearchText" ClientIDMode="Static" style="min-width:200px;"/>
    <asp:Button runat="server" ID="btnSearch" Text="Найти" style="min-width:100px;"
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
                DataFormatString="{0:MM/dd/yy h:mm tt}" />
            <asp:BoundField HeaderText="Последняя активность" DataField="LastActivityDate"
                DataFormatString="{0:MM/dd/yy h:mm tt}" />
            <asp:BoundField HeaderText="Код пользовтаеля" DataField="ProviderUserKey" />
            <asp:CheckBoxField HeaderText="Разрешен вход" DataField="IsApproved"
                HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" />
            <asp:HyperLinkField Text="<img src='/images/user-avatar.png'  border='0' />"
                DataNavigateUrlFormatString="/admin/users/edit?UserName={0}" DataNavigateUrlFields="UserName" />
            <asp:ButtonField CommandName="Delete" ButtonType="Image"
                ImageUrl="/images/social-media.png" />  
            <asp:TemplateField HeaderText="Шлагбаум">
                <ItemTemplate>
                    <asp:ImageButton ID="changeBarrier" runat="server" 
                        ImageUrl='<%# GetLockPost(Eval("ProviderUserKey").ToString()) %>'
                        OnCommand="ChangeLockPost" 
                        CommandArgument='<%# Eval("ProviderUserKey").ToString()  %>'>
                    </asp:ImageButton>
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
        <EmptyDataTemplate>Не найдено пользователей.</EmptyDataTemplate>
    </asp:GridView>
    <p></p>
    <asp:Button ID="btnNewUser" PostBackUrl="users/add" runat="server"
        Text="Создать нового пользователя" style="min-width: 304px;" />

    <script type="text/javascript">
        $(document).ready(function () {
            text = $('#ddlUserSearchTypes').val();
            if (text == "1") {
                $('#txtSearchText').attr("placeholder", "Например: vasya@ya.ru");
            }
            else {
                $('#txtSearchText').attr("placeholder", "Например: Василий");
            }

            $("#gvUsers").attr("class", "table table-bordered table-striped");
        });

        $('#ddlUserSearchTypes').change(function () {
            let text = this.value;
            if (text == "1") {
                $('#txtSearchText').attr("placeholder", "Например: vasya@ya.ru");
            }
            else if(text == "2") {
                $('#txtSearchText').attr("placeholder", "Например: T234EP99");
            }            
            else {
                $('#txtSearchText').attr("placeholder", "Например: Василий");
            }
        });
    </script>
</asp:Content>
