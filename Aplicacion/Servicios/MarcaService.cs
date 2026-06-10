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

        public Task<MarcaDto> CreateAsync(MarcaCreateDto dto)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<MarcaDto?> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateAsync(int id, MarcaUpdateDto dto)
        {
            throw new NotImplementedException();
        }
    }
}
