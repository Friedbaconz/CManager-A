using System;
using System.Collections.Generic;
using System.Text;

namespace CManager.Application.ConsoleApp.Validators.AdressValidators
{
   public static class StreetNameValidator
    {
        public static bool IsValidStreetName(string streetName)
        {
            if (string.IsNullOrWhiteSpace(streetName))
            {
                return false;
            }

            return true;
        }
    }
}
