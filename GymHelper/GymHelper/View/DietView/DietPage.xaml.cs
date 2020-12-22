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

        public DietPage()
        {
            InitializeComponent();
            viewModel = BindingContext as DietPageVM;
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            await viewModel.ReadData();
        }
    }
}