using Aplicacion.DTOs.Proveedor;
using Aplicacion.DTOs.TipoPago;
using Aplicacion.Interfaces.Repositorios;
using Aplicacion.Interfaces.Servicios;
using Domnio.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Aplicacion.Servicios
{
    public class TipoPagoService : ITipoPagoService
    {
        private readonly ITipoPagoRepository _repo;

        public TipoPagoService(ITipoPagoRepository repo)
        {
            _repo = repo;
        }

        public async Task<TipoPagoDto> CreateAsync(TipoPagoCreateDto dto)
        {
            if (string.IsNullOrWhiteSpace(dto.Nombre))
                throw new ArgumentException("El nombre es obligatorio");

            var nuevo = new TblTipoPago
            {
                Nombre = dto.Nombre,
                Descripcion = dto.Descripcion
            };

            await _repo.AddAsync(nuevo);

            return new TipoPagoDto
            {
                Id = nuevo.IdTipoPago,
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

            await _repo.DeleteAsync(entidad);
            return true;
        }

        public async Task<IEnumerable<TipoPagoDto>> GetAllAsync()
        {
            var TiposPagos = await _repo.GetAllAsync();

            return TiposPagos.Select(
                x => new TipoPagoDto
                {
                    Descripcion = x.Descripcion,
                    EstadoRegistro = x.EstadoRegistro,
                    Id = x.IdTipoPago,
                    Nombre = x.Nombre,
                });
        }

        public async Task<TipoPagoDto?> GetByIdAsync(int id)
        {
            if (id <= 0)
                throw new ArgumentException("Id Incorrecto");
            var tipoPago = await _repo.GetByIdAsync(id);

            if (tipoPago is null) return null;

            return new TipoPagoDto
            {
                Descripcion = tipoPago.Descripcion,
                EstadoRegistro = tipoPago.EstadoRegistro,
                Id = tipoPago.IdTipoPago,
                Nombre = tipoPago.Nombre,
            };
        }

        public async Task<TipoPagoDto?> GetByNombreAsync(string nombre)
        {
            if (string.IsNullOrEmpty(nombre))
            {
                throw new ArgumentException("Envié un nombre");
            }

            var nombreTipoPago = await _repo.GetByNombreAsync(nombre);
            if (nombreTipoPago is null) return null;

            return new TipoPagoDto
            {
                Descripcion = nombreTipoPago.Descripcion,
                EstadoRegistro = nombreTipoPago.EstadoRegistro,
                Id = nombreTipoPago.IdTipoPago,
                Nombre = nombreTipoPago.Nombre,
            };
        }

        public async Task<bool> UpdateAsync(int id, TipoPagoUpdateDto dto)
        {
            var existe = await _repo.GetByIdAsync(id);
            if (existe is null) return false;

            if (string.IsNullOrWhiteSpace(dto.Nombre))
            {
                throw new ArgumentException("El nombre de la forma de pago no puede estar vacio");
            }

            existe.Nombre = dto.Nombre;
            existe.Descripcion = dto.Descripcion;
            existe.EstadoRegistro = dto.EstadoRegistro;

            await _repo.UpdateAsync(existe);
            return true;
        }     
    }
}
