using GymHelper.Data.Interfaces;
using GymHelper.Models;
using GymHelper.ViewModel;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace GymHelper.Test.ViewModel.WorkoutVM
{
    public class NewWorkoutPageVMTest
    {
        private Mock<IUnitOfWork> unitOfWork;
        private readonly Mock<INavigateService> navigateService;
        protected readonly Mock<IAlertService> alertService;
        private readonly NewWorkoutPageVM viewModel;

        public NewWorkoutPageVMTest()
        {
            navigateService = new Mock<INavigateService>();
            alertService = new Mock<IAlertService>();
            TestHelper.PrepareUnitOfWork(out unitOfWork);
            App.Data.NavigateService = navigateService.Object;
            App.Data.AlertService = alertService.Object;
            App.Data.User = new User { UserId = 1 };
            viewModel = new NewWorkoutPageVM();
        }

        [Fact]
        public void AddDataCommand_Success()
        {
            //Arrange
            var workout = new Workout { Name = "Name" };

            unitOfWork.Setup(x => x.Repository<Workout>().Add(workout));

            //Act
            viewModel.AddDataCommand.Execute(workout);

            //Assert
            unitOfWork.Verify(x => x.Repository<Workout>().Add(workout), Times.Once);
            unitOfWork.Verify(x => x.SaveChanges(), Times.Once);
            navigateService.Verify(x => x.NavigateBack(), Times.Once);
        }

        [Fact]
        public void AddDataCommand_CanExecute_NullWorkout()
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
            var workout = new Workout();

            //Act
            var action = viewModel.AddDataCommand.CanExecute(workout);

            //Assert
            Assert.False(action);
        }

        [Fact]
        public void AddDataCommand_CanExecute_Success()
        {
            //Arrange
            var workout = new Workout { Name = "Name" };

            //Act
            var action = viewModel.AddDataCommand.CanExecute(workout);

            //Assert
            Assert.True(action);
        }
    }
}