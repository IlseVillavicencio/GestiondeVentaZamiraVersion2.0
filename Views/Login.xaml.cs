using GestiondeVentaZamira.Models;
using MySql.Data.MySqlClient;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace GestiondeVentaZamira.Views
{
    public partial class Login : Window
    {
        private MainWindow? _mainWindow;

        public Login()
        {
            InitializeComponent();
            
        }

        public Login(MainWindow mainWindow) : this()  // Llama al constructor sin parámetros
        {
            _mainWindow = mainWindow;
        }

        private void IniciarSesion_Click(object sender, RoutedEventArgs e)
        {
            string usuario = txtUsuario.Text.Trim();
            string contrasena = txtContrasena.Text.Trim();

            if (usuario == "Usuario") usuario = "";
            if (contrasena == "Contraseña") contrasena = "";

            try
            {
                string connectionString = "server=127.0.0.1;port=3306;user=root;password=12345;database=sistemaventazamira;";
                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    conn.Open();
                    string query = "SELECT * FROM Usuarios WHERE NombreUsuario = @usuario AND Contrasena = @contrasena";
                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@usuario", usuario);
                    cmd.Parameters.AddWithValue("@contrasena", contrasena);

                    MySqlDataReader reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        Usuario u = new Usuario
                        {
                            IdUsuario = Convert.ToInt32(reader["id_usuario"]),
                            NombreUsuario = Convert.ToString(reader["NombreUsuario"]) ?? "",
                            Contrasena = Convert.ToString(reader["Contrasena"]) ?? "",
                            Rol = Convert.ToString(reader["Rol"]) ?? "",
                            Sucursal = Convert.ToString(reader["Sucursal"]) ?? ""
                        };

                        SesionActual.UsuarioActual = u;

                        MessageBox.Show("Inicio de sesión exitoso como: " + u.Rol, "Éxito");


                        Menu VentanaCpnMenu = new Menu(u.Rol); // ✅
                        VentanaCpnMenu.Show();
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show("Credenciales incorrectas.", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error de conexión: " + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void TxtUsuario_GotFocus(object sender, RoutedEventArgs e)
        {
            if (txtUsuario.Text == "Usuario")
            {
                txtUsuario.Text = "";
                txtUsuario.Foreground = Brushes.Black;
            }
        }

        private void TxtUsuario_LostFocus(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtUsuario.Text))
            {
                txtUsuario.Text = "Usuario";
                txtUsuario.Foreground = Brushes.Gray;
            }
        }

        private void TxtContrasena_GotFocus(object sender, RoutedEventArgs e)
        {
            if (txtContrasena.Text == "Contraseña")
            {
                txtContrasena.Text = "";
                txtContrasena.Foreground = Brushes.Black;
            }
        }

        private void TxtContrasena_LostFocus(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtContrasena.Text))
            {
                txtContrasena.Text = "Contraseña";
                txtContrasena.Foreground = Brushes.Gray;
            }
        }
    }
}



