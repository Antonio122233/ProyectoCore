using Aplicacion.DTOs.Abono;
using Aplicacion.Interfaces;
using Aplicacion.Interfaces.Repositorios;
using Aplicacion.Interfaces.Servicios;
using Dominio.Enums;
using Dominio.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Aplicacion.Servicios
{
    public class AbonoService : IAbonoService
    {
        private readonly IAbonoRepository _repo;
        private readonly IVentaRepository _repoVenta;
        private readonly ITipoPagoRepository _repoTipoPago;
        private readonly IUnitOfWork _uniUnitOfWork;

        public AbonoService(IAbonoRepository repo, IVentaRepository repoVenta, ITipoPagoRepository repoTipoPago, IUnitOfWork unitOfWork)
        {
            _repo = repo;
            _repoTipoPago = repoTipoPago;
            _repoVenta = repoVenta;
            _uniUnitOfWork = unitOfWork;
        }

        public async Task<AbonoDto> CreateAsync(AbonoCreateDto dto)
        {
            var venta = await _repoVenta.GetByIdAsync(dto.IdVenta);
            if (venta == null)
            {
                throw new KeyNotFoundException("La venta no existe");
            }

            if (venta.TipoVenta != TipoVenta.CREDITO.ToString())
            {
                throw new Exception("ERROR_DE_VALIDACION: Solo se permiten abonos en ventas a crédito");
            }

            //validar si la venta ya esta pagada.
            if (venta.EstadoPago == EstadoPago.PAGADO.ToString())
            {
                throw new Exception(
                    "ERROR_DE_VALIDACION: La venta ya se encuentra pagada");
            }

            var tipoPago = await _repoTipoPago.GetByIdAsync(dto.IdTipoPago);

            if (tipoPago == null)
            {
                throw new Exception(
                    "ERROR_DE_VALIDACION: El tipo de pago no existe");
            }

            if (dto.MontoAbono <= 0)
            {
                throw new Exception(
                    "ERROR_DE_VALIDACION: El monto debe ser mayor que cero");
            }
            
            var saldoPendiente = venta.TotalVenta - venta.MontoPagado;

            if (dto.MontoAbono > saldoPendiente)
            {
                throw new Exception(
                    $"ERROR_DE_VALIDACION: El abono excede el saldo pendiente de {saldoPendiente}");
            }

            try
            {
                await _uniUnitOfWork.BeginTransactionAsync();
                //resto del codigo
                var abono = new TblAbono
                {
                    IdVenta = dto.IdVenta,
                    FechaAbono = DateTime.Now,
                    MontoAbono = dto.MontoAbono,
                    IdTipoPago = dto.IdTipoPago,
                    Observaciones = dto.Observaciones,
                    EstadoRegistro = true
                };

                venta.MontoPagado += dto.MontoAbono;
                var nuevoSaldo = venta.TotalVenta - venta.MontoPagado;
                if (nuevoSaldo <= 0)
                {
                    venta.EstadoPago =
                        EstadoPago.PAGADO.ToString();
                }
                else
                {
                    venta.EstadoPago =
                        EstadoPago.PENDIENTE.ToString();
                }

                await _repo.AddAsync(abono);

                await _repoVenta.UpdateAsync(venta);

                await _uniUnitOfWork.CommitTransactionAsync();

                return new AbonoDto
                {
                    IdAbono = abono.IdAbono,

                    IdVenta = abono.IdVenta,

                    MontoAbono = abono.MontoAbono,

                    IdTipoPago = abono.IdTipoPago,

                    NombreTipoPago = tipoPago.Nombre,

                    FechaAbono = abono.FechaAbono,

                    Observaciones = abono.Observaciones,

                    EstadoRegistro = abono.EstadoRegistro,

                    NuevoMontoPagado = venta.MontoPagado,

                    NuevoSaldoPendiente = nuevoSaldo,

                    EstadoPago = venta.EstadoPago
                };
            }
            catch (Exception)
            {

                await _uniUnitOfWork.RollbackTransactionAsync();
                throw;
            }           
        }
    }
}
