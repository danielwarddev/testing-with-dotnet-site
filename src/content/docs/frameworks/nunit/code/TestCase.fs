[<TestCase(1, 2, 3)>]
[<TestCase(5, 0, 5)>]
[<TestCase(-1, 2, 1)>]
let ``Adds Numbers Correctly`` a b expected =
    let actual = Add(a, b)
    Assert.That(actual, Is.EqualTo(expected))
