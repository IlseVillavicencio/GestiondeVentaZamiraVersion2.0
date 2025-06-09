namespace GestiondeVentaZamira.Models
{
    public class Usuario
    {
        public int IdUsuario { get; set; }
        public string NombreUsuario { get; set; } = string.Empty;
        public string Rol { get; set; } = string.Empty;
        public string Contrasena { get; set; } = string.Empty;
        public string Sucursal { get; set; } = string.Empty;

        // Constructor vacío
        public Usuario() { }

        // Constructor con parámetros
        public Usuario(int idUsuario, string nombreUsuario, string rol, string contrasena, string sucursal)
        {
            IdUsuario = idUsuario;
            NombreUsuario = nombreUsuario;
            Rol = rol;
            Contrasena = contrasena;
            Sucursal = sucursal;
        }
    }
}


