using GymHelper.Data.Interfaces;
using GymHelper.Models;
using GymHelper.View.ProductView;
using GymHelper.ViewModel;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace GymHelper.Test.ViewModel.ProductVM
{
    public class ChooseProductPageVMTest
    {
        private Mock<IUnitOfWork> unitOfWork;
        private readonly Mock<INavigateService> navigateService;
        private readonly ChooseProductPageVM viewModel;

        public ChooseProductPageVMTest()
        {
            navigateService = new Mock<INavigateService>();
            TestHelper.PrepareUnitOfWork(out unitOfWork);
            App.Data.NavigateService = navigateService.Object;
            App.Data.User = new User { Diet = new Diet() };
            viewModel = new ChooseProductPageVM();
        }

        [Fact]
        public void NavigateToAddDataCommand_Success()
        {
            //Act
            viewModel.NavigateToAddDataCommand.Execute(null);

            //Assert
            navigateService.Verify(x => x.Navigate<NewProductPage>(), Times.Once);
        }

        [Fact]
        public void NavigateToEditDataCommand_Success()
        {
            //Act
            viewModel.NavigateToEditDataCommand.Execute(It.IsAny<Product>());

            //Assert
            navigateService.Verify(x => x.Navigate<EditProductPage>(It.IsAny<Product>()), Times.Once);
        }

        [Fact]
        public void DeleteData_Success()
        {
            //Arrange
            unitOfWork.Setup(x => x.Repository<Product>()
                .ReadAllByCondition(It.IsAny<Func<Product, bool>>(), It.IsAny<int>(), It.IsAny<int>()))
                .Returns(Task.FromResult(new List<Product> { new Product() }));

            unitOfWork.Setup(x => x.Repository<Product>().CheckIfExistByCondition(It.IsAny<Func<Product, bool>>())).Returns(Task.FromResult(true));

            //Act
            viewModel.DeleteDataCommand.Execute(new Product());

            //Assert
            unitOfWork.Verify(x => x.SaveChanges(), Times.Once);
            unitOfWork.Verify(x => x.Repository<Product>().Delete(It.IsAny<Product>()), Times.Once);
            Assert.Single(viewModel.Collection);
        }

        [Fact]
        public void Refresh_Success()
        {
            //Arrange
            unitOfWork.Setup(x => x.Repository<Product>()
                .ReadAllByCondition(It.IsAny<Func<Product, bool>>(), It.IsAny<int>(), It.IsAny<int>()))
                .Returns(Task.FromResult(new List<Product> { new Product() }));

            //Act
            viewModel.RefreshCommand.Execute(null);

            //Assert
            Assert.Single(viewModel.Collection);
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

        [Fact]
        public void AddSelectedData_Success()
        {
            //Arrange
            viewModel.SelectedData = new List<Product> { new Product(), new Product() };
            var selectedDataCount = viewModel.SelectedData.Count;

            unitOfWork.Setup(x => x.Repository<Product>()
                .CheckIfExistByCondition(It.IsAny<Func<Product, bool>>())).Returns(Task.FromResult(false));

            //Act
            viewModel.AddSelectedDataCommand.Execute(null);

            //Assert
            unitOfWork.Verify(x => x.SaveChanges(), Times.Exactly(selectedDataCount));
            navigateService.Verify(x => x.NavigateBack(), Times.Once);
        }
    }
}
