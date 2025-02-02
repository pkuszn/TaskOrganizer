using System.Threading.Tasks;
using TaskOrganizer.Domain.Models;

namespace TaskOrganizer.Repository.Interfaces;
public interface IPomodoroSessionService : IRepository<PomodoroSession, int>
{
    Task<PomodoroSession> GetActivePomodoroSessionAsync(int idUser);
}
