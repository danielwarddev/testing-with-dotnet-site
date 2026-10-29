<Fact>
Public Sub Fake_Example()
    ' Arrange
    Dim userRepository = New FakeUserRepository()
    Dim user = New User With {.Id = 1, .Email = "john@example.com", .Name = "John Doe"}
    userRepository.AddUser(user)

    Dim emailServiceStub = Substitute.For(Of IEmailService)()
    emailServiceStub.IsEmailValid(Arg.Any(Of String)()).Returns(True)

    Dim userService = New UserService(userRepository, emailServiceStub)

    ' Act
    userService.RegisterNewUser(1)

    ' Assert
    Dim retrievedUser = userRepository.GetUser(user.Id)
    Assert.NotNull(retrievedUser)
    emailServiceStub.Received(1).SendWelcomeEmail(retrievedUser.Email)
End Sub

' Very simple implementation of IUserRepository that avoids using the real implementation
Public Class FakeUserRepository
    Implements IUserRepository

    Private ReadOnly _users As New Dictionary(Of Integer, User)()

    Public Function GetUser(userId As Integer) As User Implements IUserRepository.GetUser
        Dim existingUser As User = Nothing
        _users.TryGetValue(userId, existingUser)
        Return existingUser
    End Function

    Public Sub AddUser(user As User) Implements IUserRepository.AddUser
        _users(user.Id) = user
    End Sub
End Class
