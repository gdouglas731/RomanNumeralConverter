using System;
using System.Globalization;

namespace RomanNumeralDecimalConverter
{
    class Program
    {
        static void Main(string[] args)
        {
            var input = string.Empty;

            do
            {
                Console.WriteLine("Enter the number you'd like to convert to roman numerals:");
                input = Console.ReadLine();

                if (input.Length > 0)
                {
                    var romanNumeral = RomanNumeralConverter.ConvertToRomanNumerals(Convert.ToInt32(input, CultureInfo.CurrentCulture));
                    Console.WriteLine(romanNumeral);
                    Console.WriteLine(DecimalConverter.ConvertToDecimal(romanNumeral));
                }
            } while (input.Length > 0);
            
        }
    }
}