// NUnit uses [OneTimeSetUp] and [OneTimeTearDown] for class-level setup
public class MyTests
{
    private MyDatabaseContext _db;
    
    [OneTimeSetUp]
    public void OneTimeSetUp()
    {
        // Setup code that runs once before any tests in the class
        _db = new MyDatabaseContext();
    }

    [OneTimeTearDown]
    public void OneTimeTearDown()
    {
        // Teardown code that runs once after all tests in the class
        _db.Dispose();
    }

    [Test]
    public void Test1()
    {
        // Use _db for database operations
    }
}
