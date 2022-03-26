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
            string baseDir = AppDomain.CurrentDomain.BaseDirectory;

            //if "bin" is present, remove all the path starting from "bin" word
            if (baseDir.Contains("bin"))
            {
                int index = baseDir.IndexOf("bin");
                baseDir = baseDir.Substring(0, index);
                Debug.WriteLine(baseDir);
            }

            //String interpolation to reach the right path
            options.UseSqlite($"Data Source={baseDir}\\Db\\task_organizer.db");
            return new MyDbContext(options.Options);
        }
    }
}
