using TaskOrganizer.Domain.Models;
using TaskOrganizer.Repository.Interfaces;

namespace TaskOrganizer.Repository.Services;
public class TaskCategoryService(TaskOrganizerDbContext dbContext) : BaseRepository<TaskCategory, int>(dbContext), ITaskCategoryService { }

