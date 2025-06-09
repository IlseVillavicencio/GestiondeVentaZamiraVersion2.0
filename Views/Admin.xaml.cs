using GestiondeVentaZamira.Models;
using MySql.Data.MySqlClient;
using System;
using System.Collections.ObjectModel;
using System.Windows;

namespace GestiondeVentaZamira.Views
{
    public partial class Admin : Window
    {
        private ObservableCollection<Usuario> usuarios;
        private string connectionString = "server=127.0.0.1;port=3306;user=root;password=12345;database=sistemaventazamira;";

        public Admin()
        {
            InitializeComponent();
            usuarios = new ObservableCollection<Usuario>();
            tablaUsuarios.ItemsSource = usuarios;
            CargarUsuariosDesdeBD();
        }

        private void btnAgregar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (!int.TryParse(campoId.Text.Trim(), out int id))
                {
                    MessageBox.Show("ID inválido. Debe ser un número entero.", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                string nombre = campoNombre.Text.Trim();
                string rol = campoRol.Text.Trim();
                string contrasena = campoContrasena.Password.Trim();
                string sucursal = campoSucursal.Text.Trim();

                if (string.IsNullOrWhiteSpace(nombre) || string.IsNullOrWhiteSpace(rol) ||
                    string.IsNullOrWhiteSpace(contrasena) ||
                    (rol != "GerenteGeneral" && string.IsNullOrWhiteSpace(sucursal)))
                {
                    MessageBox.Show("Todos los campos deben estar llenos (Sucursal es obligatoria excepto para GerenteGeneral).", "Advertencia", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    connection.Open();
                    using (MySqlTransaction transaction = connection.BeginTransaction())
                    {
                        try
                        {
                            string query = @"
                                INSERT INTO USUARIOS (id_usuario, NombreUsuario, Contrasena, Rol, Sucursal) 
                                VALUES (@id, @nombre, @contrasena, @rol, @sucursal);";

                            using (MySqlCommand cmd = new MySqlCommand(query, connection, transaction))
                            {
                                cmd.Parameters.AddWithValue("@id", id);
                                cmd.Parameters.AddWithValue("@nombre", nombre);
                                cmd.Parameters.AddWithValue("@contrasena", contrasena); // Reemplaza esto con hash en producción
                                cmd.Parameters.AddWithValue("@rol", rol);
                                cmd.Parameters.AddWithValue("@sucursal", string.IsNullOrEmpty(sucursal) ? DBNull.Value : (object)sucursal);

                                cmd.ExecuteNonQuery();
                            }

                            transaction.Commit();

                            // Actualiza la vista
                            Usuario nuevoUsuario = new Usuario(id, nombre, rol, contrasena, sucursal);
                            usuarios.Add(nuevoUsuario);

                            campoId.Clear();
                            campoNombre.Clear();
                            campoRol.Clear();
                            campoContrasena.Clear();
                            campoSucursal.Clear();

                            MessageBox.Show("Usuario insertado correctamente.", "Éxito", MessageBoxButton.OK, MessageBoxImage.Information);
                        }
                        catch (Exception ex)
                        {
                            transaction.Rollback();
                            MessageBox.Show($"Error al insertar usuario. Se revirtió la operación.\nDetalles: {ex.Message}", "Error SQL", MessageBoxButton.OK, MessageBoxImage.Error);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error inesperado: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void CargarUsuariosDesdeBD()
        {
            usuarios.Clear();
            try
            {
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    connection.Open();
                    string query = "SELECT id_usuario, NombreUsuario, Contrasena, Rol, Sucursal FROM USUARIOS";

                    using (MySqlCommand cmd = new MySqlCommand(query, connection))
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Usuario u = new Usuario
                            {
                                IdUsuario = reader.GetInt32("id_usuario"),
                                NombreUsuario = reader.GetString("NombreUsuario"),
                                Contrasena = reader.GetString("Contrasena"),
                                Rol = reader.GetString("Rol"),
                                Sucursal = reader.IsDBNull(reader.GetOrdinal("Sucursal")) ? "" : reader.GetString("Sucursal")
                            };
                            usuarios.Add(u);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar usuarios: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}



