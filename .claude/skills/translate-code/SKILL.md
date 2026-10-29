---
name: translate-code
description: Use when translating C# code snippets in the site's documentation into F# and VB.NET equivalents for the CodeTabs component. Only translate the files or snippets explicitly asked for.
---

# Code Translation Prompt

Convert C# code snippets to F# and VB.NET equivalents for the testing-with-dotnet-site documentation.

## Task

When given a C# code snippet, provide both F# and VB.NET translations following these guidelines exactly. Don't do the entire project, but only the files asked for.

## Guidelines

### General Rules

- These are code SNIPPETS, not full compilable programs
- Preserve ellipses (`...`) exactly as they appear - do NOT convert to comments
- Do not add additional comments beyond what exists in the original
- Maintain the same structure and formatting patterns used throughout the project
- Maintain the same spacing, indentation, and newlines as the original C# snippet, especially with method chaining, fluent APIs, LINQ expressions, and similar constructs. If needed, adjust the newlines to meet conventions for the target language.
- Use proper syntax for each language while keeping the code readable

### Language-Specific Patterns

#### C# to F#

- Convert attributes: `[Attribute]` → `[<Attribute>]`
- Convert methods to functions: `public void Method()` → `let Method() =`
- Convert classes to types: `public class Name` → `type Name() =`
- Use `let` for variable declarations: `var x = value` → `let x = value`
- Convert object initialization: `new Object { Prop = value }` → `{ Prop = value }`
- Add `|> ignore` for methods that return values but aren't used
- Use module declarations when appropriate: `module ModuleName`
- Convert comments: `//` remains `//`

#### C# to VB.NET

- Convert attributes: `[Attribute]` → `<Attribute>`
- Convert access modifiers: `public` → `Public`, `private` → `Private`
- Convert methods: `public void Method()` → `Public Sub Method()`
- Convert return methods: `public Type Method()` → `Public Function Method() As Type`
- Use `Dim` for variable declarations: `var x = value` → `Dim x = value`
- Convert object initialization: `new Object { Prop = value }` → `New Object With {.Prop = value}`
- Add `End Class`, `End Sub`, `End Function` as appropriate
- Convert comments: `//` → `'`
- Convert boolean values: `true` → `True`, `false` → `False`

### Formatting Requirements

- Maintain the same indentation style as the original
- Preserve blank lines and spacing
- Keep the same logical structure
- Ensure ellipses (`...`) appear in the same locations

## Example Input/Output

### Input (C#):

```csharp
[Fact]
public void TestExample()
{
    // Arrange
    var service = new Service();
    ...

    // Act
    var result = service.DoSomething();

    // Assert
    Assert.True(result);
}
```

### Expected F# Output:

```fsharp
[<Fact>]
let TestExample() =
    // Arrange
    let service = Service()
    ...

    // Act
    let result = service.DoSomething()

    // Assert
    Assert.True(result)
```

### Expected VB.NET Output:

```vb
<Fact>
Public Sub TestExample()
    ' Arrange
    Dim service = New Service()
    ...

    ' Act
    Dim result = service.DoSomething()

    ' Assert
    Assert.True(result)
End Sub
```
