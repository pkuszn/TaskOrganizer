using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using TaskOrganizer.Domain.Models;
using TaskOrganizer.Domain.Services;
using TaskOrganizer.EFCore;
using TaskOrganizer.EFCore.Services;

namespace TaskOrganizer.UnitTests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestDatabase()
        {
            try
            {
                IDataService<TaskModel> userService = new GenericDataService<TaskModel>(new MyDbContextFactory());
                userService.CreatedMethod(new TaskModel
                {
                    TaskDesc = "test"
                });
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.InnerException.Message);
            }
        }

        [TestMethod]
        public void TestInsertionAsync()
        {
            IDataService<TaskModel> taskService = new GenericDataService<TaskModel>(new MyDbContextFactory());
            taskService.Create(new TaskModel
            {
                TaskDesc = "asdasdsadas"
            });
        }
    }
}
