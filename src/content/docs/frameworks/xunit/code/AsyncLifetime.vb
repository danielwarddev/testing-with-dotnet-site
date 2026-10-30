' The fixture implements IAsyncLifetime for async setup/teardown
Public Class MyFixture
    Implements IAsyncLifetime

    Public Property Db As MyDatabaseContext

    Public Sub New()
        ' Can still have setup in the constructor
        Db = New MyDatabaseContext()
    End Sub

    Public Function InitializeAsync() As Task Implements IAsyncLifetime.InitializeAsync
        ' Async setup code that runs once for the entire test class
        Return Task.CompletedTask
    End Function

    Public Function DisposeAsync() As Task Implements IAsyncLifetime.DisposeAsync
        ' Async teardown code that runs once for the entire test class
        Db.Dispose()
        Return Task.CompletedTask
    End Function
End Class
