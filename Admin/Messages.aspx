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

    <div class="card" style="margin: 75px auto;margin-top: 0px;">
        <div class="card-header card-header d-flex justify-content-between align-items-center">
            <span>Заявки пользователей</span>
        </div>
        <ul class="list-group list-group-flush">
            <li class="list-group-item">
                <div class="d-flex flex-row">
                    <button type="button" class="btn btn-primary" id="btnNewMessages">Новые</button>
                    <button type="button" class="btn btn-secondary" id="btnReadMessages" style="margin-left: 10px">Прочитанные</button>
                </div>
            </li>
            <li class="list-group-item" style="overflow-x:auto">
                <table class="table table-striped table-hover">
                    <thead class="thead-dark">
                        <tr>
                            <th scope="col">#</th>
                            <th scope="col">Логин</th>
                            <th scope="col">Дата</th>
                            <th scope="col">Тема</th>
                            <th scope="col">Сообщение</th>
                        </tr>
                    </thead>
                    <tbody>
                        <asp:Repeater ID="NotifyRepeater" runat="server">
                            <ItemTemplate>
                                <tr <%# GetClass(Eval("IsRead")) %>>
                                    <th scope="row">
                                        <div class="d-flex flex-row align-items-center">
                                            <asp:ImageButton ID="btn_isRead" OnCommand="ChangeIsRead" ToolTip="отметить прочитано"
                                                runat="server"  ImageUrl='<%# GetImage(Eval("IsRead")) %>'
                                                style="min-width: 20px; cursor:pointer;margin: 0 5px;"
                                                CommandArgument='<%# Eval("ID").ToString() %>' />
                                           
                                            <img src="/images/trash-solid.svg" style="max-height: 16px; margin: 3px 6px; cursor:pointer"
                                                id="barrier_dialog" data-toggle="modal" data-target="#modalMessage"
                                                onclick="deleteMess(this)"
                                                title="удалить"
                                                />

                                            <asp:Button ID="btnDelete"
                                                CssClass="btnDeleteMessage" Style="display: none" ClientIDMode="Predictable"
                                                OnCommand="DeleteMessage" runat="server" 
                                                CommandArgument='<%# new object[] { Eval("ID").ToString(), (bool)Eval("IsRead") }  %>' />

                                            <img src="/images/reply-solid.svg" style="min-width: 20px; cursor:pointer;margin: 0 5px;"
                                                id="reply_dialog" data-toggle="modal" data-target="#modalAnswer"
                                                onclick="answerMess('<%# EncodeMessage(Eval("BodyMessage")) %>','<%# Eval("ID").ToString() %>', '<%# Eval("UserId").ToString() %>',this)"
                                                title="ответить"
                                                />
                                        </div>
                                    </th>
                                    <td><a href='/admin/users/edit?UserName=<%# Eval("UserName") %>'><%# Eval("UserName") %></a></td>
                                    <td><%# Eval("CreatedAt") %></td>
                                    <td><%# Eval("TypeMessage") %></td>
                                    <td><%# Eval("BodyMessage") %></td>
                                </tr>
                            </ItemTemplate>
                        </asp:Repeater>
                    </tbody>
                </table>
            </li>
        </ul>
    </div>

    <!-- Modal -->
    <div class="modal fade" id="modalMessage" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabel" aria-hidden="true">
        <div class="modal-dialog" role="document">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title" id="exampleModalLabel">Удаление сообщения</h5>
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">&times;</span>
                    </button>
                </div>
                <div class="modal-body">
                    Вы действительно хотите удалить это сообщение?
                </div>
                <div class="modal-footer">
                    <button id="btnDelMess_ok" type="button" class="btn btn-primary" style="min-width: 75px;">OK</button>
                    <button type="button" class="btn btn-secondary" data-dismiss="modal">Отмена</button>
                </div>
            </div>
        </div>
    </div>
    
    <!-- Modal -->
    <div class="modal fade" id="modalAnswer" tabindex="-1" role="dialog" aria-labelledby="exampleTitle" aria-hidden="true">
        <div class="modal-dialog" role="document">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title" id="exampleTitle">Ответ на сообщение</h5>
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">&times;</span>
                    </button>
                </div>
                <div class="modal-prebody" style="margin: 0 20px;">
                </div>                
                <div class="modal-body">
                    <textarea id="txtAnswer" rows="4"
                        class="form-control" 
                        style="max-width:100%!important"></textarea>                        
                </div>
                <div class="modal-footer">
                    <button id="btnSendAns" type="button" class="btn btn-primary" style="min-width: 75px;">OK</button>
                    <button type="button" class="btn btn-secondary" data-dismiss="modal">Отмена</button>
                </div>
            </div>
        </div>
    </div>

</asp:Content>
