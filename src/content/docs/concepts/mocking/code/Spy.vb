<Fact>
Public Sub Spy_Example()
    ' Arrange
    ' Use NSubstitute's ForPartsOf to create a spy
    ' It will track usage, but by default not override any real implementation
    Dim emailValidator = Substitute.ForPartsOf(Of RealEmailValidator)()
    
    Dim userRepositoryStub = Substitute.For(Of IUserRepository)()
    Dim user = New User With {.Id = 1, .Email = "john@example.com", .Name = "John Doe"}
    userRepositoryStub.GetById(1).Returns(user)
    
    Dim emailServiceStub = Substitute.For(Of IEmailService)()
    emailServiceStub.IsEmailValid(Arg.Any(Of String)()) _
        .Returns(Function(args) emailValidator.IsEmailValid(CStr(args(0))))

    Dim userService = New UserService(userRepositoryStub, emailServiceStub)

    ' Act
    userService.WelcomeNewUser(1)

    ' Assert
    ' Verify how the spy was used while still having used the real implementation
    emailValidator.Received(1).IsEmailValid(user.Email)
    emailServiceStub.Received(1).SendWelcomeEmail(user.Email, user.Name)
End Sub
