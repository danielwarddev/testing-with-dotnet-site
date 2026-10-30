' The fixture doesn't need to inherit from anything, but IDisposable can be used for teardown
Public Class MyFixture
    Implements IDisposable

    Public ReadOnly Property Db As MyDatabaseContext

    Public Sub New()
        ' Setup code that runs once for the entire test class
        ' Also, must all go in the constructor
        Db = New MyDatabaseContext()
    End Sub

    Public Sub Dispose() Implements IDisposable.Dispose
        ' Teardown code that runs once for the entire test class
        Db.Dispose()
    End Sub
End Class

Public Class MyTests
    Implements IClassFixture(Of MyFixture)

    Private ReadOnly _fixture As MyFixture

    Public Sub New(fixture As MyFixture)
        _fixture = fixture
    End Sub

    <Fact>
    Public Sub Test1()
        ' Use _fixture.Db for database operations
    End Sub
End Class
