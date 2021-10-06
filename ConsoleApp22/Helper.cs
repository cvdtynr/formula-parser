using System;

namespace ConsoleApp22
{
    public static class Helper
    {
        public static int ToInt(this string i) => Convert.ToInt32(i);
        public static double ToDouble(this string i) => Convert.ToDouble(i);
    }
}
