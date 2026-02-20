using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Proyecto2_ADB_TallerMecanico.Modelos
{
    public class Cliente
    {
        public int IdCliente { get; set; }
        public string RFC { get; set; }
        public string Nombre { get; set; } 
        public string ApellidoPaterno { get; set; } 
        public string ApellidoMaterno { get; set; } 
        public string Calle { get; set; }
        public string Numero { get; set; }
        public string Colonia { get; set; }
        public string CodigoPostal { get; set; }
        public string Ciudad { get; set; }
        public string Telefono1 { get; set; }
        public string Telefono2 { get; set; }
        public string Telefono3 { get; set; }
        public string Correo_Electronico { get; set; }
        public DateTime Fecha_Registro { get; }
        public string ClienteInfo
        {
            get
            {
                return RFC + " - " + Nombre + " " + ApellidoPaterno;
            }
        }

    }
}