using System;

namespace BddTodo.Tests._Infrastructure.Extensions
{
    public static class EnumExtensions
    {
        public static byte ToByte(this Enum input)
        {
            return Convert.ToByte(input);
        }

        public static int ToInt(this Enum input)
        {
            return Convert.ToInt32(input);
        }
    }
}
