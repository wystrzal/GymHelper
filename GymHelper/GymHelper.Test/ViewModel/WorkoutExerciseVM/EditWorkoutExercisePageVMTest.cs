using GymHelper.Data.Interfaces;
using GymHelper.Models;
using GymHelper.ViewModel;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace GymHelper.Test.ViewModel.WorkoutExerciseVM
{
    public class EditWorkoutExercisePageVMTest
    {
        private Mock<IUnitOfWork> unitOfWork;
        private readonly Mock<INavigateService> navigateService;
        private readonly EditWorkoutExercisePageVM viewModel;

        public EditWorkoutExercisePageVMTest()
        {
            navigateService = new Mock<INavigateService>();
            TestHelper.PrepareUnitOfWork(out unitOfWork);
            App.Data.NavigateService = navigateService.Object;
            viewModel = new EditWorkoutExercisePageVM();
        }

        [Fact]
        public void EditData_Success()
        {
            //Arrange
            unitOfWork.Setup(x => x.Repository<WorkoutExercise>().Update(It.IsAny<WorkoutExercise>()));

            //Act
            viewModel.EditDataCommand.Execute(It.IsAny<WorkoutExercise>());

            //Assert
            unitOfWork.Verify(x => x.Repository<WorkoutExercise>().Update(It.IsAny<WorkoutExercise>()), Times.Once);
            unitOfWork.Verify(x => x.SaveChanges(), Times.Once);
            navigateService.Verify(x => x.NavigateBack(), Times.Once);
        }

        [Fact]
        public void SeriesChangedCommand_EmptyString_Success()
        {
            //Arrange
            viewModel.WorkoutExercise = new WorkoutExercise { Series = 100 };
            viewModel.WorkoutExercise.Series = 200;

            //Act
            viewModel.SeriesChangedCommand.Execute("");

            //Assert
            Assert.Equal(viewModel.OldWorkoutExercise.Series, viewModel.WorkoutExercise.Series);
        }

        [Fact]
        public void SeriesChangedCommand_Success()
        {
            //Arrange
            viewModel.WorkoutExercise = new WorkoutExercise { Series = 100 };
            viewModel.WorkoutExercise.Series = 200;

            //Act
            viewModel.SeriesChangedCommand.Execute("test");

            //Assert
            Assert.Equal(viewModel.WorkoutExercise.Series, viewModel.WorkoutExercise.Series);
        }

        [Fact]
        public void RepetitionChangedCommand_EmptyString_Success()
        {
            //Arrange
            viewModel.WorkoutExercise = new WorkoutExercise { Repetition = 100 };
            viewModel.WorkoutExercise.Repetition = 200;

            //Act
            viewModel.RepetitionChangedCommand.Execute("");

            //Assert
            Assert.Equal(viewModel.OldWorkoutExercise.Repetition, viewModel.WorkoutExercise.Repetition);
        }

        [Fact]
        public void RepetitionChangedCommand_Success()
        {
            //Arrange
            viewModel.WorkoutExercise = new WorkoutExercise { Repetition = 100 };
            viewModel.WorkoutExercise.Repetition = 200;

            //Act
            viewModel.RepetitionChangedCommand.Execute("test");

            //Assert
            Assert.Equal(viewModel.WorkoutExercise.Repetition, viewModel.WorkoutExercise.Repetition);
        }

        [Fact]
        public void WeightChangedCommand_EmptyString_Success()
        {
            //Arrange
            viewModel.WorkoutExercise = new WorkoutExercise { Weight = 100 };
            viewModel.WorkoutExercise.Weight = 200;

            //Act
            viewModel.WeightChangedCommand.Execute("");

            //Assert
            Assert.Equal(viewModel.OldWorkoutExercise.Weight, viewModel.WorkoutExercise.Weight);
        }

        [Fact]
        public void WeightChangedCommand_Success()
        {
            //Arrange
            viewModel.WorkoutExercise = new WorkoutExercise { Weight = 100 };
            viewModel.WorkoutExercise.Weight = 200;

            //Act
            viewModel.WeightChangedCommand.Execute("test");

            //Assert
            Assert.Equal(viewModel.WorkoutExercise.Weight, viewModel.WorkoutExercise.Weight);
        }
    }
}
