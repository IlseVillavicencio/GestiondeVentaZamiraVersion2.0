﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestiondeVentaZamira.Models
{
    public class Usuario
    {
        public int IdUsuario { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public string Correo { get; set; } = string.Empty;
        public string Rol { get; set; } = string.Empty;

        // Constructor vacío
        public Usuario() { }

        // Constructor con parámetros
        public Usuario(int idUsuario, string nombre)
        {
            IdUsuario = idUsuario;
            Nombre = nombre;
        }
    }
}

