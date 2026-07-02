using Dominio.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Aplicacion.Interfaces.Repositorios
{
    public interface  ICategoriaRepository : IGenericRepository<TblCategoria> 
    {
        Task<TblCategoria?> GetByNombreAsync (string nombre);
        Task<IEnumerable<TblCategoria>> GetActiveAsync();
    }                                         
}
