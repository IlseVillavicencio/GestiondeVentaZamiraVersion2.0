using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using MySql.Data.MySqlClient;

namespace GestiondeVentaZamira.Views
{
    public partial class Inventario : Window
    {
        private ObservableCollection<ProductoStock> productos;
        private MySqlConnection? connection;

        public Inventario()
        {
            InitializeComponent();
            productos = new ObservableCollection<ProductoStock>();
            inventarioDataGrid.ItemsSource = productos;
        }

        public void SetConnection(MySqlConnection connection)
        {
            this.connection = connection;
            CargarDatos();
        }

        private void CargarDatos()
        {
            if (connection == null)
            {
                MessageBox.Show("La conexión a la base de datos no ha sido inicializada.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            productos.Clear();

            string sql = @"
                SELECT p.id_producto, p.nombre_producto,
                IFNULL(SUM(CASE WHEN i.tipo_movimiento = 'entrada' THEN i.cantidad ELSE 0 END),0) - 
                IFNULL(SUM(CASE WHEN i.tipo_movimiento = 'salida' THEN i.cantidad ELSE 0 END),0) AS stock_actual
                FROM producto p
                LEFT JOIN inventario i ON p.id_producto = i.id_producto
                GROUP BY p.id_producto, p.nombre_producto";

            try
            {
                using (MySqlCommand cmd = new MySqlCommand(sql, connection))
                {
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            int id = reader.GetInt32("id_producto");
                            string nombre = reader.GetString("nombre_producto");
                            int stock = reader.GetInt32("stock_actual");

                            productos.Add(new ProductoStock(id, nombre, stock));
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
                    int stockAnterior = row.StockActual;
                    if (nuevoStock != stockAnterior)
                    {
                        int diferencia = nuevoStock - stockAnterior;
                        string tipoMovimiento = diferencia > 0 ? "entrada" : "salida";
                        int cantidadMovimiento = Math.Abs(diferencia);

                        InsertarMovimiento(row.IdProducto, tipoMovimiento, cantidadMovimiento);
                        row.StockActual = nuevoStock;
                    }
                }
                else
                {
                    MessageBox.Show("Ingrese un valor numérico válido para el stock.", "Error de entrada", MessageBoxButton.OK, MessageBoxImage.Warning);
                    e.Cancel = true;
                }
            }
        }

        private void InsertarMovimiento(int idProducto, string tipoMovimiento, int cantidad)
        {
            if (connection == null)
            {
                MessageBox.Show("No hay conexión a la base de datos para insertar movimiento.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            string sql = "INSERT INTO inventario (id_producto, tipo_movimiento, cantidad, fecha) VALUES (@idProducto, @tipoMovimiento, @cantidad, CURRENT_TIMESTAMP)";

            try
            {
                using (MySqlCommand cmd = new MySqlCommand(sql, connection))
                {
                    cmd.Parameters.AddWithValue("@idProducto", idProducto);
                    cmd.Parameters.AddWithValue("@tipoMovimiento", tipoMovimiento);
                    cmd.Parameters.AddWithValue("@cantidad", cantidad);

                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al insertar movimiento: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }

    public class ProductoStock : INotifyPropertyChanged
    {
        public int IdProducto { get; }

        public string NombreProducto { get; }

        private int stockActual;
        public int StockActual
        {
            get => stockActual;
            set
            {
                if (stockActual != value)
                {
                    stockActual = value;
                    OnPropertyChanged(nameof(StockActual));
                }
            }
        }

        public ProductoStock(int idProducto, string nombreProducto, int stockActual)
        {
            IdProducto = idProducto;
            NombreProducto = nombreProducto;
            StockActual = stockActual;
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged(string propertyName) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}


