import { expect, it } from "vitest";
import { markdownToHtml } from "satteri";
import { externalLinks } from "../plugins/external-links.mjs";

async function renderLink(url) {
  const { html } = await markdownToHtml(`[link](${url})`, { hastPlugins: [externalLinks] });
  return html.trim();
}

const EXTERNAL_ATTRIBUTES = 'target="_blank" rel="noopener noreferrer"';

it("should add the MVP tracking param to Microsoft Learn links", async () => {
  const html = await renderLink("https://learn.microsoft.com/en-us/dotnet/");

  expect(html).toBe(
    `<p><a href="https://learn.microsoft.com/en-us/dotnet/?wt.mc_id=MVP_367508" ${EXTERNAL_ATTRIBUTES}>link</a></p>`,
  );
});

it("should append the MVP tracking param with & when the Learn link has a query string", async () => {
  const html = await renderLink("https://learn.microsoft.com/en-us/dotnet/api/system.string?view=net-8.0");

  expect(html).toBe(
    `<p><a href="https://learn.microsoft.com/en-us/dotnet/api/system.string?view=net-8.0&amp;wt.mc_id=MVP_367508" ${EXTERNAL_ATTRIBUTES}>link</a></p>`,
  );
});

it("should not add the MVP tracking param twice", async () => {
  const html = await renderLink("https://learn.microsoft.com/en-us/dotnet/?wt.mc_id=MVP_367508");

  expect(html).toBe(
    `<p><a href="https://learn.microsoft.com/en-us/dotnet/?wt.mc_id=MVP_367508" ${EXTERNAL_ATTRIBUTES}>link</a></p>`,
  );
});

it("should open other external links in a new tab without the tracking param", async () => {
  const html = await renderLink("https://xunit.net/");

  expect(html).toBe(
    `<p><a href="https://xunit.net/" ${EXTERNAL_ATTRIBUTES}>link</a></p>`,
  );
});

it.each(["/frameworks/xunit/", "#section", "mailto:someone@example.com", "../mocking/"])(
  "should leave non-external link %s unchanged",
  async (url) => {
    const html = await renderLink(url);

    expect(html).toBe(`<p><a href="${url}">link</a></p>`);
  },
);
