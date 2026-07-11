using System;
using System.Collections.Generic;
using System.Text;

namespace Aplicacion.DTOs.Categoria
{
    public class CategoriaDto
    {
        public int IdCategoria { get; set; }
        public string? Descripcion { get; set; }
        public string? Comentario { get; set; }
        public bool EstadoRegistro { get; set; }
    }
}
