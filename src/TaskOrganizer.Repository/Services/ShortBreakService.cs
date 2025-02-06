using TaskOrganizer.Domain.Models;
using TaskOrganizer.Repository.Interfaces;

namespace TaskOrganizer.Repository.Services;
public class ShortBreakService(TaskOrganizerDbContext dbContext) : BaseRepository<ShortBreak, int>(dbContext), IShortBreakService { }

