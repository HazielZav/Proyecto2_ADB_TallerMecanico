<%@ Page Title="Historial del Cliente" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="HistorialOrdenes.aspx.cs" Inherits="Proyecto2_ADB_TallerMecanico.Presentacion.HistorialOrdenes" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container mt-4">
        <div class="row mb-4">
            <div class="col-md-8">
                <h2>Historial de Órdenes</h2>
                <h4 class="text-muted">
                    <asp:Label ID="lblNombreCliente" runat="server" Text="Cargando cliente..."></asp:Label>
                </h4>
            </div>
            <div class="col-md-4 text-end my-auto">
                <asp:Button ID="btnNuevaOrden" runat="server" Text="+ Crear Nueva Orden" CssClass="btn btn-success btn-lg" OnClick="btnNuevaOrden_Click" />
                <asp:Button ID="btnVolver" runat="server" Text="Volver al Inicio" CssClass="btn btn-secondary btn-lg ms-2" OnClick="btnVolver_Click" />
            </div>
        </div>

        <div class="card shadow-sm">
            <div class="card-body">
                <asp:GridView ID="gvOrdenes" runat="server" AutoGenerateColumns="False" 
                    CssClass="table table-striped table-hover border-0" GridLines="None" EmptyDataText="Este cliente aún no tiene órdenes de servicio registradas.">
                    <Columns>
                        <asp:BoundField DataField="Folio" HeaderText="Folio" />
                        <asp:BoundField DataField="Fecha_Ingreso" HeaderText="Fecha Ingreso" DataFormatString="{0:dd/MM/yyyy}" />
                        <asp:BoundField DataField="IdVehiculo" HeaderText="ID Vehículo" />
                        <asp:BoundField DataField="Estado" HeaderText="Estado" />
                        <asp:BoundField DataField="Total" HeaderText="Costo Total" DataFormatString="{0:C2}" />
                    </Columns>
                    <HeaderStyle CssClass="table-dark" />
                </asp:GridView>
            </div>
        </div>
    </div>
</asp:Content>