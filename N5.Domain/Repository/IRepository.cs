using N5.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace N5.Domain.Repository
{
    public interface IRepository<T> where T : EntityBase
    {
        IQueryable<T> All { get; }

        Task<T> Find(int id);

        void Insert(T entity);

        void Update(T entity);

        void Delete(int id);
    }
}
