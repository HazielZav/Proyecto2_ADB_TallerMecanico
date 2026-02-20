using Proyecto2_ADB_TallerMecanico.Servicios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Proyecto2_ADB_TallerMecanico
{
    public partial class _Default : Page
    {
        private OrdenService _ordenService = new OrdenService();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CargarClientes();
            }
        }

        private void CargarClientes()
        {
            try
            {
                var dtClientes = _ordenService.ObtenerTodosLosClientes();

                ddlClientes.DataSource = dtClientes;
                ddlClientes.DataTextField = "ClienteInfo"; 
                ddlClientes.DataValueField = "IdCliente"; 
                ddlClientes.DataBind();
            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('Error al cargar clientes: " + ex.Message + "');</script>");
            }
        }

        protected void btnSeleccionarCliente_Click(object sender, EventArgs e)
        {
            int idClienteSeleccionado = Convert.ToInt32(ddlClientes.SelectedValue);

            if (idClienteSeleccionado > 0)
            {
                Response.Redirect($"~/Presentacion/HistorialOrdenes.aspx?idCliente={idClienteSeleccionado}");
            }
            else
            {
                Response.Write("<script>alert('Por favor, seleccione un cliente válido.');</script>");
            }
        }
    }
}