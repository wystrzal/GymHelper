using GymHelper.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace GymHelper.Data.Services
{
    class UnitOfWork : IUnitOfWork
    {
        public IRepository<TEntity> Repository<TEntity>()
            where TEntity : class
        {
            var repositories = new Dictionary<Type, object>();

            var entityType = typeof(TEntity);

            if (!repositories.ContainsKey(entityType))
            {
                var repository = Activator.CreateInstance(typeof(Repository<>).MakeGenericType(entityType));
                repositories.Add(entityType, repository);
            }

            return (IRepository<TEntity>)repositories[entityType];
        }
    }
}
