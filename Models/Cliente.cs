using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.ComponentModel;

namespace GestiondeVentaZamira.Models
{
    public class Cliente : INotifyPropertyChanged
    {
        private int idCliente;
        private string nombre;

        public int IdCliente
        {
            get => idCliente;
            set
            {
                if (idCliente != value)
                {
                    idCliente = value;
                    OnPropertyChanged(nameof(IdCliente));
                }
            }
        }

        public string Nombre
        {
            get => nombre;
            set
            {
                if (nombre != value)
                {
                    nombre = value;
                    OnPropertyChanged(nameof(Nombre));
                }
            }
        }

        public Cliente(int idCliente, string nombre)
        {
            this.idCliente = idCliente;
            this.nombre = nombre;
        }

        public event PropertyChangedEventHandler? PropertyChanged;


        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}


