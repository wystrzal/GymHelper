using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace GymHelper.Data.Interfaces
{
    public interface IUnitOfWork
    {
        IRepository<TEntity> Repository<TEntity>()
            where TEntity : class;

        Task<bool> SaveChanges();
    }
}
