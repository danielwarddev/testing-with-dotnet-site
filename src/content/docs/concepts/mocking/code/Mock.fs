[<Fact>]
let Mock_Example() =
    // Arrange
    let emailServiceMock = Substitute.For<IEmailService>()
    let user = { Id = 1; Email = "john@example.com"; Name = "John Doe" }
    // We both set up the mock to return some predefined values...
    emailServiceMock.IsEmailValid(user.Email).Returns(true) |> ignore

    let userService = UserService(emailServiceMock)

    // Act
    let result = userService.RegisterUser(user)

    // Assert
    // ...and also verify how it was used
    emailServiceMock.Received(1).IsEmailValid(user.Email) |> ignore
    emailServiceMock.Received(1).SendWelcomeEmail(user.Email) |> ignore
