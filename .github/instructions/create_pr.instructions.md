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
3. **Generate PR**: Create pull request with title and body

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
If the change introduces new patterns or significant features, mention in the PR:
> "This should be documented in the [wiki](https://github.com/DIPSAS/DIPS.Mobile.UI/wiki). Consider adding a page about [topic]."

### Tone and Style
- **Clear**: Easy to understand what changed
- **Professional**: No informal language
- **Concise**: Avoid unnecessary context or repetition
- **Specific**: Reference actual files, components, and patterns
