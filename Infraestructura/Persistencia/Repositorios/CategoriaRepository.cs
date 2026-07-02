using Aplicacion.Interfaces.Repositorios;
using Dominio.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infraestructura.Persistencia.Repositorios
{
    public class CategoriaRepository : GenericRepository<TblCategoria> , ICategoriaRepository
    {
        public CategoriaRepository(BdTiendaContext context) : base (context)
        {
               
        }

        public async Task<IEnumerable<TblCategoria>> GetActiveAsync()
        {
            return await _dbSet.Where(x => x.EstadoRegistro).ToListAsync();
        }

        public async Task<TblCategoria?> GetByNombreAsync(string nombre)
        {
            return await _dbSet.FirstOrDefaultAsync(x => x.Descripcion == nombre);
        }
    }
}
