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

        public Repository()
        {
            dataContext = App.Data.DataContext;
            alertService = App.Data.AlertService;
        }

        public async Task<bool> Add(TEntity entity)
        {
            try
            {
                await dataContext.Set<TEntity>().AddAsync(entity);
                await dataContext.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                await alertService.DisplayAlert("Niepowodzenie", "Nie udało się dodać danych.", "Ok");
                return false;
            }
        }

        public async Task<bool> Delete(TEntity entity)
        {
            try
            {
                dataContext.Set<TEntity>().Remove(entity);
                await dataContext.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                await alertService.DisplayAlert("Niepowodzenie", "Nie udało się usunąć danych.", "Ok");
                return false;
            }
        }

        public async Task<List<TEntity>> ReadAll()
        {
            try
            {
                return await dataContext.Set<TEntity>().ToListAsync();
            }
            catch (Exception)
            {
                await alertService.DisplayAlert("Niepowodzenie", "Nie udało się pobrać danych.", "Ok");
                return null;
            }
        }

        public async Task<List<TEntity>> ReadAllByCondition(Func<TEntity, bool> condition)
        {
            try
            {
                var data = dataContext.Set<TEntity>().Where(condition).ToList();

                return await Task.FromResult(data);
            }
            catch (Exception)
            {
                await alertService.DisplayAlert("Niepowodzenie", "Nie udało się pobrać danych.", "Ok");
                return null;
            }
        }

        public async Task<List<TEntity>> ReadAllByConditionWithInclude<TProp>(Func<TEntity, bool> condition,
            Expression<Func<TEntity, TProp>> include)
        {
            try
            {
                var data = dataContext.Set<TEntity>().Include(include).Where(condition).ToList();

                return await Task.FromResult(data);
            }
            catch (Exception)
            {
                await alertService.DisplayAlert("Niepowodzenie", "Nie udało się pobrać danych.", "Ok");
                return null;
            }
        }

        public async Task<TEntity> ReadFirstByCondition(Func<TEntity, bool> condition)
        {
            try
            {
                var data = dataContext.Set<TEntity>().Where(condition).FirstOrDefault();
                return data == null ? null : await Task.FromResult(data);
            }
            catch (Exception)
            {
                await alertService.DisplayAlert("Niepowodzenie", "Nie udało się pobrać danych.", "Ok");
                return null;
            }
        }

        public async Task<TEntity> ReadFirstByConditionWithInclude<TProp>(Func<TEntity, bool> condition,
            Expression<Func<TEntity, TProp>> include)
        {
            try
            {
                var data = dataContext.Set<TEntity>().Include(include).Where(condition).FirstOrDefault();
                return data == null ? null : await Task.FromResult(data);
            }
            catch (Exception)
            {
                await alertService.DisplayAlert("Niepowodzenie", "Nie udało się pobrać danych.", "Ok");
                return null;
            }
        }

        public async Task<bool> CheckIfExistByCondition(Func<TEntity, bool> condition)
        {
            try
            {
                var data = dataContext.Set<TEntity>().Where(condition).Any();
                return await Task.FromResult(data);
            }
            catch (Exception)
            {
                await alertService.DisplayAlert("Niepowodzenie", "Nie udało się pobrać danych.", "Ok");
                return false;
            }
        }

        public async Task<bool> Update(TEntity entity)
        {
            try
            {
                dataContext.Set<TEntity>().Update(entity);
                await dataContext.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                await alertService.DisplayAlert("Niepowodzenie", "Nie udało się zaktualizować danych.", "Ok");
                return false;
            }
        }
    }
}
