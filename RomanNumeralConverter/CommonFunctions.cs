using System;

namespace RomanNumeralDecimalConverter
{
    public static class CommonFunctions
    {
        /// <summary>
        /// Returns the integer value of the specified char in the RomanNumeral enum
        /// eg. X will return 10, M = 1000, etc
        /// </summary>
        /// <param name="romanNumeral"></param>
        /// <returns></returns>
        public static int GetRomanNumeralValue(char romanNumeral)
        {
            RomanNumeral result;

            if (Enum.TryParse(romanNumeral.ToString(), out result))
            {
                return (int)result;
            }

            return 0;
        }
    }
}
