<%@ Page Title="Inicio - Sir. Ferdnz Automotive Workshop" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="Proyecto2_ADB_TallerMecanico._Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <div class="container mt-5">
        <div class="row text-center">
            <h2>Bienvenido a Sir. Ferdnz Automotive Workshop</h2>
            <p class="lead">Seleccione un cliente para ver su historial o registrar una nueva orden de servicio.</p>
        </div>

        <div class="row justify-content-center mt-4">
            <div class="col-md-6">
                <div class="card shadow-sm">
                    <div class="card-body">
                        <h4 class="card-title">Buscador de Clientes</h4>
                        <div class="form-group mt-3">
                            <label for="ddlClientes">Seleccione un Cliente:</label>
                            <asp:DropDownList ID="ddlClientes" runat="server" CssClass="form-control" AppendDataBoundItems="true">
                                <asp:ListItem Text="-- Seleccione un cliente --" Value="0" />
                            </asp:DropDownList>
                        </div>
                        <div class="form-group mt-4 text-center">
                            <asp:Button ID="btnSeleccionarCliente" runat="server" Text="Continuar" CssClass="btn btn-primary btn-lg" OnClick="btnSeleccionarCliente_Click" />
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>

</asp:Content>