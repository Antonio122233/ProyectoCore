using Aplicacion.DTOs.Marca;
using Aplicacion.DTOs.TipoPago;
using Aplicacion.Interfaces;
using Aplicacion.Interfaces.Repositorios;
using Dominio.Models;

namespace Aplicacion.Servicios
{

    public class MarcaService : IMarcaService
    {
        private readonly IMarcaRepository _repo;

        public MarcaService(IMarcaRepository repo)
        {
            _repo = repo;
        }

        //Obtener todas
        public async Task<IEnumerable<MarcaDto>> GetAllAsync()
        {
            var marcas = await _repo.GetAllAsync();
            return marcas.Select(m => new MarcaDto
            {
                Id = m.IdMarca,
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
                Descripcion = dto.Descripcion,
                EstadoRegistro  = true
            };

            await _repo.AddAsync(nueva);

            return new MarcaDto
            {
                Id = nueva.IdMarca,
                Nombre = nueva.Nombre,
                Descripcion = nueva.Descripcion,
                EstadoRegistro = nueva.EstadoRegistro
            };
        }

        //public async Task<bool> DeleteAsync(int id)
        //{
        //    var entidad = await _repo.GetByIdAsync(id);
        //    if (entidad is null) return false;

        //    await _repo.DeleteAsync(entidad);
        //    return true;
        //}

        public async Task<bool> DeleteAsync(int id)
        {
            var entidad = await _repo.GetByIdAsync(id);
            if (entidad is null) return false;

            if (!entidad.EstadoRegistro)
            {
                return false; // ya esta inactivo
            }

            entidad.EstadoRegistro =false;
            await _repo.UpdateAsync(entidad);
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
                Id = marca.IdMarca,
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

        public async Task<IEnumerable<MarcaDto>> GetActiveAsync()
        {
            var marcas = await _repo.GetActiveAsync();
            return marcas.Select(

                m => new MarcaDto
                {
                    Id = m.IdMarca,
                    Nombre = m.Nombre,
                    Descripcion = m.Descripcion,
                    EstadoRegistro = m.EstadoRegistro
                }
                );
        }

        public async Task<MarcaDto?> GetByNombreAsync(string nombre)
        {
            if (string.IsNullOrEmpty(nombre))
            {
                throw new ArgumentException("Envié un nombre");
            }

            var marca = await _repo.GetByNombreAsync(nombre);
            if (marca is null) return null;

            return new MarcaDto
            {
                Descripcion = marca.Descripcion,
                EstadoRegistro = marca.EstadoRegistro,
                Id = marca.IdMarca,
                Nombre = marca.Nombre
            };
        }
    }
}
