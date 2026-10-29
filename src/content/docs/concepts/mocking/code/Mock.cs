[Fact]
public void Mock_Example()
{
    // Arrange
    var emailServiceMock = Substitute.For<IEmailService>();
    var user = new User { Id = 1, Email = "john@example.com", Name = "John Doe" };
    // We both set up the mock to return some predefined values...
    emailServiceMock.IsEmailValid(user.Email).Returns(true);

    var userService = new UserService(emailServiceMock);

    // Act
    var result = userService.RegisterUser(user);

    // Assert
    // ...and also verify how it was used
    emailServiceMock.Received(1).IsEmailValid(user.Email);
    emailServiceMock.Received(1).SendWelcomeEmail(user.Email);
}