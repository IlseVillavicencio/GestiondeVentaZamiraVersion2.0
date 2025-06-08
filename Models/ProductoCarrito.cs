using System.ComponentModel;

namespace GestiondeVentaZamira.Models
{
    public class ProductoCarrito : INotifyPropertyChanged
    {
        private string nombre;
        private int cantidad;
        private double precioUnitario;
        private double subtotal;

        public ProductoCarrito(string nombre, int cantidad, double precioUnitario)
        {
            this.nombre = nombre;
            this.cantidad = cantidad;
            this.precioUnitario = precioUnitario;
            this.subtotal = cantidad * precioUnitario;
        }

        public string Nombre
        {
            get => nombre;
            set
            {
                if (nombre != value)
                {
                    nombre = value;
                    OnPropertyChanged(nameof(Nombre));
                }
            }
        }

        public int Cantidad
        {
            get => cantidad;
            set
            {
                if (cantidad != value)
                {
                    cantidad = value;
                    Subtotal = cantidad * precioUnitario; // recalcular subtotal
                    OnPropertyChanged(nameof(Cantidad));
                }
            }
        }

        public double PrecioUnitario
        {
            get => precioUnitario;
            set
            {
                if (precioUnitario != value)
                {
                    precioUnitario = value;
                    Subtotal = cantidad * precioUnitario; // recalcular subtotal
                    OnPropertyChanged(nameof(PrecioUnitario));
                }
            }
        }

        public double Subtotal
        {
            get => subtotal;
            private set
            {
                if (subtotal != value)
                {
                    subtotal = value;
                    OnPropertyChanged(nameof(Subtotal));
                }
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}

