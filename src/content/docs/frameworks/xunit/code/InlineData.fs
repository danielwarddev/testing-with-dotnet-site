[<Theory>]
[<InlineData(1, 2, 3)>]
[<InlineData(5, 0, 5)>]
[<InlineData(-1, 2, 1)>]
let ``Adds Numbers Correctly`` a b expected =
    let actual = Add(a, b)
    Assert.Equal(actual, expected)
