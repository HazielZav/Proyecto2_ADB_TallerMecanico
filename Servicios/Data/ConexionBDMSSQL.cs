using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Proyecto2_ADB_TallerMecanico.Servicios.Data
{
    public class ConexionBDMSSQL
    {
        public string GetCadenaConexion()
        {
            return ConfigurationManager.ConnectionStrings["CadenaSQLServer"].ConnectionString;
        }

        public SqlConnection GetConexion()
        {
            SqlConnection conexion = new SqlConnection(GetCadenaConexion());
            conexion.Open();
            return conexion;
        }
    }
}