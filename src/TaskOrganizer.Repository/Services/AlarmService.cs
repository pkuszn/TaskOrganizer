using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskOrganizer.Domain.Models;
using TaskOrganizer.Repository.Interfaces;

namespace TaskOrganizer.Repository.Services;
public class AlarmService : BaseRepository<Alarm, int>, IAlarmService
{
    public AlarmService(TaskOrganizerDbContext dbContext) : base(dbContext) {}

}
