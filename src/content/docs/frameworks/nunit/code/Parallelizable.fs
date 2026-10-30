// AddTests.fs
module AddTests

open NUnit.Framework

[<Parallelizable(ParallelScope.Children)>]
type AddTests() =
    // Tests in this class can run in parallel with each other
    ...

// SubtractTests.fs
module SubtractTests

open NUnit.Framework

[<Parallelizable(ParallelScope.Children)>]
type SubtractTests() =
    // Tests in this class can run in parallel with each other
    ...
