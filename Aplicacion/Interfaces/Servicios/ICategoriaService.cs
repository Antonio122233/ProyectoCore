using Aplicacion.DTOs.Categoria;
using System;
using System.Collections.Generic;
using System.Text;

namespace Aplicacion.Interfaces.Servicios
{
    public interface ICategoriaService
    {
        Task<IEnumerable<CategoriaDto>> GetAllAsync();
        Task<CategoriaDto?> GetByIdAsync(int id);
        Task<CategoriaDto> CreateAsync(CategoriaCreateDto dto);
        Task<bool> UpdateAsync(int id, CategoriaUpdateDto dto);
        Task<bool> DeleteAsync(int id);
        Task<CategoriaDto?> GetByNombreAsync(string nombre);
        Task<IEnumerable<CategoriaDto>> GetActiveAsync();
    }
}
