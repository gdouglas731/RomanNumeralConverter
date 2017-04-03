using System;
using System.Globalization;

namespace RomanNumeralDecimalConverter
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            string input;

            do
            {
                Console.WriteLine("Enter the number you'd like to convert to roman numerals:");
                input = Console.ReadLine();

                if (!string.IsNullOrEmpty(input))
                {
                    var romanNumeral =
                        RomanNumeralConverter.ConvertToRomanNumerals(Convert.ToInt32(input, CultureInfo.CurrentCulture));
                    Console.WriteLine(romanNumeral);
                    Console.WriteLine(DecimalConverter.ConvertToDecimal(romanNumeral));
                }
            } while (!string.IsNullOrEmpty(input));
            
        }
    }
}