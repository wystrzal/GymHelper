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

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite($"Filename={dbPath}");
        }
    }
}
