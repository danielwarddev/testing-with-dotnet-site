<Theory>
<InlineData(1, 2, 3)>
<InlineData(5, 0, 5)>
<InlineData(-1, 2, 1)>
Public Sub Adds_Numbers_Correctly(a As Integer, b As Integer, expected As Integer)
    Dim actual = Add(a, b)
    Assert.Equal(actual, expected)
End Sub
