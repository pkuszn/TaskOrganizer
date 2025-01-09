using static TaskOrganizer.Repository.Consts.Enums;

namespace TaskOrganizer.Repository.Dtos;
public readonly record struct AuthResult
{
    public bool IsAuthenticated { get; init; }
    public AuthResultEnum Result { get; init; }
}
