using Aplicacion.Interfaces;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infraestructura.Persistencia
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly BdTiendaContext _context;
        private IDbContextTransaction? _transaction;

        public UnitOfWork(BdTiendaContext context)
        {
            _context = context;
        }

        public async Task BeginTransactionAsync()
        {
           _transaction = await _context.Database.BeginTransactionAsync();
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public async Task CommitTransactionAsync()
        {
            if (_transaction != null)
            {
                await _context.SaveChangesAsync();
                await _transaction.CommitAsync();
                await _transaction.DisposeAsync();
                //No hay una transacción activa.
                _transaction = null;
            }
        }

        public async Task RollbackTransactionAsync()
        {
            if (_transaction!= null)
            {
                await _transaction.RollbackAsync();
                await _transaction.DisposeAsync();
                //No hay una transacción activa.
                _transaction = null;
            }
        }

        public void Dispose()
        {
            _transaction?.Dispose();
        }
    }
}
