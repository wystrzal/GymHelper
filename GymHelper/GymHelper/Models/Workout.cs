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

        public virtual User User { get; set; }
        public int UserId { get; set; }

        [Required]
        public string Name { get; set; }

        public DateTime Date { get; set; } = DateTime.Now;
    }
}
