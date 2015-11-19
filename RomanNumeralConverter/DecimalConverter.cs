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
            List<RomanNumeralFragment> fragments = new List<RomanNumeralFragment>();
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
        /// Returns the first RomanNumeralFragment from the specified string
        /// eg. CMIX would return CM, MMMCCCIX would return MMMCCC, etc.
        /// </summary>
        /// <param name="romanNumerals"></param>
        /// <returns></returns>
        private static RomanNumeralFragment GetFirstFragment(string romanNumerals)
        {
            var letters = string.Empty;
            if (romanNumerals.Length > 1)
            {
                var firstChar = romanNumerals[0];
                var secondChar = romanNumerals[1];

                // if subtractive, it will only use first 2 chars if the first is less than the second
                if (CommonFunctions.GetRomanNumeralValue(firstChar) < CommonFunctions.GetRomanNumeralValue(secondChar))
                {
                    return new RomanNumeralFragment(firstChar.ToString() + secondChar.ToString());
                }
                else
                {
                    // additive - loop through letters until end or we come across a subtractive fragment
                    for (int i = 0; i < romanNumerals.Length; i++)
                    {
                        var letter = romanNumerals[i];

                        // add first letter to string
                        if (letters.Length == 0)
                        {
                            letters += letter;
                        }
                        else
                        {
                            // if last letter, append to string
                            if (i == romanNumerals.Length - 1)
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
                                    return new RomanNumeralFragment(letters);
                                }
                                else
                                {
                                    // add letter to string
                                    letters += letter;
                                }
                            }
                        }
                    }

                    return new RomanNumeralFragment(letters);
                }
            }
            else
            {
                // return single letter fragment
                return new RomanNumeralFragment(romanNumerals);
            }
        }

        #endregion
    }
}