using GymHelper.Models;
using GymHelper.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace GymHelper.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DietPage : ContentPage
    {
        private readonly DietPageVM viewModel;
        private readonly Diet diet = App.Data.User.Diet;

        public DietPage()
        {
            InitializeComponent();
            viewModel = BindingContext as DietPageVM;
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            await viewModel.ReadData();

            await viewModel.GenerateCharts(diet);
            totalCalories.Text = $"Kalorie: {diet.TotalCalories}";
        }

        private async void MenuItem_Clicked(object sender, EventArgs e)
        {
            await viewModel.GenerateCharts(diet);
            totalCalories.Text = $"Kalorie: {diet.TotalCalories}";
        }
    }
}