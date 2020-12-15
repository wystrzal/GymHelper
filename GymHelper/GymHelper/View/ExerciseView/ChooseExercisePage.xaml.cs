using GymHelper.Data.Interfaces;
using GymHelper.Models;
using GymHelper.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace GymHelper.View.ExerciseView
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ChooseExercisePage : ContentPage
    {
        private readonly ChooseExercisePageVM viewModel;
        public ChooseExercisePage()
        {
            InitializeComponent();
            viewModel = BindingContext as ChooseExercisePageVM;
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            await viewModel.ReadExercises();
        }
    }
}