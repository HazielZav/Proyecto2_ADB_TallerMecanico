using Proyecto2_ADB_TallerMecanico.Servicios.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Proyecto2_ADB_TallerMecanico.Servicios
{
    public class OrdenService
    {
        private ConexionBDMSSQL _conexion = new ConexionBDMSSQL();

        public DataTable ListarClientes()
        {
            DataTable dt = new DataTable();
            using (SqlConnection con = _conexion.GetConexion())
            {
                string query = "SELECT idcliente, (nombre + ' ' + apellidopaterno) as Nombre FROM clientes";
                SqlDataAdapter da = new SqlDataAdapter(query, con);
                da.Fill(dt);
            }
            return dt;
        }

        public decimal CalcularIVA(decimal subtotal)
        {
            return subtotal * 0.16m;
        }
    }
}