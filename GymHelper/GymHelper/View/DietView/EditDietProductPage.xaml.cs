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
    public partial class EditDietProductPage : ContentPage
    {
        public EditDietProductPage(Product product)
        {
            InitializeComponent();
            var viewModel = BindingContext as EditDietProductPageVM;
            viewModel.Product = product;
            gramsEntry.Text = product.Grams.ToString();
        }
    }
}