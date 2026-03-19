---
name: Write Component Documentation
description: Generates wiki documentation for new DIPS.Mobile.UI components following established patterns and conventions.
---

# Write Component Documentation

When asked to write documentation for a new component, generate a `wiki/<ComponentName>.md` page following the template and rules below.

## Trigger Phrases

- "Document this component"
- "Write wiki page"
- "Add documentation"
- "Create wiki doc"

## Before Writing

1. **Read the component source code** — understand all public properties, bindable properties, events, commands, and options classes
2. **Check for sub-components or variants** — e.g., `NavigationListItem` alongside `ListItem`, `FilledCheckBox` alongside `CheckBox`
3. **Check for platform differences** — look at iOS/Android partial classes for behavioral differences
4. **Check accessibility support** — does the component set semantic properties, use `TouchPlatformEffect`, or have accessibility-specific features?
5. **Look at the sample app** — check `ComponentsSamples/` or `AccessibilitySamples/` for usage examples

## Template

Use the following structure. Sections marked **(if applicable)** should only be included when relevant.

```markdown
<Short description of what the component is — 1-2 sentences. Written before any heading. Explain the purpose and when to use it.>

# Inspiration **(if applicable)**

<Links to design guidelines that inspired the component, e.g., Material Design, Apple Human Interface Guidelines.>

# Usage

<XAML example showing the most common/basic usage of the component.>

> <Any important notes about defaults, required properties, or common gotchas.>

# Styles **(if applicable)**

<List available styles/variants with a brief description of each.>

# Tips and tricks **(if applicable)**

<Practical guidance: recommended patterns, common combinations with other components, or things to watch out for.>

# Accessibility **(if applicable)**

<How the component behaves with VoiceOver/TalkBack. Any required semantic properties. Special accessibility features.>

# Properties

Inspect the [components properties class](<GitHub URL to .Properties.cs file>) to further customise and use it.
```

### Sub-Component Pattern

When the component has variants or related sub-components, add them as `##` sections after the main component, each with their own `### Usage` and `### Properties`:

```markdown
## SubComponentName

<Brief description of the sub-component.>

### Usage

<XAML example>

### Properties

Inspect the [components properties class](<GitHub URL>) to further customise and use it.
```

## Writing Rules

### Intro
- Start with a plain-text paragraph **before any heading** — describe what the component is and when people would use it
- Keep it to 1-2 sentences

### Usage Sections
- Always include at least one XAML code block with `xml` as the language tag
- Use the `dui:` XML namespace prefix — do not show `xmlns` declarations (readers already know this)
- Show the simplest working example first, then more advanced configurations
- If C# code-behind is needed (e.g., ViewModel bindings, event handlers), include it after the XAML block using `csharp` as the language tag

### Properties
- Always link to the `.Properties.cs` file on GitHub: `https://github.com/DIPSAS/DIPS.Mobile.UI/blob/main/src/library/DIPS.Mobile.UI/<path>/<Component>.Properties.cs`
- Use the standard phrasing: *"Inspect the [components properties class](URL) to further customise and use it."*
- This should be the **last section** (or last sub-section per component)

### Callouts
- Use blockquotes (`>`) for notes, warnings, and platform-specific differences
- Bold the callout type: `> **NB!**`, `> **NOTE:**`, `> **Important:**`
- Use callouts for: default values, platform differences (iOS vs Android), UX warnings, required configurations

### Style
- Use `#` (h1) for major sections
- Use `##` (h2) for sub-components
- Use `###` (h3) for sections within a sub-component
- Be concise — developers scan documentation, not read it cover-to-cover
- Write in present tense: *"The component displays..."* not *"The component will display..."*
- Address the reader as *"you"*: *"Use this when you need..."*
- Refer to end-users as *"people"*: *"People can tap to..."*

### What NOT to Include
- No namespace/xmlns declarations (unless the component requires a non-standard namespace)
- No installation or NuGet instructions
- No `_Sidebar.md` references — it is auto-generated
- No Figma links (designers maintain those separately)
- Do not document internal/private APIs — only public-facing properties and usage
