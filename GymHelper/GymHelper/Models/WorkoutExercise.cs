using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace GymHelper.Models
{
    public class WorkoutExercise
    {
        public int Id { get; set; }
        public DateTime Date { get; set; } = DateTime.Now;

        [Range(0, int.MaxValue)]
        public int Series { get; set; }

        [Range(0, int.MaxValue)]
        public int Repetition { get; set; }

        [Range(0, int.MaxValue)]
        public int Weight { get; set; }

        public int ExerciseId { get; set; }
        public virtual Exercise Exercise { get; set; }
        public int WorkoutId { get; set; }
        public virtual Workout Workout { get; set; }
    }
}
