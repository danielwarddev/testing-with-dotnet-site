<Fact>
Public Sub Dummy_Example()
    ' Arrange
    ' The IEmailService dummy is never used here, but we need one to create the UserService
    Dim emailServiceDummy = Substitute.For(Of IEmailService)()
    Dim userService = New UserService(emailServiceDummy)

    ' Act
    Dim result = userService.RegisterUser("cooldude@example.com")

    ' Assert
    Assert.True(result)
End Sub
