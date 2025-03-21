using System;
using System.Text;

namespace Modules.Extensions
{
    public static class Base64Extensions
    {
        public static string ToBase64(this string source)
        {
            byte[] sourceArray = new byte[source.Length];

            for (int i = 0; i < source.Length; i++)
                sourceArray[i] = (byte)source[i];

            return Convert.ToBase64String(sourceArray);
        }

        public static string FromBase64(this string source)
        {
            byte[] sourceArray = Convert.FromBase64String(source);
            StringBuilder builder = new StringBuilder();

            for (int i = 0; i < sourceArray.Length; i++)
                builder.Append((char)sourceArray[i]);

            return builder.ToString();
        }
    }
}
