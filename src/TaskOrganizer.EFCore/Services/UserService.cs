using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using TaskOrganizer.Domain.Models;
using TaskOrganizer.Repository.Interfaces;

namespace TaskOrganizer.Repository.Services;
public class UserService : BaseRepository<User, int>, IUserService
{
    public UserService(TaskOrganizerDbContext dbContext) : base(dbContext) { }

    public async Task<bool> AuthenticateUserAsync(NetworkCredential credential)
    {
        User user = await DbContext.Users
            .Where(q => q.Login.Equals(credential.UserName))
            .FirstOrDefaultAsync();

        if (user == null)
        {
            return false;
        }

        if (!user.Password.Equals(credential.Password))
        {
            return false;
        }

        return true;
    }
}
