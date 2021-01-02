using GymHelper.Helpers.Extensions;
using GymHelper.Models;
using Microcharts;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Threading.Tasks;

namespace GymHelper.Helpers.Chart
{
    public class LastWeightsEntryPreparer : ChartEntryPreparer
    {
        private readonly List<ChartEntry> LastWeights;
        private const int takeLastExercises = 8;

        public LastWeightsEntryPreparer()
        {
            LastWeights = new List<ChartEntry>();
        }

        public override async Task<List<ChartEntry>> GetChartEntry(Exercise exercise)
        {
            var workoutExercises = await unitOfWork.Repository<WorkoutExercise>()
                .ReadAllByCondition(x => x.ExerciseId == exercise.ExerciseId, x => x.Date, takeLastExercises, false);

            foreach (var workoutExercise in workoutExercises)
            {
                FillChartEntryData(LastWeights, workoutExercise.Weight,
                    workoutExercise.Date.ToString("MMMM dd", new CultureInfo("pl-PL")).Capitalize());
            }

            return LastWeights;
        }
    }
}
