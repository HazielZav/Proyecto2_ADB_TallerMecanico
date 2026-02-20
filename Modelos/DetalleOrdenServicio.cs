using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Proyecto2_ADB_TallerMecanico.Modelos
{
    public class DetalleOrdenServicio
    {
        public int IdServicio { get; set; }
        public string NombreServicio { get; set; }
        public int Cantidad { get; set; } 
        public decimal Precio { get; set; } 

        public decimal Importe
        {
            get { return Cantidad * Precio; }
        }
    }
}