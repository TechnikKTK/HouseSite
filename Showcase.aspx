<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Showcase.aspx.cs" Inherits="_Showcase" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Login Page</title>
</head>
<body>
    <form id="form1" runat="server">
    <div align="center" style="background-color:black; padding: 5px">
    <table border="0" cellpadding="0" cellspacing="0" style="padding: 5px">
        <tr>
            <td align="center" bgcolor="Yellow"> 
                LogIn
            </td>
        </tr>
        <tr>
            <td> 
    <!--<asp:HyperLink ID="lnkCreateAccount" runat="server" class="A20" style="font-family: Arial; Font-size: 10px;" NavigateUrl="~/CreateAccount.aspx">Регистрация</asp:HyperLink>-->
            <asp:LoginView ID="LoginView1" runat="server" >
                            <AnonymousTemplate>
                                <asp:Login ID="Login" runat="server" FailureAction="Refresh" 
                                    DestinationPageUrl="~/Default.aspx" >
                                    <LayoutTemplate>
                                        <table style="width:100%;text-align:center;" cellpadding="0" cellspacing="0" 
                                            border="0">
                                            <tr>
                                                <td align="left" colspan="2">
                                                   Login:<br />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="2">
                                                    <asp:TextBox ID="UserName" runat="server" CssClass="loginTextBox" Width="158px" Text=""/>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="left">
                                                    Password 
                                                </td>
                                                <td align="right">
                                                    <!--<asp:HyperLink ID="lnkPasswordRecovery" runat="server" class="rightAllignCell" NavigateUrl="~/PasswordRecovery.aspx">забыли?</asp:HyperLink>-->
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="2">
                                                    <asp:TextBox ID="Password" runat="server" CssClass="loginTextBox" TextMode="Password"  Width="158px" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="left" style="padding-left:3px;">
                                                    <asp:CheckBox ID="RememberMe" runat="server" CssClass="loginCheck" Text="remember" />
                                                </td>
                                                <td align="right">
                                                    <asp:Button ID="Submit" runat="server" CommandName="Login" Text="Login" ValidationGroup="Login" />
                                                </td>
                                            </tr>
                                        </table>
                                        
                                        </div>
                                        <br />
                                        <!--<asp:HyperLink ID="HyperLink1" runat="server" class="rightAllignCell" NavigateUrl="~/CreateAccount.aspx">Регистрация</asp:HyperLink>-->
                                    </LayoutTemplate>
                                </asp:Login>
                            </AnonymousTemplate>
                            <RoleGroups> 
                            <asp:RoleGroup Roles="admin"> 
                                <ContentTemplate> 
                                    <asp:LoginName ID="logNmUser" runat="server" FormatString="Здравствуйте {0}" class="registerLable"/><br/> 
                                    <asp:HyperLink  ID="HyperLink1" runat="server" NavigateUrl="Posts.aspx" >Список постов</asp:HyperLink><br />
                                    <asp:LoginStatus ID="LoginStatus1" runat="server"   LoginText="Выход" LogoutText="Выход" LogoutPageUrl="Default.aspx" LogoutAction="Redirect" /> 
                                </ContentTemplate> 
                            </asp:RoleGroup> 
                            <asp:RoleGroup Roles="user"> 
                                <ContentTemplate> 
                                    <asp:LoginName ID="logNmUser" runat="server" FormatString="Здравствуйте {0}" class="registerLable"/><br/>
                                    <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl="Posts.aspx" >Список постов</asp:HyperLink><br />                                   
                                    <asp:LoginStatus ID="LoginStatus2" runat="server" LoginText="Выход" LogoutText="Выход" LogoutPageUrl="Default.aspx" LogoutAction="Redirect" />                                    
                                </ContentTemplate> 
                            </asp:RoleGroup>
                            </RoleGroups> 
                        </asp:LoginView>
           
            </td>
        </tr>
        <tr>
            <td>
                       <asp:Label Visible="false" ID="lblBadPass" runat="server" ForeColor="Red" Text="Login or password failed!"></asp:Label>
            </td>
        </tr>
    </table>
    
    </div>
    
    </form>
</body>
</html>
