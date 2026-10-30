## Development

When starting the dev server, use background mode:

```
astro dev --background
```

Manage the background server with `astro dev stop`, `astro dev status`, and `astro dev logs`.

## Documentation

Full documentation: https://docs.astro.build

Consult these guides before working on related tasks:

- [Adding pages, dynamic routes, or middleware](https://docs.astro.build/en/guides/routing/)
- [Working with Astro components](https://docs.astro.build/en/basics/astro-components/)
- [Using React, Vue, Svelte, or other framework components](https://docs.astro.build/en/guides/framework-components/)
- [Adding or managing content](https://docs.astro.build/en/guides/content-collections/)
- [Adding styles or using Tailwind](https://docs.astro.build/en/guides/styling/)
- [Supporting multiple languages](https://docs.astro.build/en/guides/internationalization/)

# Project

This site is a .NET testing knowledge base built with Astro and Starlight.

- Starlight docs: https://starlight.astro.build/getting-started/
- Use Starlight's built-in components where they fit, such as `<Card>`, `<FileTree>`, `<Tabs>` and `<Aside>`.

## Commands

- `npm run dev`: start the dev server.
- `npm run build`: build the site. Run it after content changes; it fails if a `CodeTabs` block can't find its code files.
- `npm test`: run the Vitest tests once. `npm run test:watch` runs them in watch mode.
- `npm run codegen <Name> [<Name> ...]`: create empty `<Name>.cs`, `<Name>.fs` and `<Name>.vb` files in the current folder.

## Conventions

- Import components through the `@components` alias, e.g. `import CodeTabs from '@components/CodeTabs.astro';`. Don't use relative paths.
- When adding a page, register it in the `sidebar` in `astro.config.mjs`. Pages aren't added to the sidebar automatically.
- Markdown is processed by Sätteri, not remark. Markdown plugins are Sätteri HAST plugins registered through `markdown.processor` in `astro.config.mjs` (see `src/plugins/external-links.mjs`).
- Tests live in `src/test/`, with fixtures in `src/test/fixtures/`. Write them with `it('should ...')`, not `test(...)`.

## Code samples

1. Put sample files in a `code/` folder next to the page's `index.mdx`.
2. From that folder, run `npm run codegen <Name>` to create the `.cs`, `.fs` and `.vb` files.
3. Write the C# version first.
4. Use the `translate-code` skill to write the F# and VB versions.
5. Show them on the page with `<CodeTabs baseFilePath="code/<Name>" />`.

# Site Design Philosophy

This site is a knowledge base for .NET testing. Every content and design decision should serve these principles:

## Navigation: keep it flat and clear
- No more than one level of nesting per sidebar section. If something needs a sub-sub-section, it probably belongs on its own page.
- The homepage must give visitors an obvious starting point — a clear call to action, not a table of contents dump.

## Voice: opinionated, practical, experience-driven
- This site is not a neutral documentation mirror. Its value is the *wisdom* behind the code — the libraries and patterns people actually reach for in the real world, not just a list of options.
- When there's a clear community preference or a hard-won lesson, say so directly. Hedge only when genuine trade-offs exist.
- Pair every practical recommendation with a brief *why*. "Use this" is less memorable than "use this because…"
- **Tension:** "Always explain the why" and "be concise" pull against each other. Resolve it by keeping the *why* to one sentence — not a paragraph of justification. The goal is a memorable reason, not a full argument.

## Content: ruthlessly concise
- Ten good pieces of advice read like zero. Prefer three great ones.
- Each page should have one clear takeaway. Supporting detail is fine, but don't bury the lead.
- Code examples should be minimal and purposeful — just enough to illustrate the point, no scaffolding noise.

## Scope: not a reference manual
- This site does not try to document every API surface, parameter, or overload. The official library docs exist for that.
- Content should cover what the official docs don't: judgment calls, common patterns, real-world usage, and pitfalls.
- If a page is drifting toward mirroring the official docs, cut it back to the essential insight.

## Code examples: multi-language, consistent
- All code samples use C#, F#, and Visual Basic via the `CodeTabs` component.
- Examples use the site's chosen defaults: xUnit, NSubstitute, FluentAssertions/AwesomeAssertions, Testcontainers, AutoFixture.
