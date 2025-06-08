using System;

namespace GestiondeVentaZamira.Models
{
    public class Soporte
    {
        public int IdTicket { get; set; }
        public int IdUsuario { get; set; }
        public string Asunto { get; set; }
        public string Descripcion { get; set; }
        public string Estado { get; set; }
        public DateTime FechaCreacion { get; set; }

        public Soporte(int idTicket, int idUsuario, string asunto, string descripcion, string estado, DateTime fechaCreacion)
        {
            IdTicket = idTicket;
            IdUsuario = idUsuario;
            Asunto = asunto;
            Descripcion = descripcion;
            Estado = estado;
            FechaCreacion = fechaCreacion;
        }
    }
}

