using Aplicacion.DTOs.Cliente;
using Aplicacion.Interfaces;
using Aplicacion.Interfaces.Repositorios;
using Aplicacion.Interfaces.Servicios;
using Dominio.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Aplicacion.Servicios
{
    public class ClienteService : IClienteService
    {
        private readonly IClienteRepository _repo;
        private readonly IUnitOfWork _unitOfWork;

        public ClienteService(IClienteRepository repo, IUnitOfWork unitOfWork)
        {
            _repo = repo;
            _unitOfWork = unitOfWork;
        }

        public async Task<ClienteDto> CreateAsync(ClienteCreateDto dto)
        {
            var cliente = new TblCliente
            {
                Nombre = dto.Nombre,
                Apellido = dto.Apellido,
                Telefono = dto.Telefono,
                Direccion = dto.Direccion,
                Referencias = dto.Referencias,
                FechaRegistro = DateTime.Now,
                EstadoRegistro = true
            };

            await _repo.AddAsync(cliente);

            await _unitOfWork.SaveChangesAsync();

            return new ClienteDto
            {
                IdCliente = cliente.IdCliente,
                Nombre = cliente.Nombre,
                Apellido = cliente.Apellido,
                Telefono = cliente.Telefono,
                Direccion = cliente.Direccion,
                Referencias = cliente.Referencias,
                FechaRegistro = cliente.FechaRegistro,
                EstadoRegistro = cliente.EstadoRegistro
            };
        }

        public async Task<IEnumerable<ClienteDto>> GetAllAsync()
        {
            var clientes = await _repo.GetAllAsync();

            return clientes.Select(cliente => new ClienteDto
            {
                IdCliente = cliente.IdCliente,
                Nombre = cliente.Nombre,
                Apellido = cliente.Apellido,
                Telefono = cliente.Telefono,
                Direccion = cliente.Direccion,
                Referencias = cliente.Referencias,
                FechaRegistro = cliente.FechaRegistro,
                EstadoRegistro = cliente.EstadoRegistro
            });
        }

        public async Task<ClienteDto?> GetByIdAsync(int id)
        {
            var cliente = await _repo.GetByIdAsync(id);

            if (cliente == null)
            {
                throw new KeyNotFoundException("El cliente no existe");
            }

            return new ClienteDto
            {
                IdCliente = cliente.IdCliente,
                Nombre = cliente.Nombre,
                Apellido = cliente.Apellido,
                Telefono = cliente.Telefono,
                Direccion = cliente.Direccion,
                Referencias = cliente.Referencias,
                FechaRegistro = cliente.FechaRegistro,
                EstadoRegistro = cliente.EstadoRegistro
            };
        }


        public async Task<ClienteDto> UpdateAsync(int id, ClienteUpdateDto dto)
        {
            var cliente = await _repo.GetByIdAsync(id);

            if (cliente == null)
            {
                throw new KeyNotFoundException("El cliente no existe");
            }

            cliente.Nombre = dto.Nombre;
            cliente.Apellido = dto.Apellido;
            cliente.Telefono = dto.Telefono;
            cliente.Direccion = dto.Direccion;
            cliente.Referencias = dto.Referencias;

            await _repo.UpdateAsync(cliente);

            await _unitOfWork.SaveChangesAsync();

            return new ClienteDto
            {
                IdCliente = cliente.IdCliente,
                Nombre = cliente.Nombre,
                Apellido = cliente.Apellido,
                Telefono = cliente.Telefono,
                Direccion = cliente.Direccion,
                Referencias = cliente.Referencias,
                FechaRegistro = cliente.FechaRegistro,
                EstadoRegistro = cliente.EstadoRegistro
            };
        }

        public async Task DeleteAsync(int id)
        {
            var cliente = await _repo.GetByIdAsync(id);

            if (cliente == null)
            {
                throw new KeyNotFoundException(
                    "El cliente no existe");
            }

            cliente.EstadoRegistro = false;

            await _repo.UpdateAsync(cliente);

            await _unitOfWork.SaveChangesAsync();
        }

    }
}
