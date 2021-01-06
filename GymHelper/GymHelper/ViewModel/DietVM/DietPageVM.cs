using GymHelper.Data.Interfaces;
using GymHelper.Data.Services;
using GymHelper.Helpers;
using GymHelper.Helpers.Charts;
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
    public class DietPageVM : DisplayDataViewModel<Product>, IChartPreparer<Diet>
    {
        private readonly IChartCreator chartCreator;
        public override ICommand NavigateToAddDataCommand 
            => new Command(async () => await NavigateService.Navigate<ChooseProductPage>());
        public override ICommand NavigateToEditDataCommand
            => new Command<Product>(async (product) => await NavigateService.Navigate<EditDietProductPage>(product));

        private Chart nutrientsChart;
        public Chart NutrientsChart
        {
            get { return nutrientsChart; }
            set
            {
                nutrientsChart = value;
                OnPropertyChanged("NutrientsChart");
            }
        }

        public DietPageVM()
        {
            chartCreator = new DonutChartCreator();
        }

        public override async Task ReadData()
        {
            var products = await unitOfWork.Repository<Product>()
                .ReadAllByCondition(x => x.DietId == App.Data.User.Diet.DietId);

            Collection.FillCollection(products);
        }

        public override async Task DeleteData(Product entity)
        {
            var diet = await unitOfWork.Repository<Diet>().ReadFirstByCondition(x => x.DietId == App.Data.User.Diet.DietId);
            diet.Products.Remove(entity);
            NutrientsManagement.SubtractNutrients(entity, diet);

            await unitOfWork.Repository<Product>().SaveChanges();

            await ReadData();
        }

        public async Task PrepareCharts(Diet entity)
        {
            NutrientsChart = await chartCreator.CreateChart(new NutrientsEntryPreparer(entity.DietId));
        }
    }
}
