---
name: design-system-usage
description: Rules and patterns for using DIPS.Mobile.UI design tokens, styles, and Layout effect in C# code. Apply when building UI components or views.
---

# Design System Usage

When building UI elements in C# code, always use the DIPS design system instead of hardcoded values.

## Trigger Phrases

- Building a new component or view in C#
- Creating UI elements with colors, sizes, fonts, or corner radius
- Refactoring hardcoded values to design tokens
- "Use design tokens" / "Use design system"

## Rules

### 1. Colors — Never Hardcode

```csharp
using DIPS.Mobile.UI.Resources.Colors;
using Colors = DIPS.Mobile.UI.Resources.Colors.Colors;

// ✅ Correct — use semantic color tokens from ColorName enum
label.TextColor = Colors.GetColor(ColorName.color_text_default);
grid.BackgroundColor = Colors.GetColor(ColorName.color_surface_backdrop);

// ❌ Wrong — hardcoded hex or Color values
label.TextColor = Color.FromArgb("#FFFFFF");
grid.BackgroundColor = Color.FromArgb("#DD1A1A2E");
```

**Always verify** the color name exists in `src/library/DIPS.Mobile.UI/Resources/Colors/ColorName.cs` before using it.

Common color token categories:
| Purpose | Token pattern |
|---------|--------------|
| Page/card backgrounds | `color_surface_*`, `color_background_default` |
| Text | `color_text_default`, `color_text_subtle`, `color_text_on_button`, `color_text_danger` |
| Buttons | `color_fill_button`, `color_fill_button_danger`, `color_fill_success` |
| Icons | `color_icon_default`, `color_icon_danger`, `color_icon_success` |
| Borders | `color_border_default`, `color_border_subtle` |
| Overlays | `color_surface_backdrop` |
| Disabled | `color_surface_disabled`, `color_text_disabled`, `color_fill_disabled` |

### 2. Sizes — Never Hardcode Dimensions

```csharp
using DIPS.Mobile.UI.Resources.Sizes;

// ✅ Correct — use size tokens from SizeName enum
Margin = new Thickness(Sizes.GetSize(SizeName.content_margin_small));  // 8
WidthRequest = Sizes.GetSize(SizeName.size_8);  // 32
CornerRadius = (int)Sizes.GetSize(SizeName.radius_large);  // 16

// ❌ Wrong — magic numbers
Margin = new Thickness(8);
WidthRequest = 32;
CornerRadius = 16;
```

Key size token groups:
| Purpose | Tokens | Values |
|---------|--------|--------|
| Base sizes | `size_1` to `size_25` | 4px increments (4–100) |
| Small fractions | `size_half` | 2 |
| Content padding/margins | `content_margin_xsmall` to `content_margin_xlarge` | 4, 8, 12, 20, 28 |
| Page margins | `page_margin_xsmall` to `page_margin_xlarge` | 12, 16, 24, 32 |
| Corner radius | `radius_xsmall` to `radius_xlarge` | 4, 8, 12, 16, 32 |
| Strokes | `stroke_small` to `stroke_xlarge` | 0.5, 1, 2, 5 |

### 3. Label Styles — Never Set FontSize/FontFamily Manually

```csharp
using DIPS.Mobile.UI.Resources.Styles;
using DIPS.Mobile.UI.Resources.Styles.Label;

// ✅ Correct — use label styles
label.Style = Styles.GetLabelStyle(LabelStyle.UI200);

// ❌ Wrong — manual font properties
label.FontSize = 14;
label.FontFamily = "UI";
```

Available label styles:
| Style | Font Family | Font Size |
|-------|-------------|-----------|
| `UI100` | UI | 12 |
| `UI200` | UI | 14 |
| `UI300` | UI | 16 |
| `UI400` | UI | 18 |
| `Body100` | Body | 12 |
| `Body200` | Body | 14 |
| `Body300` | Body | 16 |
| `Body400` | Body | 18 |
| `SectionHeader` | UI | 18 |
| `Header500`–`Header1000` | Header | 20–64 |

**Never** set `WidthRequest`/`HeightRequest` on labels — let them size naturally.

### 4. Button Styles

```csharp
using DIPS.Mobile.UI.Resources.Styles;
using DIPS.Mobile.UI.Resources.Styles.Button;

// ✅ Standard buttons
button.Style = Styles.GetButtonStyle(ButtonStyle.DefaultSmall);

// ✅ Close/dismiss buttons
closeButton.Style = Styles.GetButtonStyle(ButtonStyle.CloseIconSmall);
```

Available: `DefaultLarge/Small`, `CallToActionLarge/Small`, `GhostLarge/Small`, `CloseIconSmall`, `DefaultIconSmall/Large`, `GhostIconSmall/Large`, `CallToActionIconSmall/Large`, `DefaultFloatingIconLarge/Large`.

### 5. Corner Radius — Use Layout Effect, Not Border

```csharp
using LayoutEffect = DIPS.Mobile.UI.Effects.Layout.Layout;

// ✅ Correct — Layout effect with radius token
var container = new Grid { BackgroundColor = Colors.GetColor(ColorName.color_surface_default) };
LayoutEffect.SetCornerRadius(container, new CornerRadius(Sizes.GetSize(SizeName.radius_large)));

// ❌ Wrong — Border wrapper with StrokeShape
var border = new Border
{
    StrokeShape = new RoundRectangle { CornerRadius = 16 },
    Content = container
};
```

**Important**: Inside classes extending `VisualElement` (e.g., `Grid`, `ContentView`), `Layout` resolves to the `VisualElement.Layout(Rect)` method. Use a `using` alias:

```csharp
using LayoutEffect = DIPS.Mobile.UI.Effects.Layout.Layout;

// Then use:
LayoutEffect.SetCornerRadius(view, new CornerRadius(Sizes.GetSize(SizeName.radius_medium)));
```

### 6. Font Families

Only three valid font families: `"Body"`, `"UI"`, `"Header"`.

**Never** use `"monospace"` or system fonts.

Prefer label styles over manual `FontFamily` assignment. Manual `FontFamily` is acceptable on `Button` when no matching `ButtonStyle` exists.

## Verification Checklist

Before finishing, verify:
- [ ] No hardcoded hex colors — all use `Colors.GetColor(ColorName.*)`
- [ ] No hardcoded pixel values for spacing/sizing — all use `Sizes.GetSize(SizeName.*)`
- [ ] No manual `FontSize`/`FontFamily` on labels — all use `Styles.GetLabelStyle()`
- [ ] No `Border` used purely for corner radius — use `LayoutEffect.SetCornerRadius()`
- [ ] No `Shadow` on views — it causes platform-specific issues (Android elevation shadows)
- [ ] Color/size names verified against `ColorName.cs` / `SizeName.cs`
