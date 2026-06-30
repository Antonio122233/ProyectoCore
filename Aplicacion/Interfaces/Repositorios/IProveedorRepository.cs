using Dominio.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Aplicacion.Interfaces.Repositorios
{
    public interface  IProveedorRepository : IGenericRepository<TblProveedore>
    {
        Task<TblProveedore?> GetByNombreAsync(string nombre);
        Task<IEnumerable<TblProveedore>> GetActiveAsync();
    }
}
