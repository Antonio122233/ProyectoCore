using Dominio.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Aplicacion.Interfaces.Repositorios
{
    public interface ITipoPagoRepository : IGenericRepository<TblTipoPago>
    {
        Task<TblTipoPago?> GetByNombreAsync(string nombre);
    }
}
