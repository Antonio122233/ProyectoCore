using Aplicacion.Interfaces.Repositorios;
using Dominio.Models;
using Infraestructura.Persistencia;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infraestructura.Persistencia.Repositorios
{
    public class MarcaRepository : GenericRepository<TblMarca>, IMarcaRepository
    {
        public MarcaRepository(BdTiendaContext contex) : base(contex) 
        {
            
        }

        public async Task<IEnumerable<TblMarca>> GetActiveAsync()
        {
            // x=>x.EstadoRegistro es true, no necesito poner el  == true
            return await _dbSet.Where(x=>x.EstadoRegistro)
                .ToListAsync();
        }

        public async Task<TblMarca?> GetByNombreAsync(string nombre)
        {
            return await _dbSet.FirstOrDefaultAsync(x => x.Nombre == nombre);
        }
    }
}
