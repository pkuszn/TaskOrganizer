using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace TaskOrganizer.EFCore
{
    /// <summary>
    /// Class responsible for enabling design-time services for context types that do not have a public default constructor.
    /// </summary>
    public class MyDbContextFactory : IDesignTimeDbContextFactory<MyDbContext>
    {
        public MyDbContext CreateDbContext(string[] args = null)
        {
            var options = new DbContextOptionsBuilder<MyDbContext>();
            string dir = @"\TaskOrganizer\task_organizer.db";
            string dbPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            options.UseSqlite($"Data Source={dbPath}{dir}");    
            return new MyDbContext(options.Options);
        }
    }
}
