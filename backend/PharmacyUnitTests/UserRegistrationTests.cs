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
        [Theory]
        [MemberData(nameof(Data))]
        public void Register_user(User user, bool notRegistered)
        {
            var stubRepository = CreateStubRepository();
            UserService service = new UserService(stubRepository.Object);

            bool registered = service.RegisterUser(user);

            registered.ShouldBe(notRegistered);
        }

        public static IEnumerable<object[]> Data()
        {
            var retval = new List<object[]>();
            retval.Add(new object[] { CreateUser("pera", "123"), false });
            retval.Add(new object[] { CreateUser("mika", "123"), true  });

            return retval;
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
