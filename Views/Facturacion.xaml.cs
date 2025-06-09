using GestiondeVentaZamira.Models;
using MySql.Data.MySqlClient;
using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;

namespace GestiondeVentaZamira.Views
{
    public partial class Facturacion : Window
    {
        private ObservableCollection<Factura> facturas;

        public Facturacion()
        {
            InitializeComponent();

            facturas = new ObservableCollection<Factura>();
            tablaPedidos.ItemsSource = facturas;

            btnNuevaFactura.Click += BtnNuevaFactura_Click;
            btnVerDetalles.Click += BtnVerDetalles_Click;
            btnEliminar.Click += BtnEliminar_Click;

            CargarDatosFacturas();
        }

        private void CargarDatosFacturas()
        {
            facturas.Clear();

            string connectionString = "server=127.0.0.1;port=3306;user=root;password=12345;database=sistemaventazamira;";
            string query = @"
                SELECT 
                    p.id_pedido, 
                    c.nombre AS NombreCliente, 
                    p.fecha_pedido, 
                    p.estado, 
                    p.total 
                FROM PEDIDO p
                JOIN CLIENTE c ON p.id_cliente = c.id_cliente";

            try
            {
                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    conn.Open();
                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            facturas.Add(new Factura
                            {
                                IdPedido = reader.GetInt32("id_pedido"),
                                NombreCliente = reader.GetString("NombreCliente"),
                                Fecha = reader.GetDateTime("fecha_pedido"),
                                Estado = reader.GetString("estado"),
                                Total = (double)reader.GetDecimal("total")
                            });
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar facturas: " + ex.Message);
            }

            tablaPedidos.ItemsSource = facturas;
        }

        private void BtnNuevaFactura_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Crear nueva factura (funcionalidad pendiente).");
        }

        private void BtnVerDetalles_Click(object sender, RoutedEventArgs e)
        {
            var facturaSeleccionada = (Factura)tablaPedidos.SelectedItem;
            if (facturaSeleccionada != null)
            {
                MessageBox.Show($"Detalles del pedido:\n\n" +
                                $"ID: {facturaSeleccionada.IdPedido}\n" +
                                $"Cliente: {facturaSeleccionada.NombreCliente}\n" +
                                $"Fecha: {facturaSeleccionada.FechaPedido}\n" +
                                $"Estado: {facturaSeleccionada.Estado}\n" +
                                $"Total: ${facturaSeleccionada.Total:F2}");
                // Aquí puedes abrir una nueva ventana con el detalle si lo deseas
            }
            else
            {
                MessageBox.Show("Seleccione un pedido para ver detalles.");
            }
        }

        private void BtnEliminar_Click(object sender, RoutedEventArgs e)
        {
            var facturaSeleccionada = (Factura)tablaPedidos.SelectedItem;
            if (facturaSeleccionada != null)
            {
                MessageBox.Show("Funcionalidad de eliminación pendiente.\n(Eliminar de la base de datos si es necesario).");
            }
            else
            {
                MessageBox.Show("Seleccione una factura para eliminar.");
            }
        }
    }
}





