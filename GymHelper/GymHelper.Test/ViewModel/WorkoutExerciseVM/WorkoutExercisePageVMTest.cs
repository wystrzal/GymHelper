using GymHelper.Data.Interfaces;
using GymHelper.Models;
using GymHelper.View;
using GymHelper.View.ExerciseView;
using GymHelper.ViewModel;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace GymHelper.Test.ViewModel.WorkoutExerciseVM
{
    public class WorkoutExercisePageVMTest
    {
        private Mock<IUnitOfWork> unitOfWork;
        private readonly Mock<INavigateService> navigateService;
        private readonly Mock<IAlertService> alertService;
        private readonly WorkoutExercisePageVM viewModel;

        public WorkoutExercisePageVMTest()
        {
            navigateService = new Mock<INavigateService>();
            alertService = new Mock<IAlertService>();
            TestHelper.PrepareUnitOfWork(out unitOfWork);
            App.Data.NavigateService = navigateService.Object;
            App.Data.AlertService = alertService.Object;
            viewModel = new WorkoutExercisePageVM();
        }

        [Fact]
        public void NavigateToAddDataCommand_Success()
        {
            //Act
            viewModel.NavigateToAddDataCommand.Execute(null);

            //Assert
            navigateService.Verify(x => x.Navigate<ChooseExercisePage>(), Times.Once);
        }

        [Fact]
        public void NavigateToEditDataCommand_Success()
        {
            //Act
            viewModel.NavigateToEditDataCommand.Execute(It.IsAny<WorkoutExercise>());

            //Assert
            navigateService.Verify(x => x.Navigate<EditWorkoutExercisePage>(It.IsAny<WorkoutExercise>()), Times.Once);
        }

        [Fact]
        public void DeleteData_Success()
        {
            //Arrange
            unitOfWork.Setup(x => x.Repository<WorkoutExercise>()
                .ReadAllByConditionWithInclude(It.IsAny<Func<WorkoutExercise, bool>>(), It.IsAny<Expression<Func<WorkoutExercise, Exercise>>>(),
                    It.IsAny<int>(), It.IsAny<int>()))
                .Returns(Task.FromResult(new List<WorkoutExercise> { new WorkoutExercise() }));

            //Act
            viewModel.DeleteDataCommand.Execute(It.IsAny<WorkoutExercise>());

            //Assert
            unitOfWork.Verify(x => x.SaveChanges(), Times.Once);
            unitOfWork.Verify(x => x.Repository<WorkoutExercise>().Delete(It.IsAny<WorkoutExercise>()), Times.Once);
            Assert.Single(viewModel.Collection);
        }

        [Fact]
        public void Refresh_Success()
        {
            //Arrange
            unitOfWork.Setup(x => x.Repository<WorkoutExercise>()
                .ReadAllByConditionWithInclude(It.IsAny<Func<WorkoutExercise, bool>>(), It.IsAny<Expression<Func<WorkoutExercise, Exercise>>>(),
                    It.IsAny<int>(), It.IsAny<int>()))
                .Returns(Task.FromResult(new List<WorkoutExercise> { new WorkoutExercise() }));

            //Act
            viewModel.RefreshCommand.Execute(null);

            //Assert
            Assert.Single(viewModel.Collection);
        }

        [Fact]
        public void SearchData_Success()
        {
            //Arrange
            unitOfWork.Setup(x => x.Repository<WorkoutExercise>()
                .ReadAllByConditionWithInclude(It.IsAny<Func<WorkoutExercise, bool>>(), It.IsAny<Expression<Func<WorkoutExercise, Exercise>>>(),
                    It.IsAny<int>(), It.IsAny<int>()))
                .Returns(Task.FromResult(new List<WorkoutExercise> { new WorkoutExercise() }));

            //Act
            viewModel.PerformSearchCommand.Execute("");

            //Assert
            Assert.Single(viewModel.Collection);
        }

        [Fact]
        public void CopyData_Success()
        {
            //Arrange
            var workout = new Workout
            {
                WorkoutsExercises = new List<WorkoutExercise>
                {
                    new WorkoutExercise()
                }
            };

            unitOfWork.Setup(x => x.Repository<Workout>()
                .ReadFirstByConditionWithInclude(It.IsAny<Func<Workout, bool>>(),
                    It.IsAny<Expression<Func<Workout, ICollection<WorkoutExercise>>>>()))
                .Returns(Task.FromResult(workout));

            unitOfWork.Setup(x => x.SaveChanges()).Returns(Task.FromResult(true));

            //Act
            viewModel.CopyDataCommand.Execute(null);

            //Assert
            unitOfWork.Verify(x => x.Repository<Workout>().Add(It.IsAny<Workout>()), Times.Once);
            unitOfWork.Verify(x => x.SaveChanges(), Times.Exactly(2));
            alertService.Verify(x => x.DisplayAlert(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()), Times.Once);
        }
    }
}