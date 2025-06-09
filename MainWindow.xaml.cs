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
            MainContent.Content = new Login(this); // Pedido debe heredar de UserControl correctamente

        }

        // Método para cambiar de vista
        public void MostrarMenuPrincipal()
        {
            MainContent.Content = new VentanaConMenu(); // Reemplaza el contenido
        }


        public static void SetRoot(UserControl nuevaVista)
        {
            Instance.MainContent.Content = nuevaVista;
        }
    }

}