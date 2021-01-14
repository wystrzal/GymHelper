using GymHelper.Models;
using GymHelper.ViewModel;
using Microcharts;
using Microcharts.Forms;
using SkiaSharp;
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
    public partial class ChartsPage : ContentPage
    {
        private readonly ChartsPageVM viewModel;

        public ChartsPage()
        {
            InitializeComponent();
            viewModel = BindingContext as ChartsPageVM;
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            await viewModel.ReadData();
        }

        private async void ExerciseListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var exercise = (Exercise)ExerciseListView.SelectedItem;

            if (exercise != null)
            {
                await viewModel.ChartPreparer.PrepareCharts(exercise);
                SwitchCurrentView();
            }
        }

        private void SelectExerciseButton_Clicked(object sender, EventArgs e)
        {
            SwitchCurrentView();
            ExerciseListView.SelectedItem = null;
        }

        private void SwitchCurrentView()
        {
            ExerciseSelector.IsVisible = !ExerciseSelector.IsVisible;
            Charts.IsVisible = !Charts.IsVisible;
        }
    }
}