using GymHelper.Data.Interfaces;
using GymHelper.Models;
using GymHelper.ViewModel;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace GymHelper.Test.ViewModel.ExerciseVM
{
    public class NewExercisePageVMTest
    {
        private Mock<IUnitOfWork> unitOfWork;
        private readonly Mock<INavigateService> navigateService;
        protected readonly Mock<IAlertService> alertService;
        private readonly NewExercisePageVM viewModel;

        public NewExercisePageVMTest()
        {
            navigateService = new Mock<INavigateService>();
            alertService = new Mock<IAlertService>();
            TestHelper.PrepareUnitOfWork(out unitOfWork);
            App.Data.NavigateService = navigateService.Object;
            App.Data.AlertService = alertService.Object;
            App.Data.User = new User { UserId = 1 };
            viewModel = new NewExercisePageVM();
        }

        [Fact]
        public void AddDataCommand_ExerciseExist()
        {
            //Arrange
            var exercise = new Exercise { Name = "Name", UserId = 1 };

            unitOfWork.Setup(x => x.Repository<Exercise>()
                .CheckIfExistByCondition(It.IsAny<Func<Exercise, bool>>())).Returns(Task.FromResult(true));

            //Act
            viewModel.AddDataCommand.Execute(exercise);

            //Assert
            alertService.Verify(x => x.DisplayAlert(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()), Times.Once);
        }

        [Fact]
        public void AddDataCommand_Success()
        {
            //Arrange
            var exercise = new Exercise { Name = "Name", UserId = 1 };

            unitOfWork.Setup(x => x.Repository<Exercise>()
                .CheckIfExistByCondition(It.IsAny<Func<Exercise, bool>>())).Returns(Task.FromResult(false));

            //Act
            viewModel.AddDataCommand.Execute(exercise);

            //Assert
            unitOfWork.Verify(x => x.Repository<Exercise>().Add(exercise), Times.Once);
            unitOfWork.Verify(x => x.SaveChanges(), Times.Once);
            navigateService.Verify(x => x.NavigateBack(), Times.Once);
        }

        [Fact]
        public void AddDataCommand_CanExecute_NullExercise()
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
            var exercise = new Exercise();

            //Act
            var action = viewModel.AddDataCommand.CanExecute(exercise);

            //Assert
            Assert.False(action);
        }

        [Fact]
        public void AddDataCommand_CanExecute_Success()
        {
            //Arrange
            var exercise = new Exercise { Name = "Name" };

            //Act
            var action = viewModel.AddDataCommand.CanExecute(exercise);

            //Assert
            Assert.True(action);
        }
    }
}
