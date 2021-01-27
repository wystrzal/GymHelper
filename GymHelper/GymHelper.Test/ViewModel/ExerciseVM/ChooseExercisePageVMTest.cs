using GymHelper.Data.Interfaces;
using GymHelper.Models;
using GymHelper.View;
using GymHelper.ViewModel;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace GymHelper.Test.ViewModel.ExerciseVM
{
    public class ChooseExercisePageVMTest
    {
        private Mock<IUnitOfWork> unitOfWork;
        private readonly Mock<INavigateService> navigateService;
        private readonly ChooseExercisePageVM viewModel;

        public ChooseExercisePageVMTest()
        {
            navigateService = new Mock<INavigateService>();
            TestHelper.PrepareUnitOfWork(out unitOfWork);
            App.Data.NavigateService = navigateService.Object;
            App.Data.Workout = new Workout();
            viewModel = new ChooseExercisePageVM();
        }

        [Fact]
        public void NavigateToAddDataCommand_Success()
        {
            //Act
            viewModel.NavigateToAddDataCommand.Execute(null);

            //Assert
            navigateService.Verify(x => x.Navigate<NewExercisePage>(), Times.Once);
        }

        [Fact]
        public void NavigateToEditDataCommand_Success()
        {
            //Act
            viewModel.NavigateToEditDataCommand.Execute(It.IsAny<Exercise>());

            //Assert
            navigateService.Verify(x => x.Navigate<EditExercisePage>(It.IsAny<Exercise>()), Times.Once);
        }

        [Fact]
        public void DeleteData_Success()
        {
            //Arrange
            unitOfWork.Setup(x => x.Repository<Exercise>()
                .ReadAllByCondition(It.IsAny<Func<Exercise, bool>>(), It.IsAny<int>(), It.IsAny<int>()))
                .Returns(Task.FromResult(new List<Exercise> { new Exercise() }));

            //Act
            viewModel.DeleteDataCommand.Execute(It.IsAny<Exercise>());

            //Assert
            unitOfWork.Verify(x => x.SaveChanges(), Times.Once);
            unitOfWork.Verify(x => x.Repository<Exercise>().Delete(It.IsAny<Exercise>()), Times.Once);
            Assert.Single(viewModel.Collection);
        }

        [Fact]
        public void Refresh_Success()
        {
            //Arrange
            unitOfWork.Setup(x => x.Repository<Exercise>()
                .ReadAllByCondition(It.IsAny<Func<Exercise, bool>>(), It.IsAny<int>(), It.IsAny<int>()))
                .Returns(Task.FromResult(new List<Exercise> { new Exercise() }));

            //Act
            viewModel.RefreshCommand.Execute(null);

            //Assert
            Assert.Single(viewModel.Collection);
        }

        [Fact]
        public void SearchData_Success()
        {
            //Arrange
            unitOfWork.Setup(x => x.Repository<Exercise>()
                .ReadAllByCondition(It.IsAny<Func<Exercise, bool>>(), It.IsAny<int>(), It.IsAny<int>()))
                .Returns(Task.FromResult(new List<Exercise> { new Exercise() }));

            //Act
            viewModel.PerformSearchCommand.Execute("");

            //Assert
            Assert.Single(viewModel.Collection);
        }

        [Fact]
        public void AddSelectedData_Success()
        {
            //Arrange
            viewModel.SelectedData = new List<Exercise> { new Exercise(), new Exercise() };
            var selectedDataCount = viewModel.SelectedData.Count;

            unitOfWork.Setup(x => x.Repository<WorkoutExercise>()
                .CheckIfExistByCondition(It.IsAny<Func<WorkoutExercise, bool>>())).Returns(Task.FromResult(false));

            //Act
            viewModel.AddSelectedDataCommand.Execute(null);

            //Assert
            unitOfWork.Verify(x => x.Repository<WorkoutExercise>().Add(It.IsAny<WorkoutExercise>()), Times.Exactly(selectedDataCount));
            unitOfWork.Verify(x => x.SaveChanges(), Times.Exactly(selectedDataCount));
            navigateService.Verify(x => x.NavigateBack(), Times.Once);
        }
    }
}
