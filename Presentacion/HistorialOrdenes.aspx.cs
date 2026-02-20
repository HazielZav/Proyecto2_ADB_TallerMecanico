using Proyecto2_ADB_TallerMecanico.Servicios;
using Proyecto2_ADB_TallerMecanico.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Proyecto2_ADB_TallerMecanico.Presentacion;

namespace Proyecto2_ADB_TallerMecanico.Presentacion
{
    public partial class HistorialOrdenes : Page
    {
        private OrdenService _ordenService = new OrdenService();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // Verificamos que traiga un ID en la URL
                if (Request.QueryString["idCliente"] != null)
                {
                    int idCliente = Convert.ToInt32(Request.QueryString["idCliente"]);
                    CargarDatosCliente(idCliente);
                    CargarHistorial(idCliente);
                }
                else
                {
                    // Si entró directo sin elegir cliente, lo regresamos
                    Response.Redirect("Default.aspx");
                }
            }
        }

        private void CargarDatosCliente(int idCliente)
        {
            var clientes = _ordenService.ObtenerTodosLosClientes();
            var clienteInfo = clientes.FirstOrDefault(c => c.IdCliente == idCliente);

            if (clienteInfo != null)
            {
                lblNombreCliente.Text = $"Cliente: {clienteInfo.Nombre} {clienteInfo.ApellidoPaterno} | RFC: {clienteInfo.RFC}";
            }
        }

        private void CargarHistorial(int idCliente)
        {
            try
            {
                var historial = _ordenService.ObtenerOrdenesPorCliente(idCliente);
                gvOrdenes.DataSource = historial;
                gvOrdenes.DataBind();
            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('Error al cargar historial: " + ex.Message + "');</script>");
            }
        }

        protected void btnNuevaOrden_Click(object sender, EventArgs e)
        {
            // Pasamos el idCliente a la pantalla de Registro de Orden
            string idCliente = Request.QueryString["idCliente"];
            Response.Redirect($"RegistroOrden.aspx?idCliente={idCliente}");
        }

        protected void btnVolver_Click(object sender, EventArgs e)
        {
            Response.Redirect("Default.aspx");
        }
    }
}