using Aplicacion.Interfaces.Repositorios;
using Dominio.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infraestructura.Persistencia.Repositorios
{
    public class VentaRepository : GenericRepository <TblVenta>, IVentaRepository
    {
        public VentaRepository(BdTiendaContext context) : base (context)
        {
            
        }

        public async Task<TblVenta?> GetVentaCompletaAsync(int id)
        {
            return await _dbSet
                .AsNoTracking()
                .Include(x => x.IdClienteNavigation)
                .Include(x => x.IdTipoPagoNavigation)
                .Include(x => x.TblDetalleVenta)
                .FirstOrDefaultAsync(x => x.IdVenta == id);
        }

        public async Task<IEnumerable<TblVenta>> GetVentasCompletasAsync()
        {
            return await _dbSet
                .AsNoTracking()
                .Include(x => x.IdClienteNavigation)
                .Include(x => x.IdTipoPagoNavigation)
                .Include(x => x.TblDetalleVenta)
                .OrderByDescending(x => x.IdVenta)
                .ToListAsync();
        }

    }
}
