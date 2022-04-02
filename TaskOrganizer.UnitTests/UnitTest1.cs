using System;
using TaskOrganizer.Domain.Models;
using TaskOrganizer.Domain.Services;
using TaskOrganizer.EFCore;
using TaskOrganizer.EFCore.Services;
using TaskOrganizer.Model;
using AutoMapper;
using NUnit.Framework;
using TaskOrganizer.MapperProfiles;

namespace TaskOrganizer.UnitTests
{
    [TestFixture]

    public class UnitTest1
    {
        [Test]
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
            catch (Exception ex)
            {
                Console.WriteLine(ex.InnerException.Message);
            }
        }

        [Test]
        public void TestInsertionAsync()
        {
            IDataService<TaskModel> taskService = new GenericDataService<TaskModel>(new MyDbContextFactory());
            taskService.Create(new TaskModel
            {
                TaskDesc = "xD"
            });
        }

        [Test]
        public void MapTest()
        {
            var taskModel = new TodoModel()
            {
                Id = 0,
                Task = "xD",
                CreatedDate = DateTime.MaxValue,
                DoneTaskDate = DateTime.MinValue,
                IsSelected = true
            };
            var dtoTask = new TaskModel();
            var config = new MapperConfiguration(cfg => cfg.AddProfile<TaskProfile>());
        }

        [Test]
        public void MappingConfigTest()
        {
            var config = new MapperConfiguration(cfg => cfg.AddProfile<TaskProfile>());
            config.AssertConfigurationIsValid();
        }
    }
}
