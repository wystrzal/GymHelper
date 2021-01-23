using GymHelper.Data.Interfaces;
using GymHelper.Models;
using GymHelper.ViewModel;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace GymHelper.Test.ViewModel.DietVM
{
    public class EditDietProductPageVMTest
    {
        private Mock<IUnitOfWork> unitOfWork;
        private readonly Mock<INavigateService> navigateService;
        private readonly EditDietProductPageVM viewModel;

        public EditDietProductPageVMTest()
        {
            navigateService = new Mock<INavigateService>();
            SetupAppData();
            viewModel = new EditDietProductPageVM();
        }

        [Fact]
        public void EditData_Success()
        {
            //Arrange
            viewModel.Product = new Product { Grams = 100 };
            var product = new Product
            {
                Calories = 100,
                Proteins = 100,
                Carbohydrates = 100,
                Fats = 100,
                Grams = 200,
            };
            unitOfWork.Setup(x => x.Repository<Product>().Update(product));

            //Act
            viewModel.EditDataCommand.Execute(product);

            //Assert
            unitOfWork.Verify(x => x.Repository<Product>().Update(product));
            unitOfWork.Verify(x => x.SaveChanges(), Times.Once);
            Assert.Equal(200, product.Calories);
            Assert.Equal(200, App.Data.User.Diet.TotalCalories);
        }

        [Fact]
        public void GramsChangedCommand_EmptyString_Success()
        {
            //Arrange
            viewModel.Product = new Product { Grams = 100 };
            viewModel.Product.Grams = 200;

            //Act
            viewModel.GramsChangedCommand.Execute("");

            //Assert
            Assert.Equal(viewModel.OldProduct.Grams, viewModel.Product.Grams);
        }

        [Fact]
        public void GramsChangedCommand_Success()
        {
            //Arrange
            viewModel.Product = new Product { Grams = 100 };
            viewModel.Product.Grams = 200;

            //Act
            viewModel.GramsChangedCommand.Execute("test");

            //Assert
            Assert.Equal(viewModel.Product.Grams, viewModel.Product.Grams);
        }

        private void SetupAppData()
        {
            TestHelper.PrepareUnitOfWork(out unitOfWork);
            App.Data.NavigateService = navigateService.Object;
            App.Data.User = new User
            {
                Diet = new Diet
                {
                    TotalCalories = 100,
                    TotalCarbohydrates = 100,
                    TotalFats = 100,
                    TotalProteins = 100
                }
            };
        }
    }
}
