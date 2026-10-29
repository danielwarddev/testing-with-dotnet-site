<Fact>
Public Sub Stub_Example()
    ' Arrange
    Dim emailServiceStub = Substitute.For(Of IEmailService)()
    Dim userService = New UserService(emailServiceStub)

    Dim expectedUser = New User With {.Id = 1, .Name = "John Doe"}
    emailServiceStub.GetUserById(1).Returns(expectedUser)

    ' Act
    Dim actualUser = userService.GetUserById(1)

    ' Assert
    ' Only assert on the return value, not how it was used
    Assert.Equal(expectedUser, actualUser)
End Sub
