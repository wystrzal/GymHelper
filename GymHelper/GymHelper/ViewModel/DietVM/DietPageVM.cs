using GymHelper.Data.Interfaces;
using GymHelper.Data.Services;
using GymHelper.Helpers;
using GymHelper.Helpers.Charts;
using GymHelper.Helpers.Charts.ChartPreparers;
using GymHelper.Helpers.Charts.EntryPreparers;
using GymHelper.Models;
using GymHelper.View;
using GymHelper.View.ProductView;
using GymHelper.ViewModel.BaseVM;
using Microcharts;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace GymHelper.ViewModel
{
    public class DietPageVM : DisplayDataViewModel<Product>
    {
        public ChartPreparer<Diet> ChartPreparer { get; private set; }
        public override ICommand NavigateToAddDataCommand
            => new Command(async () => await NavigateService.Navigate<ChooseProductPage>());
        public override ICommand NavigateToEditDataCommand
            => new Command<Product>(async (product) => await NavigateService.Navigate<EditDietProductPage>(product));

        private Diet diet;

        public DietPageVM()
        {
            ChartPreparer = new DietChartPreparer();
        }

        private float totalCalories;
        public float TotalCalories
        {
            get
            {
                return totalCalories;
            }
            set
            {
                totalCalories = value;
                OnPropertyChanged("TotalCalories");
            }
        }

        public override async Task DeleteData(Product entity)
        {
            diet.Products.Remove(entity);
            NutrientsManagement.SubtractNutrients(entity, diet);

            await unitOfWork.SaveChanges();
            await ReadData();
            await PrepareChartView();
        }

        protected override async Task Refresh()
        {
            diet = App.Data.User.Diet;
            await PrepareChartView();
            await base.Refresh();
        }

        private async Task PrepareChartView()
        {
            await ChartPreparer.PrepareCharts(diet);
            TotalCalories = diet.TotalCalories;
        }

        protected override async Task<IEnumerable<Product>> GetData(int pageIndex, int pageSize = 10)
        {
            return await unitOfWork.Repository<Product>()
                .ReadAllByCondition(x => x.DietId == App.Data.User.Diet.DietId && x.Name.Contains(query), pageSize, pageIndex * pageSize);
        }

        protected override async Task<int> GetDataCount()
        {
            return await unitOfWork.Repository<Product>().ReadDataCount(x => x.DietId == App.Data.User.Diet.DietId && x.Name.Contains(query));
        }
    }
}