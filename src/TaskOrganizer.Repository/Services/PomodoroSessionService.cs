using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using TaskOrganizer.Domain.Models;
using TaskOrganizer.Repository.Interfaces;

namespace TaskOrganizer.Repository.Services;
public class PomodoroSessionService : BaseRepository<PomodoroSession, int>, IPomodoroSessionService
{
    public PomodoroSessionService(TaskOrganizerDbContext dbContext) : base(dbContext) { }
    public async Task<PomodoroSession> GetActivePomodoroSessionAsync(int idUser)
    {
        return await DbContext.PomodoroSessions.FirstOrDefaultAsync(q => q.IdUser == idUser && q.FinishDate.Equals(null));
    }
}
