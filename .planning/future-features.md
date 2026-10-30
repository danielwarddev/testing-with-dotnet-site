# Future Features

## Make CodeTabs throw on empty code files

`src/components/CodeTabs.astro` skips empty code files for now. If every file is empty, it renders an empty `<Tabs>`. This means the placeholder files on `arrange-act-assert` and `writing-testable-code` don't break the build while the site is still being written.

Once the placeholders are filled in, change CodeTabs to throw on an empty file, so a forgotten placeholder fails the build. Update the two empty-file tests in `src/test/CodeTabs.test.ts` to match.
