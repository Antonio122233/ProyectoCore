using Dominio.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Aplicacion.Interfaces.Repositorios
{
    public interface IMarcaRepository : IGenericRepository<TblMarca>
    {
        Task<TblMarca?> GetByNombreAsync(string nombre);
        Task<IEnumerable<TblMarca>> GetActiveAsync();

    }
}
