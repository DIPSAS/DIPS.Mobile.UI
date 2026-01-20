# DIPS.Mobile.UI - AI Coding Agent Instructions

## Important Meta-Instruction
**When you make an error and the user corrects you with specific guidance, patterns, or rules, you MUST update this instruction file with that information before proceeding.** This ensures:
- Future sessions benefit from learned patterns
- Common mistakes are not repeated
- Project-specific conventions are documented
- The instruction file evolves with the project

Add corrections to the relevant section (Critical Patterns, Common Pitfalls, etc.) or create a new section if needed.

## Project Overview
DIPS.Mobile.UI is a .NET MAUI component library for iOS and Android mobile apps in the healthcare domain. Components follow a design system with resources (colors, sizes, icons) auto-generated from Figma via DIPS.Mobile.DesignTokens pipeline.

**Key Architecture Pattern**: Multi-target library (`net10.0`, `net10.0-ios`, `net10.0-android`) using partial classes for platform-specific implementations.

## Critical Patterns

### 1. Platform-Specific Code Structure
Use partial classes to separate shared and platform logic:
```
Effects/Touch/
├── Touch.cs                    # Shared API
├── TouchPlatformEffect.cs      # Shared effect base
├── iOS/TouchPlatformEffect.cs  # iOS implementation
├── Android/TouchPlatformEffect.cs  # Android implementation
└── dotnet/TouchPlatformEffect.cs   # Stub for net10.0
```

**Pattern**: Declare `partial void Init()` in shared file, implement in platform files. Never use `#if` preprocessor directives if not necessary.

### 2. Component Architecture
Components extend MAUI controls with custom handlers and platform effects:
- **Components**: UI elements in `Components/` (Button, ListItem, NavigationListItem)
- **Effects**: Cross-cutting behaviors via `RoutingEffect` or `Behaviour` (Touch, Layout, Accessibility)
- **Handlers**: Platform-specific renderers when MAUI handlers need customization

Example: `ListItem` is a `Grid` with configurable Options (TitleOptions, IconOptions, etc.) that bind to internal child views.

**BindableProperty Pattern**: Use inline lambda expressions in `propertyChanged` callback:
```csharp
// ✅ Correct pattern used in ListItem.Properties.cs
public static readonly BindableProperty TitleProperty = BindableProperty.Create(
    nameof(Title),
    typeof(string),
    typeof(ListItem),
    propertyChanged: (bindable, _, _) => ((ListItem)bindable).AddTitle());

// ❌ Wrong - don't create separate callback methods
propertyChanged: OnTitleChanged

private static void OnTitleChanged(BindableObject bindable, object oldValue, object newValue)
{
    ((ListItem)bindable).AddTitle();
}
```

### 3. Design Resources Usage
Always use generated resource APIs (ColorResources, IconResources, SizeResources), never hardcode values:

**CRITICAL**: Before using any color, size, or icon name, **always verify it exists** by checking the generated resource files:
- Colors: `src/library/DIPS.Mobile.UI/Resources/Colors/ColorName.cs`
- Sizes: `src/library/DIPS.Mobile.UI/Resources/Sizes/SizeName.cs`
- Icons: `src/library/DIPS.Mobile.UI/Resources/Icons/IconName.cs`

```csharp
// ✅ Correct - verified names from ColorName.cs and IconName.cs
Colors.GetColor(ColorName.color_surface_default)
Colors.GetColor(ColorName.color_surface_default_selected)  // NOT color_surface_selected!
Sizes.GetSize(SizeName.content_margin_small)
Icons.GetIcon(IconName.check_circle_line)  // NOT checkbox_circle_line!
Icons.GetIcon(IconName.radio_unchecked_line)  // NOT checkbox_blank_circle_line!

// ❌ Wrong - hardcoded values
Color.FromRgb(255, 0, 0)
new Thickness(16)

// ❌ Wrong - using non-existent resource names
Colors.GetColor(ColorName.color_surface_selected)  // Does not exist!
Icons.GetIcon(IconName.checkbox_circle_line)  // Does not exist! Use check_circle_line
Icons.GetIcon(IconName.checkbox_blank_circle_line)  // Does not exist! Use radio_unchecked_line
```

**Icon Syntax in XAML**:
```xaml
<!-- ✅ Correct - verified icon names from IconName.cs -->
<dui:Image Source="{dui:Icons check_circle_line}" />
<dui:Image Source="{dui:Icons radio_unchecked_line}" />

<!-- ❌ Wrong - incorrect syntax -->
<dui:Image Source="{dui:Icon Icon=check_circle_line}" />

<!-- ❌ Wrong - non-existent icon names -->
<dui:Image Source="{dui:Icons checkbox_circle_line}" />
<dui:Image Source="{dui:Icons checkbox_blank_circle_line}" />
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

**Excluding elements from accessibility tree**: Use `AutomationProperties.SetExcludedWithChildren()` to exclude elements and their children from the accessibility tree. This is cleaner than using `SetIsInAccessibleTree()` on individual elements.

```csharp
// ✅ Correct - excludes container and all children
AutomationProperties.SetExcludedWithChildren(containerElement, true);

// ❌ Wrong - requires setting on each individual child
AutomationProperties.SetIsInAccessibleTree(titleLabel, false);
AutomationProperties.SetIsInAccessibleTree(subtitleLabel, false);
AutomationProperties.SetIsInAccessibleTree(iconImage, false);
```

### 5. MVVM Pattern
Components use MVVM with `ViewModel` base class:
- Commands: Use `AsyncCommand` or `AsyncCommand<T>` for async operations
- Property changes: Call `RaisePropertyChanged()` or use `RaiseWhenSet()`, using base ViewModel class
- Weak event pattern: Service subscriptions should use weak references

## Development Workflow

### Building

#### Local Development Builds
```bash
# Build for specific platform
dotnet build src/library/DIPS.Mobile.UI/DIPS.Mobile.UI.csproj -f net10.0-ios
dotnet build src/library/DIPS.Mobile.UI/DIPS.Mobile.UI.csproj -f net10.0-android

# Test app (Playground)
dotnet build src/app/Playground/Playground.csproj -f net10.0-ios
```

#### Pipeline Build System
The project uses a C# script-based build system (`build/build.csx`) powered by **dotnet-script**:

```bash
# Run build tasks using buildwindow.sh wrapper
./buildwindow.sh [task] [options]

# Common tasks:
./buildwindow.sh init              # Initialize solution (restore packages)
./buildwindow.sh clean             # Clean solution and remove bin/obj
./buildwindow.sh buildAndroid      # Build Android app
./buildwindow.sh buildiOS          # Build iOS app
./buildwindow.sh validateChangelog # Validate CHANGELOG.md has been updated
./buildwindow.sh packageLibrary    # Create NuGet package
```

**Key Build System Files**:
- `build/build.csx` - Main build script with task definitions (init, clean, build, package, deploy)
- `build/AwesomeBuildsystem/` - Shared build utilities for MAUI/iOS/Android
- `buildwindow.sh` - Wrapper script that bootstraps and runs build.csx
- Tasks handle: changelog validation, versioning, signing, AppCenter distribution, NuGet publishing

**Pipeline Usage**: Azure DevOps pipelines call `buildwindow.sh` with specific tasks to automate builds, validation, and deployments.

### Testing Components
1. Add sample to `src/app/Components/ComponentsSamples/` or `src/app/Components/AccessibilitySamples/` for accessibility features
2. Register in `REGISTER_YOUR_SAMPLES_HERE.cs`
3. Test live in distributed Components app

### Sample File Organization
When creating samples with multiple related files (XAML, code-behind, ViewModel):
- **Create a folder** for the sample (e.g., `ListItemInteractiveContentSamples/`)
- **Place all files** in the folder: `.xaml`, `.xaml.cs`, `ViewModel.cs`
- **Create dedicated ViewModel** class inheriting from `DIPS.Mobile.UI.MVVM.ViewModel`
- **Set BindingContext in XAML** using the ViewModel class directly in `ContentPage.BindingContext`
- **Always use `x:DataType`** on the ContentPage for compiled bindings
- **Never use** generic `<dui:ViewModel />` in XAML - always create a specific ViewModel class
- **Don't set BindingContext** in code-behind - declare it in XAML

Example structure:
```
AccessibilitySamples/VoiceOverSamples/
└── ListItemInteractiveContentSamples/
    ├── ListItemInteractiveContentSamples.xaml
    ├── ListItemInteractiveContentSamples.xaml.cs
    └── ListItemInteractiveContentSamplesViewModel.cs
```

Example XAML:
```xaml
<dui:ContentPage xmlns:local="clr-namespace:Components.Samples.MySample"
                 x:DataType="local:MySampleViewModel"
                 x:Class="Components.Samples.MySample.MySample">
    <dui:ContentPage.BindingContext>
        <local:MySampleViewModel />
    </dui:ContentPage.BindingContext>
    <!-- Content here with {Binding PropertyName} -->
</dui:ContentPage>
```

### Styling Guidelines
When creating samples or UI:
- **Label Styles**: Use `SectionHeader`, `UI100`, `UI200`, `UI300` (NOT `Body500` or similar)
  - **XAML Syntax**: ALWAYS use `Style="{dui:Styles Label=UI100}"` (NOT `Style="{StaticResource UI100}"`)
- **Font Families**: Only use `Body`, `UI`, or `Header` - never use `monospace`
- **LocalizedStrings**: Always use localized strings from `Components.Resources.LocalizedStrings` for user-facing text
- **Colors**: Always use design token colors via `{dui:Colors color_*}`
  - **Always verify** color names exist in `ColorName.cs` before using
- **Sizes**: Always use design token sizes via `{dui:Sizes size_*}`
- **ListItem Dividers**: Only use `HasBottomDivider="True"` when ListItems are grouped together. In a group, only the first N-1 items should have bottom dividers - the last item should NOT have a divider.

Example divider usage:
```xaml
<!-- Group of 3 ListItems - only first 2 have dividers -->
<dui:ListItem Title="Item 1" HasBottomDivider="True" />
<dui:ListItem Title="Item 2" HasBottomDivider="True" />
<dui:ListItem Title="Item 3" /> <!-- No divider on last item -->
```

### Creating PRs
**Always** update `CHANGELOG.md` following semantic versioning:
- Major: Breaking changes
- Minor: New features (new components, properties)
- Patch: Bug fixes, internal improvements

Format: `[Component/Feature] Description` (see existing entries for style)

## Common Pitfalls

1. **Don't** manually append "Button" to accessibility strings - `TouchPlatformEffect` handles this
2. **Don't** set Touch without `SemanticProperties.Description` - accessibility will fail
3. **Don't** use `#if ANDROID` / `#if IOS` - use partial classes in platform folders, if there is a lot of code
4. **Don't** create new colors/sizes - use design token resources or request from designers
5. **Don't** use invalid style names like `Body500` - use `SectionHeader`, `UI100-300`, etc.
6. **Don't** use `FontFamily="monospace"` - use `FontFamily="Body"`, `"UI"`, or `"Header"`
5. **Don't** forget to test on both platforms - behavior often differs

## Key Files to Reference
- `API/Builder/AppHostBuilderExtensions.cs` - Library initialization and handler registration
- `Components/ListItems/ListItem.cs` - Example of Options pattern and property binding
- `Effects/Touch/` - Complete example of cross-platform effect pattern
- `.github/instructions/create_pr.instructions.md` - PR generation rules

## Namespace Convention
All code uses `DIPS.Mobile.UI.*` namespace. Platform folders (iOS/Android/dotnet) use same namespace as parent - no `.iOS` suffix.

## Wiki
The codebase is documented in https://github.com/DIPSAS/DIPS.Mobile.UI/wiki