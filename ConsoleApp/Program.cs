using Intuit.Ipp.DataService;
using System;
using TaskOrganizer.Domain.Models;
using TaskOrganizer.Domain.Services;
using TaskOrganizer.EFCore;
using TaskOrganizer.EFCore.Services;

namespace ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            IDataService<TaskModel> taskService = new GenericDataService<TaskModel>(new MyDbContextFactory());
            taskService.Create(new TaskModel
            {
                TaskDesc = "testTask"
            }).Wait(15);
        }
    }
}
