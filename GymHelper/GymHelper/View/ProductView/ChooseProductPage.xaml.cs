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

namespace GymHelper.View.ProductView
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ChooseProductPage : ContentPage
    {
        private readonly ChooseProductPageVM viewModel;

        public ChooseProductPage()
        {
            InitializeComponent();
            viewModel = BindingContext as ChooseProductPageVM;
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            await viewModel.ReadData();
        }
    }
}