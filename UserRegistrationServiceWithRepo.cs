using System;

public class UserRegistrationServiceWithRepo
{
    private readonly IUserRepository _repository;
    public UserRegistrationServiceWithRepo(IUserRepository repository)
    {
        _repository = repository;
    }
    public bool RegisterUser(string email)
    {
        if (string.IsNullOrWhiteSpace(email))
        {
            throw new ArgumentException("Email cannot be empty",
            nameof(email));
        }
        if (!EmailValidator.IsValidEmail(email))
        {
            throw new FormatException("Invalid email format");
        }
        if (_repository.Exists(email))
        {
            throw new InvalidOperationException("User already registered");
        }
        _repository.Save(email);
        return true;


    }
}