---
title: Getting started
description: Temp
---

This site is intended to be a practical guide to writing automated tests in .NET (C#, VB, and F#) with modern practices. This means both giving options for which tools to use (such as xUnit vs. NUnit, Moq vs. NSubstitute, etc.), as well as some examples for how to do specific tasks with those libraries that developers will commonly run into (such as how to mock HttpClient).

This is not intended to be a very opinionated site, but some bias is probably unavoidable unless the content is watered down to the point of being less useful.

So, rather than be a complete regurgitation of all the docs of the frameworks and libraries it talks about, its goal is to present the most relevant information, as well as how to use it practically.

### TODO
- Choose a testing framework
- Choose an assertion library
- Testing with a database
- Test pyramid

This site uses the following in its code examples:
- Testing framework - xUnit
- Mocking library - NSubstitute
- Assertions library - FluentAssertions/AwesomeAssertions
- Testcontainers for testing with a database
- AutoFixture for creating fake test data