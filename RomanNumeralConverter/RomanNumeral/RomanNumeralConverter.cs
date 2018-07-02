using System;
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
            var result = new List<Fragment>();
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
        public static List<Fragment> GetFragmentsForRomanNumeral(RomanNumeral romanNumeral, int value)
        {
            var result = new List<Fragment>();
            var count = GetFullNumeralCount(value, romanNumeral);
            var fragmentString = GetAdditiveFragmentString(count, romanNumeral);

            if (fragmentString.Length > 0)
            {
                var fragment = new Fragment(fragmentString);

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
        public static Fragment GetSubtractiveFragment(RomanNumeral currentRomanNumeral, int value)
        {
            Fragment fragment = null;

            // TODO: Implement logic - a roman numeral can only be subtractive if the next biggest roman numeral
            // isn't double the value of the current one, for example:
            // 'I' (1) can be subtractive because 'V' (5) is 5 times the value of 'I'
            // 'V' (5) cannot be subtractive because 'X' (10) is double the value 'V'
                        
            
            var enumValues = (RomanNumeral[])Enum.GetValues(typeof(RomanNumeral));

            // Loop through enums and check if enum is a subtractor
            foreach (RomanNumeral enumValue in enumValues.OrderByDescending(numeral => (int)numeral))
            {
                if (CanBeSubtractor(enumValue))
                {
                    // check if value is greater than enumValue and if value is difference between currentRomanNumeral and enumValue
                    if ((value > (int)enumValue) && (value >= ((int)currentRomanNumeral - (int)enumValue)))
                    {
                        // if greater, we need to use a subtractive fragment
                        fragment = new Fragment(GetSubtractiveFragmentString(enumValue, currentRomanNumeral));
                        return fragment;
                    }
                }
            }

            return fragment;
        }

        /// <summary>
        /// Returns a string representation of the specified numeral and numeral count
        /// </summary>
        /// <param name="numeralCount"></param>
        /// <param name="romanNumeral"></param>
        /// <returns></returns>
        public static string GetAdditiveFragmentString(int numeralCount, RomanNumeral romanNumeral)
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
        public static string GetSubtractiveFragmentString(RomanNumeral firstLetter, RomanNumeral secondLetter)
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

        /// <summary>
        /// A roman numeral can only be subtractive if the next biggest roman numeral 
        /// is not double the value of the current one, for example: 
        /// 'I' (1) can be subtractive because 'V' (5) is 5 times the value of 'I' 
        /// 'V' (5) cannot be subtractive because 'X' (10) is double the value 'V'
        /// </summary>
        /// <param name="numeral"></param>
        /// <returns></returns>
        public static bool CanBeSubtractor(RomanNumeral numeral)
        {
            if (numeral == RomanNumeral.None)
            {
                return false;
            }

            var enumValues = (RomanNumeral[])Enum.GetValues(typeof(RomanNumeral));

            var numeralIndex = Array.IndexOf(enumValues, numeral);

            if (numeralIndex > -1)
            {
                try
                {
                    var nextBiggestRomanNumeral = enumValues.ElementAt(numeralIndex + 1);

                    // Return false if next biggest enum value is 2 times bigger than current value
                    return (int)nextBiggestRomanNumeral / (int)numeral != 2;
                }
                // Will be thrown if there is no bigger roman numeral, return false in this case
                catch (ArgumentOutOfRangeException ex)
                {                    
                    return false;                    
                }
            }

            throw new ArgumentException("Invalid numeral value", "numeral");
        }
        #endregion
    }
}