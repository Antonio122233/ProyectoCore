using Aplicacion.DTOs.Marca;
using Aplicacion.DTOs.Producto;
using Dominio.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Aplicacion.Interfaces.Servicios
{
    public interface  IProductoService
    {
        Task<IEnumerable<ProductoDto>> GetAllAsync();
        Task<ProductoDto?> GetByIdAsync(int id);
        Task<ProductoDto> CreateAsync(ProductoCreateDto dto);
        Task<bool> UpdateAsync(int id, ProductoUpdateDto dto);

        //de todos modos el delete es un borrado logico no fisico
        //este delete ver si lo dejo asi o lo hago un caso de uso.
        
        /// <summary>
        /// Borrado logico, lo pone en estado falso
        /// </summary>
        /// <param name="id">entero</param>
        /// <returns></returns>
        Task<bool> DeleteAsync(int id);         

        Task<ProductoDto?> GetByNombreAsync(string nombre);
        Task<IEnumerable<ProductoDto>> GetActiveAsync();
        Task<ProductoDto?> GetByCategoriaAsync(int idCategoria);
        Task<ProductoDto?> GetByMarcaAsync(int idMarca);
        Task<IEnumerable<ProductoDto>> ObtenerProductosStockBajoAsync();
        Task<ProductoDto?> SearchByCodigoAsync(string codigo);            
    }
}
