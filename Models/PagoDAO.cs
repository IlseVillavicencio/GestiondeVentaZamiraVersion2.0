using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;

namespace GestiondeVentaZamira.Models
{
    public class PagoDAO
    {
        public List<Pago> ObtenerPagos()
        {
            List<Pago> lista = new List<Pago>();
            string sql = "SELECT * FROM PAGO";

            using (var conn = ConexionDB.Conectar())
            {
                if (conn == null) return lista;

                using (var cmd = new MySqlCommand(sql, conn))
                {
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            lista.Add(new Pago(
                                reader.GetInt32("id_pago"),
                                reader.GetInt32("id_pedido"),
                                reader.GetDouble("monto"),
                                reader.GetString("metodo"),
                                reader.GetString("estado")
                            ));
                        }
                    }
                }
            }
            return lista;
        }

        public void InsertarPago(Pago pago)
        {
            string sql = "INSERT INTO PAGO (id_pago, id_pedido, metodo, monto, estado) VALUES (@idPago, @idPedido, @metodo, @monto, @estado)";

            using (var conn = ConexionDB.Conectar())
            {
                if (conn == null) return;

                using (var cmd = new MySqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@idPago", pago.IdPago);
                    cmd.Parameters.AddWithValue("@idPedido", pago.IdPedido);
                    cmd.Parameters.AddWithValue("@metodo", pago.Metodo);
                    cmd.Parameters.AddWithValue("@monto", pago.Monto);
                    cmd.Parameters.AddWithValue("@estado", pago.Estado);

                    try
                    {
                        int filasAfectadas = cmd.ExecuteNonQuery();
                        Console.WriteLine($"Filas afectadas: {filasAfectadas}");
                    }
                    catch (MySqlException e)
                    {
                        Console.Error.WriteLine("Error al insertar pago");
                        Console.Error.WriteLine(e.Message);
                    }
                }
            }
        }
    }
}

