using System.Net;
using System.Threading;
using System.Threading.Tasks;
using TaskOrganizer.Repository.Dtos;

namespace TaskOrganizer.Repository.Interfaces;
public interface IUserService
{
    Task<AuthResult> AuthenticateUserAsync(NetworkCredential credential, CancellationToken cancellationToken);
}
