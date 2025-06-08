namespace GestiondeVentaZamira.Models
{
    public class Producto
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public double Precio { get; set; }
        public override string ToString() => Nombre;

        public Producto(int id_producto, string nombre, double precio)
        {
            Id = id_producto;
            Nombre = nombre;
            Precio = precio;
        }
    }
}

