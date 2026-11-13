It's still worth doing, but mainly because of how you've framed it. Here's my honest take.

Where AI has made it less necessary: reference-style content. "How do I write an xUnit theory?" is answered instantly by any assistant, so pages that mirror the docs would have little value. Search traffic for basic how-to questions is also falling as people ask an assistant instead of searching.

Where it's still valuable: your Site Design Philosophy aims at what AI does worst.

- Opinions and judgement. AI hedges and lists options; you say what to use and why.
- Keeping current. Models lag behind changes like Fluent Assertions' licence change (and the Awesome Assertions fork), Moq's SponsorLink controversy, and xUnit v3. A curated site can be right where the models are out of date.
- Trust. AI makes up APIs. Code from a known expert carries weight.
- Being a source for AI. Assistants increasingly cite pages like these. Good, opinionated pages get repeated to far more people than visit directly.

What I'd reconsider:

- The F# and VB versions. They triple the work on every sample, AI can translate on demand, and the VB audience is small. F# probably justifies itself more than VB does.
- Making the samples compile. If the samples were real projects that build in CI, "these examples actually work" would set you apart from AI output. This would need ellipses handled differently.
- An  llms.txt . A Starlight plugin for this exists, and it would make the site easy for AI tools to use.

In short: as a reference manual it would be redundant, but as an opinionated, up-to-date guide built on experience, it fills a gap AI has created rather than closed.