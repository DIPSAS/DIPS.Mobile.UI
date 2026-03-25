---
name: Create PR
description: Creates a pull request with proper title, description, changelog updates, and wiki documentation checks.
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
2. **Update CHANGELOG.md** (only if `src/` has changes): Determine if Major/Minor/Patch bump is needed based on:
   - **Major**: Breaking changes (removed/changed public APIs)
   - **Minor**: New features (new components, properties, methods)
   - **Patch**: Bug fixes, internal improvements, accessibility fixes
   - Follow existing CHANGELOG.md format: `[Component/Feature] Description`
   - **Skip** if the PR has no changes under `src/` — documentation-only, build scripts, CI/CD, and config changes do not require a changelog entry
3. **Wiki documentation**: Check if documentation in `wiki/` needs to be added or updated:
   - **New feature?** → **MUST** add a new `wiki/<FeatureName>.md` page
   - **Changed behavior/API?** → **MUST** update the relevant existing wiki page
   - **Breaking change?** → **MUST** update wiki to reflect the new behavior
   - The sidebar is auto-generated — just add the `.md` file to `wiki/`
4. **Generate PR**: Create pull request with title and body, **always** using the PR template from `.github/pull_request_template.md` as the body structure. Read the template file first and fill in each section — never invent a different structure.

## PR Format Requirements

### Title
- Short and imperative (e.g., "Add accessibility support to Touch effect")
- No "Fixed" or past tense - use present tense
- Mention component/area if applicable

### Body
**Always** use `.github/pull_request_template.md` as the base. Read it before writing the body. Fill in every section — do not skip or reorder sections, do not add sections not in the template.

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
