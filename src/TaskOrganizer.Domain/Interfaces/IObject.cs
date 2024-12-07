namespace TaskOrganizer.Domain.Interfaces;

public interface IObject<T>
{
    T Id { get; }
}
