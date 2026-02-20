using Proyecto2_ADB_TallerMecanico.Modelos;
using Proyecto2_ADB_TallerMecanico.Servicios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Proyecto2_ADB_TallerMecanico.Presentacion
{
    public partial class RegistroOrden : Page
    {
        private OrdenService _ordenService = new OrdenService();

        // Propiedad para manejar la lista de detalles temporalmente en Sesión.NO TOCAR YA FUNCIONA
        private List<DetalleOrdenServicio> Carrito
        {
            get
            {
                if (Session["CarritoDetalles"] == null)
                    Session["CarritoDetalles"] = new List<DetalleOrdenServicio>();
                return (List<DetalleOrdenServicio>)Session["CarritoDetalles"];
            }
            set
            {
                Session["CarritoDetalles"] = value;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Carrito = new List<DetalleOrdenServicio>();

                if (Request.QueryString["idCliente"] != null)
                {
                    int idCliente = Convert.ToInt32(Request.QueryString["idCliente"]);
                    CargarDatosCliente(idCliente);
                    CargarVehiculos(idCliente);
                    CargarServicios();

                    lblFecha.Text = DateTime.Now.ToString("dd/MM/yyyy");
                }
                else
                {
                    Response.Redirect("~/Default.aspx");
                }
            }
        }

        private void CargarDatosCliente(int idCliente)
        {
            var clientes = _ordenService.ObtenerTodosLosClientes();
            var clienteInfo = clientes.FirstOrDefault(c => c.IdCliente == idCliente);

            if (clienteInfo != null)
            {
                lblNombreCliente.Text = $"{clienteInfo.Nombre} {clienteInfo.ApellidoPaterno} {clienteInfo.ApellidoMaterno}";
                lblRFC.Text = clienteInfo.RFC;
            }
        }

        private void CargarVehiculos(int idCliente)
        {
            var vehiculos = _ordenService.ObtenerVehiculosPorCliente(idCliente);
            ddlVehiculos.DataSource = vehiculos;
            ddlVehiculos.DataTextField = "Placas";
            ddlVehiculos.DataValueField = "IdVehiculo";
            ddlVehiculos.DataBind();
        }

        private void CargarServicios()
        {
            var servicios = _ordenService.ObtenerTodosLosServicios();
            ddlServicios.DataSource = servicios;
            ddlServicios.DataTextField = "Nombre_Servcio";
            ddlServicios.DataValueField = "IdServicios";
            ddlServicios.DataBind();
        }

        protected void btnAgregarServicio_Click(object sender, EventArgs e)
        {
            if (ddlServicios.SelectedValue != "")
            {
                int idServicio = Convert.ToInt32(ddlServicios.SelectedValue);
                int cantidad = Convert.ToInt32(txtCantidad.Text);

                var servicioOriginal = _ordenService.ObtenerTodosLosServicios().FirstOrDefault(s => s.IdServicios == idServicio);

                if (servicioOriginal != null)
                {
                    var listaActual = Carrito;

                    listaActual.Add(new DetalleOrdenServicio
                    {
                        IdServicio = servicioOriginal.IdServicios,
                        NombreServicio = servicioOriginal.Nombre_Servcio,
                        Cantidad = cantidad,
                        Precio = servicioOriginal.Costo_Base
                    });

                    Carrito = listaActual;
                    ActualizarGridYTotales();
                }
            }
        }

        private void ActualizarGridYTotales()
        {
            var lista = Carrito;

            // Refrescar la tabla visual
            gvDetalles.DataSource = lista;
            gvDetalles.DataBind();

            decimal subtotal = lista.Sum(x => x.Importe);
            decimal iva = subtotal * 0.16m;
            decimal total = subtotal + iva;

            // Formato de moneda nacional (MXN)
            lblSubtotal.Text = subtotal.ToString("C2");
            lblIVA.Text = iva.ToString("C2");
            lblTotal.Text = total.ToString("C2");
        }

        protected void btnGuardarOrden_Click(object sender, EventArgs e)
        {
            var listaDetalles = Carrito;

            if (listaDetalles.Count == 0)
            {
                Response.Write("<script>alert('Debe agregar al menos un servicio a la orden.');</script>");
                return;
            }

            if (ddlVehiculos.SelectedValue == "")
            {
                Response.Write("<script>alert('El cliente debe tener un vehículo registrado.');</script>");
                return;
            }

            decimal subtotal = listaDetalles.Sum(x => x.Importe);
            decimal totalFinal = subtotal + (subtotal * 0.16m);

            OrdenServicio nuevaOrden = new OrdenServicio
            {
                Fecha_Entrega_Estimada = DateTime.Now.AddDays(2), // Por defecto 2 días después
                IdVehiculo = Convert.ToInt32(ddlVehiculos.SelectedValue),
                Total = totalFinal,
                Detalles = listaDetalles
            };

            bool exito = _ordenService.AgregarOrden(nuevaOrden);

            if (exito)
            {
                Session.Remove("CarritoDetalles");
                Response.Redirect($"~/Presentacion/HistorialOrdenes.aspx?idCliente={Request.QueryString["idCliente"]}");
            }
            else
            {
                Response.Write("<script>alert('Ocurrió un error al guardar la orden en la base de datos.');</script>");
            }
        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            Session.Remove("CarritoDetalles");
            Response.Redirect($"~/Presentacion/HistorialOrdenes.aspx?idCliente={Request.QueryString["idCliente"]}");
        }
    }
}