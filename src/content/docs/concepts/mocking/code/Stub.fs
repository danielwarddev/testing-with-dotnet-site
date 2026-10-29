[<Fact>]
let Stub_Example() =
    // Arrange
    let emailServiceStub = Substitute.For<IEmailService>()
    let userService = UserService(emailServiceStub)

    let expectedUser = { Id = 1; Name = "John Doe" }
    emailServiceStub.GetUserById(1).Returns(expectedUser) |> ignore

    // Act
    let actualUser = userService.GetUserById(1)

    // Assert
    // Only assert on the return value, not how it was used
    Assert.Equal(expectedUser, actualUser)
