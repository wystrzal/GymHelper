using GymHelper.Data.Interfaces;
using GymHelper.Models;
using GymHelper.ViewModel;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace GymHelper.Test.ViewModel.ProductVM
{
    public class EditProductPageVMTest
    {
        private Mock<IUnitOfWork> unitOfWork;
        private readonly Mock<INavigateService> navigateService;
        private readonly EditProductPageVM viewModel;

        public EditProductPageVMTest()
        {
            navigateService = new Mock<INavigateService>();
            TestHelper.PrepareUnitOfWork(out unitOfWork);
            App.Data.NavigateService = navigateService.Object;
            App.Data.User = new User { Diet = new Diet() };
            viewModel = new EditProductPageVM();
        }

        [Fact]
        public void EditData_Success()
        {
            //Arrange
            viewModel.Product = new Product();
            var product = new Product { Name = "Name" };

            unitOfWork.Setup(x => x.Repository<Product>().CheckIfExistByCondition(It.IsAny<Func<Product, bool>>()));

            unitOfWork.Setup(x => x.Repository<Product>().Update(product));

            //Act
            viewModel.EditDataCommand.Execute(product);

            //Assert
            unitOfWork.Verify(x => x.Repository<Product>().Update(product), Times.Once);
            unitOfWork.Verify(x => x.SaveChanges(), Times.Once);
            navigateService.Verify(x => x.NavigateBack(), Times.Once);
        }

        [Fact]
        public void NameChangedCommand_EmptyString_Success()
        {
            //Arrange
            viewModel.Product = new Product { Name = "OldValue" };
            viewModel.Product.Name = "NewValue";

            //Act
            viewModel.NameChangedCommand.Execute("");

            //Assert
            Assert.Equal(viewModel.OldProduct.Name, viewModel.Product.Name);
        }

        [Fact]
        public void NameChangedCommand_Success()
        {
            //Arrange
            viewModel.Product = new Product { Name = "OldValue" };
            viewModel.Product.Name = "NewValue";

            //Act
            viewModel.NameChangedCommand.Execute("test");

            //Assert
            Assert.Equal(viewModel.Product.Name, viewModel.Product.Name);
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

        [Fact]
        public void CaloriesChangedCommand_EmptyString_Success()
        {
            //Arrange
            viewModel.Product = new Product { Calories = 100 };
            viewModel.Product.Calories = 200;

            //Act
            viewModel.CaloriesChangedCommand.Execute("");

            //Assert
            Assert.Equal(viewModel.OldProduct.Calories, viewModel.Product.Calories);
        }

        [Fact]
        public void CaloriesChangedCommand_Success()
        {
            //Arrange
            viewModel.Product = new Product { Calories = 100 };
            viewModel.Product.Calories = 200;

            //Act
            viewModel.CaloriesChangedCommand.Execute("test");

            //Assert
            Assert.Equal(viewModel.Product.Calories, viewModel.Product.Calories);
        }

        [Fact]
        public void ProteinsChangedCommand_EmptyString_Success()
        {
            //Arrange
            viewModel.Product = new Product { Proteins = 100 };
            viewModel.Product.Proteins = 200;

            //Act
            viewModel.ProteinsChangedCommand.Execute("");

            //Assert
            Assert.Equal(viewModel.OldProduct.Proteins, viewModel.Product.Proteins);
        }

        [Fact]
        public void ProteinsChangedCommand_Success()
        {
            //Arrange
            viewModel.Product = new Product { Proteins = 100 };
            viewModel.Product.Proteins = 200;

            //Act
            viewModel.ProteinsChangedCommand.Execute("test");

            //Assert
            Assert.Equal(viewModel.Product.Proteins, viewModel.Product.Proteins);
        }

        [Fact]
        public void CarbohydratesChangedCommand_EmptyString_Success()
        {
            //Arrange
            viewModel.Product = new Product { Carbohydrates = 100 };
            viewModel.Product.Carbohydrates = 200;

            //Act
            viewModel.CarbohydratesChangedCommand.Execute("");

            //Assert
            Assert.Equal(viewModel.OldProduct.Carbohydrates, viewModel.Product.Carbohydrates);
        }

        [Fact]
        public void CarbohydratesChangedCommand_Success()
        {
            //Arrange
            viewModel.Product = new Product { Carbohydrates = 100 };
            viewModel.Product.Carbohydrates = 200;

            //Act
            viewModel.CarbohydratesChangedCommand.Execute("test");

            //Assert
            Assert.Equal(viewModel.Product.Carbohydrates, viewModel.Product.Carbohydrates);
        }

        [Fact]
        public void FatsChangedCommand_EmptyString_Success()
        {
            //Arrange
            viewModel.Product = new Product { Fats = 100 };
            viewModel.Product.Fats = 200;

            //Act
            viewModel.FatsChangedCommand.Execute("");

            //Assert
            Assert.Equal(viewModel.OldProduct.Fats, viewModel.Product.Fats);
        }

        [Fact]
        public void FatsChangedCommand_Success()
        {
            //Arrange
            viewModel.Product = new Product { Fats = 100 };
            viewModel.Product.Fats = 200;

            //Act
            viewModel.FatsChangedCommand.Execute("test");

            //Assert
            Assert.Equal(viewModel.Product.Fats, viewModel.Product.Fats);
        }
    }
}