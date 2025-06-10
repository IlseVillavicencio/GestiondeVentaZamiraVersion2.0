using System;
using System.Windows;
using MySql.Data.MySqlClient;

namespace GestiondeVentaZamira.Views
{
    public partial class Producto : Window
    {
        // ✅ Cadena de conexión (ajusta según tu configuración)
        private string connectionString = "server=yamabiko.proxy.rlwy.net;port=34163;user=root;password=sFrdysrDfZtahYVhsdyzhNsKECijredS;database=railway;";

        public Producto()
        {
            InitializeComponent();
        }

        private void GuardarProducto_Click(object sender, RoutedEventArgs e)
        {
            string nombre = NombreProductoTextBox.Text.Trim();
            string precioTexto = PrecioProductoTextBox.Text.Trim();
            string descripcion = DescripcionProductoTextBox.Text.Trim();

            // ✅ Validaciones
            if (string.IsNullOrEmpty(nombre))
            {
                MessageBox.Show("Por favor, ingresa el nombre del producto.", "Validación", MessageBoxButton.OK, MessageBoxImage.Warning);
                NombreProductoTextBox.Focus();
                return;
            }

            if (!decimal.TryParse(precioTexto, out decimal precio) || precio < 0)
            {
                MessageBox.Show("Por favor, ingresa un precio válido y positivo.", "Validación", MessageBoxButton.OK, MessageBoxImage.Warning);
                PrecioProductoTextBox.Focus();
                return;
            }

            // ✅ Guardar producto en la base de datos
            try
            {
                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    conn.Open();

                    string sql = "INSERT INTO PRODUCTO (nombre, precio, descripcion, stock) VALUES (@nombre, @precio, @descripcion, @stock)";
                    using (MySqlCommand cmd = new MySqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@nombre", nombre);
                        cmd.Parameters.AddWithValue("@precio", precio);
                        cmd.Parameters.AddWithValue("@descripcion", descripcion);
                        cmd.Parameters.AddWithValue("@stock", 0); // Puedes cambiarlo si luego agregas campo para stock

                        cmd.ExecuteNonQuery();
                    }

                    MessageBox.Show($"Producto '{nombre}' guardado correctamente.\nPrecio: {precio:C}\nDescripción: {descripcion}",
                        "Producto Guardado", MessageBoxButton.OK, MessageBoxImage.Information);

                    // Limpiar los campos
                    NombreProductoTextBox.Clear();
                    PrecioProductoTextBox.Clear();
                    DescripcionProductoTextBox.Clear();
                    NombreProductoTextBox.Focus();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al guardar el producto: " + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void NombreProductoTextBox_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            // Puedes usar esto para validaciones en tiempo real si deseas
        }
    }
}


