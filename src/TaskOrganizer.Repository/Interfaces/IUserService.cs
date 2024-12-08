using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace TaskOrganizer.Repository.Interfaces;
public interface IUserService
{
    Task<bool> AuthenticateUserAsync(NetworkCredential credential, CancellationToken cancellationToken);
}
