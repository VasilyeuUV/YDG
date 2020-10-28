using System;

namespace YDG.Infrastructure.Converters
{
    internal static class StringConverter
    {
        internal static Int32 ToInt32(string str)
        {
            Int32 k;
            return Int32.TryParse(str, out k) ? k : 0;
        }

    }
}
