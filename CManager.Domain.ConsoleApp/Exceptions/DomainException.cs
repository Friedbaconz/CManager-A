namespace CManager.Domain.ConsoleApp.Exceptions;

public sealed class DomainException(string message) : Exception(message)
{
}
