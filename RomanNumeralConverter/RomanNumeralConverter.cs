using System.Collections.Generic;
using System.Linq;
using static System.String;

namespace RomanNumeralDecimalConverter
{
    public static class RomanNumeralConverter
    {
        #region Decimal > Roman Numeral
        /// <summary>
        /// Does what it says on the tin
        /// </summary>
        /// <param name="number"></param>
        /// <returns></returns>
        public static string ConvertToRomanNumerals(int number)
        {
            // Create fragment list and list of numeral characters to loop through
            var result = new List<RomanNumeralFragment>();
            var romanNumerals = new List<RomanNumeral>
            {
                RomanNumeral.M,
                RomanNumeral.D,
                RomanNumeral.C,
                RomanNumeral.L,
                RomanNumeral.X,
                RomanNumeral.V,
                RomanNumeral.I
            };


            foreach (var romanNumeral in romanNumerals)
            {
                var fragments = GetFragmentsForRomanNumeral(romanNumeral, number);

                // Deduct value of fragment from remaining number
                number = number - fragments.Sum(f => f.Value);

                result.AddRange(fragments);
            }

            // Join fragment strings together and output
            var output = Join("", result.Select(r => r.RomanNumerals).AsEnumerable());
            return output;
        }

        /// <summary>
        /// Returns one of:
        ///     0 fragments
        ///     1 additive or subtractive fragment
        ///     Both
        /// </summary>
        /// <param name="romanNumeral"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        private static List<RomanNumeralFragment> GetFragmentsForRomanNumeral(RomanNumeral romanNumeral, int value)
        {
            var result = new List<RomanNumeralFragment>();
            var count = GetFullNumeralCount(value, romanNumeral);
            var fragmentString = GetAdditiveFragmentString(count, romanNumeral);

            if (fragmentString.Length > 0)
            {
                var fragment = new RomanNumeralFragment(fragmentString);

                result.Add(fragment);

                value = value - fragment.Value;
            }

            var subtractiveFragment = GetSubtractiveFragment(romanNumeral, value);

            if (subtractiveFragment != null)
            {
                result.Add(subtractiveFragment);
            }

            return result;
        }

        /// <summary>
        /// Returns a subtractive fragment using the specified roman numeral.  Returns null if not needed
        /// </summary>
        /// <param name="currentRomanNumeral"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        private static RomanNumeralFragment GetSubtractiveFragment(RomanNumeral currentRomanNumeral, int value)
        {
            RomanNumeralFragment fragment = null;

            // check if value if difference is greater than numeral minus C
            if ((value > (int)RomanNumeral.C) && (value >= ((int)currentRomanNumeral - (int)RomanNumeral.C)))
            {
                // if greater, we need to use a C subtractive fragment
                fragment = new RomanNumeralFragment(GetSubtractiveFragmentString(RomanNumeral.C, currentRomanNumeral));
            }
            // if false, check for X and I using rules above
            else if ((value > (int)RomanNumeral.X) && (value >= ((int)currentRomanNumeral - (int)RomanNumeral.X)))
            {
                // if greater, we need to use a X subtractive fragment
                fragment = new RomanNumeralFragment(GetSubtractiveFragmentString(RomanNumeral.X, currentRomanNumeral));
            }
            else if ((value > (int)RomanNumeral.I) && (value >= ((int)currentRomanNumeral - (int)RomanNumeral.I)))
            {
                // if greater, we need to use a I subtractive fragment
                fragment = new RomanNumeralFragment(GetSubtractiveFragmentString(RomanNumeral.I, currentRomanNumeral));
            }

            return fragment;
        }

        /// <summary>
        /// Returns a string representation of the specified numeral and numeral count
        /// </summary>
        /// <param name="numeralCount"></param>
        /// <param name="romanNumeral"></param>
        /// <returns></returns>
        private static string GetAdditiveFragmentString(int numeralCount, RomanNumeral romanNumeral)
        {
            var result = Empty;

            for (var i = 0; i < numeralCount; i++)
            {
                result += romanNumeral.ToString();
            }

            return result;
        }

        /// <summary>
        /// Simply appends first and second parameters as a concatenated string
        /// </summary>
        /// <param name="firstLetter"></param>
        /// <param name="secondLetter"></param>
        /// <returns></returns>
        private static string GetSubtractiveFragmentString(RomanNumeral firstLetter, RomanNumeral secondLetter)
        {
            var result = firstLetter + secondLetter.ToString();
            return result;
        }

        ///// <summary>
        ///// Returns the number of numerals required for the specified value.  Does not include subtractive fragments
        ///// </summary>
        ///// <param name="value"></param>
        ///// <param name="numeral"></param>
        ///// <returns></returns>
        private static int GetFullNumeralCount(int value, RomanNumeral numeral)
        {
            // Get number of whole numerals to return from value
            var numeralCount = value / (int)numeral;

            return numeralCount;
        }
        #endregion
    }
}