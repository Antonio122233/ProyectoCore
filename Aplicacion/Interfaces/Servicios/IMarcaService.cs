using Aplicacion.DTOs.Marca;
using System;
using System.Collections.Generic;
using System.Text;

namespace Aplicacion.Interfaces.Servicios
{
    public interface  IMarcaService
    {
        Task<IEnumerable<MarcaDto>> GetAllAsync();
        Task<MarcaDto?> GetByIdAsync(int id);
        Task<MarcaDto> CreateAsync(MarcaCreateDto dto);
        Task<bool> UpdateAsync(int id, MarcaUpdateDto dto);
        Task<bool> DeleteAsync(int id);

    }
}
