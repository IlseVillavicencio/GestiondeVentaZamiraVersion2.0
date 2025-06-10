using MySql.Data.MySqlClient;
using System;
using System.Security.Policy;

namespace GestiondeVentaZamira.Models
{
    public static class ConexionDB
    {
        private const string ConnectionString = "server=yamabiko.proxy.rlwy.net;port=34163;user=root;password=sFrdysrDfZtahYVhsdyzhNsKECijredS;database=railway;";

        public static MySqlConnection? Conectar()
        {
            try
            {
                var conexion = new MySqlConnection(ConnectionString);
                conexion.Open();
                return conexion;
            }
            catch (MySqlException ex)
            {
                Console.WriteLine(ex.Message);
                return null; // aquí sí puede devolver null, pero el método debe admitirlo
            }
        }
    }
}

