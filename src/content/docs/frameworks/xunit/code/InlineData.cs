[Theory]
[InlineData(1, 2, 3)]
[InlineData(5, 0, 5)]
[InlineData(-1, 2, 1)]
public void Adds_Numbers_Correctly(int a, int b, int expected)
{
    var actual = Add(a, b);
    Assert.Equal(actual, expected);
}