---
applyTo: '**'
---
# Code Review Instructions

## When I say "Review" or "Review my changes"
Perform a comprehensive code review of the current changes following these rules:

### Review Process
1. Check the difference between the main branch and the current branch
2. Analyze all modified files for adherence to project patterns
3. Provide structured feedback with specific examples

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

### Review Output Format

Provide feedback in this structure:

```markdown
## Code Review Summary

### ✅ Strengths
- [List positive aspects]

### ⚠️ Issues Found
**Priority: High/Medium/Low**
1. [Issue description]
   - **File**: `path/to/file.cs`
   - **Line**: XX
   - **Problem**: [What's wrong]
   - **Fix**: [Suggested solution with code example]

### 🔍 Questions/Clarifications
- [Any unclear design decisions that need explanation]

### ✅ Approval Status
- [ ] Ready to merge
- [ ] Requires changes
- [ ] Needs discussion
```

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

### Things to Praise
- Proper use of partial classes
- Comprehensive accessibility support
- Clear separation of concerns
- Good test coverage in samples app
- Well-documented platform differences


But use your judgment to adapt the review based on the specific changes made. You are encouraged to provide constructive feedback that helps maintain code quality and consistency across the project. Be a pro reviewer!