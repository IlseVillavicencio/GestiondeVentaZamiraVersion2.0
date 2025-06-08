using GestiondeVentaZamira.Models;
using GestiondeVentaZamira.Views;
using System.Windows;


using System.Windows.Controls;

// Usa tu namespace correcto aquí
namespace GestiondeVentaZamira.Views
{
    public partial class Menu : UserControl
    {
        public Menu()
        {
            InitializeComponent();
        }

        private void IrAPedido_Click(object sender, RoutedEventArgs e)
        {
            Pedido pedidoWindow = new Pedido();
            pedidoWindow.Show();  // o ShowDialog() si quieres ventana modal

            //contenidoContentControl.Content = new Pedido();
        }

        private void IrAProducto_Click(object sender, RoutedEventArgs e)
        {

            //contenidoContentControl.Content = new Producto();

            Producto productoWindow = new Producto();
            productoWindow.Show();
        }

        private void IrAInventario_Click(object sender, RoutedEventArgs e)
        {
            //contenidoContentControl.Content = new Inventario();

            Inventario inventarioWindow = new Inventario();
            inventarioWindow.Show();
        }

        private void IrAFacturacion_Click(object sender, RoutedEventArgs e)
        {

            Facturacion facturacionWindow = new Facturacion();
            facturacionWindow.Show();
            //contenidoContentControl.Content = new GestiondeVentaZamira.Views.Facturacion();
        }

        private void IrANotificaciones_Click(object sender, RoutedEventArgs e)
        {
            //contenidoContentControl.Content = new Notificaciones();

            Notificaciones notificacionesWindow = new Notificaciones();
            notificacionesWindow.Show();
        }

        private void IrASoporte_Click(object sender, RoutedEventArgs e)
        {
            Soporte soporteWindow = new Soporte();
            soporteWindow.Show();
            //contenidoContentControl.Content = new Soporte();
        }

        private void IrALogistica_Click(object sender, RoutedEventArgs e)
        {
            Logistica logisticaWindow = new Logistica();
            logisticaWindow.Show();
            //contenidoContentControl.Content = new Logistica();
        }

        private void IrAAdmin_Click(object sender, RoutedEventArgs e)
        {
            Admin adminWindow = new Admin();
            adminWindow.Show();
            //contenidoContentControl.Content = new Admin();
        }
    }
}




