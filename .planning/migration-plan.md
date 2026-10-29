# Migration plan: fresh Astro + Starlight project

Rebuild the site on a fresh scaffold of the latest Astro and Starlight, then port the custom pieces in small chunks. Finish and verify each chunk before starting the next one.

## Chunk 0: Scaffold (you)

- [ ] Run `npm create astro@latest -- --template starlight/tailwind`.
- [ ] **Verify:** `npm run dev` serves the starter site.

## Chunk 1: Theme and branding

- [ ] Copy `src/assets/logo-light.svg`, `src/assets/logo-dark.svg`, `public/favicon.svg` and `public/favicon-light.svg`. Leave out `old_favicon.svg` and `houston.webp`.
- [ ] Port `src/styles/global.css`: the accent and gray colour palettes, `--sl-content-width`, the 15px font size, the logo size, and the xUnit hero image fix.
- [ ] Set the title, the logo and `customCss` in `astro.config.mjs`.
- [ ] Point the GitHub link to `danielwarddev/testing-with-dotnet-site`. It currently links to `withastro/starlight`.
- [ ] Delete the starter template's own pages and images.
- [ ] **Verify:** the empty site looks like the current one in light and dark mode.

## Chunk 2: Remark plugin

- [ ] Port `src/plugins/remark-external-links.mjs` and register it under `markdown.remarkPlugins`.
- [ ] Add a temporary test page with four links: a Microsoft Learn link, another external link, an internal link and an anchor link.
- [ ] **Verify:**
  - Only the Learn link gets `wt.mc_id=MVP_367508`. It uses `&` instead of `?` when the URL already has a query string.
  - Only the external links get `target="_blank"` and `rel="noopener noreferrer"`.
- [ ] Remove the test page.

## Chunk 3: CodeTabs and the `@components` alias

- [ ] Port `src/components/CodeTabs.astro`.
- [ ] Set up the `@components` alias. First try tsconfig `paths` without `baseUrl`, because `baseUrl` is deprecated in TS 6. Only add a `vite.resolve.alias` entry if the build fails.
- [ ] Migrate one page with code samples to test it, such as `concepts/mocking`.
- [ ] **Verify:**
  - The C#, F# and VB tabs all render with code in them.
  - The language you pick carries over to other pages.
  - CodeTabs builds file paths from `Astro.url.pathname`. If that resolves differently, the tabs render empty without any error.

## Chunk 4: Content

- [ ] Copy everything in `src/content/docs`: the `.md`, `.mdx`, `.cs`, `.fs` and `.vb` files and the images.
- [ ] Rebuild the sidebar in `astro.config.mjs`.
  - Add the `assertion-libraries` pages, which are missing from the current sidebar.
  - Drop the starter "Example Guide" and "Reference" entries.
- [ ] Change the relative CodeTabs imports in `frameworks/tunit` and `frameworks/mstest` to `@components/CodeTabs.astro`.
- [ ] **Verify:**
  - `npm run build` succeeds, and the page count matches the old site (28 pages before cleanup).
  - Every CodeTabs block shows its code on every page.

## Chunk 5: Tooling and AI files

- [ ] Bring over `codegen.sh` and its `codegen` npm script, `.vscode/`, `.planning/`, `README.md`, `.claude/` and `CLAUDE.md`.
- [ ] Rewrite `AGENTS.md`:
  - Remove the React leftovers: the "consts instead of functions" rule, the `handle` prefix and the `on:click` syntax.
  - Change the `class:` rule to `class:list`.
  - Keep the Site Design Philosophy section.
  - Add the code sample workflow: put files in the page's `code/` folder, create them with `npm run codegen`, write the C# first, then use the translate-code skill.
  - Add a reminder to register new pages in the sidebar.
  - Add the `dev` and `build` commands and the `@components` alias.
- [ ] **Verify:** you review `AGENTS.md`, and `npm run codegen Foo` creates `Foo.cs`, `Foo.fs` and `Foo.vb`.
