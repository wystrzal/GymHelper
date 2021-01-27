using GymHelper.Data.Interfaces;
using GymHelper.Models;
using GymHelper.View;
using GymHelper.View.ExerciseView;
using GymHelper.ViewModel;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace GymHelper.Test.ViewModel.WorkoutVM
{
    public class WorkoutPageVMTest
    {
        private Mock<IUnitOfWork> unitOfWork;
        private readonly Mock<INavigateService> navigateService;
        private readonly Mock<IAlertService> alertService;
        private readonly WorkoutPageVM viewModel;

        public WorkoutPageVMTest()
        {
            navigateService = new Mock<INavigateService>();
            alertService = new Mock<IAlertService>();
            TestHelper.PrepareUnitOfWork(out unitOfWork);
            App.Data.NavigateService = navigateService.Object;
            App.Data.AlertService = alertService.Object;
            viewModel = new WorkoutPageVM();
        }

        [Fact]
        public void NavigateToAddDataCommand_Success()
        {
            //Act
            viewModel.NavigateToAddDataCommand.Execute(null);

            //Assert
            navigateService.Verify(x => x.Navigate<NewWorkoutPage>(), Times.Once);
        }

        [Fact]
        public void NavigateToEditDataCommand_Success()
        {
            //Act
            viewModel.NavigateToEditDataCommand.Execute(It.IsAny<Workout>());

            //Assert
            navigateService.Verify(x => x.Navigate<EditWorkoutPage>(It.IsAny<Workout>()), Times.Once);
        }

        [Fact]
        public void NavigateToWorkoutExercisePage_Success()
        {
            //Act
            viewModel.SelectedWorkout = new Workout();

            //Assert
            navigateService.Verify(x => x.Navigate<WorkoutExercisePage>(), Times.Once);
        }

        [Fact]
        public void DeleteData_Success()
        {
            //Arrange
            unitOfWork.Setup(x => x.Repository<Workout>()
                .ReadAllByCondition(It.IsAny<Func<Workout, bool>>(), It.IsAny<Func<Workout, DateTime>>(), 
                    It.IsAny<int>(), It.IsAny<int>(), It.IsAny<bool>()))
                .Returns(Task.FromResult(new List<Workout> { new Workout() }));

            //Act
            viewModel.DeleteDataCommand.Execute(It.IsAny<Workout>());

            //Assert
            unitOfWork.Verify(x => x.SaveChanges(), Times.Once);
            unitOfWork.Verify(x => x.Repository<Workout>().Delete(It.IsAny<Workout>()), Times.Once);
            Assert.Single(viewModel.Collection);
        }

        [Fact]
        public void Refresh_Success()
        {
            //Arrange
            unitOfWork.Setup(x => x.Repository<Workout>()
                .ReadAllByCondition(It.IsAny<Func<Workout, bool>>(), It.IsAny<Func<Workout, DateTime>>(),
                    It.IsAny<int>(), It.IsAny<int>(), It.IsAny<bool>()))
                .Returns(Task.FromResult(new List<Workout> { new Workout() }));

            //Act
            viewModel.RefreshCommand.Execute(null);

            //Assert
            Assert.Single(viewModel.Collection);
        }

        [Fact]
        public void SearchData_Success()
        {
            //Arrange
            unitOfWork.Setup(x => x.Repository<Workout>()
                .ReadAllByCondition(It.IsAny<Func<Workout, bool>>(), It.IsAny<Func<Workout, DateTime>>(),
                    It.IsAny<int>(), It.IsAny<int>(), It.IsAny<bool>()))
                .Returns(Task.FromResult(new List<Workout> { new Workout() }));

            //Act
            viewModel.PerformSearchCommand.Execute("");

            //Assert
            Assert.Single(viewModel.Collection);
        }
    }
}