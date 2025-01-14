using System.Collections.Generic;
using System.Threading.Tasks;

namespace TaskOrganizer.Repository.Interfaces;
public interface ITaskService
{
    Task<List<TaskOrganizer.Domain.Models.Task>> GetTasksAsync(int idUser);
    Task<TaskOrganizer.Domain.Models.Task> GetTaskAsync(int idUser, int idTask);
}
