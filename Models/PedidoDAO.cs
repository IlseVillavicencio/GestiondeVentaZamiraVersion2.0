using GestiondeVentaZamira.Models;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;

namespace GestiondeVentaZamira.Models
{
    public class PedidoDAO
    {
        public static void GuardarPedido(int idCliente, string estado, List<CarritoItem> productos)
        {
            using var conn = ConexionDB.Conectar();
            if (conn == null)
                throw new Exception("No se pudo conectar a la base de datos.");

            using var transaction = conn.BeginTransaction();

            try
            {
                // Calcular total
                decimal total = 0;
                foreach (var item in productos)
                {
                    total += item.Subtotal;

                }

                // Insertar pedido
                string sqlPedido = "INSERT INTO PEDIDO (id_cliente, estado, total) VALUES (@idCliente, @estado, @total);";
                using var psPedido = new MySqlCommand(sqlPedido, conn, transaction);
                psPedido.Parameters.AddWithValue("@idCliente", idCliente);
                psPedido.Parameters.AddWithValue("@estado", estado);
                psPedido.Parameters.AddWithValue("@total", total);

                psPedido.ExecuteNonQuery();

                // Obtener el id generado del pedido
                long idPedido = psPedido.LastInsertedId;
                if (idPedido == 0)
                    throw new Exception("No se pudo obtener el id del pedido insertado.");

                // Insertar detalles
                string sqlDetalle = "INSERT INTO DETALLE_PEDIDO (id_pedido, id_producto, cantidad, precio_unitario) VALUES (@idPedido, @idProducto, @cantidad, @precioUnitario)";
                using var psDetalle = new MySqlCommand(sqlDetalle, conn, transaction);

                psDetalle.Parameters.Add("@idPedido", MySqlDbType.Int32);
                psDetalle.Parameters.Add("@idProducto", MySqlDbType.Int32);
                psDetalle.Parameters.Add("@cantidad", MySqlDbType.Int32);
                psDetalle.Parameters.Add("@precioUnitario", MySqlDbType.Double);

                foreach (var item in productos)
                {
                    psDetalle.Parameters["@idPedido"].Value = idPedido;
                    psDetalle.Parameters["@idProducto"].Value = item.IdProducto;
                    psDetalle.Parameters["@cantidad"].Value = item.Cantidad;
                    psDetalle.Parameters["@precioUnitario"].Value = item.PrecioUnitario;

                    psDetalle.ExecuteNonQuery();
                }

                // Registrar salida en INVENTARIO
                string sqlInventario = "INSERT INTO INVENTARIO (id_producto, tipo_movimiento, cantidad) VALUES (@idProducto, 'salida', @cantidad)";
                using var psInv = new MySqlCommand(sqlInventario, conn, transaction);
                psInv.Parameters.Add("@idProducto", MySqlDbType.Int32);
                psInv.Parameters.Add("@cantidad", MySqlDbType.Int32);

                foreach (var item in productos)
                {
                    psInv.Parameters["@idProducto"].Value = item.IdProducto;
                    psInv.Parameters["@cantidad"].Value = item.Cantidad;

                    psInv.ExecuteNonQuery();
                }

                transaction.Commit();
            }
            catch (Exception)
            {
                transaction.Rollback();
                throw; // re-lanzar la excepción para que el llamador la maneje
            }
        }
    }
}

