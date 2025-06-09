using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestiondeVentaZamira.Models
{
    public static class RolPermisos
    {
        public static bool PuedeVerPlantilla(string rol) => rol == "GerenteGeneral" || rol == "GerenteSucursal";
        public static bool PuedeModificarPlantilla(string rol) => rol == "GerenteSucursal";

        public static bool PuedeConsultarProductos(string rol) => rol == "GerenteGeneral" || rol == "GerenteSucursal" || rol == "Cajero";
        public static bool PuedeModificarProductos(string rol) => rol == "GerenteSucursal";

        public static string NivelReporteOperaciones(string rol)
        {
            return rol switch
            {
                "GerenteGeneral" => "Global",
                "GerenteSucursal" => "Local",
                "Cajero" => "Turno",
                _ => "Ninguno"
            };
        }

        public static bool PuedeVerCorteCaja(string rol) => rol == "GerenteSucursal" || rol == "Cajero";
        public static string TipoCorteCaja(string rol)
        {
            return rol switch
            {
                "GerenteSucursal" => "EmpleadosSucursal",
                "Cajero" => "Propio",
                _ => "Ninguno"
            };
        }

        public static bool PuedeVerVentaYTicket(string rol) => rol == "Cajero";
        public static bool PuedeVerAutorizaciones(string rol) => rol == "GerenteSucursal";
    }
}
