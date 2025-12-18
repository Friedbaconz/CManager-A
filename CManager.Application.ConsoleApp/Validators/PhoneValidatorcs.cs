using System.Net.Mail;

namespace CManager.Application.ConsoleApp.Validators;

public static class PhoneValidatorcs
{
    
    public static bool IsValidPhone(string phonenumber)
    {
            if (string.IsNullOrWhiteSpace(phonenumber))
                return false;

            var trimmedPhone = phonenumber.Replace(" ", string.Empty);

           if (trimmedPhone.Length < 9 || trimmedPhone.Length > 15)
                return false;

           return true;
    }
    
}
