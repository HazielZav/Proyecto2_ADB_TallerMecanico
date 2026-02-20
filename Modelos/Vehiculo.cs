using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Proyecto2_ADB_TallerMecanico.Modelos
{
    public class Vehiculo
    {
        public int IdVehiculo { get; set; }
        public string Numero_Serie { get; set; }
        public string Placas { get; set; }
        public string Marca { get; set; }
        public string Modelo { get; set; }
        public int Ano { get; set; }
        public string Color { get; set; }
        public int Kilometraje { get; set; }
        public string Tipo { get; set; }

        public int Antiguedad { get; }
        public int IdCliente { get; set; }
    }
}