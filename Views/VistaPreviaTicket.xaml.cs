using System.Windows;
using System.Windows.Controls;
using System.Printing;
using System.Windows.Documents;

namespace GestiondeVentaZamira.Views
{
    public partial class VistaPreviaTicket : Window
    {
        private StackPanel contenidoTicket;

        public VistaPreviaTicket(StackPanel contenidoTicket)
        {
            InitializeComponent();
            this.contenidoTicket = contenidoTicket;
            TicketContent.Children.Add(contenidoTicket);
        }

        private void BtnImprimir_Click(object sender, RoutedEventArgs e)
        {
            PrintDialog printDialog = new PrintDialog();

            if (printDialog.ShowDialog() == true)
            {
                // Clona el panel para evitar que se "saque" de la vista previa
                StackPanel panelParaImprimir = CloneStackPanel(contenidoTicket);
                printDialog.PrintVisual(panelParaImprimir, "Ticket de Factura");
            }
        }

        private StackPanel CloneStackPanel(StackPanel original)
        {
            StackPanel clone = new StackPanel();
            foreach (UIElement element in original.Children)
            {
                if (element is TextBlock textBlock)
                {
                    clone.Children.Add(new TextBlock
                    {
                        Text = textBlock.Text,
                        FontSize = textBlock.FontSize,
                        FontWeight = textBlock.FontWeight,
                        Margin = textBlock.Margin,
                        TextAlignment = textBlock.TextAlignment
                    });
                }
            }
            return clone;
        }
    }
}

