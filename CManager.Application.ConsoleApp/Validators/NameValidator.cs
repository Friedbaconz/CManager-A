namespace CManager.Application.ConsoleApp.Validators;

public static class NameValidator
{
    public static bool IsValidName(string value, int minLength = 2)
    {
        return !string.IsNullOrWhiteSpace(value) && value.Trim().Length > minLength;
    }
}
