import { within } from '@testing-library/dom';
import { experimental_AstroContainer as AstroContainer } from 'astro/container';
import { Window } from 'happy-dom';
import { readdirSync, readFileSync } from 'node:fs';
import { beforeAll, expect, it } from 'vitest';
import CodeTabs from '@components/CodeTabs.astro';

const PAGE_PATH = '/fake-test-page/';
const FIXTURE_DIRECTORY = 'src/test/fixtures/code-tabs';

let container: AstroContainer;

beforeAll(async () => {
	container = await AstroContainer.create();
});

it('should render a C#, F# and Visual Basic tab with the code from each file', async () => {
	const html = await renderCodeTabs('code/AllLanguages');
	const tabs = getTabData(html);

	expect(tabs).toEqual([
		{ label: 'C#', language: 'csharp', code: readCodeFileNormalizedLikeExpressiveCode('AllLanguages.cs') },
		{ label: 'F#', language: 'fsharp', code: readCodeFileNormalizedLikeExpressiveCode('AllLanguages.fs') },
		{ label: 'Visual Basic', language: 'vb', code: readCodeFileNormalizedLikeExpressiveCode('AllLanguages.vb') },
	]);
});

it('should find the files when the page URL has no trailing slash', async () => {
	const html = await renderCodeTabs('code/AllLanguages', '/fake-test-page');
	const tabs = getTabData(html);
	expect(tabs.map((tab) => tab.label)).toEqual(['C#', 'F#', 'Visual Basic']);
});

it('should only render tabs for the languages that have a file', async () => {
	const html = await renderCodeTabs('code/MissingFSharp');
	const tabs = getTabData(html);

	expect(tabs).toEqual([
		{ label: 'C#', language: 'csharp', code: readCodeFileNormalizedLikeExpressiveCode('MissingFSharp.cs') },
		{ label: 'Visual Basic', language: 'vb', code: readCodeFileNormalizedLikeExpressiveCode('MissingFSharp.vb') },
	]);
});

it('should throw when no code files exist for the base file path', async () => {
	await expect(renderCodeTabs('code/DoesNotExist')).rejects.toThrow(
		`CodeTabs found no .cs, .fs or .vb files for "code/DoesNotExist" on page ${PAGE_PATH}`,
	);
});

function renderCodeTabs(baseFilePath: string, pagePath = PAGE_PATH) {
	return container.renderToString(CodeTabs, {
		props: { baseFilePath, codeFiles: getFixtureCodeFiles() },
		request: new Request(`http://localhost${pagePath}`),
	});
}

function getFixtureCodeFiles() {
	return Object.fromEntries(
		readdirSync(FIXTURE_DIRECTORY).map((fileName) => [
			`/src/content/docs${PAGE_PATH}code/${fileName}`,
			readFileSync(`${FIXTURE_DIRECTORY}/${fileName}`, 'utf8'),
		]),
	);
}

function getTabData(html: string) {
	const document = new Window().document as unknown as Document;
	document.body.innerHTML = html;

	const body = within(document.body);
	const labels = body.getAllByRole('tab').map((tab) => tab.textContent.trim());
	const panels = body.getAllByRole('tabpanel', { hidden: true });

	return labels.map((label, i) => ({
		label,
		language: panels[i].querySelector('[data-language]')!.getAttribute('data-language'),
		code: decodeExpressiveCodeNewlines(panels[i].querySelector('[data-code]')!.getAttribute('data-code') ?? ''),
	}));
}

// Starlight uses Expressive Code to render nice-looking code blocks
// It changes \n into \u007f, which would make a direct string comparison against our code file fail
// So, decode the newlines back to how the file has them to allow for simple assertions
// Note that this makes the asserts stronger but a little brittle
// Pro: we assert the exact code content is rendered as expected
// Con: if EC ever changes how it renders, these tests may break even though nothing is actually wrong with the app
function decodeExpressiveCodeNewlines(value: string) {
	return value.replaceAll('\u007f', '\n');
}
 
// Expressive Code removes trailing whitespace and replaces \r\n with \nWe can't get that information back, because we can't know what the original line endings or whitespace counts were.
// So, normalize the line endings and whitespace to look like how EC renders it to make sure our
// assertions are accurate
function readCodeFileNormalizedLikeExpressiveCode(fileName: string) {
	return readFileSync(`${FIXTURE_DIRECTORY}/${fileName}`, 'utf8').replaceAll('\r\n', '\n').trimEnd();
}
