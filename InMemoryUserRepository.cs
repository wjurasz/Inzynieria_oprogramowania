using System;

public class InMemoryUserRepository : IUserRepository
{
    private readonly List<string> _users = new();
    public void Save(string email)
    {
        _users.Add(email);
    }
    public bool Exists(string email)
    {
        return _users.Contains(email);
    }
}