using Domnio.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Aplicacion.Interfaces
{
    public interface IMarcaRepository : IGenericRepository<TblMarca>
    {
        Task<TblMarca?> GetByNombreAsync(string nombre);
    }
}
