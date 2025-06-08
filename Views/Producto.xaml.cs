using System;
using System.Windows;

namespace GestiondeVentaZamira.Views
{
    public partial class Producto : Window
    {
        public Producto()
        {
            InitializeComponent();
        }

        private void GuardarProducto_Click(object sender, RoutedEventArgs e)
        {
            string nombre = NombreProductoTextBox.Text.Trim();
            string precioTexto = PrecioProductoTextBox.Text.Trim();
            string descripcion = DescripcionProductoTextBox.Text.Trim();

            if (string.IsNullOrEmpty(nombre))
            {
                MessageBox.Show("Por favor, ingresa el nombre del producto.", "Validación", MessageBoxButton.OK, MessageBoxImage.Warning);
                NombreProductoTextBox.Focus();
                return;
            }

            if (!decimal.TryParse(precioTexto, out decimal precio) || precio < 0)
            {
                MessageBox.Show("Por favor, ingresa un precio válido y positivo.", "Validación", MessageBoxButton.OK, MessageBoxImage.Warning);
                PrecioProductoTextBox.Focus();
                return;
            }

            // Aquí agregas la lógica para guardar el producto...

            MessageBox.Show($"Producto '{nombre}' guardado correctamente.\nPrecio: {precio:C}\nDescripción: {descripcion}",
                "Producto Guardado", MessageBoxButton.OK, MessageBoxImage.Information);

            NombreProductoTextBox.Clear();
            PrecioProductoTextBox.Clear();
            DescripcionProductoTextBox.Clear();
            NombreProductoTextBox.Focus();
        }

        private void NombreProductoTextBox_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {

        }
    }
}


