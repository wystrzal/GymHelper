using SQLite;
using SQLiteNetExtensions.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace GymHelper.Models
{
    public class Workout : BaseModel
    {
        private int workoutId;
        public int WorkoutId
        {
            get { return workoutId; }
            set
            {
                workoutId = value;
                OnPropertyChanged("WorkoutId");
            }
        }

        public virtual User User { get; set; }

        private int userId;
        public int UserId
        {
            get { return userId; }
            set
            {
                userId = value;
                OnPropertyChanged("UserId");
            }
        }

        private string name;
        [Required]
        public string Name
        {
            get { return name; }
            set
            {
                name = value;
                OnPropertyChanged("Name");
            }
        }

        public DateTime Date { get; set; } = DateTime.Now;
    }
}
