using System;
using System.Collections.Generic;
using System.Data;
using MySql.Data.MySqlClient;

namespace GestiondeVentaZamira.Models
{
    public class EnvioDAO
    {
        public List<Envio> ObtenerEnvios()
        {
            var lista = new List<Envio>();
            string sql = "SELECT * FROM ENVIO";

            using (var conn = ConexionDB.Conectar())
            {
                if (conn == null) return lista;

                using (var cmd = new MySqlCommand(sql, conn))
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        lista.Add(new Envio(
                            reader.GetInt32("id_envio"),
                            reader.GetInt32("id_pedido"),
                            reader.GetString("metodo_entrega"),
                            reader.GetString("direccion_entrega"),
                            reader.GetString("estado"),
                            !reader.IsDBNull(reader.GetOrdinal("fecha_estimada")) ? reader.GetDateTime("fecha_estimada") : default
                        ));
                    }
                }
            }

            return lista;
        }

        public void InsertarEnvio(Envio envio)
        {
            string sql = @"INSERT INTO ENVIO 
                           (id_envio, id_pedido, metodo_entrega, direccion_entrega, estado, fecha_estimada) 
                           VALUES (@idEnvio, @idPedido, @metodoEntrega, @direccionEntrega, @estado, @fechaEstimada)";

            using (var conn = ConexionDB.Conectar())
            {
                if (conn == null) return;

                using (var cmd = new MySqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@idEnvio", envio.IdEnvio);
                    cmd.Parameters.AddWithValue("@idPedido", envio.IdPedido);
                    cmd.Parameters.AddWithValue("@metodoEntrega", envio.MetodoEntrega);
                    cmd.Parameters.AddWithValue("@direccionEntrega", envio.DireccionEntrega);
                    cmd.Parameters.AddWithValue("@estado", envio.Estado);
                    if (envio.FechaEstimada != default)
                        cmd.Parameters.AddWithValue("@fechaEstimada", envio.FechaEstimada);
                    else
                        cmd.Parameters.Add("@fechaEstimada", MySqlDbType.Date).Value = DBNull.Value;

                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}

