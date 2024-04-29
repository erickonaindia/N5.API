using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;
using N5.Domain.Entities;
using N5.Domain.Repository;
using N5.Infrastructure.Persistence;
using N5.Infrastructure.Repository;
using System;

namespace N5.Infrastructure.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private ApplicationDbContext _context { get; }

        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
        }
        public void Commit()
        {
            _context.SaveChanges();
        }

        public async Task CommitAsync()
        {
            await _context.SaveChangesAsync();
        }

        public void SetModified<T>(T entity) where T : EntityBase
        {
            GetSet<T>().Attach(entity);
            _context.Entry(entity).State = EntityState.Modified;
        }

        public DbSet<T> GetSet<T>() where T : EntityBase
        {
            return _context.Set<T>();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                _context.Dispose();
            }
        }
    }
}
