using Microsoft.EntityFrameworkCore;
using N5.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace N5.Domain.Repository
{
    public interface IUnitOfWork : IDisposable
    {
        void Commit();

        Task CommitAsync();

        void SetModified<T>(T entity) where T : EntityBase;

        DbSet<T> GetSet<T>() where T : EntityBase;
    }
}
