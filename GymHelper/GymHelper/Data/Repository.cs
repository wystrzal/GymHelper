﻿using GymHelper.Data.Interfaces;
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

        public async Task<List<TEntity>> ReadAllByCondition<TKey>(Func<TEntity, bool> condition,
            Func<TEntity, TKey> orderBy, bool orderASC = true)
        {
            try
            {
                List<TEntity> data;

                if (orderASC)
                {
                    data = dataContext.Set<TEntity>().Where(condition).OrderBy(orderBy).ToList();
                }
                else
                {
                    data = dataContext.Set<TEntity>().Where(condition).OrderByDescending(orderBy).ToList();
                }

                return await Task.FromResult(data);
            }
            catch (Exception)
            {
                await alertService.DisplayAlert("Niepowodzenie", "Nie udało się pobrać danych.", "Ok");
                return null;
            }
        }


        public async Task<List<TEntity>> ReadAllByCondition(Func<TEntity, bool> condition, int take, int skip = 0)
        {
            try
            {
                var data = dataContext.Set<TEntity>().Where(condition).Skip(skip).Take(take).ToList();

                return await Task.FromResult(data);
            }
            catch (Exception)
            {
                await alertService.DisplayAlert("Niepowodzenie", "Nie udało się pobrać danych.", "Ok");
                return null;
            }
        }

        public async Task<List<TEntity>> ReadAllByCondition<TKey>(Func<TEntity, bool> condition,
            Func<TEntity, TKey> orderBy, int take, int skip = 0, bool orderASC = true)
        {
            try
            {
                List<TEntity> data;

                if (orderASC)
                {
                    data = dataContext.Set<TEntity>().Where(condition).OrderBy(orderBy).Skip(skip).Take(take).ToList();
                }
                else
                {
                    data = dataContext.Set<TEntity>().Where(condition).OrderByDescending(orderBy).Skip(skip).Take(take).ToList();
                }

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

        public async Task<List<TEntity>> ReadAllByConditionWithInclude<TProp>(Func<TEntity, bool> condition,
            Expression<Func<TEntity, TProp>> include, int take, int skip = 0)
        {
            try
            {
                var data = dataContext.Set<TEntity>().Include(include).Where(condition).Skip(skip).Take(take).ToList();

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

        public async Task<TEntity> ReadFirstByCondition<TKey>(Func<TEntity, bool> condition,
            Func<TEntity, TKey> orderBy, bool orderASC = true)
        {
            try
            {
                TEntity data;

                if (orderASC)
                {
                    data = dataContext.Set<TEntity>().Where(condition).OrderBy(orderBy).FirstOrDefault();
                }
                else
                {
                    data = dataContext.Set<TEntity>().Where(condition).OrderByDescending(orderBy).FirstOrDefault();
                }

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

        public async Task<int> ReadDataCount(Func<TEntity, bool> condition)
        {
            var dataCount = dataContext.Set<TEntity>().Where(condition).Count();
            return await Task.FromResult(dataCount);
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