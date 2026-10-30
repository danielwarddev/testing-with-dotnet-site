' AddTests.vb
<Parallelizable(ParallelScope.Children)>
Public Class AddTests
    ' Tests in this class can run in parallel with each other
    ...
End Class

' SubtractTests.vb
<Parallelizable(ParallelScope.Children)>
Public Class SubtractTests
    ' Tests in this class can run in parallel with each other
    ...
End Class
