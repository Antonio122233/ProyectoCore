using Aplicacion.DTOs.Cliente;
using System;
using System.Collections.Generic;
using System.Text;

namespace Aplicacion.Interfaces.Servicios
{
    public interface IClienteService
    {
        Task<IEnumerable<ClienteDto>> GetAllAsync();
        Task<ClienteDto?> GetByIdAsync(int id);
        Task<ClienteDto> CreateAsync(ClienteCreateDto dto);
        Task<ClienteDto> UpdateAsync(int id, ClienteUpdateDto dto);
        Task DeleteAsync(int id);
    }
}
