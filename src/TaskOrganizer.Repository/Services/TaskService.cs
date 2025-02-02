using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TaskOrganizer.Repository.Interfaces;

namespace TaskOrganizer.Repository.Services;
public class TaskService : BaseRepository<Domain.Models.Task, int>, ITaskService
{
    public TaskService(TaskOrganizerDbContext dbContext) : base(dbContext) { }
    public async Task<Domain.Models.Task> GetTaskAsync(int idUser, int idTask)
    {
        return await DbContext.Tasks.FirstOrDefaultAsync(q => q.IdUser == idUser && q.Id == idTask);
    }

    public async Task<List<Domain.Models.Task>> GetTasksAsync(int idUser)
    {
        return await DbContext.Tasks.Where(q => q.IdUser == idUser).ToListAsync();
    }
}
