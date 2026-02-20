using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Proyecto2_ADB_TallerMecanico.Modelos
{
    public class Vehiculos
    {
        int IdVehiculo { get; set; }
        string Numero_Serie { get; set; }
        string Placas { get; set; }
        string Marca { get; set; }
        string Modelo { get; set; }
        int Ano { get; set; }
        string Color { get; set; }
        int Kilometraje { get; set; }
        string Tipo { get; set; }
        int IdCliente { get; set; }

        int Antiguedad
        {
            get
            {
                return DateTime.Now.Year - Ano;
            }
        }
    }
}