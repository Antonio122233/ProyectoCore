using Aplicacion.DTOs.Compra;
using Aplicacion.Interfaces;
using Aplicacion.Interfaces.Repositorios;
using Dominio.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Aplicacion.Servicios
{
    public class CompraService : ICompraService
    {
        private readonly ICompraRepository _repo;
        private readonly IProductoRepository _repProducto;
        private readonly IUnitOfWork _unitOfWork;

        public CompraService(ICompraRepository repo, IProductoRepository repProducto, IUnitOfWork unitOfWork)
        {
            _repo = repo;
            _repProducto = repProducto;
            _unitOfWork = unitOfWork;
        }

        /// <summary>
        /// Crea una compra
        /// </summary>
        /// <param name="dto">CompraCreateDto</param>
        /// <returns>CompraDto</returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<CompraDto> CreateAsync(CompraCreateDto dto)
        {
            //validaciones                        
            try
            {
                decimal totalCompra = 0;
                var detallesCompra = new List<TblDetalleCompra>();               

                await _unitOfWork.BeginTransactionAsync();

                var idsProductos = dto.Detalles.
                    Select(x => x.IdProducto).Distinct().ToList();

                var productos = (await _repProducto.GetByIdsAsync(idsProductos)).ToList();

                if (!productos.Any())
                {
                    throw new Exception("ERROR_DE_VALIDACION: Debe enviar los productos");
                }

                if (productos.Count() != idsProductos.Count() ) 
                {
                    throw new Exception("ERROR_DE_VALIDACION: Uno o más productos no existen");
                }

                foreach (var detalle in dto.Detalles)
                {
                    var producto = productos.First(x => x.IdProducto == detalle.IdProducto);
                    var subtotal = detalle.Cantidad * detalle.PrecioCompra;
                    totalCompra = totalCompra + subtotal;
                    detallesCompra.Add(new()
                    {
                        IdProducto = detalle.IdProducto,
                        PrecioCompra = detalle.PrecioCompra,
                        Cantidad = detalle.Cantidad,
                        Subtotal = subtotal,
                        EstadoRegistro = true
                    });
                    producto.ExistenciaActual = producto.ExistenciaActual + detalle.Cantidad;                  
                }

                foreach (var produto in productos)
                {
                    await _repProducto.UpdateAsync(produto);
                }

                var compra =new TblCompra
                {
                    IdProveedor = dto.IdProveedor,
                    IdTipoPago = dto.IdTipoPago,
                    NumFactura = dto.NumFactura,
                    Observaciones = dto.Observaciones,
                    FechaCompra = dto.FechaCompra,
                    EstadoRegistro = true,
                    TotalCompra = totalCompra,

                    TblDetalleCompras = detallesCompra
                };

                //Procesar detalles
                //Calcular total
                //Crear Compra
                //Actualizar stock
                //Guardar Compra
                await _repo.AddAsync(compra);
                await _unitOfWork.CommitTransactionAsync();
            }
            catch (Exception ex)
            {
                await _unitOfWork.RollbackTransactionAsync();
                throw;
            }

            return null!;
        }

        public async Task<IEnumerable<CompraDto>> GetByFechaAsync(DateTime fecha)
        {
            var compras = await _repo.GetByFechaAsync(fecha);
            return compras.Select(x => new CompraDto
            {
                IdCompra = x.IdCompra,
                IdProveedor = x.IdProveedor,
                NumFactura = x.NumFactura,
                TotalCompra = x.TotalCompra,
                FechaCompra = x.FechaCompra,
                EstadoRegistro = x.EstadoRegistro,
                NombreProveedor = x.IdProveedorNavigation?.Nombre,
                NombreTipoPago = x.IdTipoPagoNavigation?.Descripcion,
                Detalles = x.TblDetalleCompras.Select
                (x => new DetalleCompraDto
                {
                    IdProducto = x.IdProducto,
                    Cantidad = x.Cantidad,
                    PrecioCompra = x.PrecioCompra,
                    EstadoRegistro = x.EstadoRegistro,
                    Subtotal = x.Subtotal
                }).ToList()
            });
        }

        public async Task<IEnumerable<CompraDto>> GetByProveedorAsync(int idProveedor)
        {
            var compras = await _repo.GetByProveedor(idProveedor);
            if (compras is null) return null;
            if (!compras.Any()) return null;

            return compras.Select(
                x => new CompraDto
                {
                    IdCompra = x.IdCompra,
                    IdProveedor = x.IdProveedor,
                    NumFactura = x.NumFactura,
                    TotalCompra = x.TotalCompra,
                    FechaCompra = x.FechaCompra,
                    EstadoRegistro = x.EstadoRegistro,
                    NombreProveedor = x.IdProveedorNavigation?.Nombre,
                    NombreTipoPago = x.IdTipoPagoNavigation?.Descripcion,
                    Detalles = x.TblDetalleCompras.Select(
                        y => new DetalleCompraDto
                        {
                            Cantidad = y.Cantidad,
                            EstadoRegistro = y.EstadoRegistro,
                            IdProducto = y.IdProducto,
                            PrecioCompra = y.PrecioCompra,
                            Subtotal = y.Subtotal
                        }
                        ).ToList()
                }
                ).ToList();

        }

        public async Task<CompraDto?> GetByIdAsync(int id)
        {
            var compra = await _repo.GetByIdAsync(id);
            if (compra is null) return null;
            return new CompraDto
            {
                IdCompra = compra.IdCompra,
                IdProveedor = compra.IdProveedor,
                NumFactura = compra.NumFactura,
                TotalCompra = compra.TotalCompra,
                FechaCompra = compra.FechaCompra,
                EstadoRegistro = compra.EstadoRegistro,
                NombreProveedor = compra.IdProveedorNavigation?.Nombre,
                NombreTipoPago = compra.IdTipoPagoNavigation?.Descripcion,
                Detalles = compra.TblDetalleCompras.Select(
                    y => new DetalleCompraDto
                    {
                        Cantidad = y.Cantidad,
                        EstadoRegistro = y.EstadoRegistro,
                        IdProducto = y.IdProducto,
                        PrecioCompra = y.PrecioCompra,
                        Subtotal = y.Subtotal

                    }).ToList()
            };
        }
        public Task<bool> DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }
    }
}
