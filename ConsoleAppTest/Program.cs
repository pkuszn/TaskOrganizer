using Microsoft.EntityFrameworkCore.Internal;
using System;
using TaskOrganizer.Domain.Models;
using TaskOrganizer.Domain.Services;
using TaskOrganizer.EFCore;
using TaskOrganizer.EFCore.Services;

namespace ConsoleAppTest
{
    class Program
    {
        static void Main(string[] args)
        {
            IDataService<TaskModel> taskService = new GenericDataService<TaskModel>(new MyDbContextFactory());
            taskService.Create(new TaskModel
            {
                Id = 1,
                TaskDesc = "dupa",
                CreatedDate = DateTime.Now,
                DoneTaskDate = DateTime.Now,
                IsSelected = true
            }); 
        }
    }
}
