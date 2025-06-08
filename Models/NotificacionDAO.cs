using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using GestiondeVentaZamira.Models;

namespace GestiondeVentaZamira.Models
{
    public class NotificacionDAO
    {
        public List<GestiondeVentaZamira.Models.Notificacion> ObtenerNotificaciones()
        {
            var lista = new List<Notificacion>();
            string sql = "SELECT * FROM NOTIFICACION";

            using (var conn = ConexionDB.Conectar())
            {
                if (conn == null) return lista; // Retorna lista vacía si no hay conexión

                using (var cmd = new MySqlCommand(sql, conn))
                {
                    try
                    {
                        using (var reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                var notificacion = new Notificacion(
                                    reader.GetInt32("id_notificacion"),
                                    reader.GetInt32("id_usuario"),
                                    reader.GetString("mensaje"),
                                    reader.GetDateTime("fecha"),
                                    reader.GetBoolean("leida")
                                );
                                lista.Add(notificacion);
                            }
                        }
                    }
                    catch (MySqlException ex)
                    {
                        Console.WriteLine("Error al obtener notificaciones: " + ex.Message);
                    }
                }
            }

            return lista;
        }

        public void InsertarNotificacion(Notificacion noti)
        {
            string sql = "INSERT INTO NOTIFICACION (id_notificacion, id_usuario, mensaje, fecha, leida) VALUES (@id_notificacion, @id_usuario, @mensaje, @fecha, @leida)";

            using (var conn = ConexionDB.Conectar())
            {
                if (conn == null) return;

                using (var cmd = new MySqlCommand(sql, conn))
                {
                    try
                    {
                        cmd.Parameters.AddWithValue("@id_notificacion", noti.IdNotificacion);
                        cmd.Parameters.AddWithValue("@id_usuario", noti.IdUsuario);
                        cmd.Parameters.AddWithValue("@mensaje", noti.Mensaje);
                        cmd.Parameters.AddWithValue("@fecha", noti.Fecha);
                        cmd.Parameters.AddWithValue("@leida", noti.Leida);

                        cmd.ExecuteNonQuery();
                    }
                    catch (MySqlException ex)
                    {
                        Console.WriteLine("Error al insertar notificación: " + ex.Message);
                    }
                }
            }
        }
    }
}

