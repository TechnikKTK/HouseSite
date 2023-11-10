<%@ Page Title="" Language="C#" MasterPageFile="~/Users/MasterPage.master" AutoEventWireup="true" CodeFile="Management.aspx.cs" Inherits="Management" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <title>панель управления
    </title>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentNav" runat="Server">

    <li class="nav-item">
        <a class="nav-link active" aria-current="page" href="#">Главная</a>
    </li>
    <li class="nav-item">
        <a class="nav-link" href="/notify">Уведомления</a>
    </li>
    <li class="nav-item">
        <a class="nav-link" href="/messages">Мои обращения</a>
    </li>

    <li class="nav-item dropdown">
        <a class="nav-link dropdown-toggle" href="#" id="navbarDropdown" role="button" data-bs-toggle="dropdown" aria-expanded="false">Действия
        </a>
        <ul class="dropdown-menu" aria-labelledby="navbarDropdown">
            <li><a class="dropdown-item" href="/phone-by-auto">Получить телефон владельца авто</a></li>
            <li><a class="dropdown-item" href="/send-message">Отправить сообщение администратору</a></li>
        </ul>
    </li>

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <% if (lblBarrierLocked.Text.Contains("заблокирован доступ"))
       {  %>
        
        <div class="alert alert-barrier d-flex align-items-center" role="alert" id="alert" style="cursor:pointer;margin-top: 3px;justify-content: center; width:99%; margin-left:3px">
            <div class="flex-column" style="justify-content: space-between;">
                <span id="alert_text" onclick="location.href='/send-message'">Внимание!!! Доступ к шлагбауму заблокирован.</span>
            </div>
        </div>

    <% } %>

    <div class="accordion" id="accordionExample">
        <div class="accordion-item">
            <h2 class="accordion-header">
                <button class="accordion-button" type="button" data-bs-toggle="collapse" data-bs-target="#collapseOne" aria-expanded="true" aria-controls="collapseOne">
                    Основные сведения
                </button>
            </h2>
            <div id="collapseOne" class="accordion-collapse collapse show" data-bs-parent="#accordionExample">
                <div class="accordion-body">                    
                    <lk-doc-card-row class="header-inner">
                        <h6 class="accordion-header">
                            Фамилия, имя и отчество
                        </h6>
                    </lk-doc-card-row>
                    <lk-doc-card-row class="ng-star-inserted">
                        <div class="mt-0 ng-star-inserted">
                            <div class="text-plain mt-0 ng-star-inserted">
                                <asp:Label runat="server" ID="lblFio" />
                            </div>
                        </div>
                    </lk-doc-card-row> 
                    <lk-doc-card-row class="header-inner">
                        <h6 class="accordion-header">
                            Email
                        </h6>
                    </lk-doc-card-row>
                    <lk-doc-card-row class="ng-star-inserted">
                        <div class="ng-star-inserted">
                            <div class="text-plain mt-0 ng-star-inserted">
                                <asp:HyperLink runat="server" ID="lblEmail" />
                            </div>
                        </div>
                    </lk-doc-card-row>
                    <lk-doc-card-row class="header-inner">
                        <h6 class="accordion-header">
                            Телефон
                        </h6>
                    </lk-doc-card-row>
                    <lk-doc-card-row class="ng-star-inserted">
                        <div class="ng-star-inserted">
                            <div class="text-plain mt-0 ng-star-inserted">
                                <asp:Label runat="server" ID="lblPhone" />
                            </div>
                        </div>
                    </lk-doc-card-row>
                    <lk-doc-card-row class="header-inner">
                        <h6 class="accordion-header">
                            Дополнительный телефон
                        </h6>
                    </lk-doc-card-row>
                    <lk-doc-card-row class="ng-star-inserted">
                        <div class="ng-star-inserted">
                            <div class="text-plain mt-0 ng-star-inserted">
                                <asp:Label runat="server" ID="lblPhoneAdv" />
                            </div>
                        </div>
                    </lk-doc-card-row> 
                </div>
            </div>
        </div>
        <div class="accordion-item">
            <h2 class="accordion-header">
                <button class="accordion-button collapsed" type="button" data-bs-toggle="collapse" data-bs-target="#collapseTwo" aria-expanded="false" aria-controls="collapseTwo">
                    Сведения о помещении
                </button>
            </h2>
            <div id="collapseTwo" class="accordion-collapse collapse" data-bs-parent="#accordionExample">
                <div class="accordion-body">
                    <lk-doc-card-row class="header-inner">
                        <h6 class="accordion-header">
                            Общая информация
                        </h6>
                    </lk-doc-card-row>
                    <lk-doc-card-row class="ng-star-inserted d-flex flex-column">
                        <div class="mt-0 ng-star-inserted">
                            <div class="text-plain mt-1 ng-star-inserted">
                                 Подъезд: <asp:Label runat="server" ID="lblEntrance" />
                            </div>
                        </div>
                        <div class="mt-0 ng-star-inserted">
                            <div class="text-plain mt-1 ng-star-inserted">
                                 Этаж: <asp:Label runat="server" ID="lblFloor" />
                            </div>
                        </div>
                        <div class="mt-0 ng-star-inserted">
                            <div class="text-plain mt-1 ng-star-inserted">
                                 Помещение: <asp:Label runat="server" ID="lblFlatNumber" />
                            </div>
                        </div>
                    </lk-doc-card-row>                    
                    <lk-doc-card-row class="header-inner">
                        <h6 class="accordion-header">
                            Вид владения собственностью
                        </h6>
                    </lk-doc-card-row>
                    <lk-doc-card-row class="ng-star-inserted">
                        <div class="ng-star-inserted">
                            <div class="text-plain ng-star-inserted">
                                <asp:Label runat="server" ID="lblStatus" />
                            </div>
                        </div>
                    </lk-doc-card-row>
                </div>
            </div>
        </div>
        <div class="accordion-item">
            <h2 class="accordion-header">
                <button class="accordion-button collapsed" type="button" data-bs-toggle="collapse" data-bs-target="#collapseThree" aria-expanded="false" aria-controls="collapseThree">
                    Автомобиль (автомобили)
                </button>
            </h2>
            <div id="collapseThree" class="accordion-collapse collapse" data-bs-parent="#accordionExample" style="">
                <div class="accordion-body">
                     <lk-doc-card-row class="header-inner">
                        <h6 class="accordion-header">
                            Основной автомобиль
                        </h6>
                    </lk-doc-card-row>
                    <lk-doc-card-row class="ng-star-inserted">
                        <div class="ng-star-inserted d-flex flex-row">
                            <div class="text-plain ng-star-inserted">
                                <asp:Label runat="server" ID="lblAutoMark" />
                            </div>
                            <div class="text-plain ng-star-inserted">
                                <asp:Label runat="server" ID="lblAutoNumber" />
                            </div>
                        </div>
                    </lk-doc-card-row>
                    <lk-doc-card-row class="header-inner">
                        <h6 class="accordion-header">
                            Другой автомобиль
                        </h6>
                    </lk-doc-card-row>
                    <lk-doc-card-row class="ng-star-inserted">
                        <div class="ng-star-inserted d-flex flex-row">
                            <div class="text-plain ng-star-inserted">
                                <asp:Label runat="server" ID="lblAutoMarkAdv" />
                            </div>
                            <div class="text-plain ng-star-inserted">
                                <asp:Label runat="server" ID="lblAutoNumberAdv" />
                            </div>
                        </div>
                    </lk-doc-card-row>
                </div>
            </div>
        </div>
        <div class="accordion-item">
            <h2 class="accordion-header">
                <button class="accordion-button collapsed" type="button" data-bs-toggle="collapse" data-bs-target="#collapse4" aria-expanded="false" aria-controls="collapseThree">
                    Дополнительные сведения
                </button>
            </h2>
            <div id="collapse4" class="accordion-collapse collapse" data-bs-parent="#accordionExample">
                <div class="accordion-body">
                    <lk-doc-card-row class="header-inner">
                        <h6 class="accordion-header">
                            Чат SOS
                        </h6>
                    </lk-doc-card-row>
                    <lk-doc-card-row class="ng-star-inserted">
                        <div class="ng-star-inserted">
                            <div class="text-plain ng-star-inserted">
                                <asp:Label runat="server" ID="lblSOS" />
                            </div>
                        </div>
                    </lk-doc-card-row>
                    <lk-doc-card-row class="header-inner">
                        <h6 class="accordion-header">
                            Доступ к видеонаблюдению
                        </h6>
                    </lk-doc-card-row>
                    <lk-doc-card-row class="ng-star-inserted flex-column">
                        <div class="ng-star-inserted d-flex flex-row">
                            <div class="text-plain ng-star-inserted text-muted">
                                <asp:Label runat="server" ID="lblRemoteCamera" />
                            </div>
                        </div>                   
                    </lk-doc-card-row>
                    <lk-doc-card-row class="header-inner">
                        <h6 class="accordion-header">
                            Доступ к шлагбауму
                        </h6>
                    </lk-doc-card-row>
                    <lk-doc-card-row class="ng-star-inserted flex-column">
                        <div class="ng-star-inserted d-flex flex-row">
                            <div class="text-plain ng-star-inserted text-muted">
                                <asp:Label runat="server" ID="lblBarrierLocked" />
                            </div>
                        </div>                   
                    </lk-doc-card-row>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
