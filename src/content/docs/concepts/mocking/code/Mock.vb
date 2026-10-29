<Fact>
Public Sub Mock_Example()
    ' Arrange
    Dim emailServiceMock = Substitute.For(Of IEmailService)()
    Dim user = New User With {.Id = 1, .Email = "john@example.com", .Name = "John Doe"}
    ' We both set up the mock to return some predefined values...
    emailServiceMock.IsEmailValid(user.Email).Returns(True)

    Dim userService = New UserService(emailServiceMock)

    ' Act
    Dim result = userService.RegisterUser(user)

    ' Assert
    ' ...and also verify how it was used
    emailServiceMock.Received(1).IsEmailValid(user.Email)
    emailServiceMock.Received(1).SendWelcomeEmail(user.Email)
End Sub
