using System.Windows;
using System.Windows.Controls;

namespace GestiondeVentaZamira.Views
{
    public partial class Menu : Window
    {
        // Campo privado para almacenar el rol del usuario
        private string RolUsuario;

        // Constructor que recibe el rol del usuario
        public Menu(string rol)
        {
            InitializeComponent();
            RolUsuario = rol;
            AplicarVisibilidadPorRol();

        }

        // Método que ajusta la visibilidad de los botones según el rol
        private void AplicarVisibilidadPorRol()
        {
            switch (RolUsuario.ToLower())
            {
                case "gerentegeneral":
                    // Todos visibles
                    break;

                case "gerentesucursal":
                    btnPedido.Visibility = Visibility.Visible;
                    btnProducto.Visibility = Visibility.Visible;
                    btnInventario.Visibility = Visibility.Collapsed;
                    btnFacturacion.Visibility = Visibility.Visible;
                    btnSoporte.Visibility = Visibility.Visible;
                    btnLogistica.Visibility = Visibility.Collapsed;
                    btnAdmin.Visibility = Visibility.Collapsed;
                    break;

                case "cajero":
                    btnPedido.Visibility = Visibility.Visible;
                    btnProducto.Visibility = Visibility.Collapsed;
                    btnInventario.Visibility = Visibility.Collapsed;
                    btnFacturacion.Visibility = Visibility.Visible;
                    btnSoporte.Visibility = Visibility.Collapsed;
                    btnLogistica.Visibility = Visibility.Collapsed;
                    btnAdmin.Visibility = Visibility.Collapsed;
                    break;

                default:
                    // Si el rol no es reconocido, ocultar todo
                    btnPedido.Visibility = Visibility.Collapsed;
                    btnProducto.Visibility = Visibility.Collapsed;
                    btnInventario.Visibility = Visibility.Collapsed;
                    btnFacturacion.Visibility = Visibility.Collapsed;
                    btnSoporte.Visibility = Visibility.Collapsed;
                    btnLogistica.Visibility = Visibility.Collapsed;
                    btnAdmin.Visibility = Visibility.Collapsed;
                    break;
            }
        }

        // Métodos de navegación a las ventanas correspondientes
        private void IrAPedido_Click(object sender, RoutedEventArgs e)
        {
            new Pedido().Show();
        }

        private void IrAProducto_Click(object sender, RoutedEventArgs e)
        {
            new Producto().Show();
        }

        private void IrAInventario_Click(object sender, RoutedEventArgs e)
        {
            new Inventario().Show();
        }

        private void IrAFacturacion_Click(object sender, RoutedEventArgs e)
        {
            new Facturacion().Show();
        }

        private void IrANotificaciones_Click(object sender, RoutedEventArgs e)
        {
            new Notificaciones().Show();
        }

        private void IrASoporte_Click(object sender, RoutedEventArgs e)
        {
            new Soporte().Show();
        }

        private void IrALogistica_Click(object sender, RoutedEventArgs e)
        {
            new Logistica().Show();
        }

        private void IrAAdmin_Click(object sender, RoutedEventArgs e)
        {
            new Admin().Show();
        }
    }
}





