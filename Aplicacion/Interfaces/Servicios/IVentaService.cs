using Aplicacion.DTOs.Venta;
using System;
using System.Collections.Generic;
using System.Text;

namespace Aplicacion.Interfaces.Servicios
{
    public interface IVentaService
    {
        Task<VentaDto> CreateAsync(VentaCreateDto dto);
    }
}
