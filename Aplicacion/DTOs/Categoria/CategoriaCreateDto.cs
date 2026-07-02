using System;
using System.Collections.Generic;
using System.Text;

namespace Aplicacion.DTOs.Categoria
{
    public class CategoriaCreateDto
    {
        public string? Descripcion { get; set; } = "";
        public string? Comentario { get; set; }
    }
}
