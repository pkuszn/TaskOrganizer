using System.Collections.Generic;
using System.Threading.Tasks;
using TaskOrganizer.Domain.Models;

namespace TaskOrganizer.Repository.Interfaces;
public interface IPomodoroService : IRepository<Pomodoro, int> 
{
    Task<List<Pomodoro>> GetPomodoroAsync(int idPomodoroSession);
}
