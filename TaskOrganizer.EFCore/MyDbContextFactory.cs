﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            options.UseSqlite("Data Source=task_organizer_db.sqlite");
            return new MyDbContext(options.Options);
        }
    }
}
