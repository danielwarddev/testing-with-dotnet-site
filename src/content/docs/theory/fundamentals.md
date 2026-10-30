---
title: Fundamentals
description: Temp
---

## How do I know when I've written enough tests?

### Short answer

There's no hard number of tests, though anything under ~70% coverage should probably make you raise an eyebrow. Instead, you should continue writing tests until you have confidence to deploy your code to production once it's merged.

### Long answer

The main goal of any kind of software testing is to give us confidence that our system works as expected.

The key word there is **confidence** - unfortunately, there is no metric or empirical way we can use to be 100% assured that our system actually *is* working as expected. More tests give us more confidence, but complete assurance of no bugs is never possible.

So, instead, we can answer this question with another question:

>**At what point do you have confidence to deploy your code to production?**

Ideally, your test suite should give you that confidence, since manual testing is relatively slow and error-prone. If your test suite doesn't give you that confidence, ask yourself:

>**What would help give me that confidence in my current test suite?**

Likely, you're already aware of something missing from your tests - some weird use cases or code paths, an existing bug, etc. So, the answer would be to make sure those situations are also handled by your test suite to ensure your code is working.

As a very general rule, you probably won't get that confidence under around 70% code coverage. Keep in mind, though, that code coverage is not a perfect metric, and chasing 100% coverage can often be a waste of time.

:::caution[Goodhart's Law]
When a measure becomes a target, it ceases to be a good measure. Metrics like code coverage can be helpful, but ultimately aren't our goal.
:::

#### Confident green

This concept is sometimes called "**confident green**." When your test suite passes and you see the green checkmark ✅, you should be confident that your system works as expected.

Likewise, if your test suite fails and shows red ❌, you should *not* be confident. If you are, then you probably have flaky tests that you don't actually rely on.