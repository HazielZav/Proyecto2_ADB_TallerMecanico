<%@ Page Title="Nueva Orden de Servicio" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="RegistroOrden.aspx.cs" Inherits="Proyecto2_ADB_TallerMecanico.Presentacion.RegistroOrden" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container mt-4">
        
        <div class="card shadow-sm border-primary">
            <div class="card-header bg-primary text-white text-center">
                <h3>Registro de Órdenes de Servicio - Sir. Ferdnz</h3>
            </div>
            
            <div class="card-body">
                <div class="row bg-light p-3 mb-3 rounded border">
                    <div class="col-md-6">
                        <h5 class="text-primary">Datos del Cliente</h5>
                        <p class="mb-1"><strong>Cliente:</strong> <asp:Label ID="lblNombreCliente" runat="server" Text=""></asp:Label></p>
                        <p class="mb-1"><strong>RFC:</strong> <asp:Label ID="lblRFC" runat="server" Text=""></asp:Label></p>
                    </div>
                    <div class="col-md-6 text-end">
                        <h5 class="text-primary">Detalles de la Orden</h5>
                        <p class="mb-1"><strong>Folio:</strong> <span class="badge bg-secondary fs-6">AUTO</span></p>
                        <p class="mb-1"><strong>Fecha:</strong> <asp:Label ID="lblFecha" runat="server" Text=""></asp:Label></p>
                    </div>
                </div>

                <div class="row mb-4">
                    <div class="col-md-5">
                        <label class="form-label fw-bold">Seleccione un Vehículo:</label>
                        <asp:DropDownList ID="ddlVehiculos" runat="server" CssClass="form-control border-primary">
                        </asp:DropDownList>
                    </div>
                    
                    <div class="col-md-7 border-start">
                        <label class="form-label fw-bold">Agregar Servicio:</label>
                        <div class="input-group">
                            <asp:DropDownList ID="ddlServicios" runat="server" CssClass="form-control">
                            </asp:DropDownList>
                            <span class="input-group-text">Cant:</span>
                            <asp:TextBox ID="txtCantidad" runat="server" CssClass="form-control text-center" TextMode="Number" Text="1" min="1" Width="80px"></asp:TextBox>
                            <asp:Button ID="btnAgregarServicio" runat="server" Text="Agregar" CssClass="btn btn-outline-primary" OnClick="btnAgregarServicio_Click" />
                        </div>
                    </div>
                </div>

                <asp:GridView ID="gvDetalles" runat="server" AutoGenerateColumns="False" 
                    CssClass="table table-bordered table-striped text-center align-middle" 
                    EmptyDataText="No hay servicios agregados a la orden."
                    OnRowCommand="gvDetalles_RowCommand"> <Columns>
                        <asp:BoundField DataField="IdServicio" HeaderText="ID SERVICIO" />
                        <asp:BoundField DataField="NombreServicio" HeaderText="DESCRIPCIÓN" ItemStyle-CssClass="text-start" />
                        <asp:BoundField DataField="Cantidad" HeaderText="CANTIDAD" />
                        <asp:BoundField DataField="Precio" HeaderText="PRECIO" DataFormatString="{0:C2}" />
                        <asp:BoundField DataField="Importe" HeaderText="IMPORTE" DataFormatString="{0:C2}" />
        
                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:Button ID="btnEliminar" runat="server" Text="X" CssClass="btn btn-danger btn-sm fw-bold" 
                                    CommandName="EliminarServicio" CommandArgument='<%# Container.DataItemIndex %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                    <HeaderStyle CssClass="table-dark" />
                </asp:GridView>

                <div class="row mt-4">
                    <div class="col-md-4 offset-md-8 text-end">
                        <table class="table table-sm table-borderless">
                            <tr>
                                <th>SUBTOTAL:</th>
                                <td><asp:Label ID="lblSubtotal" runat="server" Text="$0.00"></asp:Label></td>
                            </tr>
                            <tr>
                                <th>IVA (16%):</th>
                                <td><asp:Label ID="lblIVA" runat="server" Text="$0.00"></asp:Label></td>
                            </tr>
                            <tr class="fs-4 text-success border-top">
                                <th>TOTAL:</th>
                                <td><strong><asp:Label ID="lblTotal" runat="server" Text="$0.00"></asp:Label></strong></td>
                            </tr>
                        </table>
                    </div>
                </div>
            </div>
            
            <div class="card-footer text-center bg-white p-3">
                <asp:Button ID="btnGuardarOrden" runat="server" Text="Generar y Guardar Orden" CssClass="btn btn-success btn-lg px-5" OnClick="btnGuardarOrden_Click" />
                <asp:Button ID="btnCancelar" runat="server" Text="Cancelar" CssClass="btn btn-danger btn-lg ms-3" OnClick="btnCancelar_Click" />
            </div>
        </div>
    </div>
</asp:Content>