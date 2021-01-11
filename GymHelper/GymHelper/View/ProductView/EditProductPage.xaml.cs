using GymHelper.Data.Interfaces;
using GymHelper.Helpers.Extensions;
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
            ((EditProductPageVM)BindingContext).Product = product;
        }

        private void NameEntry_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(((Entry)sender).Text))
            {
                ((EditProductPageVM)BindingContext).Product.Name = ((EditProductPageVM)BindingContext).OldProduct.Name;
            }
        }

        private void GramsEntry_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(((Entry)sender).Text))
            {
                ((EditProductPageVM)BindingContext).Product.Grams = ((EditProductPageVM)BindingContext).OldProduct.Grams;
            }
        }

        private void CaloriesEntry_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(((Entry)sender).Text))
            {
                ((EditProductPageVM)BindingContext).Product.Calories = ((EditProductPageVM)BindingContext).OldProduct.Calories;
            }
        }

        private void ProteinsEntry_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(((Entry)sender).Text))
            {
                ((EditProductPageVM)BindingContext).Product.Proteins = ((EditProductPageVM)BindingContext).OldProduct.Proteins;
            }
        }

        private void CarbohydratesEntry_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(((Entry)sender).Text))
            {
                ((EditProductPageVM)BindingContext).Product.Carbohydrates = ((EditProductPageVM)BindingContext).OldProduct.Carbohydrates;
            }
        }

        private void FatsEntry_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(((Entry)sender).Text))
            {
                ((EditProductPageVM)BindingContext).Product.Fats = ((EditProductPageVM)BindingContext).OldProduct.Fats;
            }
        }
    }
}