[Fact]
public void Dummy_Example()
{
    // Arrange
    // The IEmailService dummy is never used here, but we need one to create the UserService
    var emailServiceDummy = Substitute.For<IEmailService>();
    var userService = new UserService(emailServiceDummy);

    // Act
    var result = userService.RegisterUser("cooldude@example.com");

    // Assert
    Assert.True(result);
}