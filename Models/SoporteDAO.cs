using MySql.Data.MySqlClient;
using GestiondeVentaZamira.Models;
using System;
using System.Collections.Generic;

namespace GestiondeVentaZamira.Models
{
    public class SoporteDAO
    {
        public List<Soporte> ObtenerTickets()
        {
            var lista = new List<Soporte>();
            string sql = "SELECT * FROM SOPORTE";

            using (MySqlConnection? conn = ConexionDB.Conectar())
            {
                if (conn == null)
                {
                    Console.WriteLine("No se pudo establecer la conexión a la base de datos.");
                    return lista;
                }

                using (MySqlCommand cmd = new MySqlCommand(sql, conn))
                {
                    try
                    {
                        using (MySqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                lista.Add(new Soporte(
                                    reader.GetInt32("id_ticket"),
                                    reader.GetInt32("id_usuario"),
                                    reader.GetString("asunto"),
                                    reader.GetString("descripcion"),
                                    reader.GetString("estado"),
                                    reader.GetDateTime("fecha_creacion")
                                ));
                            }
                        }
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e);
                    }
                }
            }
            return lista;
        }

        public bool InsertarSoporte(Soporte ticket)
        {
            string sql = "INSERT INTO SOPORTE (id_ticket, id_usuario, asunto, descripcion, estado, fecha_creacion) VALUES (@id_ticket, @id_usuario, @asunto, @descripcion, @estado, @fecha_creacion)";

            using (MySqlConnection? conn = ConexionDB.Conectar())
            {
                if (conn == null)
                {
                    Console.WriteLine("No se pudo establecer la conexión a la base de datos.");
                    return false;
                }

                using (MySqlCommand cmd = new MySqlCommand(sql, conn))
                {
                    try
                    {
                        cmd.Parameters.AddWithValue("@id_ticket", ticket.IdTicket);
                        cmd.Parameters.AddWithValue("@id_usuario", ticket.IdUsuario);
                        cmd.Parameters.AddWithValue("@asunto", ticket.Asunto);
                        cmd.Parameters.AddWithValue("@descripcion", ticket.Descripcion);
                        cmd.Parameters.AddWithValue("@estado", ticket.Estado);
                        cmd.Parameters.AddWithValue("@fecha_creacion", ticket.FechaCreacion);

                        int filas = cmd.ExecuteNonQuery();
                        return filas > 0;
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e);
                        return false;
                    }
                }
            }
        }

    }
}

