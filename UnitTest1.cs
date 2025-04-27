using FluentAssertions;
using Moq;

namespace ZadanieLab2

{
    public class UnitTest1
    {
        [Fact]
        public void Test1()
        {

        }


        [Fact]
        public void RegisterUser_ValidEmail_ShouldRegisterUser()
        {
            var service = new UserRegistrationService();
            var email = "test@example.com";
            var result = service.RegisterUser(email);
            Assert.True(result);
            Assert.True(service.IsUserRegistered(email));
        }



        [Fact]
        public void Zad2RegisterUser_InvalidEmail_ShouldNotRegisterUser()
        {
            var service = new UserRegistrationService();
            var email = "invalid-email";
            var result = service.RegisterUser(email);
            Assert.False(result);
            Assert.False(service.IsUserRegistered(email));
        }

        [Theory]
        [InlineData("test@example.com", true)] 
        [InlineData("user123@domain.co", true)]
        [InlineData("invalid@-email", false)]
        [InlineData("missingatsign.com", false)]
        [InlineData("another.test@site", true)]
        public void RegisterUser_CheckUser(string email, bool expectedResult)
        {
            var service = new UserRegistrationService();

            var result = service.RegisterUser(email);

            Assert.Equal(expectedResult, result);
            Assert.Equal(expectedResult, service.IsUserRegistered(email));
        }


        [Fact]
        public void RegisterUser_ValidEmail_ShouldBeStoredInRepository()
        {
            var repository = new InMemoryUserRepository();
            var service = new UserRegistrationServiceWithRepo(repository);
            service.RegisterUser("test@example.com");
            Assert.True(repository.Exists("test@example.com"));
        }

        [Fact]
        public void RegisterUser_NewUser_ShouldCallSaveMethod()
        {
            // Tworzymy mock repozytorium
            var repositoryMock = new Mock<IUserRepository>();
            // Konfigurujemy mock – symulujemy, ¿e email nie istnieje w bazie
            repositoryMock.Setup(r =>
            r.Exists("test@example.com")).Returns(false);
            var service = new
            UserRegistrationServiceWithRepo(repositoryMock.Object);
            // Wywo³ujemy testowan¹ metodê
            var result = service.RegisterUser("test@example.com");
            // Sprawdzamy, czy u¿ytkownik zosta³ zarejestrowany
            Assert.True(result);
            // Weryfikujemy, czy metoda Save zosta³a wywo³ana raz
            repositoryMock.Verify(r => r.Save("test@example.com"), Times.Once);
        }

        //[Fact]
        //public void RegisterUser_ExistingUser_ShouldNotCallSave()
        //{
        //    var repositoryMock = new Mock<IUserRepository>();
        //    repositoryMock.Setup(r =>
        //    r.Exists("existing@example.com")).Returns(true);
        //    var service = new
        //    UserRegistrationServiceWithRepo(repositoryMock.Object);
        //    var result = service.RegisterUser("existing@example.com");
        //    Assert.False(result);
        //    // Sprawdzamy, czy Save NIE zosta³a wywo³ana
        //    repositoryMock.Verify(r => r.Save(It.IsAny<string>()),
        //    Times.Never);
        //}

        [Fact]
        public void RegisterUser_EmptyEmail_ShouldThrowArgumentException()
        {
            var repositoryMock = new Mock<IUserRepository>();
            var service = new UserRegistrationServiceWithRepo(repositoryMock.Object);
            Assert.Throws<ArgumentException>(() => service.RegisterUser(""));
        }

        [Fact]
        public void RegisterUser_ValidEmail_Fluent_ShouldRegisterUser()
        {
            var service = new UserRegistrationService();
            var email = "test@example.com";
            var result = service.RegisterUser(email);
            result.Should().BeTrue();
            service.IsUserRegistered(email).Should().BeTrue();
        }

        [Fact]
        public void
        RegisterUser_EmptyEmail_Fluent_ShouldThrowArgumentException()
        {
            var repositoryMock = new Mock<IUserRepository>();
            var service = new
            UserRegistrationServiceWithRepo(repositoryMock.Object);
            service.Invoking(s =>
            s.RegisterUser("")).Should().Throw<ArgumentException>()
            .WithMessage("Email cannot be empty*");
        }


        //ZADANIA
        // ZADANIE 1
        [Fact]
        public void Zad1RegisterUser_DuplicateEmail()
        {
            var repository = new InMemoryUserRepository();
            var service = new UserRegistrationServiceWithRepo(repository);
            var email = "duplicate@example.com";

            var firstRegistrationResult = service.RegisterUser(email);

            Assert.True(firstRegistrationResult);

            var exception = Assert.Throws<InvalidOperationException>(() => service.RegisterUser(email));
            Assert.Equal("User already registered", exception.Message);
        }
        //ZADANIE 2
        [Fact]
        public void RegisterUser_InvalidEmail_ShouldNotRegisterUser() 
        {
            var service = new UserRegistrationService();
            var email = "invalid-email";
            var result = service.RegisterUser(email);
            Assert.False(result);
            Assert.False(service.IsUserRegistered(email)); 
 }

        //ZADANIE 3

        [Theory]
        [InlineData("test@example.com", true)]
        [InlineData("user123@domain.co", true)]
        [InlineData("invalid@-email", false)]
        [InlineData("missingatsign.com", false)]
        [InlineData("another.test@site", true)]
        public void Zad3RegisterUser_CheckUsers(string email, bool expectedResult)
        {
            var service = new UserRegistrationService();

            var result = service.RegisterUser(email);

            Assert.Equal(expectedResult, result);
            Assert.Equal(expectedResult, service.IsUserRegistered(email));
        }

    }



}
