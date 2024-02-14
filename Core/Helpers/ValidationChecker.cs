namespace Core.Helpers
{
    public static class ValidationChecker
    {
        public static void ValidateUserPassword(string userPassword)
        {
            if (userPassword.Length < 8)
            {
                throw new Exception("Password must be at least 8 characters long.");
            }

            if (!userPassword.Any(char.IsUpper))
            {
                throw new Exception("Password must contain at least one uppercase character.");
            }

            if (!userPassword.Any(c => char.IsSymbol(c) || char.IsPunctuation(c)))
            {
                throw new Exception("Password must contain at least one symbol.");
            }

            if (!userPassword.Any(char.IsDigit))
            {
                throw new Exception("Password must contain at least one numeric value.");
            }
        }

        public static void ValidateUserEmailAddress(string userEmail)
        {
            // Check if the email is null or empty
            if (string.IsNullOrEmpty(userEmail))
            {
                throw new Exception("Email address is required");
            }

            // Use regular expression to check the email format
            if (!System.Text.RegularExpressions.Regex.IsMatch(
                userEmail,
                @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$"))
            {
                throw new Exception("Invalid email address format");
            }
        }

        public static void ValidateUserName(string userName)
        {
            if (userName.Length < 5)
            {
                throw new Exception("Username must be at least 5 characters long.");
            }
        }
    }
}
