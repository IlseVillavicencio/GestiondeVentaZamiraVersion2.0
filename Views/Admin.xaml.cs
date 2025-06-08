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

                if (string.IsNullOrEmpty(nombre))
                {
                    MessageBox.Show("El nombre no puede estar vacío.", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                // Validar que no exista un usuario con el mismo ID
                foreach (var usuario in usuarios)
                {
                    if (usuario.IdUsuario == id)
                    {
                        MessageBox.Show("Ya existe un usuario con ese ID.", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                        return;
                    }
                }

                Usuario nuevoUsuario = new Usuario(id, nombre);
                usuarios.Add(nuevoUsuario);

                campoId.Clear();
                campoNombre.Clear();
            }
            catch (FormatException)
            {
                MessageBox.Show("ID inválido, debe ser un número entero.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
