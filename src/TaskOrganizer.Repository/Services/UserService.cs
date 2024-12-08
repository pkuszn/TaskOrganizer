using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using TaskOrganizer.Domain.Models;
using TaskOrganizer.Repository.Interfaces;

namespace TaskOrganizer.Repository.Services;
public class UserService : IUserService
{
    private readonly IRepository<User, int> Repository;
    public UserService(IRepository<User, int> repository) 
    {
        Repository = repository ?? throw new ArgumentNullException(nameof(repository));
    }

    public async Task<bool> AuthenticateUserAsync(NetworkCredential credential, CancellationToken cancellationToken)
    {
        User user = await Repository.GetAsync(q => q.Login.Equals(credential.UserName), cancellationToken);

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
