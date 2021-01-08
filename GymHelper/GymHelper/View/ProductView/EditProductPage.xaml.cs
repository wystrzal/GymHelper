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
            nameEntry.Placeholder += $" ({product.Name.Capitalize()})";
            gramsEntry.Text = "";
            caloriesEntry.Text = "";
            proteinsEntry.Text = "";
            carbohydratesEntry.Text = "";
            fatsEntry.Text = "";
        }
    }
}