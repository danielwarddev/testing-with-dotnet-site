// The fixture doesn't need to inherit from anything, but IDisposable can be used for teardown
type MyFixture() =
    // Setup code that runs once for the entire test class
    // Also, must all go in the constructor
    let mutable db = MyDatabaseContext()
    
    member this.Db = db

    interface IDisposable with
        member this.Dispose() =
            // Teardown code that runs once for the entire test class
            db.Dispose()

type MyTests(fixture: MyFixture) =
    let _fixture = fixture

    [<Fact>]
    member this.Test1() =
        // Use _fixture.Db for database operations