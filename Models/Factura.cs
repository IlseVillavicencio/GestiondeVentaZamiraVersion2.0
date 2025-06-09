using System;

namespace GestiondeVentaZamira.Models
{
    public class Factura
    {
        public int IdPedido { get; set; }
        public string NombreCliente { get; set; } = string.Empty;
        public DateTime Fecha { get; set; }
        public string Estado { get; set; } = string.Empty;
        public double Total { get; set; }

        public string FechaPedido => Fecha.ToString("yyyy-MM-dd HH:mm");
    }
}



