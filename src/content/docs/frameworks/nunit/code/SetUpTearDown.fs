// NUnit uses [OneTimeSetUp] and [OneTimeTearDown] for class-level setup
type MyTests() =
    let mutable db : MyDatabaseContext = Unchecked.defaultof<_>
    
    [<OneTimeSetUp>]
    member this.OneTimeSetUp() =
        // Setup code that runs once before any tests in the class
        db <- MyDatabaseContext()

    [<OneTimeTearDown>]
    member this.OneTimeTearDown() =
        // Teardown code that runs once after all tests in the class
        db.Dispose()

    [<Test>]
    member this.Test1() =
        // Use db for database operations
        ()
