using System;

namespace Exercise01
{
    public static class Exercise01
    {
        public static string Towards(System.Numerics.BigInteger n)
        {
            if (n < 0)
                throw new NotSupportedException("negative numbers not supported");
            if (n == 0)
                return "zero";
            if (n < 10)
                return ConvertDigitToString(n);
            if (n < 20)
                return ConvertTeensToString(n);
            if (n < 100)
                return ConvertHighTensToString(n);
            if (n < 1000)
                return ConvertBigNumberToString(n, (int)1e2, "hundred");
            if (n < (System.Numerics.BigInteger) 1e6)
                return ConvertBigNumberToString(n, (int)1e3, "thousand");
            if (n < (System.Numerics.BigInteger) 1e9)
                return ConvertBigNumberToString(n, (int)1e6, "million");
            if (n < (System.Numerics.BigInteger) 1e12)
                return ConvertBigNumberToString(n, (int)1e9, "billion");
            if (n < (System.Numerics.BigInteger) 1e15)
                return ConvertBigNumberBigToString(n, (System.Numerics.BigInteger)1e12, "trillion");
            if (n < (System.Numerics.BigInteger) 1e18)
                return ConvertBigNumberBigToString(n, (System.Numerics.BigInteger)1e15, "quadrillion");
            if (n < (System.Numerics.BigInteger) 1e21)
                return ConvertBigNumberBigToString(n, (System.Numerics.BigInteger)1e18, "quintillion");
            if (n < (System.Numerics.BigInteger)1e24)
                return ConvertBigNumberBigToString(n, (System.Numerics.BigInteger)1e21, "sextillion");
            if (n < (System.Numerics.BigInteger)1e27)
                return ConvertBigNumberBigToString(n, (System.Numerics.BigInteger)1e24, "septillion");
            if (n < (System.Numerics.BigInteger)1e30)
                return ConvertBigNumberBigToString(n, (System.Numerics.BigInteger)1e27, "0ctillion");

            return ConvertBigNumberBigToString(n, (System.Numerics.BigInteger)1e30, "nonillion");
        }

        private static string ConvertDigitToString(System.Numerics.BigInteger _i)
        {
            long i = long.Parse(_i.ToString());
            switch (i)
            {
                case 0: return "";
                case 1: return "one";
                case 2: return "two";
                case 3: return "three";
                case 4: return "four";
                case 5: return "five";
                case 6: return "six";
                case 7: return "seven";
                case 8: return "eight";
                case 9: return "nine";
                default:
                    throw new IndexOutOfRangeException(String.Format("{0} not a digit", i));
            }
        }

        //assumes a number between 10 & 19
        private static string ConvertTeensToString(System.Numerics.BigInteger _n)
        {
            long n = long.Parse(_n.ToString());
            switch (n)
            {
                case 10: return "ten";
                case 11: return "eleven";
                case 12: return "twelve";
                case 13: return "thirteen";
                case 14: return "fourteen";
                case 15: return "fiveteen";
                case 16: return "sixteen";
                case 17: return "seventeen";
                case 18: return "eighteen";
                case 19: return "nineteen";
                default:
                    throw new IndexOutOfRangeException(String.Format("{0} not a teen", n));
            }
        }

        //assumes a number between 20 and 99
        private static string ConvertHighTensToString(System.Numerics.BigInteger _n)
        {
            long n = long.Parse(_n.ToString());
            int tensDigit = (int)(Math.Floor((double)n / 10.0));

            string tensStr;
            switch (tensDigit)
            {
                case 2: tensStr = "twenty"; break;
                case 3: tensStr = "thirty"; break;
                case 4: tensStr = "forty"; break;
                case 5: tensStr = "fifty"; break;
                case 6: tensStr = "sixty"; break;
                case 7: tensStr = "seventy"; break;
                case 8: tensStr = "eighty"; break;
                case 9: tensStr = "ninety"; break;
                default:
                    throw new IndexOutOfRangeException(String.Format("{0} not in range 20-99", n));
            }
            if (n % 10 == 0) return tensStr;
            string onesStr = ConvertDigitToString(n - tensDigit * 10);
            return tensStr + "-" + onesStr;
        }

        private static string ConvertBigNumberToString(System.Numerics.BigInteger n, int baseNum, string baseNumStr)
        {
            // special case: use commas to separate portions of the number, unless we are in the hundreds
            string separator = (baseNumStr != "hundred") ? ", " : " ";

            // Strategy: translate the first portion of the number, then recursively translate the remaining sections.
            // Step 1: strip off first portion, and convert it to string:
            System.Numerics.BigInteger bigPart = (System.Numerics.BigInteger)(Math.Floor((double)n / baseNum));
            string bigPartStr = Towards(bigPart) + " " + baseNumStr;
            // Step 2: check to see whether we're done:
            if (n % baseNum == 0) return bigPartStr;
            // Step 3: concatenate 1st part of string with recursively generated remainder:
            System.Numerics.BigInteger restOfNumber = n - bigPart * baseNum;
            return bigPartStr + separator + Towards(restOfNumber);
        }

        private static string ConvertBigNumberBigToString(System.Numerics.BigInteger n, System.Numerics.BigInteger baseNum, string baseNumStr)
        {
            // special case: use commas to separate portions of the number, unless we are in the hundreds
            string separator = (baseNumStr != "hundred") ? ", " : " ";

            // Strategy: translate the first portion of the number, then recursively translate the remaining sections.
            // Step 1: strip off first portion, and convert it to string:
            //double result = Math.Exp(BigInteger.Log(x) - BigInteger.Log(y));
            System.Numerics.BigInteger bigPart = (System.Numerics.BigInteger)Math.Exp(System.Numerics.BigInteger.Log(n) - System.Numerics.BigInteger.Log(baseNum));
            //System.Numerics.BigInteger bigPart = (System.Numerics.BigInteger)(Math.Floor(n / baseNum));
            string bigPartStr = Towards(bigPart) + " " + baseNumStr;
            // Step 2: check to see whether we're done:
            if (n % baseNum == 0) return bigPartStr;
            // Step 3: concatenate 1st part of string with recursively generated remainder:
            System.Numerics.BigInteger restOfNumber = n - bigPart * baseNum;
            return bigPartStr + separator + Towards(restOfNumber);
        }

    }
}
