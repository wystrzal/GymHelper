using System;
using System.Collections.Generic;
using System.Text;

namespace GymHelper.Helpers.Extensions
{
    public static class StringExtensions
    {
        public static string Capitalize(this string stringValue)
        {
            if (string.IsNullOrWhiteSpace(stringValue))
            {
                return "";
            }

            return char.ToUpper(stringValue[0]) + stringValue.Substring(1);
        }
    }
}
