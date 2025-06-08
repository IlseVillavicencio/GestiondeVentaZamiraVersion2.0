using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation; // si usas Pages y NavigationWindow

namespace SistemaVentaZamira.Utils
{
    public static class SceneManager
    {
        private static Window? mainWindow;

        // Debes llamar esta función al iniciar, por ejemplo en App.xaml.cs
        public static void SetMainWindow(Window window)
        {
            mainWindow = window;
        }

        // Cambiar escena usando UserControl o Page (depende cómo estructures tu app)
        public static void ChangeScene(string xamlName)
        {
            try
            {
                // Carga el UserControl o Page desde recursos XAML
                // La ruta puede variar según dónde tengas los XAML
                var uri = new Uri($"/YourAssemblyName;component/fxml/{xamlName}.xaml", UriKind.Relative);

                // Para Pages con NavigationWindow:
                // var page = Application.LoadComponent(uri) as Page;
                // if (mainWindow is NavigationWindow navWindow && page != null)
                // {
                //     navWindow.Navigate(page);
                // }

                // Para UserControls dentro de una ventana con un ContentControl llamado 'MainContent':
                var content = Application.LoadComponent(uri);

                if (mainWindow != null)
                {
                    if (mainWindow.Content is ContentControl contentControl)
                    {
                        contentControl.Content = content;
                    }
                    else
                    {
                        // Si la ventana solo tiene Content, reemplazarlo:
                        mainWindow.Content = content;
                    }
                }
            }
            catch (Exception)
            {
                // Manejo de errores (puedes mejorar con logs)
            }
        }
    }
}
