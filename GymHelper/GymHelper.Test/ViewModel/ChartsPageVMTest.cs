using GymHelper.Data.Interfaces;
using GymHelper.Helpers.Charts.ChartPreparers;
using GymHelper.Models;
using GymHelper.ViewModel;
using Microcharts;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace GymHelper.Test.ViewModel
{
    public class ChartsPageVMTest
    {
        private Mock<IUnitOfWork> unitOfWork;
        private readonly Mock<INavigateService> navigateService;
        private readonly ChartsPageVM viewModel;

        public ChartsPageVMTest()
        {
            navigateService = new Mock<INavigateService>();
            TestHelper.PrepareUnitOfWork(out unitOfWork);
            App.Data.NavigateService = navigateService.Object;
            viewModel = new ChartsPageVM();
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
        public void SwitchView_Success()
        {
            //Arrange
            var exerciseSelectorIsVisible = viewModel.ExerciseSelectorIsVisible;
            var chartsIsVisible = viewModel.ChartsIsVisible;

            //Act
            viewModel.SwitchViewCommand.Execute(null);

            //Assert
            Assert.Equal(!exerciseSelectorIsVisible, viewModel.ExerciseSelectorIsVisible);
            Assert.Equal(!chartsIsVisible, viewModel.ChartsIsVisible);
        }

        [Fact]
        public void SelectExercise_Success()
        {
            //Arrange
            var exerciseSelectorIsVisible = viewModel.ExerciseSelectorIsVisible;
            var chartsIsVisible = viewModel.ChartsIsVisible;
            var chartPreparer = viewModel.ChartPreparer as ExerciseChartPreparer;

            unitOfWork.Setup(x => x.Repository<WorkoutExercise>()
                .ReadAllByCondition(It.IsAny<Func<WorkoutExercise, bool>>(), It.IsAny<Func<WorkoutExercise, DateTime>>(), It.IsAny<int>(),
                    It.IsAny<int>(), It.IsAny<bool>()))
                .Returns(Task.FromResult(new List<WorkoutExercise>()));

            unitOfWork.Setup(x => x.Repository<WorkoutExercise>()
                .ReadFirstByCondition(It.IsAny<Func<WorkoutExercise, bool>>(), It.IsAny<Func<WorkoutExercise, int>>(), It.IsAny<bool>()))
                .Returns(Task.FromResult(new WorkoutExercise()));

            //Act
            viewModel.SelectedExercise = new Exercise();

            //Assert
            Assert.Equal(!exerciseSelectorIsVisible, viewModel.ExerciseSelectorIsVisible);
            Assert.Equal(!chartsIsVisible, viewModel.ChartsIsVisible);
            Assert.Equal(typeof(LineChart), chartPreparer.LastWeightsChart.GetType());
            Assert.Equal(typeof(LineChart), chartPreparer.LastRepetitionsChart.GetType());
            Assert.Equal(typeof(LineChart), chartPreparer.MonthHighestWeightsChart.GetType());
            Assert.NotNull(chartPreparer.LastRepetitionsChart);
            Assert.NotNull(chartPreparer.LastWeightsChart);
            Assert.NotNull(chartPreparer.MonthHighestWeightsChart);
        }
    }
}