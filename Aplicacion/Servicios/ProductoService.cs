using Aplicacion.DTOs.Marca;
using Aplicacion.DTOs.Producto;
using Aplicacion.Interfaces;
using Aplicacion.Interfaces.Repositorios;
using Dominio.Models;
using System;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using System.Text;

namespace Aplicacion.Servicios
{
    public class ProductoService : IProductoService
    {
        private readonly IProductoRepository _repo;
        public ProductoService(IProductoRepository repo)
        {
            _repo = repo;
        }

        public async Task<ProductoDto> CreateAsync(ProductoCreateDto dto)
        {
            if (string.IsNullOrWhiteSpace(dto.Nombre))
                throw new ArgumentException("El nombre es obligatorio");

            var nuevo = new TblProducto
            {
                CodigoProducto = dto.Nombre,
                Color = dto.Color,
                Costo = dto.Costo,
                Descripcion = dto.Descripcion,
                EstadoRegistro = true,
                ExistenciaActual = dto.ExistenciaActual,
                ExistenciaMinima = dto.ExistenciaMinima,
                IdCategoria = dto.IdCategoria,
                IdMarca = dto.IdMarca,
                Material = dto.Material,
                Nombre = dto.Nombre,
                Precio = dto.Precio,
                Talla = dto.Talla,
            };

            await _repo.AddAsync(nuevo);

            return new ProductoDto
            {
                IdProducto = nuevo.IdProducto,
                Color = nuevo.Color,
                CodigoProducto = nuevo.CodigoProducto,
                Costo = nuevo.Costo,
                Descripcion = nuevo.Descripcion,
                EstadoRegistro = nuevo.EstadoRegistro,
                ExistenciaActual = nuevo.ExistenciaActual,
                ExistenciaMinima = nuevo.ExistenciaMinima,
                FechaBaja = nuevo.FechaBaja,
                FechaRegistro = nuevo.FechaRegistro,
                IdCategoria = nuevo.IdCategoria,
                IdMarca = nuevo.IdMarca,
                Material = nuevo.Material,
                Nombre = nuevo.Nombre,
                Precio = nuevo.Precio,
                Talla = nuevo.Talla
            };
        }


        public async Task<bool> DeleteAsync(int id)
        {
            var entidad = await _repo.GetByIdAsync(id);
            if (entidad is null) return false;

            if (!entidad.EstadoRegistro)
            {
                return false; // ya esta inactivo
            }

            entidad.EstadoRegistro = false;
            await _repo.UpdateAsync(entidad);
            return true;
        }

        public async Task<IEnumerable<ProductoDto>> GetActiveAsync()
        {
            var productos = await _repo.GetActiveAsync();
            return productos.Select(

                m => new ProductoDto
                {
                    IdProducto = m.IdProducto,
                    Color = m.Color,
                    CodigoProducto = m.CodigoProducto,
                    Costo = m.Costo,
                    Descripcion = m.Descripcion,
                    EstadoRegistro = m.EstadoRegistro,
                    ExistenciaActual = m.ExistenciaActual,
                    ExistenciaMinima = m.ExistenciaMinima,
                    FechaBaja = m.FechaBaja,
                    FechaRegistro = m.FechaRegistro,
                    IdCategoria = m.IdCategoria,
                    IdMarca = m.IdMarca,
                    Material = m.Material,
                    Nombre = m.Nombre,
                    Precio = m.Precio,
                    Talla = m.Talla
                }
                );
        }

        public async Task<IEnumerable<ProductoDto>> GetAllAsync()
        {
            var productos = await _repo.GetAllAsync();
            return productos.Select(m => new ProductoDto
            {
                IdProducto = m.IdProducto,
                Color = m.Color,
                CodigoProducto = m.CodigoProducto,
                Costo = m.Costo,
                Descripcion = m.Descripcion,
                EstadoRegistro = m.EstadoRegistro,
                ExistenciaActual = m.ExistenciaActual,
                ExistenciaMinima = m.ExistenciaMinima,
                FechaBaja = m.FechaBaja,
                FechaRegistro = m.FechaRegistro,
                IdCategoria = m.IdCategoria,
                IdMarca = m.IdMarca,
                Material = m.Material,
                Nombre = m.Nombre,
                Precio = m.Precio,
                Talla = m.Talla
            });
        }

        public Task<ProductoDto?> GetByCategoriaAsync(int idCategoria)
        {
            throw new NotImplementedException();
        }

        public async Task<ProductoDto?> GetByIdAsync(int id)
        {
            if (id <= 0)
                throw new ArgumentException("ID inválido");

            var producto = await _repo.GetByIdAsync(id);

            if (producto is null) return null;

            return new ProductoDto

            {
                IdProducto = producto.IdProducto,
                Color = producto.Color,
                CodigoProducto = producto.CodigoProducto,
                Costo = producto.Costo,
                Descripcion = producto.Descripcion,
                EstadoRegistro = producto.EstadoRegistro,
                ExistenciaActual = producto.ExistenciaActual,
                ExistenciaMinima = producto.ExistenciaMinima,
                FechaBaja = producto.FechaBaja,
                FechaRegistro = producto.FechaRegistro,
                IdCategoria = producto.IdCategoria,
                IdMarca = producto.IdMarca,
                Material = producto.Material,
                Nombre = producto.Nombre,
                Precio = producto.Precio,
                Talla = producto.Talla
            };
        }

        public Task<ProductoDto?> GetByMarcaAsync(int idMarca)
        {
            throw new NotImplementedException();
        }

        public Task<ProductoDto?> GetByNombreAsync(string nombre)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<ProductoDto>> ObtenerProductosStockBajoAsync()
        {
            throw new NotImplementedException();
        }

        public Task<ProductoDto?> SearchByCodigoAsync(string codigo)
        {
            throw new NotImplementedException();
        }

        public Task<ProductoDto?> SearchByNombreAsync(string nombre)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateAsync(int id, ProductoUpdateDto dto)
        {
            throw new NotImplementedException();
        }
    }
}
