using System;
using System.Collections.Generic;
using System.Text;

namespace GymHelper.Data.Interfaces
{
    public interface IUnitOfWork
    {
        IRepository<TEntity> Repository<TEntity>()
            where TEntity : class;
    }
}
