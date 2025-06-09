using GestiondeVentaZamira.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

public class VentanaConMenu : Window
{
    public VentanaConMenu()
    {
        this.Title = "Menú principal";
        this.Width = 800;
        this.Height = 600;

        this.Content = new Menu(); // Tu UserControl
    }
}
