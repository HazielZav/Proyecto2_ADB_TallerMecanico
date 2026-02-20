using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Proyecto2_ADB_TallerMecanico.Modelos
{
    public class Servicio
    {
        public int IdServicios { get; set; }
        public string Nombre_Servcio { get; set; }
        public string Descripcion { get; set; }
        public decimal Costo_Base { get; set; }
        public  decimal Tiempo_Estimado_Hrs { get; set; }
    }
}