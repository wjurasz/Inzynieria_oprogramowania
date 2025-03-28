
        //ZADANIA
        // ZADANIE 1
        [Fact]
        public void RegisterUser_DuplicateEmail()
        {
            var repository = new InMemoryUserRepository();
            var service = new UserRegistrationServiceWithRepo(repository);
            var email = "duplicate@example.com";

            var firstRegistrationResult = service.RegisterUser(email);

            Assert.True(firstRegistrationResult); 
            
            var exception = Assert.Throws<InvalidOperationException>(() => service.RegisterUser(email));
            Assert.Equal("User already registered", exception.Message);
        }


        //ZADANIE 3

        [Theory]
        [InlineData("test@example.com", true)]
        [InlineData("user123@domain.co", true)]
        [InlineData("invalid@-email", false)]
        [InlineData("missingatsign.com", false)]
        [InlineData("another.test@site", true)]
        public void RegisterUser_CheckUsers(string email, bool expectedResult)
        {
            var service = new UserRegistrationService();

            var result = service.RegisterUser(email);

            Assert.Equal(expectedResult, result);
            Assert.Equal(expectedResult, service.IsUserRegistered(email));
        }

    }
