---
name: Review PR
description: Workflow and rules for AI-assisted pull request reviews.
---

# Code Review Instructions

## CRITICAL RULES

### NEVER Approve or Request Changes

**AI agents must NEVER use `--approve` or `--request-changes` flags.**

| Action | Allowed? | Why |
|--------|----------|-----|
| `gh pr review --approve` | **NEVER** | Approval is a human decision |
| `gh pr review --request-changes` | **NEVER** | Blocking PRs is a human decision |

---

## When I say "Review" or "Review my changes"
Perform a comprehensive code review of the current changes following these rules:

### Review Process
1. Check the difference between the main branch and the current branch
2. Analyze all modified files for adherence to project patterns
3. Provide structured feedback with specific examples

### Post PR Overview Comment

When reviewing a PR, add a comment that gives the reviewer a clear understanding of the PR:

1. **Pull request overview** — A short summary based on the title, description, and changed files. Explain *what* the PR does and *why*.

2. **File summary** — A collapsible table listing each changed file with a brief description of the change. Use a `<details>` block so it can be minimized:

   ```markdown
   ### Pull request overview

   <one or two paragraphs summarizing the purpose and approach of the PR>

   ### Changes

   - <bullet per logical change group>

   <details>
   <summary>📁 File summary (<N> files changed)</summary>

   | File | Description |
   |------|-------------|
   | `path/to/file.cs` | Brief description of what changed |
   | `path/to/other.cs` | Brief description of what changed |

   </details>
   ```

### Review Criteria

#### 1. Platform-Specific Implementation
- ✅ **Check**: Platform code uses partial classes in `iOS/`, `Android/`, `dotnet/` folders
- ✅ **Check**: Shared namespace (no `.iOS` or `.Android` suffix)
- ✅ **Check**: Platform stubs exist for `net10.0` target in `dotnet/` folder

#### 2. Design Resources Compliance
- ❌ **Flag**: Hardcoded colors (e.g., `Color.FromRgb()`, `Colors.Red`)
- ❌ **Flag**: Hardcoded sizes (e.g., `new Thickness(16)`, `FontSize = 14`)
- ❌ **Flag**: Hardcoded icon paths or image sources
- ✅ **Check**: Uses `Colors.GetColor(ColorName.*)`, `Sizes.GetSize(SizeName.*)`, `Icons.GetIcon(IconName.*)`

#### 3. Accessibility Standards
- ❌ **Flag**: Touch/tap behavior without `SemanticProperties.Description`
- ❌ **Flag**: Manual "Button" suffix in accessibility strings
- ✅ **Check**: `OnAccessibilityDescriptionSet()` called when description exists
- ✅ **Check**: Both iOS (`UIAccessibilityTrait.Button`) and Android (`AccessibilityDelegate`) implementations
- ❌ **Flag**: Interactive elements without semantic descriptions

#### 4. MVVM Pattern Adherence
- ❌ **Flag**: Direct property assignments without raising property changed
- ❌ **Flag**: Synchronous commands wrapping async operations

#### 5. Memory Management
- ✅ **Check**: Event handlers unsubscribed in `OnDetached()` or handler disposal
- ✅ **Check**: Weak event pattern for service subscriptions
- ❌ **Flag**: Strong references to views or pages in long-lived services
- ❌ **Flag**: Missing disposal of platform-specific resources

#### 6. CHANGELOG.md Updates
- ❌ **Flag**: Missing CHANGELOG.md entry
- ✅ **Check**: Correct semantic versioning (Major/Minor/Patch)
- ✅ **Check**: Format follows: `[Component/Feature] Description`
- ✅ **Check**: Platform-specific differences mentioned if applicable

#### 7. Cross-Platform Consistency
- ✅ **Check**: Feature works on both iOS and Android
- ✅ **Check**: Platform-specific behavior documented
- ❌ **Flag**: iOS-only or Android-only implementation without documented reason
- ✅ **Check**: Visual consistency with design system

#### 8. Wiki Documentation (wiki/)
- ❌ **Flag**: New feature (component, effect, handler, service) without a corresponding wiki page in `wiki/`
- ❌ **Flag**: Code changes that break or contradict existing wiki documentation (renamed APIs, changed behavior, removed features)
- ✅ **Check**: If existing documented behavior changed, the relevant `wiki/*.md` page is updated in the same PR
- ✅ **Check**: New wiki pages follow naming convention: `PascalCase.md` or `Hyphenated-Words.md`
- ✅ **Check**: New wiki pages will appear in the auto-generated sidebar (no manual `_Sidebar.md` edits needed)
- **Guidance**: Review all modified/added files and cross-reference with pages in `wiki/` to determine if documentation is affected


### Common Review Patterns

**For Accessibility Changes:**
- Verify VoiceOver/TalkBack announcement text
- Check semantic focus order
- Ensure screen reader can navigate all interactive elements

**For Component Changes:**
- Verify component registered in samples app
- Check Options pattern implementation
- Validate design token usage

**For Effect/Handler Changes:**
- Verify platform-specific implementations exist
- Check proper disposal/cleanup
- Validate event handler subscriptions

**For Breaking Changes:**
- Flag as **Major** version bump
- Require migration guide in PR description
- Document deprecated APIs

**For Documentation (wiki/):**
- Check if any changed code contradicts existing wiki pages
- Verify new features have a corresponding `wiki/<FeatureName>.md` page
- If behavior or API changed, verify the relevant wiki page is updated
- Praise when documentation is proactively added or updated

But use your judgment to adapt the review based on the specific changes made. You are encouraged to provide constructive feedback that helps maintain code quality and consistency across the project. Be a pro reviewer!
