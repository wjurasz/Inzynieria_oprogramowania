using System;

public interface IUserRepository
{
		void Save(string email);

		bool Exists(string email);
}
