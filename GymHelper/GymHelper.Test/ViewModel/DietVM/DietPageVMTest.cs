using GymHelper.Data.Interfaces;
using GymHelper.Models;
using GymHelper.View;
using GymHelper.View.ProductView;
using GymHelper.ViewModel;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace GymHelper.Test.ViewModel.DietVM
{
    public class DietPageVMTest
    {
        private Mock<IUnitOfWork> unitOfWork;
        private readonly Mock<INavigateService> navigateService;
        private readonly DietPageVM viewModel;
        private readonly Product product;

        public DietPageVMTest()
        {
            product = new Product();
            navigateService = new Mock<INavigateService>();
            SetupAppData();
            viewModel = new DietPageVM();
        }

        [Fact]
        public void NavigateToAddDataCommand_Success()
        {
            //Act
            viewModel.NavigateToAddDataCommand.Execute(null);

            //Assert
            navigateService.Verify(x => x.Navigate<ChooseProductPage>(), Times.Once);
        }

        [Fact]
        public void NavigateToEditDataCommand_Success()
        {
            //Act
            viewModel.NavigateToEditDataCommand.Execute(It.IsAny<Product>());

            //Assert
            navigateService.Verify(x => x.Navigate<EditDietProductPage>(It.IsAny<Product>()), Times.Once);
        }

        [Fact]
        public void DeleteData_Success()
        {
            //Arrange
            unitOfWork.Setup(x => x.Repository<Product>()
                .ReadAllByCondition(It.IsAny<Func<Product, bool>>(), It.IsAny<int>(), It.IsAny<int>()))
                .Returns(Task.FromResult(new List<Product> { new Product() }));

            unitOfWork.Setup(x => x.Repository<Diet>()
                .ReadFirstByCondition(It.IsAny<Func<Diet, bool>>())).Returns(Task.FromResult(new Diet()));

            viewModel.RefreshCommand.Execute(null);

            //Act
            viewModel.DeleteDataCommand.Execute(product);

            //Assert
            unitOfWork.Verify(x => x.SaveChanges(), Times.Once);
            Assert.Single(App.Data.User.Diet.Products);
            Assert.Single(viewModel.Collection);
            Assert.Equal(App.Data.User.Diet.TotalCalories, viewModel.TotalCalories);
        }

        [Fact]
        public void Refresh_Success()
        {
            //Arrange
            unitOfWork.Setup(x => x.Repository<Product>()
                .ReadAllByCondition(It.IsAny<Func<Product, bool>>(), It.IsAny<int>(), It.IsAny<int>()))
                .Returns(Task.FromResult(new List<Product> { new Product() }));

            unitOfWork.Setup(x => x.Repository<Diet>()
                .ReadFirstByCondition(It.IsAny<Func<Diet, bool>>())).Returns(Task.FromResult(new Diet()));

            //Act
            viewModel.RefreshCommand.Execute(null);

            //Assert
            Assert.Single(viewModel.Collection);
            Assert.Equal(App.Data.User.Diet.TotalCalories, viewModel.TotalCalories);
        }

        [Fact]
        public void SearchData_Success()
        {
            //Arrange
            unitOfWork.Setup(x => x.Repository<Product>()
                .ReadAllByCondition(It.IsAny<Func<Product, bool>>(), It.IsAny<int>(), It.IsAny<int>()))
                .Returns(Task.FromResult(new List<Product> { new Product() }));

            //Act
            viewModel.PerformSearchCommand.Execute("");

            //Assert
            Assert.Single(viewModel.Collection);
        }

        private void SetupAppData()
        {
            TestHelper.PrepareUnitOfWork(out unitOfWork);
            App.Data.NavigateService = navigateService.Object;
            App.Data.User = new User
            {
                Diet = new Diet
                {
                    Products = new List<Product>
                    {
                        product,
                        new Product()
                    },
                    TotalCalories = 100
                }
            };
        }
    }
}
