using System.Collections.Generic;
using System.Windows;

namespace GestiondeVentaZamira.Views
{
    public partial class Soporte : Window
    {
        public Soporte()
        {
            InitializeComponent();
            CargarTickets();
        }

        private void CargarTickets()
        {
            var tickets = new List<string>
            {
                "Ticket #001 - Problema con factura",
                "Ticket #002 - Error en sistema de pagos",
                "Ticket #003 - Consulta sobre productos",
                "Ticket #004 - Problema técnico con la app"
            };

            ListaTicketsListView.ItemsSource = tickets;
        }

        private void NuevoTicketButton_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Aquí iría la lógica para crear un nuevo ticket.", "Nuevo Ticket");
        }

        private void VerDetallesButton_Click(object sender, RoutedEventArgs e)
        {
            var seleccionado = ListaTicketsListView.SelectedItem;
            if (seleccionado == null)
            {
                MessageBox.Show("Por favor, seleccione un ticket para ver los detalles.", "Sin selección");
                return;
            }

            string ticketSeleccionado = seleccionado.ToString()!;
            MessageBox.Show($"Detalles del ticket:\n{ticketSeleccionado}", "Detalles del Ticket");
        }

    }
}



