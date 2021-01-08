using SQLite;
using SQLiteNetExtensions.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace GymHelper.Models
{
    public class Workout
    {
        public int WorkoutId { get; set; }

        [Required]
        public string Name { get; set; }

        public int UserId { get; set; }
        public User User { get; set; }

        public DateTime Date { get; set; } = DateTime.Now;
        public ICollection<WorkoutExercise> WorkoutsExercises { get; set; }
    }
}
