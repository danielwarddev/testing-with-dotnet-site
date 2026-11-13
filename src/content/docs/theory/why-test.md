---
title: Why test?
description: Temp
---

In general, we test to have confidence in our product, though this short explanation perhaps undersells all the benefits.

Aside from that, writing tests also provides other benefits, such as:

- **System requirements are documented through test names and what they assert**. Traditional documentation and code comments are prone to becoming stale and no longer matching the code they describe, but tests break the build if they don't accurately describe the system, so they must stay in line.
- If following TDD (and even when not to a lesser degree), **tests force us to become the first consumer of our code**. This helps lead to better design choices.
- **Being able to deploy code straight to production after merging**, which prevents batched releases and lowers the risk of defects.


## How many tests is enough?

 It's important to note that **there is no empirical standard at which you can know it is safe to deploy code to production.** You can have 100% test coverage and still have bugs, and you can have 80% test coverage and have all the real production paths reasonably covered. How do we know when we have enough tests, then?


## Confident green

A good standard to go by is the idea of **confident green**. This means that when you see your test suite pass (which usually shows a green checkmark, hence the "green"), you should have the confidence to deploy it straight to production.

If you don't have that confidence, ask yourself why you don't. In my experience, people usually have right away a case they're thinking of that would prevent them having that confidence. In that case, ask:

- Can we cover that case with automated tests?
- If not, how can we work towards being able to test it with an automated test?
- Finally, *should* we be covering that with automated tests?

It's okay if adding more test cases to reach this is a slow process and doesn't happen immediately, but it absolutely is realistic and attainable. As Kent Beck said, [make the change easy, then make the change](https://x.com/KentBeck/status/250733358307500032).

## Unconfident red

By contrast, you should also have **unconfident red**. This means that, if your test suite fails (and shows you a red X or something similar), you should be assured that something is indeed wrong.

Flaky tests will prevent you from having this. An easy code smell to look for is if you have to add `[Skip]` (or a similar functionality) to specific tests because they're unreliable.