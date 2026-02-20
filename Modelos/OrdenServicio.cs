using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Proyecto2_ADB_TallerMecanico.Modelos
{
    public class OrdenServicio
    {
        public int Folio { get; set; } 
        public DateTime Fecha_Ingreso { get; set; } 
        public DateTime Fecha_Entrega_Estimada { get; set; }
        public DateTime Fecha_Ingreso_Real { get; set; }
        public string Estado {  get; set; }
        public decimal Subtotal { get; set; }
        public decimal IVA { get; set; } 
        public decimal Total { get; set; }
        public int IdVehiculo { get; set; }
        public List<DetalleOrdenServicio> Detalles { get; set; }
    }
}