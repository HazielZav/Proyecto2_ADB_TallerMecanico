using Proyecto2_ADB_TallerMecanico.Modelos;
using Proyecto2_ADB_TallerMecanico.Servicios.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;

namespace Proyecto2_ADB_TallerMecanico.Servicios
{
    public class OrdenService
    {
        private ConexionBDMSSQL _conexion = new ConexionBDMSSQL();

        public bool AgregarOrden(OrdenServicio orden)
        {
            using (SqlConnection con = _conexion.GetConexion())
            {
                using (SqlTransaction trans = con.BeginTransaction())
                {
                    try
                    {
                        string queryOrden = @"INSERT INTO ordenes_servicio (fecha_entrega_estimada, estado, costo_total, idvehiculo) 
                                      VALUES (@fechaEst, @estado, @costoTotal, @idVehiculo);
                                      SELECT SCOPE_IDENTITY();";

                        int nuevoFolio;
                        using (SqlCommand cmdOrden = new SqlCommand(queryOrden, con, trans))
                        {
                            cmdOrden.Parameters.AddWithValue("@fechaEst", orden.Fecha_Entrega_Estimada);
                            cmdOrden.Parameters.AddWithValue("@estado", "Abierta");
                            cmdOrden.Parameters.AddWithValue("@costoTotal", orden.Total);
                            cmdOrden.Parameters.AddWithValue("@idVehiculo", orden.IdVehiculo);

                            // SCOPE_IDENTITY regresa un decimal, lo convertimos a int
                            nuevoFolio = Convert.ToInt32(cmdOrden.ExecuteScalar());
                        }

                        // 2. Insertar los Servicios asociados a esta orden
                        if (orden.Detalles != null && orden.Detalles.Count > 0)
                        {
                            string queryServicio = @"INSERT INTO detalle_orden_servicios (folio_orden, idservicio, cantidad, precio_aplicado) 
                                             VALUES (@folio, @idServicio, @cantidad, @precio)";

                            foreach (var detalle in orden.Detalles)
                            {
                                using (SqlCommand cmdServ = new SqlCommand(queryServicio, con, trans))
                                {
                                    cmdServ.Parameters.AddWithValue("@folio", nuevoFolio);
                                    cmdServ.Parameters.AddWithValue("@idServicio", detalle.IdServicio);
                                    cmdServ.Parameters.AddWithValue("@cantidad", detalle.Cantidad);
                                    cmdServ.Parameters.AddWithValue("@precio", detalle.Precio);
                                    cmdServ.ExecuteNonQuery();
                                }
                            }
                        }

                        trans.Commit();
                        return true;
                    }
                    catch (Exception ex)
                    {
                        trans.Rollback();
                        return false;
                    }
                }
            }
        }

        public List<OrdenServicio> ObtenerTodasLasOrdenes()
        {
            List<OrdenServicio> lista = new List<OrdenServicio>();
            using (SqlConnection con = _conexion.GetConexion())
            {
                string query = "SELECT * FROM ordenes_servicio ORDER BY fecha_ingreso DESC";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            lista.Add(new OrdenServicio
                            {
                                Folio = Convert.ToInt32(reader["folio"]),
                                Fecha_Ingreso = Convert.ToDateTime(reader["fecha_ingreso"]),
                                Fecha_Entrega_Estimada = reader["fecha_entrega_estimada"] != DBNull.Value ? Convert.ToDateTime(reader["fecha_entrega_estimada"]) : DateTime.MinValue,
                                Estado = reader["estado"].ToString(),
                                Total = reader["costo_total"] != DBNull.Value ? Convert.ToDecimal(reader["costo_total"]) : 0,
                                IdVehiculo = Convert.ToInt32(reader["idvehiculo"])
                            });
                        }
                    }
                }
            }
            return lista;
        }

        public List<Cliente> ObtenerTodosLosClientes()
        {
            List<Cliente> lista = new List<Cliente>();
            using (SqlConnection con = _conexion.GetConexion())
            {
                string query = "SELECT * FROM clientes";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            lista.Add(new Cliente
                            {
                                IdCliente = Convert.ToInt32(reader["idcliente"]),
                                RFC = reader["rfc"].ToString(),
                                Nombre = reader["nombre"].ToString(),
                                ApellidoPaterno = reader["apellidopaterno"].ToString(),
                                ApellidoMaterno = reader["apellidomaterno"].ToString(),
                                Calle = reader["calle"].ToString(),
                                Numero = reader["numero"].ToString(),
                                Colonia = reader["colonia"].ToString(),
                                CodigoPostal = reader["codigopostal"].ToString(),
                                Ciudad = reader["ciudad"].ToString(),
                                Telefono1 = reader["telefono1"].ToString(),
                                Telefono2 = reader["telefono2"].ToString(),
                                Telefono3 = reader["telefono3"].ToString(),
                                Correo_Electronico = reader["correo_electronico"].ToString()
                            });
                        }
                    }
                }
            }
            return lista;
        }

        public List<OrdenServicio> ObtenerOrdenesPorCliente(int idCliente)
        {
            List<OrdenServicio> lista = new List<OrdenServicio>();
            using (SqlConnection con = _conexion.GetConexion())
            {
                // Join con vehículos para saber de qué cliente es la orden
                string query = @"SELECT o.* FROM ordenes_servicio o
                             INNER JOIN vehiculos v ON o.idvehiculo = v.idvehiculo
                             WHERE v.idcliente = @IdCliente";

                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@IdCliente", idCliente);
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            lista.Add(new OrdenServicio
                            {
                                Folio = Convert.ToInt32(reader["folio"]),
                                Fecha_Ingreso = Convert.ToDateTime(reader["fecha_ingreso"]),
                                Estado = reader["estado"].ToString(),
                                Total = reader["costo_total"] != DBNull.Value ? Convert.ToDecimal(reader["costo_total"]) : 0,
                                IdVehiculo = Convert.ToInt32(reader["idvehiculo"])
                            });
                        }
                    }
                }
            }
            return lista;
        }

        public List<Vehiculo> ObtenerVehiculosPorCliente(int idCliente)
        {
            List<Vehiculo> lista = new List<Vehiculo>();
            using (SqlConnection con = _conexion.GetConexion())
            {
                string query = "SELECT * FROM vehiculos WHERE idcliente = @IdCliente";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@IdCliente", idCliente);
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            lista.Add(new Vehiculo
                            {
                                IdVehiculo = Convert.ToInt32(reader["idvehiculo"]),
                                Numero_Serie = reader["numero_serie"].ToString(),
                                Placas = reader["placas"].ToString(),
                                Marca = reader["marca"].ToString(),
                                Modelo = reader["modelo"].ToString(),
                                Ano = Convert.ToInt32(reader["ano"]),
                                Color = reader["color"].ToString(),
                                Kilometraje = Convert.ToInt32(reader["kilometraje"]),
                                Tipo = reader["tipo"].ToString(),
                                IdCliente = Convert.ToInt32(reader["idcliente"])
                            });
                        }
                    }
                }
            }
            return lista;
        }

        public List<Servicio> ObtenerTodosLosServicios()
        {
            List<Servicio> lista = new List<Servicio>();
            using (SqlConnection con = _conexion.GetConexion())
            {
                string query = "SELECT * FROM servicios";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            lista.Add(new Servicio
                            {
                                IdServicios = Convert.ToInt32(reader["idservicio"]),
                                Nombre_Servcio = reader["nombre_servicio"].ToString(),
                                Descripcion = reader["descripcion"].ToString(),
                                Costo_Base = Convert.ToDecimal(reader["costo_base"]),
                                Tiempo_Estimado_Hrs = Convert.ToDecimal(reader["tiempo_estimado_hrs"])
                            });
                        }
                    }
                }
            }
            return lista;
        }
        public decimal CalcularIVA(decimal subtotal)
        {
            return subtotal * 0.16m;
        }
    }
}