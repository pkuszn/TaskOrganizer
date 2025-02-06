using TaskOrganizer.Domain.Models;
using TaskOrganizer.Repository.Interfaces;

namespace TaskOrganizer.Repository.Services;
public class PomodoroIntervalService(TaskOrganizerDbContext dbContext) : BaseRepository<PomodoroInterval, int>(dbContext), IPomodoroIntervalService { }
