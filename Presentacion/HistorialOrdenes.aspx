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
                    CssClass="table table-striped table-hover border-0 align-middle text-center" GridLines="None" 
                    EmptyDataText="Este cliente aún no tiene órdenes de servicio registradas."
                    OnRowCommand="gvOrdenes_RowCommand">
                    <Columns>
                        <asp:BoundField DataField="Folio" HeaderText="Folio" />
                        <asp:BoundField DataField="Fecha_Ingreso" HeaderText="Fecha Ingreso" DataFormatString="{0:dd/MM/yyyy}" />
                        <asp:BoundField DataField="IdVehiculo" HeaderText="ID Vehículo" />
                        <asp:BoundField DataField="Estado" HeaderText="Estado" />
                        <asp:BoundField DataField="Total" HeaderText="Costo Total" DataFormatString="{0:C2}" />
                        
                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:Button ID="btnVer" runat="server" Text="Ver Detalles" CssClass="btn btn-info btn-sm text-white" 
                                    CommandName="VerDetalles" CommandArgument='<%# Eval("Folio") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                    <HeaderStyle CssClass="table-dark" />
                </asp:GridView>
            </div>
        </div>

        <asp:Panel ID="pnlDetalles" runat="server" Visible="false" CssClass="card shadow-sm mt-4 border-info">
            <div class="card-header bg-info text-white d-flex justify-content-between align-items-center">
                <h4 class="mb-0">Detalles del Folio: <asp:Label ID="lblFolioSeleccionado" runat="server"></asp:Label></h4>
                <asp:Button ID="btnCerrarDetalles" runat="server" Text="X" CssClass="btn btn-danger btn-sm" OnClick="btnCerrarDetalles_Click" />
            </div>
            <div class="card-body">
                <asp:GridView ID="gvDetallesOrden" runat="server" AutoGenerateColumns="False" 
                    CssClass="table table-bordered table-sm text-center mb-0">
                    <Columns>
                        <asp:BoundField DataField="NombreServicio" HeaderText="Servicio Aplicado" ItemStyle-CssClass="text-start" />
                        <asp:BoundField DataField="Cantidad" HeaderText="Cantidad" />
                        <asp:BoundField DataField="Precio" HeaderText="Precio Unitario" DataFormatString="{0:C2}" />
                        <asp:BoundField DataField="Importe" HeaderText="Importe" DataFormatString="{0:C2}" />
                    </Columns>
                    <HeaderStyle CssClass="table-light" />
                </asp:GridView>
            </div>
        </asp:Panel>
    </div>
</asp:Content>