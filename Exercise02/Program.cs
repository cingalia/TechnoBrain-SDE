using System;
using System.Text.RegularExpressions;

namespace Exercise02
{
    class Program
    {
        static void Main(string[] args)
        {
            //Get user input
            Console.WriteLine("Please enter numeric value");
            var _input = Console.ReadLine();

            var input = Regex.Replace(_input, ",", "");

            System.Numerics.BigInteger amount_int = System.Numerics.BigInteger.Parse(input);

            var _output = Exercise01.Exercise01.Towards(amount_int);

            var output = ReplaceLast(",", " and ", _output);

            Console.WriteLine(output);

            Console.ReadKey();
        }

        static string ReplaceLast(string find, string replace, string str)
        {
            int lastIndex = str.LastIndexOf(find);

            if (lastIndex == -1)
            {
                return str;
            }

            string beginString = str.Substring(0, lastIndex);
            string endString = str.Substring(lastIndex + find.Length);

            return beginString + replace + endString;
        }
    }
}
