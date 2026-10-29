import { experimental_AstroContainer as AstroContainer } from 'astro/container';
import { readdirSync, readFileSync } from 'node:fs';
import { beforeAll, expect, test } from 'vitest';
import CodeTabs from '@components/CodeTabs.astro';

const PAGE_PATH = '/fake-test-page/';
const FIXTURE_DIRECTORY = 'src/test/fixtures/code-tabs';

let container: AstroContainer;

beforeAll(async () => {
	container = await AstroContainer.create();
});

test('renders a C#, F# and Visual Basic tab with the code from each file', async () => {
	const tabs = getTabs(await renderCodeTabs('code/AllLanguages'));

	expect(tabs).toEqual([
		{ label: 'C#', language: 'csharp', code: readFixture('AllLanguages.cs') },
		{ label: 'F#', language: 'fsharp', code: readFixture('AllLanguages.fs') },
		{ label: 'Visual Basic', language: 'vb', code: readFixture('AllLanguages.vb') },
	]);
});

test('finds the files when the page URL has no trailing slash', async () => {
	const tabs = getTabs(await renderCodeTabs('code/AllLanguages', '/fake-test-page'));
	expect(tabs.map((tab) => tab.label)).toEqual(['C#', 'F#', 'Visual Basic']);
});

test('only renders tabs for the languages that have a file', async () => {
	const tabs = getTabs(await renderCodeTabs('code/MissingFSharp'));

	expect(tabs).toEqual([
		{ label: 'C#', language: 'csharp', code: readFixture('MissingFSharp.cs') },
		{ label: 'Visual Basic', language: 'vb', code: readFixture('MissingFSharp.vb') },
	]);
});

test('throws when no code files exist for the base file path', async () => {
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

function decodeAttribute(value: string) {
	return value
		.replaceAll('\u007f', '\n')
		.replace(/&#x([0-9a-f]+);/gi, (_, hex) => String.fromCodePoint(parseInt(hex, 16)))
		.replaceAll('&quot;', '"')
		.replaceAll('&lt;', '<')
		.replaceAll('&gt;', '>')
		.replaceAll('&amp;', '&');
}

function getTabs(html: string) {
	const labels = [...html.matchAll(/role="tab" href="#tab-panel-[^"]+"[^>]*>([^<]+)</g)].map((match) => match[1].trim());
	const panels = html.split(/id="tab-panel-[^"]+"[^>]*role="tabpanel"/).slice(1);

	return labels.map((label, index) => ({
		label,
		language: panels[index].match(/data-language="([^"]+)"/)?.[1],
		code: decodeAttribute(panels[index].match(/data-code="([^"]*)"/)?.[1] ?? ''),
	}));
}

function readFixture(fileName: string) {
	return readFileSync(`${FIXTURE_DIRECTORY}/${fileName}`, 'utf8').replaceAll('\r\n', '\n').trimEnd();
}
