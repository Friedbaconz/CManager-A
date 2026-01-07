using System;
using System.Collections.Generic;
using System.Text;

namespace CManager.Application.ConsoleApp.Validators.AdressValidators
{
    public static class OrtValidator
    {
        public static bool IsValidOrt(string streetName)
        {
            if (string.IsNullOrWhiteSpace(streetName))
            {
                return false;
            }

            return true;
        }
    }
}
