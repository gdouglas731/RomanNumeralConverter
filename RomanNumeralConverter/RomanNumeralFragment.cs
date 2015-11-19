using System;
using System.Globalization;

namespace RomanNumeralDecimalConverter
{
    public class RomanNumeralFragment
    {
        public RomanNumeralFragment(string letters)
        {
            RomanNumerals = letters;
        }

        public string RomanNumerals { get; set; }
        public int Value { 
            get            
            {
                var result = 0;

                if (IsAdditive)
	            {
		            foreach (var chr in RomanNumerals)
                    {
                        result += CommonFunctions.GetRomanNumeralValue(chr);
                    } 
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
                if (this.RomanNumerals.Length <= 1)
                    return false;

                // if first letter is less than second letter, return false
                RomanNumeral firstLetter;

                if (Enum.TryParse<RomanNumeral>(RomanNumerals[0].ToString(), out firstLetter) == false)
                    return false;

                RomanNumeral secondLetter;

                if (Enum.TryParse<RomanNumeral>(RomanNumerals[1].ToString(), out secondLetter) == false)
                    return false;

                return (int)firstLetter >= (int)secondLetter;
            }
        }

        public bool IsSubtractive { 
            get
            {
                // if only one character long, return false
                if (this.RomanNumerals.Length <= 1)
                    return false;

                // if first letter is less than second letter, return true
                RomanNumeral firstLetter;

                if (Enum.TryParse<RomanNumeral>(RomanNumerals[0].ToString(CultureInfo.CurrentCulture), out firstLetter) == false)
                    return false;

                RomanNumeral secondLetter;

                if (Enum.TryParse<RomanNumeral>(RomanNumerals[1].ToString(CultureInfo.CurrentCulture), out secondLetter) == false)
                    return false;

                return (int)firstLetter < (int)secondLetter;
            }
        }
    }
}
