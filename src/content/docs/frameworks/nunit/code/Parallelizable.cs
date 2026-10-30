// AddTests.cs
[Parallelizable(ParallelScope.Children)]
public class AddTests
{
    // Tests in this class can run in parallel with each other
    ...
}

// SubtractTests.cs  
[Parallelizable(ParallelScope.Children)]
public class SubtractTests
{
    // Tests in this class can run in parallel with each other
    ...
}
