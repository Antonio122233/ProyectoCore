using Aplicacion.DTOs.Proveedor;
using Aplicacion.DTOs.TipoPago;
using Domnio.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Aplicacion.Interfaces.Servicios
{
    public interface ITipoPagoService
    {
        Task<IEnumerable<TipoPagoDto>> GetAllAsync();
        Task<TipoPagoDto?> GetByIdAsync(int id);
        Task<TipoPagoDto> CreateAsync(TipoPagoCreateDto dto);
        Task<bool> UpdateAsync(int id, TipoPagoUpdateDto dto);
        Task<bool> DeleteAsync(int id);
        Task<TipoPagoDto?> GetByNombreAsync(string nombre);
    }
}

