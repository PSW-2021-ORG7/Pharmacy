using System.Collections.Generic;
using backend.Model;
using backend.Repositories.Interfaces;
using backend.Services;
using Moq;
using Shouldly;
using Xunit;

namespace PharmacyUnitTests
{
    public class UserRegistrationAndLoginTests
    {
        [Fact]
        public void Register_user_with_same_username()
        {
            var stubRepository = CreateStubRepository();
            UserService service = new UserService(stubRepository.Object);
            User pera = CreateUser("pera", "123");

            bool registrated = service.RegisterUser(pera);

            registrated.ShouldBeFalse();
        }

        [Fact]
        public void Register_user_with_unique_username()
        {
            var stubRepository = CreateStubRepository();
            UserService service = new UserService(stubRepository.Object);
            User mika = CreateUser("mika", "123");

            bool registrated = service.RegisterUser(mika);

            registrated.ShouldBeTrue();
        }

        private static Mock<IUserRepository> CreateStubRepository()
        {
            var stubRepository = new Mock<IUserRepository>();
            var users = new List<User>();
            User pera = CreateUser ("pera", "pera123");
            users.Add(pera);

            stubRepository.Setup(x => x.GetAll()).Returns(users);

            return stubRepository;
        }

        private static User CreateUser (string username, string password)
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
