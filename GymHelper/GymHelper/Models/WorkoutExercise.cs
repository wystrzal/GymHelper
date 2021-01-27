using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace GymHelper.Models
{
    public class WorkoutExercise : ICloneable
    {
        public int Id { get; set; }
        public DateTime Date { get; set; } = DateTime.Now;
        public int Series { get; set; }
        public int Repetition { get; set; }
        public int Weight { get; set; }
        public int ExerciseId { get; set; }
        public Exercise Exercise { get; set; }
        public int WorkoutId { get; set; }
        public Workout Workout { get; set; }

        public object Clone()
        {
            return (WorkoutExercise)MemberwiseClone();
        }
    }
}
