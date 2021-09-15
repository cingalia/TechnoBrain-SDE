using System;
using System.Text.RegularExpressions;

namespace Exercise02
{
    class Program
    {
        static void Main(string[] args)
        {
            //Main Method
            Interact();

            Console.ReadKey();    
        }

        static void Interact()
        {
            //Get user input
            Console.WriteLine("Please enter numeric value");
            var _input = Console.ReadLine();

            var input = Regex.Replace(_input, ",", "");

            System.Numerics.BigInteger amount_int = System.Numerics.BigInteger.Parse(input);

            var _output = Exercise01.Exercise01.Towards(amount_int);

            int pos = _output.LastIndexOf(" ") + 1;
            var lastWordAfterComma = _output.Substring(pos, _output.Length - pos);

            var output = lastWordAfterComma.Contains("-")
                ? ReplaceLast(lastWordAfterComma, " and " + lastWordAfterComma, _output)
                : ReplaceLast(",", " and ", _output);
            Console.WriteLine(output);
            
            Interact();
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
