using System;

namespace WebServiceManagementSystem
{
    public static class PasswordGenerator
    {
        private static Random random = new Random();

        public static string GeneratePassword(int length, bool includeSpecialChars = true)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            const string specialChars = "!@#$%^&*()-_=+<>?";

            string validChars = chars + (includeSpecialChars ? specialChars : "");
            char[] password = new char[length];

            for (int i = 0; i < length; i++)
            {
                password[i] = validChars[random.Next(validChars.Length)];
            }

            return new string(password);
        }
    }
}
