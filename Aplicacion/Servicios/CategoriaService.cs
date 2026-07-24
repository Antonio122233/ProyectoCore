using Aplicacion.DTOs.Categoria;
using Aplicacion.Interfaces;
using Aplicacion.Interfaces.Repositorios;
using Dominio.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Aplicacion.Servicios
{
    public class CategoriaService : ICategoriaService
    {
        private readonly ICategoriaRepository _repo;
        private readonly IUnitOfWork _unitOfWork;

        public CategoriaService(ICategoriaRepository repo, IUnitOfWork unitOfWork)
        {
            _repo = repo;
            _unitOfWork = unitOfWork;
        }

        public async Task<CategoriaDto> CreateAsync(CategoriaCreateDto dto)
        {
            if (string.IsNullOrWhiteSpace(dto.Descripcion))
            {
                throw new ArgumentNullException("El nombre es obligatorio");
            }

            var nuevo = new TblCategoria
            {
                Comentario = dto.Comentario,
                Descripcion = dto.Descripcion,
                EstadoRegistro = true
            };

            await _repo.AddAsync(nuevo);
            await _unitOfWork.SaveChangesAsync();

            return new CategoriaDto
            {
                IdCategoria = nuevo.IdCategoria,
                Comentario = nuevo.Comentario,
                Descripcion = nuevo.Descripcion,
                EstadoRegistro = nuevo.EstadoRegistro
            };
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var entidad = await _repo.GetByIdAsync(id);
            if (entidad is null)
            {
                return false;
            }

            if (!entidad.EstadoRegistro)
            {
                return false; // ya estaba inactivo
            }

            //lo dejamos inactivo, borrado logico
            entidad.EstadoRegistro = false;
            await _repo.UpdateAsync(entidad);
            await _unitOfWork.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<CategoriaDto>> GetActiveAsync()
        {
            var categorias = await _repo.GetActiveAsync();
            return categorias.Select(
                x => new CategoriaDto
                {
                    Comentario = x.Comentario,
                    Descripcion = x.Descripcion,
                    EstadoRegistro = x.EstadoRegistro,
                    IdCategoria = x.IdCategoria
                }
                );
        }

        public async Task<IEnumerable<CategoriaDto>> GetAllAsync()
        {
            var categorias = await _repo.GetAllAsync();
            return categorias.Select(x =>
            new CategoriaDto
            {
                Comentario = x.Comentario,
                Descripcion = x.Descripcion,
                EstadoRegistro = x.EstadoRegistro,
                IdCategoria = x.IdCategoria
            });
        }

        public async Task<CategoriaDto?> GetByIdAsync(int id)
        {
            if (id <= 0)
            {
                throw new ArgumentException("Id no puede ser negativo");
            }

            var categoria = await _repo.GetByIdAsync(id);
            if (categoria is null)
            {
                return null;
            }

            return new CategoriaDto
            {
                Comentario = categoria.Comentario,
                Descripcion = categoria.Descripcion,
                EstadoRegistro = categoria.EstadoRegistro,
                IdCategoria = categoria.IdCategoria
            };

        }

        public async Task<CategoriaDto?> GetByNombreAsync(string nombre)
        {
            if (string.IsNullOrWhiteSpace(nombre))
            {
                throw new ArgumentException("Envie un nombre");
            }

            var categoria = await _repo.GetByNombreAsync(nombre);
            if (categoria is null)
            {
                return null;
            }

            return new CategoriaDto
            {
                Comentario = categoria.Comentario,
                Descripcion = categoria.Descripcion,
                EstadoRegistro = categoria.EstadoRegistro,
                IdCategoria = categoria.IdCategoria
            };
        }

        public async Task<bool> UpdateAsync(int id, CategoriaUpdateDto dto)
        {
            if (id <= 0)
            {
                throw new ArgumentException("Id no puede ser negativo");
            }

            if (dto is null || string.IsNullOrWhiteSpace(dto.Descripcion))
            {
                throw new ArgumentException("Debe enviar los datos a actualizar");
            }

            var existe = await _repo.GetByIdAsync(id);
            if (existe is null)
            {
                return false;
            }

            existe.Descripcion = dto.Descripcion;
            existe.EstadoRegistro = dto.EstadoRegistro;
            existe.Comentario = dto.Comentario;

            await _repo.UpdateAsync(existe);
            await _unitOfWork.SaveChangesAsync();
            return true;
        }
    }
}
