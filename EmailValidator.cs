using System;

public static class EmailValidator
{
    public static bool IsValidEmail(string email)
    {
        return !string.IsNullOrWhiteSpace(email) &&
        email.Contains(".") &&
        !email.Contains(" ") &&
        email.Count(x => x == '@') == 1;
    }
}