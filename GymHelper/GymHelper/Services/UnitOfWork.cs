using GymHelper.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace GymHelper.Data.Services
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DataContext dataContext;
        private readonly IAlertService alertService;
        public UnitOfWork()
        {
            dataContext = App.Data.DataContext;
            alertService = App.Data.AlertService;
        }

        public IRepository<TEntity> Repository<TEntity>()
            where TEntity : class
        {
            var repositories = new Dictionary<Type, object>();

            var entityType = typeof(TEntity);

            if (!repositories.ContainsKey(entityType))
            {
                var repository = Activator.CreateInstance(typeof(Repository<>).MakeGenericType(entityType), dataContext);
                repositories.Add(entityType, repository);
            }

            return (IRepository<TEntity>)repositories[entityType];
        }

        public async Task<bool> SaveChanges()
        {
            try
            {
                return await dataContext.SaveChangesAsync() > 0;
            }
            catch (Exception)
            {
                await alertService.DisplayAlert("Niepowodzenie", "Nie udało się zapisać danych.", "Ok");
                return false;
            }
        }
    }
}
