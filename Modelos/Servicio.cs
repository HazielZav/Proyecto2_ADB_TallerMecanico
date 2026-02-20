using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Proyecto2_ADB_TallerMecanico.Modelos
{
    public class Servicio
    {
        int IdServicios { get; set; }
        string Nombre_Servcio { get; set; }
        string Descripcion { get; set; }
        decimal Costo_Base { get; set; }
        decimal Tiempo_Estimado_Hrs { get; set; }
    }
}