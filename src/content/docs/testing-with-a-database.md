---
title: Testing with a database
description: Temp
---

Tests will often need to reach out to a database to test the system under test properly. However, hitting a real database can have its problems, and there are other some options available.

**This site recommends using Testcontainers** as a general solution to writing tests involving a database.

Though this page describes each option in a bit more detail below, here is a table briefling summarizing each option:

<!-- https://www.tablesgenerator.com/markdown_tables -->
<!-- | Method | Pros | Cons |
|---|---|---|
| Real database | Simulates production database | Heavy maintenance costs - reset to clean state between runs; manual cleanup at times; keep up to parity with production database; can't have multiple things accessing it at once |
| In-memory provider | Quick setup | Unlike real database; not relational; discouraged by Microsoft |
| SQLite provider | Quick setup | Unlike real database (unless production also using SQLite) |
| Docker | Simulates production database | Requires creation and maintenance of Dockerfile(s); requires team knowledge of Docker |
| Testcontainers | Simulates production database, quick setup | May be some niche situations that Testcontainers can't cover | -->
| Method | Uses real production engine | Maintenance cost |
|---|---|---|
| Real database | Yes | Very high |
| In-memory provider | No | Low |
| SQLite provider | No (unless using SQLite in production) | Low |
| Docker | Yes | Medium - High |
| Testcontainers | Yes | Low |

## Using the EF Core in-memory or SQLite provider

With either of these providers, it's important to keep in mind that ultimately, **if you're testing against a different database engine, you're testing a different system**. This is true to some degree with many tests we write, though testing against a different database engine can potentially lead to your tests producing false positives or false negatives.

While the EF Core in-memory provider offers quick setup, using it for tests is highly discouraged, and [even the Microsoft docs recommend against this](https://learn.microsoft.com/en-us/ef/core/testing/choosing-a-testing-strategy#in-memory-as-a-database-fake). It has a range of issues for use in testing, such as it not actually being a relational database.

The SQLite provider is a bit better, being a true relational database. However, as [the Microsoft docs also discourage use of this provider for testing](https://learn.microsoft.com/en-us/ef/core/testing/choosing-a-testing-strategy#sqlite-as-a-database-fake), using a different database engine in your tests than your production database can lead to different results.

### False positives and negatives

"False positives" refers to tests passing in cases where the real production app would fail. "False negatives" refers to the opposite - tests failing, while in production the real app would succeed.

Using a different database engine in your tests than in production can lead to false positives and negatives. While relatively rare, they can be very costly when they happen, due to confusion, time to fix, and defects shipped to production.

#### TODO
For instance, 

## Using a real database

Accessing a real database in tests allows for using the same database engine as production, though it has relatively high maintenance costs. For example:

* If any tests change the database state, you may need to ensure the state is reset between each run of the test suite. If that fails, manual intervention may be needed.
* Similar to the above reason, you may also need to ensure that no other service (or person) changes anything in the database while the test suite is running. This is particularly painful if the team is deploying to production frequently.
* Since the database is a separate entity from the real production database, care must be taken with additional maintenance to keep it up to parity, including schema updates and database version upgrades.

## Using a Docker container

Using a Docker container (or Docker Compose, depending on the test suite's needs) allows for using the same database engine as production, and can be a clean solution to accessing a database in tests. However, it requires team knowledge (and potential upskilling) of Docker and manual creation and maintenance of the Dockerfile(s).

## Using Testcontainers

[Testcontainers](https://testcontainers.com/) allows for using the same database engine as production, while also avoiding the need to maintain a Dockerfile or a real database.