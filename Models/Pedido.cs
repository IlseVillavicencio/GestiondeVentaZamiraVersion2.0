 using GestiondeVentaZamira.Models;
using System;
using System.Collections.Generic;

namespace GestiondeVentaZamiraa.Models
{
    public class Pedido
    {
        public int IdPedido { get; set; }
        public int IdCliente{ get; set; }
        public DateTime Fecha { get; set; }
        public string Estado { get; set; }
        public double Total { get; set; }
        public List<DetallePedido> Detalles { get; set; }

        public Pedido(int idPedido, int idCliente, DateTime fecha, string estado, double total, List<DetallePedido> detalles)
        {
            IdPedido = idPedido;
            IdCliente = idCliente;
            Fecha = fecha;
            Estado = estado;
            Total = total;
            Detalles = detalles;
        }

        // Constructor sin parámetros opcional (útil para ciertos frameworks o serialización)
        public Pedido()
        {

            Detalles = new List<DetallePedido>();
            Estado = string.Empty;
        }
    }
}

