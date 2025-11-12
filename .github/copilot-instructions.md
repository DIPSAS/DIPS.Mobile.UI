# DIPS.Mobile.UI - AI Coding Agent Instructions

## Project Overview
DIPS.Mobile.UI is a .NET MAUI component library for iOS and Android mobile apps in the healthcare domain. Components follow a design system with resources (colors, sizes, icons) auto-generated from Figma via DIPS.Mobile.DesignTokens pipeline.

**Key Architecture Pattern**: Multi-target library (`net9.0`, `net9.0-ios`, `net9.0-android`) using partial classes for platform-specific implementations.

## Critical Patterns

### 1. Platform-Specific Code Structure
Use partial classes to separate shared and platform logic:
```
Effects/Touch/
├── Touch.cs                    # Shared API
├── TouchPlatformEffect.cs      # Shared effect base
├── iOS/TouchPlatformEffect.cs  # iOS implementation
├── Android/TouchPlatformEffect.cs  # Android implementation
└── dotnet/TouchPlatformEffect.cs   # Stub for net9.0
```

**Pattern**: Declare `partial void Init()` in shared file, implement in platform files. Never use `#if` preprocessor directives if not necessary.

### 2. Component Architecture
Components extend MAUI controls with custom handlers and platform effects:
- **Components**: UI elements in `Components/` (Button, ListItem, NavigationListItem)
- **Effects**: Cross-cutting behaviors via `RoutingEffect` or `Behaviour` (Touch, Layout, Accessibility)
- **Handlers**: Platform-specific renderers when MAUI handlers need customization

Example: `ListItem` is a `Grid` with configurable Options (TitleOptions, IconOptions, etc.) that bind to internal child views.

### 3. Design Resources Usage
Always use generated resource APIs (ColorResources, IconResources, SizeResources), never hardcode values:
```csharp
// ✅ Correct
Colors.GetColor(ColorName.color_surface_default)
Sizes.GetSize(SizeName.content_margin_small)
Icons.GetIcon(IconName.arrow_right_s_line)

// ❌ Wrong
Color.FromRgb(255, 0, 0)
new Thickness(16)
```

Resources auto-update from design tokens - direct values break design system consistency.

### 4. Accessibility Requirements
When adding touch/tap behavior, **always** require `SemanticProperties.Description`:
```csharp
// TouchPlatformEffect pattern
if (!string.IsNullOrEmpty(SemanticProperties.GetDescription(Element)))
{
    OnAccessibilityDescriptionSet(); // Sets UIAccessibilityTrait.Button or Android delegate
}
```

Screen readers (VoiceOver/TalkBack) announce "Button" automatically when description is set.

### 5. MVVM Pattern
Components use MVVM with `ViewModel` base class:
- Commands: Use `AsyncCommand` or `AsyncCommand<T>` for async operations
- Property changes: Call `RaisePropertyChanged()` or use `RaiseWhenSet()`, using base ViewModel class
- Weak event pattern: Service subscriptions should use weak references

## Development Workflow

### Building
```bash
# Build for specific platform
dotnet build src/library/DIPS.Mobile.UI/DIPS.Mobile.UI.csproj -f net9.0-ios
dotnet build src/library/DIPS.Mobile.UI/DIPS.Mobile.UI.csproj -f net9.0-android

# Test app (Playground)
dotnet build src/app/Playground/Playground.csproj -f net9.0-ios
```

### Testing Components
1. Add sample to `src/app/Components/ComponentsSamples/`
2. Register in `REGISTER_YOUR_SAMPLES_HERE.cs`
3. Test live in distributed Components app

### Creating PRs
**Always** update `CHANGELOG.md` following semantic versioning:
- Major: Breaking changes
- Minor: New features (new components, properties)
- Patch: Bug fixes, internal improvements

Format: `[Component/Feature] Description` (see existing entries for style)

## Common Pitfalls

1. **Don't** manually append "Button" to accessibility strings - `TouchPlatformEffect` handles this
2. **Don't** set Touch without `SemanticProperties.Description` - accessibility will fail
3. **Don't** use `#if ANDROID` / `#if IOS` - use partial classes in platform folders
4. **Don't** create new colors/sizes - use design token resources or request from designers
5. **Don't** forget to test on both platforms - behavior often differs

## Key Files to Reference
- `API/Builder/AppHostBuilderExtensions.cs` - Library initialization and handler registration
- `Components/ListItems/ListItem.cs` - Example of Options pattern and property binding
- `Effects/Touch/` - Complete example of cross-platform effect pattern
- `.github/instructions/create_pr.instructions.md` - PR generation rules

## Namespace Convention
All code uses `DIPS.Mobile.UI.*` namespace. Platform folders (iOS/Android/dotnet) use same namespace as parent - no `.iOS` suffix.
