using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace GymHelper.Data.Interfaces
{
    public interface IRepository<TEntity> where TEntity : class
    {
        Task Add(TEntity entity);
        Task Delete(TEntity entity);
        Task<List<TEntity>> ReadAll();
        Task<List<TEntity>> ReadAllByCondition(Func<TEntity, bool> condition);
        Task<List<TEntity>> ReadAllByCondition(Func<TEntity, bool> condition, int take, int skip = 0);
        Task<List<TEntity>> ReadAllByCondition<TKey>(Func<TEntity, bool> condition, Func<TEntity, TKey> orderBy, bool orderASC = true);
        Task<List<TEntity>> ReadAllByCondition<TKey>(Func<TEntity, bool> condition,
            Func<TEntity, TKey> orderBy, int take, bool orderASC = true);
        Task<List<TEntity>> ReadAllByConditionWithInclude<TProp>(Func<TEntity, bool> condition,
            Expression<Func<TEntity, TProp>> include);
        Task<TEntity> ReadFirstByCondition(Func<TEntity, bool> condition);
        Task<TEntity> ReadFirstByCondition<TKey>(Func<TEntity, bool> condition,
            Func<TEntity, TKey> orderBy, bool orderASC = true);
        Task<TEntity> ReadFirstByConditionWithInclude<TProp>(Func<TEntity, bool> condition,
            Expression<Func<TEntity, TProp>> include);
        Task<bool> CheckIfExistByCondition(Func<TEntity, bool> condition);
        Task Update(TEntity entity);
    }
}
