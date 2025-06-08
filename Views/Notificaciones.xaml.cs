using System.Windows;
using System.Windows.Controls;

namespace GestiondeVentaZamira.Views
{
    public partial class Notificaciones : UserControl
    {
        public Notificaciones()
        {
            InitializeComponent();

            listaNotificaciones.Items.Add("Notificación 1: Pedido completado.");
            listaNotificaciones.Items.Add("Notificación 2: Producto agotado.");
            listaNotificaciones.Items.Add("Notificación 3: Nueva factura disponible.");
        }

        private void marcarComoLeido(object sender, RoutedEventArgs e)
        {
            var seleccionados = listaNotificaciones.SelectedItems;
            if (seleccionados.Count == 0)
            {
                MessageBox.Show("Selecciona alguna notificación para marcarla como leída.");
                return;
            }

            while (listaNotificaciones.SelectedItems.Count > 0)
            {
                listaNotificaciones.Items.Remove(listaNotificaciones.SelectedItems[0]);
            }
        }

        private void eliminarTodas(object sender, RoutedEventArgs e)
        {
            if (listaNotificaciones.Items.Count == 0)
            {
                MessageBox.Show("No hay notificaciones para eliminar.");
                return;
            }

            var respuesta = MessageBox.Show("¿Eliminar todas las notificaciones?", "Confirmar", MessageBoxButton.YesNo);
            if (respuesta == MessageBoxResult.Yes)
            {
                listaNotificaciones.Items.Clear();
            }
        }
    }
}
