using Aplicacion.DTOs.Proveedor;
using Aplicacion.DTOs.TipoPago;
using Dominio.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Aplicacion.Interfaces
{
    public interface ITipoPagoService
    {
        Task<IEnumerable<TipoPagoDto>> GetAllAsync();
        Task<TipoPagoDto?> GetByIdAsync(int id);
        Task<TipoPagoDto> CreateAsync(TipoPagoCreateDto dto);
        Task<bool> UpdateAsync(int id, TipoPagoUpdateDto dto);
        Task<bool> DeleteAsync(int id);
        Task<TipoPagoDto?> GetByNombreAsync(string nombre);

        Task<IEnumerable<TipoPagoDto>> GetActiveAsync();
    }
}

