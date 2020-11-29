using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace GymHelper.Data.Interfaces
{
    public interface IRepository<TEntity> where TEntity : class
    {
        Task<bool> Add(TEntity entity);
        Task<bool> Delete(TEntity entity);
        Task<List<TEntity>> ReadAll();
        Task<List<TEntity>> ReadAllByCondition(Func<TEntity, bool> condition);
        Task<List<TEntity>> ReadAllByConditionWithInclude<TProp>(Func<TEntity, bool> condition,
            Expression<Func<TEntity, TProp>> include);
        Task<TEntity> ReadFirstByCondition(Func<TEntity, bool> condition);
        Task<TEntity> ReadFirstByConditionWithInclude<TProp>(Func<TEntity, bool> condition,
            Expression<Func<TEntity, TProp>> include);
        Task<bool> CheckIfExistByCondition(Func<TEntity, bool> condition);
        Task<bool> Update(TEntity entity);
    }
}
