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
    public partial class EditProductPage : ContentPage
    {
        public EditProductPage(Product product)
        {
            InitializeComponent();
            var viewModel = BindingContext as EditProductPageVM;
            viewModel.Product = product;
            nameEntry.Text = product.Name;
            gramsEntry.Text = product.Grams.ToString();
            caloriesEntry.Text = product.Calories.ToString();
            proteinEntry.Text = product.Protein.ToString();
            carbohydratesEntry.Text = product.Carbohydrates.ToString();
            fatEntry.Text = product.Fat.ToString();
        }
    }
}