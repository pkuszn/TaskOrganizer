using TaskOrganizer.Domain.Models;
using TaskOrganizer.Repository.Interfaces;

namespace TaskOrganizer.Repository.Services;
public class LongBreakService(TaskOrganizerDbContext dbContext) : BaseRepository<LongBreak, int>(dbContext), ILongBreakService { }

