using System;
using System.Collections.Generic;
using System.Linq;

namespace RomanNumeralDecimalConverter
{
    public static class DecimalConverter
    {
        #region Roman Numeral > Decimal
        /// <summary>
        /// Does what it says on the tin
        /// </summary>
        /// <param name="romanNumerals"></param>
        /// <returns></returns>
        public static int ConvertToDecimal(string romanNumerals)
        {
            var fragments = new List<Fragment>();
            // Remove fragments from string until the string is empty
            do
            {
                var fragment = GetFirstFragment(romanNumerals);
                fragments.Add(fragment);
                romanNumerals = romanNumerals.Remove(romanNumerals.IndexOf(fragment.RomanNumerals, StringComparison.CurrentCulture), fragment.RomanNumerals.Length);

            } while (romanNumerals.Length > 0);

            // Return the value of the combined fragments
            return fragments.Sum(f => f.Value);
        }

        /// <summary>
        /// Returns the first Fragment from the specified string
        /// eg. CMIX would return CM, MMMCCCIX would return MMMCCC, etc.
        /// </summary>
        /// <param name="romanNumerals"></param>
        /// <returns></returns>
        private static Fragment GetFirstFragment(string romanNumerals)
        {            
            if (romanNumerals.Length > 1)
            {
                var firstChar = romanNumerals[0];
                var secondChar = romanNumerals[1];

                var isSubtractive = CommonFunctions.GetRomanNumeralValue(firstChar) < CommonFunctions.GetRomanNumeralValue(secondChar);

                // if subtractive, it will only use first 2 chars if the first is less than the second
                if (isSubtractive)
                {
                    return new Fragment(firstChar + secondChar.ToString());
                }
                return GetAdditiveFragments(romanNumerals);
            }
            // return single letter fragment
            return new Fragment(romanNumerals);
        }

        /// <summary>
        /// Loops through letters in romanNumerals until the end of the string, or until a subtractive fragment is found
        /// </summary>
        /// <param name="romanNumerals"></param>
        /// <returns></returns>
        private static Fragment GetAdditiveFragments(string romanNumerals)
        {
            var letters = string.Empty;
            // additive - loop through letters until end or we come across a subtractive fragment
            for (var i = 0; i < romanNumerals.Length; i++)
            {
                var letter = romanNumerals[i];

                // add first letter to string
                if (letters.Length == 0)
                {
                    letters += letter;
                }
                // if last letter, append to string
                else if (i == romanNumerals.Length - 1)
                {
                    letters += letter;
                }
                else
                {
                    //check if current and subsequent letter are subtractive
                    var secondLetter = romanNumerals[i + 1];

                    if (CommonFunctions.GetRomanNumeralValue(letter) < CommonFunctions.GetRomanNumeralValue(secondLetter))
                    {
                        // if subtractive, return fragment without current letter
                        return new Fragment(letters);
                    }
                    // add letter to string
                    letters += letter;
                }
            }

            return new Fragment(letters);
        }

        #endregion
    }
}