using SQLite;
using SQLiteNetExtensions.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace GymHelper.Models
{
    public class Exercise : ICloneable
    {
        public int ExerciseId { get; set; }
        public string Name { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public ICollection<WorkoutExercise> WorkoutsExercises { get; set; }

        public object Clone()
        {
            return (Exercise)MemberwiseClone();
        }
    }
}
