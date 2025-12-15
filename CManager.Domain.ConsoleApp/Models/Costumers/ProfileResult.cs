namespace CManager.Domain.ConsoleApp.Models.Costumers;

public class ProfileResult(bool success, string message)
{
    public bool Success { get; set; } = success;
    public string? Message { get; set; } = message;
}

