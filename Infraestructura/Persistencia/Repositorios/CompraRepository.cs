using Aplicacion.Interfaces.Repositorios;
using Dominio.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infraestructura.Persistencia.Repositorios
{
    public class CompraRepository : GenericRepository<TblCompra>, ICompraRepository
    {
        public CompraRepository(BdTiendaContext context) : base(context)
        {
        }

        public async Task<IEnumerable<TblCompra>> GetByFechaAsync(DateTime Fecha)
        {
            //el .date compara solo las fechas.                                   
            return await _dbSet
                .Include(x => x.IdProveedorNavigation)
                .Include(x => x.IdTipoPagoNavigation)
                .Include(x => x.TblDetalleCompras)
                .Where(x => x.FechaCompra.Date == Fecha.Date)
                .AsNoTracking()
                .TagWith("--OBTENER COMPRAS POR FECHA")
                .ToListAsync();
        }

        public async Task<IEnumerable<TblCompra>> GetByProveedor(int idProveedor)
        {
            return await _dbSet
                .Include(x => x.IdProveedorNavigation)
                .Include(x => x.IdTipoPagoNavigation)
                .Include(x => x.TblDetalleCompras)
               .Where(c => c.IdProveedor == idProveedor && c.EstadoRegistro)
               .AsNoTracking()
               .TagWith("--OBTENER COMPRAS POR PROVEEDOR")
               .ToListAsync();
        }
    }
}
