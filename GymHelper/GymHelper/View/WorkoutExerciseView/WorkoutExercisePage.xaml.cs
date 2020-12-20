using GymHelper.Data.Interfaces;
using GymHelper.Models;
using GymHelper.ViewModel;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace GymHelper.View.ExerciseView
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class WorkoutExercisePage : ContentPage
    {
        private readonly WorkoutExercisePageVM viewModel;

        public WorkoutExercisePage()
        {
            InitializeComponent();
            viewModel = BindingContext as WorkoutExercisePageVM;
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            await viewModel.ReadData();
        }
    }
}