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
	const tabs = getTabs(await renderCodeTabs('code/AllLanguages'));

	expect(tabs).toEqual([
		{ label: 'C#', language: 'csharp', code: readCodeFileAsRendered('AllLanguages.cs') },
		{ label: 'F#', language: 'fsharp', code: readCodeFileAsRendered('AllLanguages.fs') },
		{ label: 'Visual Basic', language: 'vb', code: readCodeFileAsRendered('AllLanguages.vb') },
	]);
});

it('should find the files when the page URL has no trailing slash', async () => {
	const tabs = getTabs(await renderCodeTabs('code/AllLanguages', '/fake-test-page'));
	expect(tabs.map((tab) => tab.label)).toEqual(['C#', 'F#', 'Visual Basic']);
});

it('should only render tabs for the languages that have a file', async () => {
	const tabs = getTabs(await renderCodeTabs('code/MissingFSharp'));

	expect(tabs).toEqual([
		{ label: 'C#', language: 'csharp', code: readCodeFileAsRendered('MissingFSharp.cs') },
		{ label: 'Visual Basic', language: 'vb', code: readCodeFileAsRendered('MissingFSharp.vb') },
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

function getTabs(html: string) {
	const document = new Window().document as unknown as Document;
	document.body.innerHTML = html;

	const body = within(document.body);
	const labels = body.getAllByRole('tab').map((tab) => tab.textContent.trim());
	const panels = body.getAllByRole('tabpanel', { hidden: true });

	return labels.map((label, index) => ({
		label,
		language: panels[index].querySelector('[data-language]')?.getAttribute('data-language'),
		code: decodeExpressiveCodeText(panels[index].querySelector('[data-code]')?.getAttribute('data-code') ?? ''),
	}));
}

// Starlight uses Expressive Code to render nice-looking code blocks
// We assert against the actual rendered HTML to make sure it matches the code files' contents,
// so we need to undo its encoding to make that assertion, which is what this method is for
// Note that this makes the asserts strong but brittle
// Pro: we assert the exact code content is rendered as expected
// Con: if EC ever changes how it renders, these tests may break even though nothing is actually wrong with the app
function decodeExpressiveCodeText(value: string) {
	return value.replaceAll('\u007f', '\n');
}
 
// Similar to decodeExpressiveCodeText, this is needed because of Expressive Code
// EC removes trailing whitespace and replaces \r\n with \n. We can't get that information back,
// because we can't know what the original line endings or whitespace counts were.
// So, just normalize the line endings and whitespace to look like how EC does it
// This lets us assert against it successfully
function readCodeFileAsRendered(fileName: string) {
	return readFileSync(`${FIXTURE_DIRECTORY}/${fileName}`, 'utf8').replaceAll('\r\n', '\n').trimEnd();
}
