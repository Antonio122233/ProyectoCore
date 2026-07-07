using System;
using System.Collections.Generic;
using System.Text;

namespace Aplicacion.DTOs.Producto
{
    public class ProductoCreateDto
    {
        public int? IdCategoria { get; set; }         
        public int IdMarca { get; set; }              
        public string? CodigoProducto { get; set; }   
        public string Nombre { get; set; } = null!;   
        public string? Descripcion { get; set; }      
        public string? Color { get; set; }            
        public string? Talla { get; set; }            
        public string? Material { get; set; }         
        public decimal Costo { get; set; }            
        public decimal Precio { get; set; }           
        public int ExistenciaActual { get; set; }     
        public int ExistenciaMinima { get; set; }     
        public DateOnly FechaRegistro { get; set; }                         

    }
}
