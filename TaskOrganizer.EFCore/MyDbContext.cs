using Microsoft.EntityFrameworkCore;
using System;
using System.IO;
using TaskOrganizer.Domain.Models;

namespace TaskOrganizer.EFCore
{
    public class MyDbContext : DbContext
    {
        public DbSet<TaskModel> tasks { get; set; }
        public MyDbContext(DbContextOptions options) : base(options) { }
    }

}

