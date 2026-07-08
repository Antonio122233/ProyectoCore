using Aplicacion.DTOs.Marca;
using Aplicacion.DTOs.TipoPago;
using System;
using System.Collections.Generic;
using System.Text;

namespace Aplicacion.Interfaces
{
    public interface  IMarcaService
    {
        Task<IEnumerable<MarcaDto>> GetAllAsync();
        Task<MarcaDto?> GetByIdAsync(int id);
        Task<MarcaDto> CreateAsync(MarcaCreateDto dto);
        Task<bool> UpdateAsync(int id, MarcaUpdateDto dto);
        Task<bool> DeleteAsync(int id);
        Task<IEnumerable<MarcaDto>> GetActiveAsync();
        Task<MarcaDto?> GetByNombreAsync(string nombre);

    }
}
