namespace CManager.Domain.ConsoleApp.Models.Costumers;

public sealed class ObjectResult<T>(bool success, string message, T? data): ProfileResult(success, message)
{
    public T? Result { get; set; } = data;
}