namespace CManager.Application.ConsoleApp.Validators.AdressValidators;

public static class PostNumbersValidator
{
    public static bool IsValidPostNumbers(string postNumbers)
    {
        if (string.IsNullOrWhiteSpace(postNumbers))
            return false;

        var trimmedzip = postNumbers.Replace(" ", string.Empty);

        if (postNumbers.Length < 5 || postNumbers.Length > 6)
            return false;

        foreach (char c in postNumbers)
        {
            if (!char.IsLetterOrDigit(c))
                return false;
        }
        return true;
    }
}
