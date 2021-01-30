using GymHelper.Data.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace GymHelper.Data
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        private readonly DataContext dataContext;
        private readonly IAlertService alertService;

        public Repository(DataContext dataContext)
        {
            this.dataContext = dataContext;
            alertService = App.Data.AlertService;
        }

        public async Task Add(TEntity entity)
        {
            try
            {
                await dataContext.Set<TEntity>().AddAsync(entity);
            }
            catch (Exception)
            {
                await alertService.DisplayAlert("Niepowodzenie", "Nie udało się dodać danych.", "Ok");
            }
        }

        public async Task Delete(TEntity entity)
        {
            try
            {
                dataContext.Set<TEntity>().Remove(entity);
            }
            catch (Exception)
            {
                await alertService.DisplayAlert("Niepowodzenie", "Nie udało się usunąć danych.", "Ok");
            }
        }

        public async Task<List<TEntity>> ReadAllByCondition(Expression<Func<TEntity, bool>> condition, int take, int skip = 0)
        {
            try
            {
                return await dataContext.Set<TEntity>().Where(condition).Skip(skip).Take(take).ToListAsync();
            }
            catch (Exception)
            {
                await alertService.DisplayAlert("Niepowodzenie", "Nie udało się pobrać danych.", "Ok");
                return null;
            }
        }

        public async Task<List<TEntity>> ReadAllByCondition<TKey>(Expression<Func<TEntity, bool>> condition,
            Expression<Func<TEntity, TKey>> orderBy, int take, int skip = 0, bool orderASC = true)
        {
            try
            {
                var query = dataContext.Set<TEntity>().Where(condition);

                if (orderASC)
                {
                    query = query.OrderBy(orderBy);
                }
                else
                {
                    query = query.OrderByDescending(orderBy);
                }

                return await query.Skip(skip).Take(take).ToListAsync();
            }
            catch (Exception)
            {
                await alertService.DisplayAlert("Niepowodzenie", "Nie udało się pobrać danych.", "Ok");
                return null;
            }
        }

        public async Task<List<TEntity>> ReadAllByConditionWithInclude<TProp>(Expression<Func<TEntity, bool>> condition,
            Expression<Func<TEntity, TProp>> include, int take, int skip = 0)
        {
            try
            {
                return await dataContext.Set<TEntity>().Include(include).Where(condition).Skip(skip).Take(take).ToListAsync();
            }
            catch (Exception)
            {
                await alertService.DisplayAlert("Niepowodzenie", "Nie udało się pobrać danych.", "Ok");
                return null;
            }
        }

        public async Task<TEntity> ReadFirstByCondition(Expression<Func<TEntity, bool>> condition)
        {
            try
            {
                return await dataContext.Set<TEntity>().Where(condition).FirstOrDefaultAsync() ?? null;
            }
            catch (Exception)
            {
                await alertService.DisplayAlert("Niepowodzenie", "Nie udało się pobrać danych.", "Ok");
                return null;
            }
        }

        public async Task<TEntity> ReadFirstByCondition<TKey>(Expression<Func<TEntity, bool>> condition,
            Expression<Func<TEntity, TKey>> orderBy, bool orderASC = true)
        {
            try
            {
                var query = dataContext.Set<TEntity>().Where(condition);

                if (orderASC)
                {
                    query = query.OrderBy(orderBy);
                }
                else
                {
                    query = query.OrderByDescending(orderBy);
                }

                return await query.FirstOrDefaultAsync() ?? null;
            }
            catch (Exception)
            {
                await alertService.DisplayAlert("Niepowodzenie", "Nie udało się pobrać danych.", "Ok");
                return null;
            }
        }

        public async Task<TEntity> ReadFirstByConditionWithInclude<TProp>(Expression<Func<TEntity, bool>> condition,
            Expression<Func<TEntity, TProp>> include)
        {
            try
            {
                return await dataContext.Set<TEntity>().Include(include).Where(condition).FirstOrDefaultAsync() ?? null;
            }
            catch (Exception)
            {
                await alertService.DisplayAlert("Niepowodzenie", "Nie udało się pobrać danych.", "Ok");
                return null;
            }
        }

        public async Task<int> ReadDataCount(Expression<Func<TEntity, bool>> condition)
        {
            return await dataContext.Set<TEntity>().Where(condition).CountAsync();
        }

        public async Task<bool> CheckIfExistByCondition(Expression<Func<TEntity, bool>> condition)
        {
            try
            {
                return await dataContext.Set<TEntity>().Where(condition).AnyAsync();
            }
            catch (Exception)
            {
                await alertService.DisplayAlert("Niepowodzenie", "Nie udało się pobrać danych.", "Ok");
                return false;
            }
        }

        public async Task Update(TEntity entity)
        {
            try
            {
                dataContext.Set<TEntity>().Update(entity);
            }
            catch (Exception)
            {
                await alertService.DisplayAlert("Niepowodzenie", "Nie udało się zaktualizować danych.", "Ok");
            }
        }
    }
}