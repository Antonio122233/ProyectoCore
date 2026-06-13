using Aplicacion.Interfaces.Repositorios;
using Domnio.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infraestructura.Persistencia.Repositorios
{
    public class ProveedorRepository :GenericRepository<TblProveedore>, IProveedorRepository
    {
        public ProveedorRepository(BdTiendaContext contenxt) : base(contenxt)
        {
                
        }

        public  async Task<TblProveedore?> GetByNombreAsync(string nombre)
        {
            return await _dbSet.FirstOrDefaultAsync(x => x.Nombre == nombre);
        }
    }
}
