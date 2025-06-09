using MySql.Data.MySqlClient;  // Usando MySQL
using GestiondeVentaZamira.Models;
using System;
using System.Collections.Generic;

namespace GestiondeVentaZamira.Models
{
    public class ProductoDAO
    {
        // Obtener todos los productos
        public List<Producto> ObtenerTodos()
        {
            var productos = new List<Producto>();
            string sql = "SELECT id_producto, nombre, precio FROM PRODUCTO";

            using (MySqlConnection? conn = ConexionDB.Conectar())
            {
                if (conn == null)
                {
                    Console.WriteLine("Error: No se pudo establecer conexión con la base de datos.");
                    return productos;
                }

                using (MySqlCommand cmd = new MySqlCommand(sql, conn))
                {
                    try
                    {
                        using (MySqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                int idProducto = reader.GetInt32("id_producto");
                                string nombre = reader.GetString("nombre");
                                decimal precio = reader.GetDecimal("precio");

                                productos.Add(new Producto(idProducto, nombre, precio));
                            }
                        }
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine($"Error al obtener productos: {e.Message}");
                    }
                }
            }

            return productos;
        }

        // Insertar un nuevo producto
        public bool Insertar(Producto producto)
        {
            string sql = "INSERT INTO PRODUCTO (nombre, precio) VALUES (@nombre, @precio)";

            using (MySqlConnection? conn = ConexionDB.Conectar())
            {
                if (conn == null)
                {
                    Console.WriteLine("Error: No se pudo establecer conexión con la base de datos.");
                    return false;
                }

                using (MySqlCommand cmd = new MySqlCommand(sql, conn))
                {
                    try
                    {
                        cmd.Parameters.AddWithValue("@nombre", producto.Nombre);
                        cmd.Parameters.AddWithValue("@precio", producto.Precio);

                        int filas = cmd.ExecuteNonQuery();
                        return filas > 0;
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine($"Error al insertar producto: {e.Message}");
                        return false;
                    }
                }
            }
        }

        // Buscar por nombre
        public Producto? BuscarPorNombre(string nombre)
        {
            string sql = "SELECT id_producto, nombre, precio FROM PRODUCTO WHERE nombre = @nombre";

            using (MySqlConnection? conn = ConexionDB.Conectar())
            {
                if (conn == null)
                {
                    Console.WriteLine("Error: No se pudo establecer conexión con la base de datos.");
                    return null;
                }

                using (MySqlCommand cmd = new MySqlCommand(sql, conn))
                {
                    try
                    {
                        cmd.Parameters.AddWithValue("@nombre", nombre);

                        using (MySqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                int idProducto = reader.GetInt32("id_producto");
                                decimal precio = reader.GetDecimal("precio");
                                return new Producto(idProducto, nombre, precio);
                            }
                        }
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine($"Error al buscar producto: {e.Message}");
                    }
                }
            }

            return null;
        }
    }
}

