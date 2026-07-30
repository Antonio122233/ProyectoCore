using Aplicacion.Interfaces.Repositorios;
using Dominio.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infraestructura.Persistencia.Repositorios
{
    public class ProductoRepository : GenericRepository<TblProducto>, IProductoRepository
    {
        public ProductoRepository(BdTiendaContext context) : base(context)
        {

        }

        public async Task<IEnumerable<TblProducto>> GetActiveAsync()
        {            
            return await _dbSet.Where(x => x.EstadoRegistro)
               .ToListAsync();
        }

        public async Task<TblProducto?> GetByNombreAsync(string nombre)
        {
            return await _dbSet.FirstOrDefaultAsync(x => x.Nombre == nombre);
        }

        public async Task<TblProducto?> GetByCategoriaAsync(int idCategoria)
        {
            return await _dbSet.FirstOrDefaultAsync(x => x.IdCategoria == idCategoria); 
        }

        public async Task<TblProducto?> GetByMarcaAsync(int idMarca)
        {
            return await _dbSet.FirstOrDefaultAsync(x => x.IdMarca == idMarca);
        }

        public async Task<IEnumerable<TblProducto>> ObtenerProductosStockBajoAsync()
        {
            return await _dbSet.Where(x=>x.ExistenciaActual<=x.ExistenciaMinima).ToListAsync();
        }

        public async Task<TblProducto?> SearchByCodigoAsync(string codigo)
        {
            return await _dbSet.FirstOrDefaultAsync(x => x.CodigoProducto == codigo);
        }

        public async Task<IEnumerable<TblProducto>> GetByIdsAsync(IEnumerable<int> ids)
        {
            return await _dbSet.Where(x => ids.Contains(x.IdProducto))
                  .ToListAsync();
        }
    }
}
