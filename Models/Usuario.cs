using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestiondeVentaZamira.Models
{
    public class Usuario
    {
        public int IdUsuario { get; set; }
        
        public string NombreUsuario = string.Empty;
        public string Contrasena { get; set; } = string.Empty;
        public string Rol { get; set; } = string.Empty;
        public string Sucursal { get; set; } = string.Empty;

        // Constructor vacío
        public Usuario() { }

        // Constructor con parámetros
        public Usuario(int idUsuario, string nombreUsuario, string contrasena, string rol, string sucursal)
        {
            IdUsuario = idUsuario;
            NombreUsuario = nombreUsuario;
            Contrasena = contrasena;
            Rol = rol;
            Sucursal = sucursal;

        }
    }
}

