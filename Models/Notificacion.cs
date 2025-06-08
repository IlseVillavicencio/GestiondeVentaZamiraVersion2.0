using System;

namespace GestiondeVentaZamira.Models
{
    public class Notificacion
    {
        public int IdNotificacion { get; set; }
        public int IdUsuario { get; set; }
        public string Mensaje { get; set; }
        public DateTime Fecha { get; set; }
        public bool Leida { get; set; }

        public override string ToString()
        {
            return $"{(Leida ? "[Leída]" : "[Nueva]")} {Fecha:g} - {Mensaje}";
        }

        public Notificacion(int idNotificacion, int idUsuario, string mensaje, DateTime fecha, bool leida)
        {
            IdNotificacion = idNotificacion;
            IdUsuario = idUsuario;
            Mensaje = mensaje;
            Fecha = fecha;
            Leida = leida;
        }
    }
}

