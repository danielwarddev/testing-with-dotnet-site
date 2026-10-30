# Migration plan: fresh Astro + Starlight project

Rebuild the site on a fresh scaffold of the latest Astro and Starlight, then port the custom pieces in small chunks. Finish and verify each chunk before starting the next one.

## Chunk 0: Scaffold (you)

- [ ] Run `npm create astro@latest -- --template starlight/tailwind`.
- [ ] **Verify:** `npm run dev` serves the starter site.

## Chunk 1: Theme and branding

- [x] Copy `src/assets/logo-light.svg`, `src/assets/logo-dark.svg`, `public/favicon.svg` and `public/favicon-light.svg`. Leave out `old_favicon.svg` and `houston.webp`.
- [x] Port `src/styles/global.css`: the accent and gray colour palettes, `--sl-content-width`, the 15px font size, the logo size, and the xUnit hero image fix.
- [x] Set the title, the logo and `customCss` in `astro.config.mjs`.
- [x] Point the GitHub link to `danielwarddev/testing-with-dotnet-site`. It currently links to `withastro/starlight`.
- [x] Delete the starter template's own pages and images.
- [x] **Verify:** the empty site looks like the current one in light and dark mode. Colours, logo, header and fonts match. Starlight now gives the "On this page" sidebar a minimum width of `--sl-sidebar-width`, so on screens narrower than about 1650px the content column is narrower than on the old site.

## Chunk 2: Markdown plugin

- [x] Port `src/plugins/remark-external-links.mjs`. Astro 7 renders Markdown with Sätteri, and `markdown.remarkPlugins` is deprecated, so the plugin became a Sätteri HAST plugin in `src/plugins/external-links.mjs`, registered through `markdown.processor: satteri({ hastPlugins })`.
- [x] Add temporary `.md` and `.mdx` test pages with four kinds of link: a Microsoft Learn link, another external link, an internal link and an anchor link.
- [x] **Verify:**
  - Only the Learn link gets `wt.mc_id=MVP_367508`. It uses `&` instead of `?` when the URL already has a query string.
  - Only the external links get `target="_blank"` and `rel="noopener noreferrer"`.
  - Starlight's own transforms (asides, heading anchor links) still work.
- [x] Remove the test pages.

## Chunk 3: CodeTabs and the `@components` alias

- [x] Port `src/components/CodeTabs.astro`.
- [x] Set up the `@components` alias. tsconfig `paths` without `baseUrl` works, so no `vite.resolve.alias` entry is needed.
- [x] Migrate one page with code samples to test it, such as `concepts/mocking`.
- [x] **Verify:**
  - [x] The C#, F# and VB tabs all render with code in them.
  - [x] The language you pick carries over to other pages.
  - [x] CodeTabs builds file paths from `Astro.url.pathname`, and in dev, a URL with no trailing slash rendered empty tabs. CodeTabs now adds the trailing slash itself, and throws an error if it finds no code files.

## Chunk 4: Content

- [x] Copy everything in `src/content/docs`: the `.md`, `.mdx`, `.cs`, `.fs` and `.vb` files and the images. Left out the starter pages (`guides/example.md`, `reference/example.md`) and two empty placeholders: `frameworks/tunit.md`, which had the same URL as `frameworks/tunit/index.mdx`, and `guides/mocking-httpclient.md`, which duplicated `how-do-i/mocking-httpclient`.
- [x] Rebuild the sidebar in `astro.config.mjs`.
  - Add the `assertion-libraries` pages, which are missing from the current sidebar.
  - Drop the starter "Example Guide" and "Reference" entries.
  - Starlight 0.39 removed `autogenerate` with a `label`, so "Naming tests" now uses `items: [{ autogenerate: { directory: 'naming' } }]`.
- [x] `index.mdx` uses `houston.webp` twice: for the hero image and an `<Image>`. Point both at a different image, because Houston isn't being brought over. Both now use the light and dark site logos, and the hero button goes to Getting started instead of the Example Guide.
- [x] Change the relative CodeTabs imports in `frameworks/tunit` and `frameworks/mstest` to `@components/CodeTabs.astro`.
- [x] 14 placeholder code files on `arrange-act-assert` and `writing-testable-code` are empty. CodeTabs now skips empty files instead of treating them as missing, the same as the old site. It still throws when no files exist. Throwing on empty files is in `future-features.md`.
- [x] **Verify:**
  - `npm run build` succeeds with 25 pages: the old site's 28 minus the 3 dropped pages.
  - Every CodeTabs block shows its code on every page.

## Chunk 5: Tooling and AI files

- [x] Bring over `codegen.sh` and its `codegen` npm script, `.vscode/`, `.planning/`, `README.md`, `.claude/` and `CLAUDE.md`.
  - `codegen.sh` now changes to `$INIT_CWD` first. `npm run` always starts scripts in the project root, so it used to create the files there instead of the folder it was run from.
  - `.vscode/`: brought over the old `extensions.json` and `settings.json`. `launch.json` was identical.
  - `README.md`: kept the new starter README. The old one's only custom part was a note about a `vite` override, which the new project doesn't need.
- [x] Rewrite `AGENTS.md`:
  - Remove the React leftovers: the "consts instead of functions" rule, the `handle` prefix and the `on:click` syntax. Already done in the old repo (`797a2f2`).
  - Change the `class:` rule to `class:list`. No `class:` rule was left after that cleanup.
  - Keep the Site Design Philosophy section.
  - Add the code sample workflow: put files in the page's `code/` folder, create them with `npm run codegen`, write the C# first, then use the translate-code skill.
  - Add a reminder to register new pages in the sidebar.
  - Add the `dev` and `build` commands and the `@components` alias.
  - Also added `npm test`, the Sätteri plugin note and the test conventions.
- [ ] **Verify:**
  - [ ] You review `AGENTS.md`.
  - [x] `npm run codegen Foo` creates `Foo.cs`, `Foo.fs` and `Foo.vb` in the folder it's run from.
