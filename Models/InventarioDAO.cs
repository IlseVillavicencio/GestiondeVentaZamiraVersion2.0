using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;

namespace GestiondeVentaZamira.Models
{
    public class InventarioDAO
    {
        public List<Inventario> ObtenerMovimientos()
        {
            List<Inventario> lista = new List<Inventario>();
            string sql = "SELECT * FROM INVENTARIO";

            using (var conn = ConexionDB.Conectar())
            {
                if (conn == null)
                {
                    Console.WriteLine("Error: no se pudo conectar a la base de datos.");
                    return lista; // vacío si no hay conexión
                }

                using (var cmd = new MySqlCommand(sql, conn))
                {
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            lista.Add(new Inventario(
                                reader.GetInt32("id_movimiento"),
                                reader.GetInt32("id_producto"),
                                reader.GetString("tipo_movimiento"),
                                reader.GetInt32("cantidad"),
                                reader.GetDateTime("fecha")
                            ));
                        }
                    }
                }
            }

            return lista;
        }

        public void InsertarMovimiento(Inventario mov)
        {
            string sql = "INSERT INTO INVENTARIO (id_movimiento, id_producto, tipo_movimiento, cantidad, fecha) VALUES (@idMovimiento, @idProducto, @tipoMovimiento, @cantidad, @fecha)";

            using (var conn = ConexionDB.Conectar())
            {
                if (conn == null)
                {
                    Console.WriteLine("Error: no se pudo conectar a la base de datos.");
                    return;
                }

                using (var cmd = new MySqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@idMovimiento", mov.IdMovimiento);
                    cmd.Parameters.AddWithValue("@idProducto", mov.IdProducto);
                    cmd.Parameters.AddWithValue("@tipoMovimiento", mov.TipoMovimiento);
                    cmd.Parameters.AddWithValue("@cantidad", mov.Cantidad);
                    cmd.Parameters.AddWithValue("@fecha", mov.Fecha);

                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}

