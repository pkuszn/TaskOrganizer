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
            IDataService<TaskModel> userService = new GenericDataService<TaskModel>(new MyDbContextFactory());
            userService.Create(new TaskModel
            {
                TaskDesc = "testTask",
                CreatedDate = DateTime.Now,
                DoneTaskDate = DateTime.UtcNow,
                IsDoneTask = true
            }).Wait();
        }
    }
}
