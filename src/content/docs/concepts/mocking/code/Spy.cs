[Fact]
public void Spy_Example()
{
    // Arrange
    // Use NSubstitute's ForPartsOf to create a spy
    // It will track usage, but by default not override any real implementation
    var emailValidator = Substitute.ForPartsOf<RealEmailValidator>();
    
    var userRepositoryStub = Substitute.For<IUserRepository>();
    var user = new User { Id = 1, Email = "john@example.com", Name = "John Doe" };
    userRepositoryStub.GetById(1).Returns(user);
    
    var emailServiceStub = Substitute.For<IEmailService>();
    emailServiceStub.IsEmailValid(Arg.Any<string>())
        .Returns(args => emailValidator.IsEmailValid((string)args[0]));

    var userService = new UserService(userRepositoryStub, emailServiceStub);

    // Act
    userService.WelcomeNewUser(1);

    // Assert
    // Verify how the spy was used while still having used the real implementation
    emailValidator.Received(1).IsEmailValid(user.Email);
    emailServiceStub.Received(1).SendWelcomeEmail(user.Email, user.Name);
}