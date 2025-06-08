using System;

namespace GestiondeVentaZamira.Models
{
    public class Envio
    {
        public int IdEnvio { get; set; }
        public int IdPedido { get; set; }
        public string MetodoEntrega { get; set; }
        public string DireccionEntrega { get; set; }
        public string Estado { get; set; }
        public DateTime FechaEstimada { get; set; }

        public Envio(int idEnvio, int idPedido, string metodoEntrega, string direccionEntrega, string estado, DateTime fechaEstimada)
        {
            IdEnvio = idEnvio;
            IdPedido = idPedido;
            MetodoEntrega = metodoEntrega;
            DireccionEntrega = direccionEntrega;
            Estado = estado;
            FechaEstimada = fechaEstimada;
        }
    }
}

