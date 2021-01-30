using GymHelper.Data.Interfaces;
using GymHelper.Models;
using GymHelper.ViewModel;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace GymHelper.Test.ViewModel.ExerciseVM
{
    public class EditExercisePageVMTest
    {
        private Mock<IUnitOfWork> unitOfWork;
        private readonly Mock<INavigateService> navigateService;
        private readonly EditExercisePageVM viewModel;

        public EditExercisePageVMTest()
        {
            navigateService = new Mock<INavigateService>();
            TestHelper.PrepareUnitOfWork(out unitOfWork);
            App.Data.NavigateService = navigateService.Object;
            viewModel = new EditExercisePageVM();
        }

        [Fact]
        public void EditData_Success()
        {
            //Arrange
            var exercise = new Exercise { Name = "Name" };
            unitOfWork.Setup(x => x.Repository<Exercise>().Update(exercise));

            //Act
            viewModel.EditDataCommand.Execute(exercise);

            //Assert
            unitOfWork.Verify(x => x.Repository<Exercise>().Update(exercise), Times.Once);
            unitOfWork.Verify(x => x.SaveChanges(), Times.Once);
            navigateService.Verify(x => x.NavigateBack(), Times.Once);
        }

        [Fact]
        public void NameChangedCommand_EmptyString_Success()
        {
            //Arrange
            viewModel.Exercise = new Exercise { Name = "OldValue" };
            viewModel.Exercise.Name = "NewValue";

            //Act
            viewModel.NameChangedCommand.Execute("");

            //Assert
            Assert.Equal(viewModel.OldExercise.Name, viewModel.Exercise.Name);
        }

        [Fact]
        public void NameChangedCommand_Success()
        {
            //Arrange
            viewModel.Exercise = new Exercise { Name = "OldValue" };
            viewModel.Exercise.Name = "NewValue";

            //Act
            viewModel.NameChangedCommand.Execute("test");

            //Assert
            Assert.Equal(viewModel.Exercise.Name, viewModel.Exercise.Name);
        }
    }
}