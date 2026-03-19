---
applyTo: '**'
---
# Pull Request Creation Instructions

## Trigger Phrases
When I say **any** of these:
- "Create PR"
- "Make PR" 
- "PR"
- "Create Pull Request"
- "Make a PR"
- "Generate PR"
- "Generate PR description"

## Action Steps
1. **Check changes**: Get the diff between main branch and current branch
2. **Update CHANGELOG.md**: Determine if Major/Minor/Patch bump is needed based on:
   - **Major**: Breaking changes (removed/changed public APIs)
   - **Minor**: New features (new components, properties, methods)
   - **Patch**: Bug fixes, internal improvements, accessibility fixes
   - Follow existing CHANGELOG.md format: `[Component/Feature] Description`
3. **Wiki documentation**: Check if documentation in `wiki/` needs to be added or updated:
   - **New feature?** → **MUST** add a new `wiki/<FeatureName>.md` page
   - **Changed behavior/API?** → **MUST** update the relevant existing wiki page
   - **Breaking change?** → **MUST** update wiki to reflect the new behavior
   - The sidebar is auto-generated — just add the `.md` file to `wiki/`
4. **Generate PR**: Create pull request with title and body

## PR Format Requirements

### Title
- Short and imperative (e.g., "Add accessibility support to Touch effect")
- No "Fixed" or past tense - use present tense
- Mention component/area if applicable

### Body Structure
```markdown
### Description of Change
[Clear explanation of what changed and why]

### Problem (if applicable)
[What issue was being solved]

### Solution
[How it was fixed]

### Platform Differences
[Mention iOS vs Android differences if any]

### Breaking Changes (if applicable)
- List any breaking changes
- Provide migration guide

### Files Changed
- Brief overview of major file changes

## Additional Considerations

### Wiki Documentation
Documentation lives in the `wiki/` folder and is auto-synced to the GitHub wiki on merge to main.
- **New features**: A wiki page in `wiki/` is **required** — not optional. Add it in the PR.
- **Changed behavior**: Update the relevant `wiki/*.md` page in the same PR.
- **Breaking changes**: Wiki must reflect the new API/behavior.
- The `_Sidebar.md` is auto-generated from filenames — no manual sidebar edits needed.

### Tone and Style
- **Clear**: Easy to understand what changed
- **Professional**: No informal language
- **Concise**: Avoid unnecessary context or repetition
- **Specific**: Reference actual files, components, and patterns
