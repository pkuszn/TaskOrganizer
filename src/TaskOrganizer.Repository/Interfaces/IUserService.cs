using System.Net;
using System.Threading.Tasks;
using TaskOrganizer.Domain.Models;
using TaskOrganizer.Repository.Dtos;

namespace TaskOrganizer.Repository.Interfaces;
public interface IUserService : IRepository<User, int>
{
    Task<AuthResult> AuthenticateUserAsync(NetworkCredential credential);
}
