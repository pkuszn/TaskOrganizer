using System.Net;
using System.Threading.Tasks;
using TaskOrganizer.Domain.Models;
using TaskOrganizer.Repository.Dtos;
using TaskOrganizer.Repository.Interfaces;
using static TaskOrganizer.Repository.Consts.Enums;

namespace TaskOrganizer.Repository.Services;
public class UserService : BaseRepository<User, int>, IUserService
{
    public UserService(TaskOrganizerDbContext dbContext) : base(dbContext) { }
    public async Task<AuthResult> AuthenticateUserAsync(NetworkCredential credential)
    {
        if (string.IsNullOrWhiteSpace(credential?.UserName) || string.IsNullOrWhiteSpace(credential?.Password))
        {
            return new AuthResult
            {
                IsAuthenticated = false,
                Result = AuthResultEnum.Unexpected 
            };
        }

        User user = await GetAsync(q => q.Login.Equals(credential.UserName));
        if (user == null)
        {
            return new AuthResult
            {
                IsAuthenticated = false,
                Result = AuthResultEnum.InvalidUsername
            };
        }

        bool isPasswordValid = user.Password.Equals(credential.Password);
        if (!isPasswordValid)
        {
            return new AuthResult
            {
                IsAuthenticated = false,
                Result = AuthResultEnum.InvalidPassword
            };
        }

        return new AuthResult
        {
            IsAuthenticated = true,
            Result = AuthResultEnum.OK,
            IdUser = user.Id
        };
    }
}
