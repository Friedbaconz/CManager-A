namespace CManager.Application.ConsoleApp.Helpers.Costumers;

public static class IdGenerator
{
    public static string Generate() => Guid.NewGuid().ToString();
}
