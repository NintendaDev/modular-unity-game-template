using System.Text.RegularExpressions;

namespace Modules.Text
{
    public static class StringExtensions
    {
        public static string ToSnakeCase(this string input)
        {
            if (string.IsNullOrEmpty(input))
                return input;
            
            string snakeCase = Regex.Replace(input, "([a-z0-9])([A-Z])", "$1_$2");
            
            return snakeCase.ToLower();
        }
    }
}