namespace GestiondeVentaZamira.Models
{
    public class DetallePedido
    {
        public int IdPedido { get; set; }
        public int IdProducto { get; set; }
        public string Producto { get; set; }
        public int Cantidad { get; set; }
        public double PrecioUnitario { get; set; }
        public double Subtotal { get; set; }

        public DetallePedido(int idPedido, int idProducto, string producto, int cantidad, double precioUnitario, double subtotal)
        {
            IdPedido = idPedido;
            IdProducto = idProducto;
            Producto = producto;
            Cantidad = cantidad;
            PrecioUnitario = precioUnitario;
            Subtotal = subtotal;
        }
    }
}

