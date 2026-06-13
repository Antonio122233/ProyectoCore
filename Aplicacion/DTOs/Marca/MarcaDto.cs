using System;
using System.Collections.Generic;
using System.Text;

namespace Aplicacion.DTOs.Marca
{
    public class MarcaDto
    {
        public int Id { get; set; }
        public string? Nombre { get; set; }
        public string? Descripcion { get; set; }
        public bool EstadoRegistro { get; set; } = true;
    }
}
