using GymHelper.Data.Interfaces;
using GymHelper.Models;
using GymHelper.ViewModel;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace GymHelper.Test.ViewModel.ProductVM
{
    public class NewProductPageVMTest
    {
        private Mock<IUnitOfWork> unitOfWork;
        private readonly Mock<INavigateService> navigateService;
        protected readonly Mock<IAlertService> alertService;
        private readonly NewProductPageVM viewModel;

        public NewProductPageVMTest()
        {
            navigateService = new Mock<INavigateService>();
            alertService = new Mock<IAlertService>();
            TestHelper.PrepareUnitOfWork(out unitOfWork);
            App.Data.NavigateService = navigateService.Object;
            App.Data.AlertService = alertService.Object;
            App.Data.User = new User { UserId = 1 };
            viewModel = new NewProductPageVM();
        }

        [Fact]
        public void AddDataCommand_ExerciseExist()
        {
            //Arrange
            var product = new Product { Name = "Name" };

            unitOfWork.Setup(x => x.Repository<Product>()
                .CheckIfExistByCondition(It.IsAny<Func<Product, bool>>())).Returns(Task.FromResult(true));

            //Act
            viewModel.AddDataCommand.Execute(product);

            //Assert
            alertService.Verify(x => x.DisplayAlert(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()), Times.Once);
        }

        [Fact]
        public void AddDataCommand_Success()
        {
            //Arrange
            var product = new Product { Name = "Name" };

            unitOfWork.Setup(x => x.Repository<Product>()
                .CheckIfExistByCondition(It.IsAny<Func<Product, bool>>())).Returns(Task.FromResult(false));

            //Act
            viewModel.AddDataCommand.Execute(product);

            //Assert
            unitOfWork.Verify(x => x.Repository<Product>().Add(product), Times.Once);
            unitOfWork.Verify(x => x.SaveChanges(), Times.Once);
            navigateService.Verify(x => x.NavigateBack(), Times.Once);
        }

        [Fact]
        public void AddDataCommand_CanExecute_NullProduct()
        {
            //Act
            var action = viewModel.AddDataCommand.CanExecute(null);

            //Assert
            Assert.False(action);
        }

        [Fact]
        public void AddDataCommand_CanExecute_EmptyString()
        {
            //Arrange
            var product = new Product();

            //Act
            var action = viewModel.AddDataCommand.CanExecute(product);

            //Assert
            Assert.False(action);
        }

        [Fact]
        public void AddDataCommand_CanExecute_Success()
        {
            //Arrange
            var product = new Product { Name = "Name" };

            //Act
            var action = viewModel.AddDataCommand.CanExecute(product);

            //Assert
            Assert.True(action);
        }
    }
}