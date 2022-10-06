using System.Text.RegularExpressions;

namespace Tools.ObjectHelpers
{
    public static class StringHelper
    {
        public static bool NotContainSpace(string text)
        {
            return !text.Contains(" ");
        }

        public static bool IsValidMailAddress(string text)
        {
            Regex regex = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");
            return regex.Match(text).Success;
        }

        public static bool IsValidPhoneNumber(string text)
        {
            if (Regex.IsMatch(text, "^[0-9]*$"))
            {
                if (text.Length == 10 || text.Length == 11)
                {
                    return true;
                }
            }
            return false;
        }
    }
}
