using GymHelper.Helpers.Extensions;
using GymHelper.Models;
using Microcharts;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Threading.Tasks;


namespace GymHelper.Helpers.Charts
{
    public class LastRepetitionsEntryPreparer : ChartEntryPreparer
    {
        private readonly List<ChartEntry> LastRepetitions;
        private const int takeLastExercises = 8;
        private readonly int exerciseId;

        public LastRepetitionsEntryPreparer(int exerciseId)
        {
            LastRepetitions = new List<ChartEntry>();
            this.exerciseId = exerciseId;
        }

        public override async Task<List<ChartEntry>> PrepareChartEntry()
        {
            var workoutExercises = await unitOfWork.Repository<WorkoutExercise>()
                .ReadAllByCondition(x => x.ExerciseId == exerciseId, x => x.Date, takeLastExercises, orderASC: false);

            foreach (var workoutExercise in workoutExercises)
            {
                FillChartEntryData(LastRepetitions, workoutExercise.Repetition,
                    workoutExercise.Date.ToString("MMMM dd", new CultureInfo("pl-PL")).Capitalize());
            }

            return LastRepetitions;
        }
    }
}
