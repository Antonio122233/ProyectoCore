using Aplicacion.Interfaces.Repositorios;
using Dominio.Models;
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
    }
}
