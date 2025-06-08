using GestiondeVentaZamira.Models;
using System.Collections.Generic;

namespace GestiondeVentaZamiraa.Models
{
    public class UsuarioDAO
    {
        private List<Usuario> usuarios;

        public UsuarioDAO()
        {
            usuarios = new List<Usuario>();
            // Aquí podrías inicializar con datos de prueba o cargar desde BD
        }

        public List<Usuario> GetUsuarios()
        {
            return usuarios;
        }

        public void AgregarUsuario(Usuario usuario)
        {
            usuarios.Add(usuario);
        }

        // Más métodos como actualizar, eliminar, buscar...
    }
}

