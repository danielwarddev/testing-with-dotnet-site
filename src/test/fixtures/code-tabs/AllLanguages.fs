[<Fact>]
let AllLanguages() =
    let items = [ "a & b" ]
    Assert.Single(items)
