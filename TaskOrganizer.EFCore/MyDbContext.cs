using Microsoft.EntityFrameworkCore;
using System;
using System.IO;

namespace TaskOrganizer.EFCore
{
    public class MyDbContext : DbContext
    {
        public DbSet<TaskContext> tasks { get; set; }

        /// <summary>
        /// Method to configure the database. Is called for each time instance of the context is created
        /// </summary>
        /// <param name="optionsBuilder"></param>
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=task_organizer_db.sqlite");
        }
    }

}

