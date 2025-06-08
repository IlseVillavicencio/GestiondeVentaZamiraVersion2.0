using MySql.Data.MySqlClient;
using GestiondeVentaZamira.Models;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Transactions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace GestiondeVentaZamira.Views
{
    public partial class Pedido : Window
    {
        private readonly List<CarritoItem> carrito = new List<CarritoItem>();
        private readonly string connectionString = "server=127.0.0.1;port=3306;user=root;password=12345;database=sistemaventazamira;";
        private bool _isInitialized;

        public Pedido()
        {
            InitializeComponent();
            _isInitialized = true;
            CargarProductos();
            // Cargar opciones en los ComboBox
            CargarOpcionesComboBoxes();
        }


        private void CargarOpcionesComboBoxes()
        {
            // Método de entrega
            MetodoEntregaComboBox.Items.Clear();
            MetodoEntregaComboBox.Items.Add(new ComboBoxItem { Content = "Envío a domicilio" });
            MetodoEntregaComboBox.Items.Add(new ComboBoxItem { Content = "Recoger en tienda" });

            // Estado del pedido
            EstadoComboBox.Items.Clear();
            EstadoComboBox.Items.Add(new ComboBoxItem { Content = "Pendiente" });
            EstadoComboBox.Items.Add(new ComboBoxItem { Content = "Procesando" });
            EstadoComboBox.Items.Add(new ComboBoxItem { Content = "Enviado" });
            EstadoComboBox.Items.Add(new ComboBoxItem { Content = "Entregado" });

            // Método de pago
            MetodoPagoComboBox.Items.Clear();
            MetodoPagoComboBox.Items.Add(new ComboBoxItem { Content = "Tarjeta de crédito" });
            MetodoPagoComboBox.Items.Add(new ComboBoxItem { Content = "PayPal" });
            MetodoPagoComboBox.Items.Add(new ComboBoxItem { Content = "Efectivo" });
        }

        private void CargarProductos()
        {
            try
            {
                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    conn.Open();
                    string query = "SELECT id_producto, nombre, precio FROM producto";
                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    MySqlDataReader reader = cmd.ExecuteReader();

                    ProductoComboBox.Items.Clear();
                    while (reader.Read())
                    {
                        ProductoComboBox.Items.Add(reader["nombre"].ToString());
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar productos: " + ex.Message);
            }
        }

        private void AgregarProductoAlCarrito(object sender, System.Windows.RoutedEventArgs e)
        {
            if (ProductoComboBox.SelectedItem == null || string.IsNullOrWhiteSpace(CantidadProductoTextField.Text))
            {
                MessageBox.Show("Seleccione un producto y cantidad válida.");
                return;
            }

            if (!int.TryParse(CantidadProductoTextField.Text, out int cantidad))
            {
                MessageBox.Show("Cantidad no válida.");
                return;
            }

            if (ProductoComboBox.SelectedItem is not string producto)
            {
                MessageBox.Show("Producto no válido.");
                return;
            }

            decimal precio = ObtenerPrecioProducto(producto);

            if (precio <= 0)
            {
                MessageBox.Show("Error al obtener precio.");
                return;
            }

            carrito.Add(new CarritoItem
            {
                Producto = producto,
                Cantidad = cantidad,
                PrecioUnitario = precio
            });

            ActualizarCarritoGrid();
            CalcularTotal();
            CantidadProductoTextField.Clear();
        }

        private decimal ObtenerPrecioProducto(string producto)
        {
            try
            {
                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    conn.Open();
                    string query = "SELECT precio FROM producto WHERE nombre = @nombre";
                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@nombre", producto);
                    object result = cmd.ExecuteScalar();
                    return result != null ? Convert.ToDecimal(result) : 0;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al obtener precio: " + ex.Message);
                return 0;
            }
        }

        private void ActualizarCarritoGrid()
        {
            CarritoDataGrid.ItemsSource = null;
            CarritoDataGrid.ItemsSource = carrito;
        }

        private void CalcularTotal()
        {
            decimal total = CalcularTotalDecimal();
            TotalTextField.Text = total.ToString("C");
        }

        private decimal CalcularTotalDecimal()
        {
            decimal total = 0;
            foreach (var item in carrito)
            {
                total += item.Subtotal;
            }
            return total;
        }

        private void MetodoEntregaComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (!_isInitialized || DireccionTextField == null || MetodoEntregaComboBox == null)
                return;

            var selectedItem = MetodoEntregaComboBox.SelectedItem as ComboBoxItem;
            string selectedText = selectedItem?.Content?.ToString() ?? "";

            if (selectedText == "Envío a domicilio")
            {
                DireccionTextField.IsEnabled = true;
            }
            else
            {
                DireccionTextField.IsEnabled = false;
                DireccionTextField.Clear();
            }
        }

        private void CantidadProductoTextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !int.TryParse(e.Text, out _);
        }

        private void RealizarPedido(object sender, System.Windows.RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(NombreUsuarioTextField.Text) ||
                string.IsNullOrWhiteSpace(CorreoUsuarioTextField.Text) ||
                carrito.Count == 0)
            {
                MessageBox.Show("Por favor, complete la información del cliente y agregue productos.");
                return;
            }

            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();
                MySqlTransaction transaction = conn.BeginTransaction();

                try
                {
                    long clienteId;

                    // Buscar cliente por correo
                    string buscarCliente = "SELECT id_cliente FROM cliente WHERE correo = @correo";
                    MySqlCommand cmdBuscarCliente = new MySqlCommand(buscarCliente, conn);
                    cmdBuscarCliente.Parameters.AddWithValue("@correo", CorreoUsuarioTextField.Text);
                    object clienteExistente = cmdBuscarCliente.ExecuteScalar();

                    if (clienteExistente != null)
                    {
                        // Cliente existe, obtenemos su id
                        clienteId = Convert.ToInt64(clienteExistente);
                    }
                    else
                    {
                        // Cliente no existe, lo insertamos
                        string insertCliente = "INSERT INTO cliente (nombre, correo) VALUES (@nombre, @correo)";
                        MySqlCommand cmdCliente = new MySqlCommand(insertCliente, conn);
                        cmdCliente.Parameters.AddWithValue("@nombre", NombreUsuarioTextField.Text);
                        cmdCliente.Parameters.AddWithValue("@correo", CorreoUsuarioTextField.Text);
                        cmdCliente.ExecuteNonQuery();
                        clienteId = cmdCliente.LastInsertedId;
                    }

                    // Insertar pedido
                    string insertPedido = @"
                        INSERT INTO pedido 
                        (id_cliente, fecha_pedido, estado, total)
                        VALUES 
                        (@id_cliente, @fecha_pedido, @estado,  @total)";
                    MySqlCommand cmdPedido = new MySqlCommand(insertPedido, conn);
                    cmdPedido.Parameters.AddWithValue("@id_cliente", clienteId);
                    cmdPedido.Parameters.AddWithValue("@fecha_pedido", FechaPedidoDatePicker.SelectedDate ?? DateTime.Now);
                    
                    cmdPedido.Parameters.AddWithValue("@estado", ((ComboBoxItem)EstadoComboBox.SelectedItem)?.Content?.ToString() ?? "Pendiente");
                    //cmdPedido.Parameters.AddWithValue("@metodo_entrega", ((ComboBoxItem)MetodoEntregaComboBox.SelectedItem)?.Content?.ToString() ?? "");
                    //cmdPedido.Parameters.AddWithValue("@direccion_entrega", DireccionTextField.Text ?? "");
                    //cmdPedido.Parameters.AddWithValue("@fecha_entrega", FechaEntregaDatePicker.SelectedDate ?? (object)DBNull.Value);
                    //cmdPedido.Parameters.AddWithValue("@metodo_pago", ((ComboBoxItem)MetodoPagoComboBox.SelectedItem)?.Content?.ToString() ?? "");
                    //cmdPedido.Parameters.AddWithValue("@numero_pago", NumeroCuentaTextField.Text ?? "");
                    cmdPedido.Parameters.AddWithValue("@total", CalcularTotalDecimal());

                    var estadoItem = EstadoComboBox.SelectedItem as ComboBoxItem;
                    string estado = estadoItem?.Content?.ToString() ?? "Pendiente";
                    

                    cmdPedido.ExecuteNonQuery();
                    long pedidoId = cmdPedido.LastInsertedId;

                    // Insertar detalles del pedido
                    foreach (var item in carrito)
                    {
                        // Obtener el id_producto usando el nombre del producto
                        string getProductIdQuery = "SELECT id_producto FROM producto WHERE nombre = @nombre";
                        MySqlCommand getProductIdCmd = new MySqlCommand(getProductIdQuery, conn);
                        getProductIdCmd.Parameters.AddWithValue("@nombre", item.Producto);
                        object result = getProductIdCmd.ExecuteScalar();

                        if (result == null)
                        {
                            throw new Exception($"No se encontró el producto '{item.Producto}' en la base de datos.");
                        }

                        int idProducto = Convert.ToInt32(result);

                        string insertDetalle = @"
                            INSERT INTO detalle_pedido
                            (id_pedido, id_producto, cantidad, precio_unitario, producto, subtotal) 
                            VALUES 
                            (@id_pedido, @id_producto, @cantidad, @precio_unitario, @producto, @subtotal)";
                        MySqlCommand cmdDetalle = new MySqlCommand(insertDetalle, conn);
                        cmdDetalle.Parameters.AddWithValue("@id_pedido", pedidoId);
                        cmdDetalle.Parameters.AddWithValue("@id_producto", idProducto);
                        cmdDetalle.Parameters.AddWithValue("@cantidad", item.Cantidad);
                        cmdDetalle.Parameters.AddWithValue("@precio_unitario", item.PrecioUnitario);
                        cmdDetalle.Parameters.AddWithValue("@producto", item.Producto); // si tienes el nombre del producto
                        cmdDetalle.Parameters.AddWithValue("@subtotal", item.Subtotal);

                        cmdDetalle.ExecuteNonQuery();
                    }

                        // --- INSERTAR PAGO ---

                        string metodoPago = ((MetodoPagoComboBox.SelectedItem as ComboBoxItem)?.Content?.ToString()) ?? "";
                        // Mapear nombres amigables a valores exactos de la BD
                        if (metodoPago.Contains("tarjeta")) metodoPago = "tarjeta";
                        else if (metodoPago.Contains("paypal")) metodoPago = "paypal";
                        else metodoPago = "transferencia"; // o "efectivo" si ajustas la BD

                        string insertPago = @"
                            INSERT INTO pago (id_pedido, metodo, monto, estado)
                            VALUES (@id_pedido, @metodo, @monto, @estado)";
                        MySqlCommand cmdPago = new MySqlCommand(insertPago, conn, transaction);
                        cmdPago.Parameters.AddWithValue("@id_pedido", pedidoId);
                        cmdPago.Parameters.AddWithValue("@metodo", metodoPago);
                        cmdPago.Parameters.AddWithValue("@monto", CalcularTotalDecimal());
                        cmdPago.Parameters.AddWithValue("@estado", "pendiente"); // o lo que consideres default
                        cmdPago.ExecuteNonQuery();

                        // --- INSERTAR ENVÍO (si aplica) ---
                        string metodoEntrega = ((ComboBoxItem)MetodoEntregaComboBox.SelectedItem)?.Content?.ToString() ?? "";
                        
                    if (metodoEntrega == "Envío a domicilio")
                        {
                            string insertEnvio = @"
                                INSERT INTO envio (id_pedido, metodo_entrega, direccion_entrega, estado, fecha_estimada, fecha_entrega)
                                VALUES (@id_pedido, @metodo_entrega, @direccion_entrega, @estado, @fecha_estimada, @fecha_entrega)";
                            MySqlCommand cmdEnvio = new MySqlCommand(insertEnvio, conn, transaction);
                            cmdEnvio.Parameters.AddWithValue("@id_pedido", pedidoId);
                            cmdEnvio.Parameters.AddWithValue("@metodo_entrega", metodoEntrega);
                            cmdEnvio.Parameters.AddWithValue("@direccion_entrega", DireccionTextField.Text ?? "");
                            cmdEnvio.Parameters.AddWithValue("@estado", "preparando");
                            cmdEnvio.Parameters.AddWithValue("@fecha_estimada", FechaEntregaDatePicker.SelectedDate ?? (object)DBNull.Value);
                            cmdEnvio.Parameters.AddWithValue("@fecha_entrega", DBNull.Value); // aún no entregado
                            cmdEnvio.ExecuteNonQuery();
                        }

                    transaction.Commit();
                    MessageBox.Show("Pedido realizado correctamente.");
                    LimpiarFormulario();

                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    MessageBox.Show("Error al realizar pedido: " + ex.Message);
                }
            }
        }

        private void LimpiarFormulario()
        {
            NombreUsuarioTextField.Clear();
            CorreoUsuarioTextField.Clear();
            DireccionTextField.Clear();
            FechaEntregaDatePicker.SelectedDate = null;
            ProductoComboBox.SelectedIndex = -1;
            CantidadProductoTextField.Clear();
            MetodoEntregaComboBox.SelectedIndex = -1;
            EstadoComboBox.SelectedIndex = -1;
            MetodoPagoComboBox.SelectedIndex = -1;
            NumeroCuentaTextField.Clear();
            carrito.Clear();
            ActualizarCarritoGrid();
            TotalTextField.Clear();
        }
    }
}

