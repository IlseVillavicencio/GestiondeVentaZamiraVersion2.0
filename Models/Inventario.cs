using System;

namespace GestiondeVentaZamira.Models
{
    public class Inventario
    {
        public int IdMovimiento { get; set; }
        public int IdProducto { get; set; }
        public string TipoMovimiento { get; set; }
        public int Cantidad { get; set; }
        public DateTime Fecha { get; set; }

        public Inventario(int idMovimiento, int idProducto, string tipoMovimiento, int cantidad, DateTime fecha)
        {
            IdMovimiento = idMovimiento;
            IdProducto = idProducto;
            TipoMovimiento = tipoMovimiento;
            Cantidad = cantidad;
            Fecha = fecha;
        }
    }
}

