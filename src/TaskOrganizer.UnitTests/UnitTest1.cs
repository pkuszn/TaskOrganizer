namespace TaskOrganizer.UnitTests;

[TestFixture]

public class UnitTest1
{

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
            Id = 7,
            Task = "asdasdsadazxzzzzzzzzzzzz",
            CreatedDate = DateTime.MaxValue,
            DoneTaskDate = DateTime.MinValue,
            IsSelected = true
        };
        var dtoTask = new TaskModel();
        var config = new MapperConfiguration(cfg => cfg.AddProfile<TaskProfile>());
        IDataService<TaskModel> taskService = new GenericDataService<TaskModel>(new MyDbContextFactory());
        taskService.Create(dtoTask);


    }

    [Test]
    public void MappingConfigTest()
    {
        var config = new MapperConfiguration(cfg => cfg.AddProfile<TaskProfile>());
        config.AssertConfigurationIsValid();
    }
}
