using Aplicacion.DTOs.Producto;
using Aplicacion.Interfaces;
using Aplicacion.Interfaces.Repositorios;
using Dominio.Models;

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

        public async Task<ProductoDto?> GetByCategoriaAsync(int idCategoria)
        {
            if (idCategoria <= 0)
                throw new ArgumentException("ID inválido");

            var producto = await _repo.GetByCategoriaAsync(idCategoria);

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

        public async Task<ProductoDto?> GetByMarcaAsync(int idMarca)
        {
            if (idMarca <= 0)
                throw new ArgumentException("ID inválido");

            var producto = await _repo.GetByCategoriaAsync(idMarca);

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

        public async Task<ProductoDto?> GetByNombreAsync(string nombre)
        {
            if (string.IsNullOrWhiteSpace(nombre))
            {
                throw new ArgumentException("Envie un nombre");
            }

            var producto = await _repo.GetByNombreAsync(nombre);
            if (producto is null)
            {
                return null;
            }

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

        public async Task<IEnumerable<ProductoDto>> ObtenerProductosStockBajoAsync()
        {
            var productos = await _repo.ObtenerProductosStockBajoAsync();
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

        public async Task<ProductoDto?> SearchByCodigoAsync(string codigo)
        {
            if (string.IsNullOrWhiteSpace(codigo))
            {
                throw new ArgumentException("Envie un codigo");
            }

            var producto = await _repo.GetByNombreAsync(codigo);
            if (producto is null)
            {
                return null;
            }

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
      
        public async Task<bool> UpdateAsync(int id, ProductoUpdateDto dto)
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

            existe.Color = dto.Color;
            existe.Costo = dto.Costo;
            existe.Descripcion = dto.Descripcion;
            existe.ExistenciaMinima = dto.ExistenciaMinima;
            existe.ExistenciaActual = dto.ExistenciaActual;
            existe.EstadoRegistro = dto.EstadoRegistro;
            existe.FechaRegistro = dto.FechaRegistro;
            existe.Talla = dto.Talla;
            existe.CodigoProducto = dto.CodigoProducto;            
            existe.FechaBaja = dto.FechaBaja;
            existe.Nombre = dto.Nombre;
            existe.Material =dto.Material;
            existe.Precio = dto.Precio;
            existe.IdCategoria = dto.IdCategoria;
            existe.IdMarca = dto.IdMarca;


            await _repo.UpdateAsync(existe);
            return true;
        }
    }
}
