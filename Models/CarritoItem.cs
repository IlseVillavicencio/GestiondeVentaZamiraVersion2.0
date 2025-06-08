using System;

namespace GestiondeVentaZamira.Models
{
    public class CarritoItem
    {
        // ID del producto, puede ser nulo si el producto aún no está en la base de datos
        public int? IdProducto { get; set; }

        // Nombre o descripción del producto
        public string Producto { get; set; } = string.Empty;

        // Cantidad del producto en el carrito
        public int Cantidad { get; set; }

        // Precio unitario del producto (mejor usar decimal en lugar de double para dinero)
        public decimal PrecioUnitario { get; set; }

        // Subtotal calculado (PrecioUnitario * Cantidad)
        public decimal Subtotal => PrecioUnitario * Cantidad;

        // Constructor vacío necesario para inicializaciones con object initializer
        public CarritoItem() { }

        // Constructor para nuevos productos aún no guardados (sin ID)
        public CarritoItem(string producto, int cantidad, decimal precioUnitario)
        {
            Producto = producto;
            Cantidad = cantidad;
            PrecioUnitario = precioUnitario;
        }

        // Constructor para productos existentes con ID
        public CarritoItem(int idProducto, string producto, int cantidad, decimal precioUnitario)
        {
            IdProducto = idProducto;
            Producto = producto;
            Cantidad = cantidad;
            PrecioUnitario = precioUnitario;
        }
    }
}


