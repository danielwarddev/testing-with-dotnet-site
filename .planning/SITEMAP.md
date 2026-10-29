# Site Map & Content Backlog

> **Generated:** March 8, 2026  
> **Site:** Testing with .NET — an educational Astro/Starlight static site about .NET testing  
> **Audience:** All levels (.NET developers from beginner to advanced)  
> **Stack:** Astro 5 + Starlight + Tailwind CSS 4 + MDX  
> **Languages:** Code examples in C#, F#, and Visual Basic (via `CodeTabs` component)

---

## Site Map

```
Testing with .NET
├── Home (index.mdx) .......................... ⬜ PLACEHOLDER — default Starlight template
├── Getting Started ........................... 🟡 PARTIAL — good intro, has TODO list
│
├── Theory
│   ├── Why Test? ............................. ⬜ STUB
│   ├── Fundamentals .......................... ✅ MOSTLY COMPLETE
│   └── Testing Pyramid & Other Shapes ........ ⬜ STUB
│
├── Concepts
│   ├── Arrange, Act, Assert .................. ✅ COMPLETE (with code tabs)
│   ├── Mocking / Test Doubles ................ ✅ COMPLETE (dummy, stub, spy, mock, fake)
│   └── Writing Testable Code ................. ✅ COMPLETE (DI, interfaces, pure functions)
│
├── Frameworks
│   ├── xUnit ................................. ✅ MOSTLY COMPLETE (minor TODOs)
│   ├── NUnit ................................. ✅ MOSTLY COMPLETE (minor TODOs)
│   ├── MSTest ................................ ⬜ STUB
│   └── TUnit ................................. ⬜ STUB (+ orphan tunit.md duplicate)
│
├── Assertion Libraries
│   ├── Built-in Assertions ................... ⬜ STUB
│   ├── Fluent Assertions ..................... ⬜ STUB
│   ├── Shouldly .............................. ⬜ STUB
│   └── Awesome Assertions .................... ⬜ STUB
│
├── Mocking Libraries
│   ├── NSubstitute ........................... ⬜ STUB
│   ├── Moq ................................... ⬜ STUB
│   └── FakeItEasy ............................ ⬜ STUB
│
├── Libraries
│   ├── AutoFixture ........................... ⬜ STUB
│   └── Testcontainers ........................ ⬜ STUB
│
├── How Do I…?
│   └── Mocking HttpClient .................... ✅ COMPLETE (5 approaches)
│
├── Testing with a Database ................... ✅ MOSTLY COMPLETE (minor TODO)
│
├── Naming
│   └── Naming Tests .......................... ⬜ STUB (one orphan sentence)
│
├── Guides
│   ├── Example Guide ......................... ⬜ PLACEHOLDER — default Starlight template
│   └── Mocking HttpClient (guide) ............ ⬜ STUB (duplicate of how-do-i page)
│
└── Reference
    └── Example Reference ..................... ⬜ PLACEHOLDER — default Starlight template
```

### Legend

| Icon | Meaning |
|------|---------|
| ✅ | Complete or mostly complete — has real, substantive content |
| 🟡 | Partial — started but needs significant work |
| ⬜ | Stub/Placeholder — frontmatter only or default template |

### Completeness Summary

| Status | Count |
|--------|-------|
| Complete / Mostly complete | **8** |
| Partial | **1** |
| Stub (empty) | **16** |
| Placeholder (default template) | **3** |
| **Total pages** | **28** |

---

## Sidebar vs. Content Gaps

The Starlight sidebar in `astro.config.mjs` currently defines these sections. Several content areas **exist as files but are not in the sidebar**, and some sidebar items **link to stubs**.

### In sidebar but stub/empty

| Sidebar Section | Page | Status |
|----------------|------|--------|
| Frameworks | MSTest | ⬜ Stub |
| Frameworks | TUnit | ⬜ Stub |
| Libraries | AutoFixture | ⬜ Stub |
| Libraries | Testcontainers | ⬜ Stub |
| Guides | Example Guide | ⬜ Placeholder |
| Reference | Example Reference | ⬜ Placeholder |
| Naming | Naming Tests | ⬜ Stub |

### Content exists but NOT in sidebar

| Section | Pages | Status |
|---------|-------|--------|
| Theory | Why Test?, Fundamentals, Testing Pyramid | 1 mostly complete, 2 stubs |
| Concepts | Arrange-Act-Assert | ✅ Complete (but missing from sidebar) |
| Assertion Libraries | Built-in, Fluent, Shouldly, Awesome | All stubs |
| Mocking Libraries | NSubstitute, Moq, FakeItEasy | All stubs |
| Top-level | Testing with a Database | ✅ Mostly complete (not in sidebar) |

### Known Duplicates / Orphans

1. **`frameworks/tunit.md`** — orphan file alongside `frameworks/tunit/index.mdx` (both stubs)
2. **`guides/mocking-httpclient.md`** — empty stub duplicate of the complete `how-do-i/mocking-httpclient/index.mdx`

---

## Prioritized Content Backlog

Priorities based on your selections: Frameworks → Theory → Mocking & Assertion Libraries.

### Priority 1 — Frameworks (MSTest & TUnit)

These are in the sidebar already, linking to empty pages. Filling them gives immediate value.

| # | Task | File | Notes |
|---|------|------|-------|
| 1.1 | **Write MSTest page** | `frameworks/mstest/index.mdx` | Cover `[TestMethod]`, `[DataRow]`, `[TestInitialize]`/`[TestCleanup]`, parallel config. Follow xUnit/NUnit page structure. Add C#/F#/VB code tabs. |
| 1.2 | **Write TUnit page** | `frameworks/tunit/index.mdx` | Cover TUnit's source-gen approach, `[Test]`, `[Arguments]`, parallel execution, setup/teardown. Add code tabs. |
| 1.3 | **Finish xUnit TODOs** | `frameworks/xunit/index.mdx` | Add CollectionFixture and assembly fixture sections. |
| 1.4 | **Finish NUnit TODOs** | `frameworks/nunit/index.mdx` | Expand "shared data" section. |
| 1.5 | **Delete orphan `frameworks/tunit.md`** | `frameworks/tunit.md` | Duplicate of `frameworks/tunit/index.mdx`. |

### Priority 2 — Theory

Foundational content for beginners. Not yet in sidebar — needs sidebar entry.

| # | Task | File | Notes |
|---|------|------|-------|
| 2.1 | **Write "Why Test?" page** | `theory/why-test.md` | Benefits of testing, cost of bugs, refactoring confidence, documentation value. |
| 2.2 | **Write "Testing Pyramid" page** | `theory/testing-pyramid.md` | Classic pyramid, diamond, trophy. Unit vs integration vs E2E. Include diagram. |
| 2.3 | **Add Theory section to sidebar** | `astro.config.mjs` | Add sidebar group with Why Test, Fundamentals, Testing Pyramid. Consider placing near top. |

### Priority 3 — Mocking Libraries

| # | Task | File | Notes |
|---|------|------|-------|
| 3.1 | **Write NSubstitute page** | `mocking/nsubstitute/index.md` | Setup, returns, argument matchers, received calls, auto-substitute. |
| 3.2 | **Write Moq page** | `mocking/moq/index.md` | Setup, returns, verify, callbacks. Note: mention Moq controversy if relevant. |
| 3.3 | **Write FakeItEasy page** | `mocking/fakeiteasy/index.md` | A.CallTo, returns, assertions. |
| 3.4 | **Add Mocking Libraries to sidebar** | `astro.config.mjs` | New sidebar group. |

### Priority 4 — Assertion Libraries

| # | Task | File | Notes |
|---|------|------|-------|
| 4.1 | **Write Built-in Assertions page** | `assertion-libraries/built-in/index.md` | xUnit Assert, NUnit Assert, MSTest Assert — compare syntax. |
| 4.2 | **Write Fluent Assertions page** | `assertion-libraries/fluent-assertions/index.md` | Core syntax, collections, exceptions, object graphs. |
| 4.3 | **Write Shouldly page** | `assertion-libraries/shouldly/index.md` | Core syntax, comparison with FluentAssertions. |
| 4.4 | **Write Awesome Assertions page** | `assertion-libraries/awesome-assertions/index.md` | Overview and getting started (fork of FluentAssertions). |
| 4.5 | **Add Assertion Libraries to sidebar** | `astro.config.mjs` | New sidebar group. |

### Priority 5 — Site Polish & Navigation

| # | Task | File | Notes |
|---|------|------|-------|
| 5.1 | **Customize home page** | `index.mdx` | Replace default Starlight template with project-specific hero, feature cards, learning path. |
| 5.2 | **Add Arrange-Act-Assert to sidebar** | `astro.config.mjs` | It's in Concepts folder but commented out / missing from sidebar items. |
| 5.3 | **Add "Testing with a Database" to sidebar** | `astro.config.mjs` | Mostly-complete page not accessible via navigation. |
| 5.4 | **Clean up Getting Started TODOs** | `getting-started.md` | Remove TODO list, flesh out content into a proper onboarding flow. |
| 5.5 | **Write Naming Tests page** | `naming/naming-tests.md` | Conventions, `[DisplayName]`, MethodName_Scenario_Expected vs Given-When-Then, etc. |
| 5.6 | **Remove placeholder pages** | `guides/example.md`, `reference/example.md` | Or replace with real content. |
| 5.7 | **Remove or redirect duplicate** | `guides/mocking-httpclient.md` | Duplicate of the complete how-do-i page. |
| 5.8 | **Fix GitHub social link** | `astro.config.mjs` | Currently points to `withastro/starlight` — should point to this project's repo. |
| 5.9 | **Define scope on Getting Started** | `getting-started.md` | Make explicit that this site is not a reference manual — it covers judgment, patterns, and real-world usage, not full API docs. Sets reader expectations up front. |

### Priority 6 — Libraries

| # | Task | File | Notes |
|---|------|------|-------|
| 6.1 | **Write AutoFixture page** | `libraries/autofixture.md` | Auto-generation, customization, integration with xUnit. |
| 6.2 | **Write Testcontainers page** | `libraries/testcontainers.md` | Setup, database containers, custom Dockerfiles. |

### Priority 7 — Future Content (from PRD)

These are mentioned in the PRD but don't have files yet:

| # | Topic | Notes |
|---|-------|-------|
| 7.1 | **TDD (Test-Driven Development)** | Red-green-refactor cycle. Commented out in sidebar. |
| 7.2 | **BDD (Behavior-Driven Development)** | SpecFlow, Gherkin. Commented out in sidebar. |
| 7.3 | **Test Patterns** | Builder pattern, Object Mother, etc. Commented out in sidebar. |
| 7.4 | **Types of Tests deep-dive** | Unit vs integration vs functional vs E2E beyond pyramid page. |
| 7.5 | **More How-Do-I guides** | Common tasks: mocking DateTime, testing async code, testing EF Core, etc. |

---

## Technical Notes

- **CodeTabs component** (`src/components/CodeTabs.astro`): Loads `.cs`, `.fs`, `.vb` files from a `code/` subfolder relative to the page URL. Any new framework/concept page that needs multi-language examples should use this pattern.
- **codegen.sh**: Utility script to scaffold `.cs`/`.fs`/`.vb` file triples for new code examples.
- **Remark plugin** (`src/plugins/remark-external-links.mjs`): Auto-adds `target="_blank"` and `rel="noopener noreferrer"` to external links.
- **Description fields**: Most stub pages have `description: Temp` — these should be updated with real SEO descriptions when content is written.
