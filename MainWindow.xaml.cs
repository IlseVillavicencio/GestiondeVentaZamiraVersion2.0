using GestiondeVentaZamira.Views;
using System.Windows;
using System.Windows.Controls;
using Pedido = GestiondeVentaZamira.Views.Pedido;


namespace GestiondeVentaZamira
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static MainWindow Instance { get; private set; } = null!;

        public MainWindow()
        {
            InitializeComponent();
            var pedido = new Pedido(); // Pedido debe heredar de UserControl correctamente

        }

        // Cargar la vista inicial "Pedido" en el ContentControl
        private void AbrirVentanaPedido_Click(object sender, RoutedEventArgs e)
        {
            var ventanaPedido = new Views.Pedido(); // Clase tipo Window
            ventanaPedido.Show();
        }


        public static void SetRoot(UserControl nuevaVista)
        {
            Instance.MainContent.Content = nuevaVista;
        }
    }

}