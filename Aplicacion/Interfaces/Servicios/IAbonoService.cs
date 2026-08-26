using Aplicacion.DTOs.Abono;
using System;
using System.Collections.Generic;
using System.Text;

namespace Aplicacion.Interfaces.Servicios
{
    public interface  IAbonoService
    {
        Task<AbonoDto> CreateAsync(AbonoCreateDto dto);
    }
}
