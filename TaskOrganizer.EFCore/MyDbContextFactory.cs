using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System;

namespace TaskOrganizer.EFCore;

/// <summary>
/// Class responsible for enabling design-time services for context types that do not have a public default constructor.
/// </summary>
public class MyDbContextFactory : IDesignTimeDbContextFactory<MyDbContext>
{
    public MyDbContextFactory()
    {
        
    }

    public MyDbContext CreateDbContext(string[] args = null)
    {
        DbContextOptionsBuilder<MyDbContext> options = new();
        string dir = @"\TaskOrganizer\task_organizer.db";
        string dbPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
        options.UseSqlite($"Data Source={dbPath}{dir}");    
        return new MyDbContext(options.Options);
    }
}
