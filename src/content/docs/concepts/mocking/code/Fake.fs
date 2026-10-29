[<Fact>]
let Fake_Example() =
    // Arrange
    let userRepository = FakeUserRepository()
    let user = { Id = 1; Email = "john@example.com"; Name = "John Doe" }
    userRepository.AddUser(user)

    let emailServiceStub = Substitute.For<IEmailService>()
    emailServiceStub.IsEmailValid(Arg.Any<string>()).Returns(true) |> ignore

    let userService = UserService(userRepository, emailServiceStub)

    // Act
    userService.RegisterNewUser(1)

    // Assert
    let retrievedUser = userRepository.GetUser(user.Id)
    Assert.NotNull(retrievedUser)
    emailServiceStub.Received(1).SendWelcomeEmail(retrievedUser.Email) |> ignore

// Very simple implementation of IUserRepository that avoids using the real implementation
and FakeUserRepository() =
    let users = Dictionary<int, User>()
    
    interface IUserRepository with
        member _.GetUser(userId: int) =
            match users.TryGetValue(userId) with
            | true, user -> user
            | false, _ -> null
        
        member _.AddUser(user: User) =
            users.[user.Id] <- user
    
    member this.GetUser(userId: int) = (this :> IUserRepository).GetUser(userId)
    member this.AddUser(user: User) = (this :> IUserRepository).AddUser(user)
