using GymHelper.View.ExerciseView;
using GymHelper.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace GymHelper.View.WorkoutView
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class WorkoutPage : ContentPage
    {
        private readonly WorkoutPageVM viewModel;
        public WorkoutPage()
        {
            InitializeComponent();
            viewModel = BindingContext as WorkoutPageVM;
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            await viewModel.ReadWorkouts();
        }

        private void WorkoutListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            viewModel.NavigateService.Navigate<ChooseExercisePage>();
        }
    }
}