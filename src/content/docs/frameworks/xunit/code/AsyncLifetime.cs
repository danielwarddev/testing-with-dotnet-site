public class MyFixture : IAsyncLifetime
{
    public MyDatabaseContext Db { get; private set; }
    
    public MyFixture()
    {
        // Can still have setup in the constructor
        Db = new MyDatabaseContext();
    }

    public Task InitializeAsync()
    {
        // Async setup code that runs once for the entire test class
        return Task.CompletedTask;
    }

    public Task DisposeAsync()
    {
        // Async teardown code that runs once for the entire test class
        Db.Dispose();
        return Task.CompletedTask;
    }
}