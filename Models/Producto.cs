using System.ComponentModel;

namespace GestiondeVentaZamira.Models
{
    public class Producto
    {
        public int Id { get; }
        public string Nombre { get; }
        public string Descripcion { get; }
        public decimal Precio { get; }
        public int Stock { get; }

        // Constructor alternativo más flexible
        public Producto(int id, string nombre, decimal precio)
        {
            Id = id;
            Nombre = nombre;
            Descripcion = "";
            Precio = precio;
            Stock = 0;
        }

        // Constructor completo
        public Producto(int id, string nombre, string descripcion, decimal precio, int stock)
        {
            Id = id;
            Nombre = nombre;
            Descripcion = descripcion;
            Precio = precio;
            Stock = stock;
        }
    }
}
       
