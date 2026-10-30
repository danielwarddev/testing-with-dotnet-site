<TestCase(1, 2, 3)>
<TestCase(5, 0, 5)>
<TestCase(-1, 2, 1)>
Public Sub Adds_Numbers_Correctly(a As Integer, b As Integer, expected As Integer)
    Dim actual = Add(a, b)
    Assert.That(actual, [Is].EqualTo(expected))
End Sub
