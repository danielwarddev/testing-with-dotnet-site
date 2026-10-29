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
			customCss: ['./src/styles/global.css'],
		}),
	],
	vite: {
		plugins: [tailwindcss()],
	},
});
