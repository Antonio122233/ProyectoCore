using Aplicacion.DTOs.Compra;
using System;
using System.Collections.Generic;
using System.Text;

namespace Aplicacion.Interfaces.Servicios
{
    public interface ICompraService
    {
        Task<IEnumerable<CompraDto>> GetByFechaAsync(DateTime fecha);
        Task<IEnumerable<CompraDto>> GetByProveedorAsync(int idProveedor);
        Task<CompraDto> CreateAsync(CompraCreateDto dto);
        Task<bool> DeleteAsync(int id);
        Task<CompraDto?> GetByIdAsync(int id);
    }
}
