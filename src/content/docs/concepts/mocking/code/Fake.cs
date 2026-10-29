[Fact]
public void Fake_Example()
{
    // Arrange
    var userRepository = new FakeUserRepository();
    var user = new User { Id = 1, Email = "john@example.com", Name = "John Doe" };
    userRepository.AddUser(user);

    var emailServiceStub = Substitute.For<IEmailService>();
    emailServiceStub.IsEmailValid(Arg.Any<string>()).Returns(true);

    var userService = new UserService(userRepository, emailServiceStub);

    // Act
    userService.RegisterNewUser(1);

    // Assert
    var retrievedUser = userRepository.GetUser(user.Id);
    Assert.NotNull(retrievedUser);
    emailServiceStub.Received(1).SendWelcomeEmail(retrievedUser.Email);

// Very simple implementation of IUserRepository that avoids using the real implementation
public class FakeUserRepository : IUserRepository
{
    private readonly Dictionary<int, User> _users = new Dictionary<int, User>();

    public void GetUser(int userId)
    {
        _users.TryGetValue(userId, out var existingUser);
        return existingUser;
    }

    public void AddUser(User user)
    {
        _users[user.Id] = user;
    }
}