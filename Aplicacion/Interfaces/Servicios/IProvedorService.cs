using Aplicacion.DTOs.Marca;
using Aplicacion.DTOs.Proveedor;
using System;
using System.Collections.Generic;
using System.Text;

namespace Aplicacion.Interfaces.Servicios
{
    public interface  IProvedorService
    {
        Task<IEnumerable<ProveedorDto>> GetAllAsync();
        Task<ProveedorDto?> GetByIdAsync(int id);
        Task<ProveedorDto> CreateAsync(ProveedorCreateDto dto);
        Task<bool> UpdateAsync(int id, ProveedorUpdateDto dto);
        Task<bool> DeleteAsync(int id);
        Task<ProveedorDto?> GetByNombreAsync(string nombre);
        Task<IEnumerable<ProveedorDto>> GetActiveAsync();
    }
}
