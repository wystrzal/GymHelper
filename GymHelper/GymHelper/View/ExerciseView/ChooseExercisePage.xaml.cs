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
        private readonly ToolbarItem addSelectedExercises;

        public ChooseExercisePage()
        {
            InitializeComponent();
            viewModel = BindingContext as ChooseExercisePageVM;
            addSelectedExercises = new ToolbarItem();
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            addSelectedExercises.Text = "Dodaj";
            addSelectedExercises.Command = viewModel.AddSelectedDataCommand;
            await viewModel.ReadData();
        }

        private void CheckBox_CheckedChanged(object sender, CheckedChangedEventArgs e)
        {
            var checkbox = (CheckBox)sender;
            var exercise = checkbox.BindingContext as Exercise;

            if (checkbox.IsChecked)
            {
                ToolbarItems.Clear();
                ToolbarItems.Add(addSelectedExercises);
                viewModel.SelectedData.Add(exercise);
            }

            if (!checkbox.IsChecked)
            {
                ToolbarItems.Clear();
                ToolbarItems.Add(newExercise);
                viewModel.SelectedData.Remove(exercise);
            }
        }
    }
}