using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TaskOrganizer.Domain.Models;
using TaskOrganizer.Repository.Interfaces;

namespace TaskOrganizer.Repository.Services;
public class PomodoroService : BaseRepository<Pomodoro, int>, IPomodoroService
{
    public PomodoroService(TaskOrganizerDbContext dbContext) : base(dbContext) { }

    public async Task<List<Pomodoro>> GetPomodoroAsync(int idPomodoroSession)
    {
        return await DbContext.Pomodoros.Where(q => q.IdPomodoroSession == idPomodoroSession).ToListAsync();
    }
}
