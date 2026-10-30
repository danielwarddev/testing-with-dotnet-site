// The fixture doesn't need to inherit from anything, but IDisposable can be used for teardown
public class MyFixture : IDisposable
{
    public MyDatabaseContext Db { get; private set; }
    
    public MyFixture()
    {
        // Setup code that runs once for the entire test class
        // Also, must all go in the constructor
        Db = new MyDatabaseContext();
    }

    public void Dispose()
    {
        // Teardown code that runs once for the entire test class
        Db.Dispose();
    }
}

public class MyTests : IClassFixture<MyFixture>
{
    private readonly MyFixture _fixture;

    public MyTests(MyFixture fixture)
    {
        _fixture = fixture;
    }

    [Fact]
    public void Test1()
    {
        // Use _fixture.Db for database operations
    }
}