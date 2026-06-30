using Aplicacion.DTOs.Marca;
using Aplicacion.DTOs.Proveedor;
using Aplicacion.Interfaces;
using Aplicacion.Interfaces.Repositorios;
using Dominio.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Aplicacion.Servicios
{
    public class ProveedorService : IProvedorService
    {
        private readonly IProveedorRepository _repo;

        public ProveedorService(IProveedorRepository repo)
        {
            _repo = repo;
        }

        public async Task<ProveedorDto> CreateAsync(ProveedorCreateDto dto)
        {
            if (string.IsNullOrWhiteSpace(dto.Nombre))
                throw new ArgumentException("El nombre es obligatorio");

            var nuevo = new TblProveedore
            {
                Nombre = dto.Nombre,
                Descripcion = dto.Descripcion
            };

            await _repo.AddAsync(nuevo);

            return new ProveedorDto
            {
                Id = nuevo.IdProveedor,
                Nombre = nuevo.Nombre,
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
                return false; //ya esta inactivo
            }

            entidad.EstadoRegistro= false;
            await _repo.UpdateAsync(entidad);
            return true;
        }

        public async Task<IEnumerable<ProveedorDto>> GetActiveAsync()
        {
            var proveedores = await _repo.GetActiveAsync();
            return proveedores.Select(
                x => new ProveedorDto
                {
                    Descripcion = x.Descripcion,
                    EstadoRegistro = x.EstadoRegistro,
                    Id = x.IdProveedor,
                    Nombre = x.Nombre
                }
                );
        }

        public async Task<IEnumerable<ProveedorDto>> GetAllAsync()
        {
            var proveedores = await _repo.GetAllAsync();

            return proveedores.Select(
                x => new ProveedorDto
                {
                    Descripcion = x.Descripcion,
                    EstadoRegistro = x.EstadoRegistro,
                    Id = x.IdProveedor,
                    Nombre = x.Nombre,
                });
        }

        public async Task<ProveedorDto?> GetByIdAsync(int id)
        {
            if (id <= 0)
                throw new ArgumentException("Id Incorrecto");
            var proveedor = await _repo.GetByIdAsync(id);

            if (proveedor is null) return null;

            return new ProveedorDto
            {
                Descripcion = proveedor.Descripcion,
                EstadoRegistro = proveedor.EstadoRegistro,
                Id = proveedor.IdProveedor,
                Nombre = proveedor.Nombre,
            };
        }

        public async Task<ProveedorDto?> GetByNombreAsync(string nombre)
        {
            if (string.IsNullOrEmpty(nombre))
            {
                throw new ArgumentException("Envié un nombre");
            }

            var nombreProveedor = await _repo.GetByNombreAsync(nombre);
            if (nombreProveedor is null) return null;

            return new ProveedorDto
            {
                Descripcion = nombreProveedor.Descripcion,
                EstadoRegistro = nombreProveedor.EstadoRegistro,
                Id = nombreProveedor.IdProveedor,
                Nombre = nombreProveedor.Nombre,
            };
        }

        public async Task<bool> UpdateAsync(int id, ProveedorUpdateDto dto)
        {
            var existe = await _repo.GetByIdAsync(id);
            if (existe is null) return false;

            if (string.IsNullOrWhiteSpace(dto.Nombre))
            {
                throw new ArgumentException("El nombre del proveedor no puede estar vacio");
            }

            existe.Nombre = dto.Nombre;
            existe.Descripcion = dto.Descripcion;
            existe.EstadoRegistro = dto.EstadoRegistro;

            await _repo.UpdateAsync(existe);
            return true;
        }
    }
}
