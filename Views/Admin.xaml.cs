using GestiondeVentaZamira.Models;
using System;
using System.Collections.ObjectModel;
using System.Windows;

namespace GestiondeVentaZamira.Views
{
    public partial class Admin : Window
    {
        private ObservableCollection<Usuario> usuarios;

        public Admin()
        {
            InitializeComponent();
            usuarios = new ObservableCollection<Usuario>();
            tablaUsuarios.ItemsSource = usuarios;
        }

        private void btnAgregar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                int id = int.Parse(campoId.Text.Trim());
                string nombre = campoNombre.Text.Trim();
                string correo = campoCorreo.Text.Trim();
                string rol = campoRol.Text.Trim();
                string contrasena = campoContrasena.Password.Trim();

                if (string.IsNullOrEmpty(nombre) || string.IsNullOrEmpty(correo) ||
                    string.IsNullOrEmpty(rol) || string.IsNullOrEmpty(contrasena))
                {
                    MessageBox.Show("Todos los campos deben estar llenos.", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                // Validar que no exista un usuario con el mismo ID
                foreach (var u in usuarios)
                {
                    if (u.IdUsuario == id)
                    {
                        MessageBox.Show("Ya existe un usuario con ese ID.", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                        return;
                    }
                }

                // Crear el nuevo usuario
                Usuario nuevoUsuario = new Usuario(id, nombre, correo, rol, contrasena);
                usuarios.Add(nuevoUsuario);

                // Limpiar campos
                campoId.Clear();
                campoNombre.Clear();
                campoCorreo.Clear();
                campoRol.Clear();
                campoContrasena.Clear();
            }
            catch (FormatException)
            {
                MessageBox.Show("ID inválido. Debe ser un número entero.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error inesperado: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}

