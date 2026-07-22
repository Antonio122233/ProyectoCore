using Aplicacion.DTOs.Compra;
using Aplicacion.Interfaces.Repositorios;
using System;
using System.Collections.Generic;
using System.Text;

namespace Aplicacion.Interfaces
{
    public class CompraService : ICompraService
    {
        private readonly ICompraRepository _repo;

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

        public CompraService(ICompraRepository repo)
        {
            _repo = repo;
        }

        public Task<CompraDto> CreateAsync(CompraCreateDto dto)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<CompraDto?> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<CompraDto>> GetByProveedorAsync(int idProveedor)
        {
            throw new NotImplementedException();
        }
    }
}
