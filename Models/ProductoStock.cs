using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestiondeVentaZamira.Models
{
    public class ProductoStock : INotifyPropertyChanged
    {
        public int IdProducto { get; }

        public string Nombre { get; }

        public string Descripcion { get; }

        public decimal Precio { get; }

        private int stock;
        public int Stock
        {
            get => stock;
            set
            {
                if (stock != value)
                {
                    stock = value;
                    OnPropertyChanged(nameof(Stock));
                }
            }
        }

        public ProductoStock(int idProducto, string nombre, string descripcion, decimal precio, int stock)
        {
            IdProducto = idProducto;
            Nombre = nombre;
            Descripcion = descripcion;
            Precio = precio;
            Stock = stock;
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged(string propertyName) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
