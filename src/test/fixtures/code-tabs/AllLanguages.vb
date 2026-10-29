<Fact>
Public Sub AllLanguages()
    Dim items = New List(Of String) From {"a & b"}
    Assert.Single(items)
End Sub
