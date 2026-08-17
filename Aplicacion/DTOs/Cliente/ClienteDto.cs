using System;
using System.Collections.Generic;
using System.Text;

namespace Aplicacion.DTOs.Cliente
{
    public class ClienteDto
    {
        public int IdCliente { get; set; }

        public string Nombre { get; set; } = string.Empty;

        public string? Apellido { get; set; }

        public string? Telefono { get; set; }

        public string? Direccion { get; set; }

        public string? Referencias { get; set; }

        public DateTime FechaRegistro { get; set; }

        public bool EstadoRegistro { get; set; }
    }
}

