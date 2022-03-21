using Microsoft.EntityFrameworkCore;
using System;
using System.IO;
using TaskOrganizer.Domain.Models;

namespace TaskOrganizer.EFCore
{
    public class MyDbContext : DbContext
    {
        public DbSet<TaskModel> TaskModels { get; set; }
        public MyDbContext(DbContextOptions options) : base(options) {}

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }
    }

}

