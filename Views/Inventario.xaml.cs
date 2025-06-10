using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using MySql.Data.MySqlClient;
using GestiondeVentaZamira.Models;

namespace GestiondeVentaZamira.Views
{
    public partial class Inventario : Window
    {
        private ObservableCollection<ProductoStock> productos;

        // Aquí está tu cadena de conexión
        private string connectionString = "server=yamabiko.proxy.rlwy.net;port=34163;user=root;password=sFrdysrDfZtahYVhsdyzhNsKECijredS;database=railway;";

        public Inventario()
        {
            InitializeComponent();
            productos = new ObservableCollection<ProductoStock>();
            inventarioDataGrid.ItemsSource = productos;
            CargarDatos(); // Ya puedes llamar directamente
        }

        private void CargarDatos()
        {
            productos.Clear();

            string sql = "SELECT id_producto, nombre, descripcion, precio, stock FROM PRODUCTO";

            try
            {
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    connection.Open();

                    using (MySqlCommand cmd = new MySqlCommand(sql, connection))
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            int id = reader.GetInt32("id_producto");
                            string nombre = reader.GetString("nombre");
                            string descripcion = reader.GetString("descripcion");
                            decimal precio = reader.GetDecimal("precio");
                            int stock = reader.GetInt32("stock");

                            productos.Add(new ProductoStock(id, nombre, descripcion, precio, stock));
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar datos: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void InventarioDataGrid_CellEditEnding(object sender, DataGridCellEditEndingEventArgs e)
        {
            if (e.Column.Header.ToString() != "Stock")
                return;

            var row = (ProductoStock)e.Row.Item;

            if (e.EditingElement is TextBox textBox)
            {
                if (int.TryParse(textBox.Text, out int nuevoStock))
                {
                    if (nuevoStock != row.Stock)
                    {
                        ActualizarStock(row.IdProducto, nuevoStock);
                        row.Stock = nuevoStock;
                    }
                }
                else
                {
                    MessageBox.Show("Ingrese un valor numérico válido para el stock.", "Error de entrada", MessageBoxButton.OK, MessageBoxImage.Warning);
                    e.Cancel = true;
                }
            }
        }

        private void ActualizarStock(int idProducto, int nuevoStock)
        {
            string sql = "UPDATE PRODUCTO SET stock = @nuevoStock WHERE id_producto = @idProducto";

            try
            {
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    connection.Open();

                    using (var transaction = connection.BeginTransaction())
                    {
                        using (MySqlCommand cmd = new MySqlCommand(sql, connection, transaction))
                        {
                            cmd.Parameters.AddWithValue("@nuevoStock", nuevoStock);
                            cmd.Parameters.AddWithValue("@idProducto", idProducto);
                            cmd.ExecuteNonQuery();
                        }

                        transaction.Commit();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al actualizar el stock: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void ActualizarProducto(ProductoStock producto)
        {
            string sql = "UPDATE PRODUCTO SET nombre = @nombre, stock = @stock WHERE id_producto = @idProducto";

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();

                using (var transaction = connection.BeginTransaction())
                {
                    using (MySqlCommand cmd = new MySqlCommand(sql, connection, transaction))
                    {
                        cmd.Parameters.AddWithValue("@nombre", producto.Nombre);
                        cmd.Parameters.AddWithValue("@stock", producto.Stock);
                        cmd.Parameters.AddWithValue("@idProducto", producto.IdProducto);

                        cmd.ExecuteNonQuery();
                    }

                    transaction.Commit();
                }
            }
        }

        private void BtnEditarProductos_Click(object sender, RoutedEventArgs e)
        {
            if (inventarioDataGrid.SelectedItem is ProductoStock productoSeleccionado)
            {
                try
                {
                    ActualizarProducto(productoSeleccionado);
                    MessageBox.Show("Producto actualizado correctamente.", "Éxito", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error al actualizar el producto: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
            {
                MessageBox.Show("Seleccione un producto para editar.", "Aviso", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }
    }
}




