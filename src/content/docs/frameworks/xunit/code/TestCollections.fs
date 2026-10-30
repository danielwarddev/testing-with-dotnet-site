// AddTests.fs
module AddTests

open Xunit

[<Collection("Test Collection 1")>]
type AddTests() =
    ...

// SubtractTests.fs
module SubtractTests

open Xunit

[<Collection("Test Collection 1")>]
type SubtractTests() =
    ...