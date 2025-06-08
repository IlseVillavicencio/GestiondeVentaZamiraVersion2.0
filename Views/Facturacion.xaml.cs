using GestiondeVentaZamira.Models;
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
            tablaFacturas.ItemsSource = facturas;

            btnNuevaFactura.Click += BtnNuevaFactura_Click;
            btnVerDetalles.Click += BtnVerDetalles_Click;
            btnEliminar.Click += BtnEliminar_Click;

            CargarDatosDeEjemplo();
        }

        private void CargarDatosDeEjemplo()
        {
            // Ejemplo: agregar algunas facturas
            facturas.Add(new Factura { Id = 1, Cliente = "Cliente A", Total = 250.75 });
            facturas.Add(new Factura { Id = 2, Cliente = "Cliente B", Total = 120.00 });
            facturas.Add(new Factura { Id = 3, Cliente = "Cliente C", Total = 300.50 });
        }

        private void BtnNuevaFactura_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Crear nueva factura...");
        }

        private void BtnVerDetalles_Click(object sender, RoutedEventArgs e)
        {
            var facturaSeleccionada = (Factura)tablaFacturas.SelectedItem;
            if (facturaSeleccionada != null)
            {
                MessageBox.Show($"Ver detalles de factura ID: {facturaSeleccionada.Id}");
            }
            else
            {
                MessageBox.Show("Seleccione una factura para ver detalles.");
            }
        }

        private void BtnEliminar_Click(object sender, RoutedEventArgs e)
        {
            var facturaSeleccionada = (Factura)tablaFacturas.SelectedItem;
            if (facturaSeleccionada != null)
            {
                facturas.Remove(facturaSeleccionada);
                MessageBox.Show("Factura eliminada.");
            }
            else
            {
                MessageBox.Show("Seleccione una factura para eliminar.");
            }
        }
    }
}


