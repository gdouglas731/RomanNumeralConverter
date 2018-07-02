using System;
using System.Globalization;
using System.Linq;

namespace RomanNumeralDecimalConverter
{
    /// <summary>
    /// Defines the structure and properties of a Roman Numeral Fragment.
    /// A fragment can be defined as either additive or subtractive.
    /// An additive fragments value is calculated by adding the decimal values of the letters
    /// A subtractive fragments value is calculated by subtracting the value of the first letter from the second
    /// </summary>
    public class Fragment
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="letters"></param>
        public Fragment(string letters)
        {
            RomanNumerals = letters;
        }

        /// <summary>
        /// String representation of Roman Numeral Fragment
        /// </summary>
        public string RomanNumerals { get; set; }

        /// <summary>
        /// Calculates the integer value of the fragment
        /// </summary>
        public int Value { 
            get            
            {
                var result = 0;

                if (IsAdditive)
	            {
	                result += RomanNumerals.Sum(CommonFunctions.GetRomanNumeralValue);
	            }
                else if(IsSubtractive)
                {
                    result = CommonFunctions.GetRomanNumeralValue(RomanNumerals[1]) - CommonFunctions.GetRomanNumeralValue(RomanNumerals[0]);
                }
                else
                {
                    result = CommonFunctions.GetRomanNumeralValue(RomanNumerals[0]);
                }

                return result;
            }
        }

        public bool IsAdditive
        {
            get
            {
                // if only one character long, return false
                if (RomanNumerals.Length <= 1)
                    return false;

                // if first letter is less than second letter, return false
                RomanNumeral firstLetter;

                if (Enum.TryParse(RomanNumerals[0].ToString(), out firstLetter) == false)
                    return false;

                RomanNumeral secondLetter;

                if (Enum.TryParse(RomanNumerals[1].ToString(), out secondLetter) == false)
                    return false;

                return (int)firstLetter >= (int)secondLetter;
            }
        }

        public bool IsSubtractive { 
            get
            {
                // if only one character long, return false
                if (RomanNumerals.Length <= 1)
                    return false;

                // if first letter is less than second letter, return true
                RomanNumeral firstLetter;

                if (Enum.TryParse(RomanNumerals[0].ToString(CultureInfo.CurrentCulture), out firstLetter) == false)
                    return false;

                RomanNumeral secondLetter;

                if (Enum.TryParse(RomanNumerals[1].ToString(CultureInfo.CurrentCulture), out secondLetter) == false)
                    return false;

                return (int)firstLetter < (int)secondLetter;
            }
        }
    }
}
