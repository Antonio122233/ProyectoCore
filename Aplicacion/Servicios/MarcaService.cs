using Aplicacion.DTOs;
using Aplicacion.Interfaces;
using Domnio.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Aplicacion.Servicios
{

    public class MarcaService : IMarcaService
    {
        private readonly IGenericRepository<TblMarca> _repo;

        public MarcaService(IGenericRepository<TblMarca> repo)
        {
            _repo = repo;
        }

        //Obtener todas
        public async Task<IEnumerable<MarcaDto>> GetAllAsync()
        {
            var marcas = await _repo.GetAllAsync();
            return marcas.Select(m => new MarcaDto
            {
                Id = m.Id,
                Nombre = m.Nombre,
                Descripcion = m.Descripcion,
                EstadoRegistro = m.EstadoRegistro
            });
        }

        public async Task<MarcaDto> CreateAsync(MarcaCreateDto dto)
        {
            if (string.IsNullOrWhiteSpace(dto.Nombre))
                throw new ArgumentException("El nombre es obligatorio");

            var nueva = new TblMarca
            {
                Nombre = dto.Nombre,
                Descripcion = dto.Descripcion               
            };

            await _repo.AddAsync(nueva);

            return new MarcaDto
            {
                Id = nueva.Id,
                Nombre = nueva.Nombre,
                Descripcion = nueva.Descripcion,
                EstadoRegistro = nueva.EstadoRegistro
            };
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var entidad = await _repo.GetByIdAsync(id);
            if (entidad is null) return false;

            await _repo.DeleteAsync(entidad);
            return true;
        }

        public async Task<MarcaDto?> GetByIdAsync(int id)
        {
            if (id <= 0)
                throw new ArgumentException("ID inválido");
            var marca = await _repo.GetByIdAsync(id);

            if (marca is null) return null;

            return new MarcaDto

            {
                Id = marca.Id,
                Nombre = marca.Nombre,
                Descripcion = marca.Descripcion,
                EstadoRegistro = marca.EstadoRegistro
            };
        }

        public async Task<bool> UpdateAsync(int id, MarcaUpdateDto dto)
        {
            var existente = await _repo.GetByIdAsync(id);
            if (existente is null) return false;

            if (string.IsNullOrWhiteSpace(dto.Nombre))
                throw new ArgumentException("El nombre no puede estar vacío");

            existente.Nombre = dto.Nombre;
            existente.Descripcion = dto.Descripcion;
            existente.EstadoRegistro = dto.EstadoRegistro;

            await _repo.UpdateAsync(existente);

            return true;
        }
    }
}
