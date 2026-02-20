using MySql.Data.MySqlClient;
using Proyecto2_ADB_TallerMecanico.Modelos;
using Proyecto2_ADB_TallerMecanico.Servicios.Data;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Proyecto2_ADB_TallerMecanico.Servicios
{
    public class ServicioService
    {
        private ConexionBDMSSQL _conexion = new ConexionBDMSSQL();

        public List<Servicio> ListarServicios()
        {
            List<Servicio> servicios = new List<Servicio>();
            using (SqlConnection con = _conexion.GetConexion())
            {
                string query = "SELECT IdServicios, Nombre_Servcio, Descripcion, Costo_Base, Tiempo_Estimado_Hrs FROM Servicios";

                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    new Servicio(
                        
                        );
                }
            }

        }
    }
}