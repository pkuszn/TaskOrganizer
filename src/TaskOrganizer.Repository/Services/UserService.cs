using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using TaskOrganizer.Domain.Models;
using TaskOrganizer.Repository.Dtos;
using TaskOrganizer.Repository.Interfaces;
using static TaskOrganizer.Repository.Consts.Enums;

namespace TaskOrganizer.Repository.Services;
public class UserService : IUserService
{
    private readonly IRepository<User, int> Repository;
    public UserService(IRepository<User, int> repository) 
    {
        Repository = repository ?? throw new ArgumentNullException(nameof(repository));
    }

    public async Task<AuthResult> AuthenticateUserAsync(NetworkCredential credential, CancellationToken cancellationToken)
    {
        if (string.IsNullOrWhiteSpace(credential?.UserName) || string.IsNullOrWhiteSpace(credential?.Password))
        {
            return new AuthResult
            {
                IsAuthenticated = false,
                Result = AuthResultEnum.Unexpected 
            };
        }

        User user = await Repository.GetAsync(q => q.Login.Equals(credential.UserName), cancellationToken);
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
            Result = AuthResultEnum.OK
        };
    }
}
