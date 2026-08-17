using Aplicacion.Interfaces.Repositorios;
using Dominio.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infraestructura.Persistencia.Repositorios
{
    public class ClienteRepository : GenericRepository<TblCliente> , IClienteRepository
    {
        public ClienteRepository( BdTiendaContext context) : base (context)
        {
            
        }
    }
}
