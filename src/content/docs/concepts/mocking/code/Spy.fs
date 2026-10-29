[<Fact>]
let Spy_Example() =
    // Arrange
    // Use NSubstitute's ForPartsOf to create a spy
    // It will track usage, but by default not override any real implementation
    let emailValidator = Substitute.ForPartsOf<RealEmailValidator>()
    
    let userRepositoryStub = Substitute.For<IUserRepository>()
    let user = { Id = 1; Email = "john@example.com"; Name = "John Doe" }
    userRepositoryStub.GetById(1).Returns(user) |> ignore
    
    let emailServiceStub = Substitute.For<IEmailService>()
    emailServiceStub.IsEmailValid(Arg.Any<string>())
        .Returns(fun args -> emailValidator.IsEmailValid(args.[0] :?> string)) |> ignore

    let userService = UserService(userRepositoryStub, emailServiceStub)

    // Act
    userService.WelcomeNewUser(1)

    // Assert
    // Verify how the spy was used while still having used the real implementation
    emailValidator.Received(1).IsEmailValid(user.Email) |> ignore
    emailServiceStub.Received(1).SendWelcomeEmail(user.Email, user.Name) |> ignore
