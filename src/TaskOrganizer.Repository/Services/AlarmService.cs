using TaskOrganizer.Domain.Models;
using TaskOrganizer.Repository.Interfaces;

namespace TaskOrganizer.Repository.Services;
public class AlarmService(TaskOrganizerDbContext dbContext) : BaseRepository<Alarm, int>(dbContext), IAlarmService { }
