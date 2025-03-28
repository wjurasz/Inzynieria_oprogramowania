using System;


    public class UserRegistrationService
    {
        private readonly List<string> _registeredEmails = new();
        public bool RegisterUser(string email)
        {
            if (!EmailValidator.IsValidEmail(email))
            {
                return false;
            }
            _registeredEmails.Add(email);
            return true;
        }
        public bool IsUserRegistered(string email)
        {
            return _registeredEmails.Contains(email);
        }
    }

