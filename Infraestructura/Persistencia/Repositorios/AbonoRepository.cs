using Aplicacion.Interfaces.Repositorios;
using Dominio.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infraestructura.Persistencia.Repositorios
{
    public class AbonoRepository : GenericRepository<TblAbono>,IAbonoRepository
    {
        public AbonoRepository(BdTiendaContext context) : base (context)
        {
            
        }
    }
}
