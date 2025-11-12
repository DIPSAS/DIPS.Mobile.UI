---
applyTo: '**'
---
# Copilot Instructions

## When I say "Create PR" or "Create Pull Request"
Generate a pull request description following these rules:

- **Title:** Short and imperative.
- **Tone:** Clear, professional, concise.
- **Avoid:** Repetition, unnecessary context

If you can infer a problem that was solved, first state what the problem was, and how it was fixed.

Mention differences between the platforms, if there are any.

If the user says “Create PR” or similar (“Make a PR”, “Generate PR description”), 
you should:
1. Check the difference between the main branch and the current branch.
2. Produce a properly formatted PR title and body as described above.

Make sure to always bump the CHANGELOG.md, you yourself can figure out if major, minor or patch should be bumped. Follow the same conventions used in CHANGELOG.md.

If you feel like this should be documented in the project's wiki https://github.com/DIPSAS/DIPS.Mobile.UI/wiki, then document it. Links between pages are encouraged. This should probably also be mentioned in the PR.
