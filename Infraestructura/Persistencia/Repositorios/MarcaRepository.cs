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
        public async Task<TblMarca?> GetByNombreAsync(string nombre)
        {
            return await _dbSet.FirstOrDefaultAsync(x => x.Nombre == nombre);
        }
    }
}
