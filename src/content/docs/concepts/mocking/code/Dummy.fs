[<Fact>]
let Dummy_Example() =
    // Arrange
    // The IEmailService dummy is never used here, but we need one to create the UserService
    let emailServiceDummy = Substitute.For<IEmailService>()
    let userService = UserService(emailServiceDummy)

    // Act
    let result = userService.RegisterUser("cooldude@example.com")

    // Assert
    Assert.True(result)
