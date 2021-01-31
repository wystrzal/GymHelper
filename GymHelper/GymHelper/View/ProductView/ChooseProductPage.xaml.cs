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
        private readonly ToolbarItem addSelectedProducts;

        public ChooseProductPage()
        {
            InitializeComponent();
            viewModel = BindingContext as ChooseProductPageVM;
            addSelectedProducts = new ToolbarItem();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            addSelectedProducts.Text = "Dodaj";
            addSelectedProducts.Command = viewModel.AddSelectedDataCommand;
        }

        private void CheckBox_CheckedChanged(object sender, CheckedChangedEventArgs e)
        {
            var checkbox = (CheckBox)sender;
            var product = checkbox.BindingContext as Product;

            if (checkbox.IsChecked)
            {
                viewModel.SelectedData.Add(product);
                ToolbarItems.Clear();
                ToolbarItems.Add(addSelectedProducts);
            }

            if (!checkbox.IsChecked)
            {
                viewModel.SelectedData.Remove(product);
                if (viewModel.SelectedData.Count <= 0)
                {
                    ToolbarItems.Clear();
                    ToolbarItems.Add(NewProduct);
                }
            }
        }
    }
}