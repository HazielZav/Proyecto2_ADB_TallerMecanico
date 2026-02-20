using Proyecto2_ADB_TallerMecanico.Modelos;
using Proyecto2_ADB_TallerMecanico.Servicios.Data;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Proyecto2_ADB_TallerMecanico.Servicios
{
    public class DetalleOrdenServicioService
    {
        private ConexionBDMSSQL _conexion = new ConexionBDMSSQL();

        public List<DetalleOrdenServicio> ObtenerDetallesPorFolio(int folio)
        {
            List<DetalleOrdenServicio> lista = new List<DetalleOrdenServicio>();
            using (SqlConnection con = _conexion.GetConexion())
            {
                string query = @"SELECT d.idservicio, s.nombre_servicio, d.cantidad, d.precio_aplicado 
                             FROM detalle_orden_servicios d
                             INNER JOIN servicios s ON d.idservicio = s.idservicio
                             WHERE d.folio_orden = @Folio";

                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@Folio", folio);
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            lista.Add(new DetalleOrdenServicio
                            {
                                IdServicio = Convert.ToInt32(reader["idservicio"]),
                                NombreServicio = reader["nombre_servicio"].ToString(),
                                Cantidad = Convert.ToInt32(reader["cantidad"]),
                                Precio = Convert.ToDecimal(reader["precio_aplicado"])
                            });
                        }
                    }
                }
            }
            return lista;
        }
    }
}