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

namespace GymHelper.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class EditExercisePage : ContentPage
    {
        public EditExercisePage(Exercise exercise)
        {
            InitializeComponent();
            ((EditExercisePageVM)BindingContext).Exercise = exercise;
        }

        private void NameEntry_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (string.IsNullOrEmpty(((Entry)sender).Text))
            {
                ((EditExercisePageVM)BindingContext).Exercise.Name = ((EditExercisePageVM)BindingContext).OldExercise.Name;
            }
        }
    }
}