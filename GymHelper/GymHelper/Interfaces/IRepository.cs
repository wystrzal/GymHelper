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
        Task<List<TEntity>> ReadAllByCondition(Expression<Func<TEntity, bool>> condition, int take, int skip = 0);
        Task<List<TEntity>> ReadAllByCondition<TKey>(Expression<Func<TEntity, bool>> condition,
            Expression<Func<TEntity, TKey>> orderBy, int take, int skip = 0, bool orderASC = true);
        Task<List<TEntity>> ReadAllByConditionWithInclude<TProp>(Expression<Func<TEntity, bool>> condition,
            Expression<Func<TEntity, TProp>> include, int take, int skip = 0);
        Task<TEntity> ReadFirstByCondition(Expression<Func<TEntity, bool>> condition);
        Task<TEntity> ReadFirstByCondition<TKey>(Expression<Func<TEntity, bool>> condition,
            Expression<Func<TEntity, TKey>> orderBy, bool orderASC = true);
        Task<TEntity> ReadFirstByConditionWithInclude<TProp>(Expression<Func<TEntity, bool>> condition,
            Expression<Func<TEntity, TProp>> include);
        Task<int> ReadDataCount(Expression<Func<TEntity, bool>> condition);
        Task<bool> CheckIfExistByCondition(Expression<Func<TEntity, bool>> condition);
        Task Update(TEntity entity);
    }
}
