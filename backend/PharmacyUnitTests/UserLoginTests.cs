using backend.DTO;
using backend.Model;
using backend.Repositories.Interfaces;
using backend.Services;
using Moq;
using Shouldly;
using System.Collections.Generic;
using Xunit;

namespace PharmacyUnitTests
{
    public class UserLoginTests
    {
        [Fact]
        public void Login_With_Valid_Credentials()
        {
            var stubRepository = CreateStubRepository();
            UserService service = new UserService(stubRepository.Object);
            UserLoginRequestDTO credentials = new UserLoginRequestDTO();
            credentials.Username = "pera";
            credentials.Password = "pera";

            User user = service.GetUserByLoginCredentials(credentials);

            user.ShouldNotBe(null);
        }

        [Fact]
        public void Login_With_Invalid_Credentials()
        {
            var stubRepository = CreateStubRepository();
            UserService service = new UserService(stubRepository.Object);
            UserLoginRequestDTO credentials = new UserLoginRequestDTO();
            credentials.Username = "pera";
            credentials.Password = "123";

            User user = service.GetUserByLoginCredentials(credentials);

            user.ShouldBe(null);
        }

        private static Mock<IUserRepository> CreateStubRepository()
        {
            var stubRepository = new Mock<IUserRepository>();
            var users = new List<User>();
            User pera = CreateUser("pera", "pera");
            users.Add(pera);

            stubRepository.Setup(x => x.GetAll()).Returns(users);

            return stubRepository;
        }

        private static User CreateUser(string username, string password)
        {
            User user = new User();
            user.FirstName = "name";
            user.LastName = "surname";
            user.Username = username;
            user.Password = password;
            user.Role = User.UserRole.Client;

            return user;
        }
    }
}
