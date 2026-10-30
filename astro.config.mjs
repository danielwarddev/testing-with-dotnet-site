// @ts-check
import { defineConfig } from 'astro/config';
import starlight from '@astrojs/starlight';
import tailwindcss from '@tailwindcss/vite';
import { satteri } from '@astrojs/markdown-satteri';
import { externalLinks } from './src/plugins/external-links.mjs';

// https://astro.build/config
export default defineConfig({
	markdown: {
		processor: satteri({
			hastPlugins: [externalLinks],
		}),
	},
	integrations: [
		starlight({
			title: 'Testing with .NET',
			logo: {
				light: './src/assets/logo-light.svg',
				dark: './src/assets/logo-dark.svg',
			},
			social: [
				{ icon: 'github', label: 'GitHub', href: 'https://github.com/danielwarddev/testing-with-dotnet-site' },
			],
			sidebar: [
				{
					label: 'Frameworks',
					items: [
						{ label: 'xUnit', slug: 'frameworks/xunit' },
						{ label: 'NUnit', slug: 'frameworks/nunit' },
						{ label: 'TUnit', slug: 'frameworks/tunit' },
						{ label: 'MSTest', slug: 'frameworks/mstest' },
					],
				},
				{
					label: 'Concepts',
					items: [
						{ label: 'Mocking', slug: 'concepts/mocking' },
						{ label: 'Writing testable code', slug: 'concepts/writing-testable-code' },
						// { label: 'Assertions', slug: 'concepts/assertions' },
						// { label: 'Test Patterns', slug: 'concepts/test-patterns' },
						// { label: 'Test-Driven Development', slug: 'concepts/tdd' },
						// { label: 'Behavior-Driven Development', slug: 'concepts/bdd' },
					],
				},
				{
					label: 'How do I ...?',
					items: [{ label: 'Mocking HttpClient', slug: 'how-do-i/mocking-httpclient' }],
				},
				{
					label: 'Libraries',
					items: [
						{ label: 'AutoFixture', slug: 'libraries/autofixture' },
						{ label: 'TestContainers', slug: 'libraries/testcontainers' },
					],
				},
				{
					label: 'Assertion libraries',
					items: [
						{ label: 'Built-in assertions', slug: 'assertion-libraries/built-in' },
						{ label: 'Awesome Assertions', slug: 'assertion-libraries/awesome-assertions' },
						{ label: 'Fluent Assertions', slug: 'assertion-libraries/fluent-assertions' },
						{ label: 'Shouldly', slug: 'assertion-libraries/shouldly' },
					],
				},
				{ label: 'Getting started', slug: 'getting-started' },
				{
					label: 'Naming tests',
					items: [{ autogenerate: { directory: 'naming' } }],
				},
			],
			customCss: ['./src/styles/global.css'],
		}),
	],
	vite: {
		plugins: [tailwindcss()],
	},
});
