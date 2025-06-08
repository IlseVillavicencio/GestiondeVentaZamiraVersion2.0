using System.Windows;

namespace GestiondeVentaZamira.Views
{
    public partial class Logistica : Window
    {
        public Logistica()
        {
            InitializeComponent();
        }

        private void BtnProgramar_Click(object sender, RoutedEventArgs e)
        {
            string pedido = campoPedido.Text.Trim();
            string direccion = campoDireccion.Text.Trim();

            if (!string.IsNullOrEmpty(pedido) && !string.IsNullOrEmpty(direccion))
            {
                listaEnvios.Items.Add($"🚚 Pedido {pedido} a {direccion}");
                campoPedido.Clear();
                campoDireccion.Clear();
            }
            else
            {
                MessageBox.Show("Por favor, ingrese número de pedido y dirección.", "Entrada incompleta", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }
    }
}

