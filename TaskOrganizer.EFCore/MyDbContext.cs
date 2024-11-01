﻿using Microsoft.EntityFrameworkCore;
using TaskOrganizer.Domain.Models;

namespace TaskOrganizer.EFCore;

public class MyDbContext : DbContext
{
    public DbSet<TaskModel> TaskModels { get; set; }
    public DbSet<PomodoroModel> PomodoroModels { get; set; }
    public MyDbContext(DbContextOptions options) : base(options) {}

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
    }
}

