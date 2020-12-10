using GymHelper.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace GymHelper.Data
{
    public class DataContext : DbContext
    {
        private readonly string dbPath;
        public DataContext(string dbPath) : base()
        {
            this.dbPath = dbPath;
            Database.EnsureCreated();
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Workout> Workouts { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite($"Filename={dbPath}");
        }
    }
}
