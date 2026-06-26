using Aplicacion.Interfaces.Repositorios;
using Dominio.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infraestructura.Persistencia.Repositorios
{
    public class TipoPagoRepository : GenericRepository <TblTipoPago> , ITipoPagoRepository
    {
        public TipoPagoRepository(BdTiendaContext context) :base(context) 
        {
            
        }

        public async Task<TblTipoPago?> GetByNombreAsync(string nombre)
        {
            return await _dbSet.FirstOrDefaultAsync(x => x.Nombre == nombre);
        }
    }
}
