using Aplicacion.DTOs.Venta;
using Aplicacion.Interfaces;
using Aplicacion.Interfaces.Repositorios;
using Aplicacion.Interfaces.Servicios;
using Dominio.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Aplicacion.Servicios
{
    public class VentaService : IVentaService
    {
        private readonly IVentaRepository _repo;
       
        private readonly ITipoPagoRepository _repoTipoPago;
        private readonly IProductoRepository _repoProducto;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IClienteRepository _repoCliente;

        public VentaService(IVentaRepository repo,
      
        ITipoPagoRepository repoTipoPago,
        IProductoRepository repoProducto,
        IClienteRepository repoCliente,
        IUnitOfWork unitOfWork)
        {
            _repo = repo;            
            _repoTipoPago = repoTipoPago;
            _repoProducto = repoProducto;
            _unitOfWork = unitOfWork;
            _repoCliente = repoCliente;
        }

        public async Task<VentaDto> CreateAsync (VentaCreateDto dto)
        {
            TblVenta? venta = null;
            var detallesVenta = new List<TblDetalleVenta>();

            if (dto.Detalles == null || !dto.Detalles.Any())
            {
                throw new Exception(
                    "ERROR_DE_VALIDACION: Debe agregar al menos un producto");
            }

            var tipoPago = await _repoTipoPago.GetByIdAsync(Convert.ToInt32(dto.IdTipoPago));
            if (tipoPago == null )
            {
                throw new Exception("ERROR_DE_VALIDACION: El tipo de pago no existe");
            }

            var cliente = await _repoCliente.GetByIdAsync(dto.IdCliente);

            if (cliente == null)
            {
                throw new Exception( "ERROR_DE_VALIDACION: El cliente no existe");
            }

            try
            {
                decimal totalVenta = 0;
                await _unitOfWork.BeginTransactionAsync();

                var idsProductos = dto.Detalles .Select(x => x.IdProducto).Distinct().ToList();

                var productos =(await _repoProducto.GetByIdsAsync(idsProductos))
                    .ToList();

                if (!productos.Any())
                {
                    throw new Exception(
                        "ERROR_DE_VALIDACION: Debe enviar productos válidos");
                }

                if (productos.Count != idsProductos.Count)
                {
                    throw new Exception(
                        "ERROR_DE_VALIDACION: Uno o más productos no existen");
                }


                //validaciones de negocio
                foreach (var detalle in dto.Detalles)
                {
                    var producto = productos.First(
                        x => x.IdProducto == detalle.IdProducto);

                    if (producto.ExistenciaActual < detalle.Cantidad)
                    {
                        throw new Exception(
                            $"ERROR_DE_VALIDACION: Stock insuficiente para {producto.Nombre}");
                    }
                }


                foreach (var detalle in dto.Detalles)
                {
                    var producto = productos.First(x => x.IdProducto == detalle.IdProducto);

                    var subtotal = detalle.Cantidad * detalle.PrecioVenta;

                    totalVenta = totalVenta + subtotal;

                    detallesVenta.Add(new TblDetalleVenta
                    {
                         IdProducto =detalle.IdProducto, 
                         Cantidad = detalle.Cantidad,
                         PrecioVenta = detalle.PrecioVenta ,
                         EstadoRegistro =true
                    });

                    producto.ExistenciaActual = producto.ExistenciaActual - detalle.Cantidad;
                }

                foreach (var producto in productos)
                {
                    await _repoProducto.UpdateAsync(producto);
                }

                venta = new TblVenta
                {
                    IdCliente = dto.IdCliente,

                    TipoVenta = "CONTADO",

                    TotalVenta = totalVenta,

                    MontoPagado = totalVenta,

                    IdTipoPago = dto.IdTipoPago,

                    EstadoPago = "PAGADO",

                    FechaVenta = DateTime.Now,

                    EstadoRegistro = true,

                    TblDetalleVenta = detallesVenta
                };


                await _repo.AddAsync(venta);
                await _unitOfWork.CommitTransactionAsync();


                return new VentaDto
                {
                    IdVenta = venta!.IdVenta,

                    IdCliente = venta.IdCliente,

                    IdTipoPago = venta.IdTipoPago,

                    NombreTipoPago = tipoPago.Nombre,

                    TipoVenta = venta.TipoVenta,

                    TotalVenta = venta.TotalVenta,

                    MontoPagado = venta.MontoPagado,

                    EstadoPago = venta.EstadoPago,

                    FechaVenta = venta.FechaVenta,

                    EstadoRegistro = venta.EstadoRegistro,

                    NombreCliente = $"{cliente.Nombre} {cliente.Apellido}",

                    Detalles = detallesVenta.Select(x =>
                        new DetalleVentaDto
                        {
                            IdProducto = x.IdProducto,
                            Cantidad = x.Cantidad,
                            PrecioVenta = x.PrecioVenta,
                            SubTotal = x.Cantidad * x.PrecioVenta,
                            EstadoRegistro = x.EstadoRegistro
                        }).ToList()
                };
            }
            catch (Exception ex) 
            {
                await _unitOfWork.RollbackTransactionAsync();
                throw new Exception($"INTERNAL_ERROR:{ex.InnerException?.Message ?? ex.Message}");
            }           
        }

    }
}
