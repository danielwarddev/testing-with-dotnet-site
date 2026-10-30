[TestCase(1, 2, 3)]
[TestCase(5, 0, 5)]
[TestCase(-1, 2, 1)]
public void Adds_Numbers_Correctly(int a, int b, int expected)
{
    var actual = Add(a, b);
    Assert.That(actual, Is.EqualTo(expected));
}