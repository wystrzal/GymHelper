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

        protected override void OnAppearing()
        {
            base.OnAppearing();
            addSelectedExercises.Text = "Dodaj";
            addSelectedExercises.Command = viewModel.AddSelectedDataCommand;
        }

        private void CheckBox_CheckedChanged(object sender, CheckedChangedEventArgs e)
        {
            var checkbox = (CheckBox)sender;
            var exercise = checkbox.BindingContext as Exercise;

            if (checkbox.IsChecked)
            {
                viewModel.SelectedData.Add(exercise);
                ToolbarItems.Clear();
                ToolbarItems.Add(addSelectedExercises);
            }

            if (!checkbox.IsChecked)
            {
                viewModel.SelectedData.Remove(exercise);
                if (viewModel.SelectedData.Count <= 0)
                {
                    ToolbarItems.Clear();
                    ToolbarItems.Add(NewExercise);
                }
            }
        }
    }
}