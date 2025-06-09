using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestiondeVentaZamira.Models
{
    public static class SesionActual
    {
        public static Usuario? UsuarioActual { get; set; }

        public static bool EsGerenteGeneral => UsuarioActual?.Rol == "GerenteGeneral";
        public static bool EsGerenteSucursal => UsuarioActual?.Rol == "GerenteSucursal";
        public static bool EsCajero => UsuarioActual?.Rol == "Cajero";
    }
}
