using Aplicacion.Interfaces.Repositorios;
using Dominio.Models;
using Infraestructura.Persistencia;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using static System.Runtime.InteropServices.JavaScript.JSType;


namespace Infraestructura.Persistencia.Repositorios
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        //COMO PRIVATE, deberia acceder al context con el set en que cada repo que implemente el generico

        //private readonly BdTiendaContext _context;
        //private readonly DbSet<T> _dbSet;

        //return await _context.Set<TblMarca>()
        //.FirstOrDefaultAsync(m => m.Nombre == nombre)


        protected readonly BdTiendaContext _context;
        protected readonly DbSet<T> _dbSet;

        public GenericRepository(BdTiendaContext context)
        {
            _context = context;
            _dbSet = context.Set<T>();
        }
       
        public async Task<IEnumerable<T>> GetAllAsync()        
            => await _dbSet.ToListAsync();

        public async Task<T?> GetByIdAsync(int id)
            =>await _dbSet.FindAsync(id);

        public async Task UpdateAsync(T entity)
        {
           _dbSet.Update(entity);
            await _context.SaveChangesAsync();
        }

        public async Task AddAsync(T entity)
        {
           await _dbSet.AddAsync(entity);
           await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(T entity)
        {
            _dbSet.Remove(entity);
            await _context.SaveChangesAsync();
        }      
    }
}
