' NUnit uses [OneTimeSetUp] and [OneTimeTearDown] for class-level setup
Public Class MyTests
    Private _db As MyDatabaseContext
    
    <OneTimeSetUp>
    Public Sub OneTimeSetUp()
        ' Setup code that runs once before any tests in the class
        _db = New MyDatabaseContext()
    End Sub

    <OneTimeTearDown>
    Public Sub OneTimeTearDown()
        ' Teardown code that runs once after all tests in the class
        _db.Dispose()
    End Sub

    <Test>
    Public Sub Test1()
        ' Use _db for database operations
    End Sub
End Class
