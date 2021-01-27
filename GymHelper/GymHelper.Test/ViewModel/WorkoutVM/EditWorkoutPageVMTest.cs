using GymHelper.Data.Interfaces;
using GymHelper.Models;
using GymHelper.ViewModel;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace GymHelper.Test.ViewModel.WorkoutVM
{
    public class EditWorkoutPageVMTest
    {
        private Mock<IUnitOfWork> unitOfWork;
        private readonly Mock<INavigateService> navigateService;
        private readonly EditWorkoutPageVM viewModel;

        public EditWorkoutPageVMTest()
        {
            navigateService = new Mock<INavigateService>();
            TestHelper.PrepareUnitOfWork(out unitOfWork);
            App.Data.NavigateService = navigateService.Object;
            viewModel = new EditWorkoutPageVM();
        }

        [Fact]
        public void EditData_Success()
        {
            //Arrange
            var workout = new Workout { Name = "Name" };
            unitOfWork.Setup(x => x.Repository<Workout>().Update(workout));

            //Act
            viewModel.EditDataCommand.Execute(workout);

            //Assert
            unitOfWork.Verify(x => x.Repository<Workout>().Update(workout), Times.Once);
            unitOfWork.Verify(x => x.SaveChanges(), Times.Once);
            navigateService.Verify(x => x.NavigateBack(), Times.Once);
        }

        [Fact]
        public void NameChangedCommand_EmptyString_Success()
        {
            //Arrange
            viewModel.Workout = new Workout { Name = "OldValue" };
            viewModel.Workout.Name = "NewValue";

            //Act
            viewModel.NameChangedCommand.Execute("");

            //Assert
            Assert.Equal(viewModel.OldWorkout.Name, viewModel.Workout.Name);
        }

        [Fact]
        public void NameChangedCommand_Success()
        {
            //Arrange
            viewModel.Workout = new Workout { Name = "OldValue" };
            viewModel.Workout.Name = "NewValue";

            //Act
            viewModel.NameChangedCommand.Execute("test");

            //Assert
            Assert.Equal(viewModel.Workout.Name, viewModel.Workout.Name);
        }
    }
}