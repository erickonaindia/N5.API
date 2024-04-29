using Microsoft.EntityFrameworkCore;
using N5.Domain.Entities;
using N5.Domain.Repository;

namespace N5.Infrastructure.Repository
{
    public class Repository<T> : IRepository<T> where T : EntityBase
    {
        private readonly IUnitOfWork _unitOfWork;

        private DbSet<T> GetDbSet()
        {
            return _unitOfWork.GetSet<T>();
        }

        public Repository(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IQueryable<T> All => GetDbSet();

        public void Insert(T entity)
        {
            entity.Id = (int)entity.GetType().GetProperty("Id")!.GetValue(entity, null)!;
            GetDbSet().Add(entity);
        }

        public Task<T> Find(int id)
        {
            return GetDbSet().FirstOrDefaultAsync(x => x.Id == id)!;
        }

        public void Update(T entity)
        {
            _unitOfWork.SetModified<T>(entity);
        }

        public void Delete(int id)
        {
            T existing = GetDbSet().FirstOrDefault(x => x.Id == id)!;

            if (existing != null)
            {
                GetDbSet().Remove(existing);
            }
        }
    }
}
