namespace GestiondeVentaZamira.Models
{
    public class Pago
    {
        public int IdPago { get; set; }
        public int IdPedido { get; set; }
        public double Monto { get; set; }
        public string Metodo { get; set; }
        public string Estado { get; set; }

        public Pago(int idPago, int idPedido, double monto, string metodo, string estado)
        {
            IdPago = idPago;
            IdPedido = idPedido;
            Monto = monto;
            Metodo = metodo;
            Estado = estado;
        }
    }
}

