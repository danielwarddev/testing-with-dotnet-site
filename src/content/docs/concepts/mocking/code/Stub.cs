[Fact]
public void Stub_Example()
{
    // Arrange
    var emailServiceStub = new Substitute.For<IEmailService>();
    var userService = new UserService(emailServiceStub);

    var expectedUser = new User { Id = 1, Name = "John Doe" };
    emailServiceStub.GetUserById(1).Returns(expectedUser);

    // Act
    var actualUser = userService.GetUserById(1);

    // Assert
    // Only assert on the return value, not how it was used
    Assert.Equal(expectedUser, actualUser);
}