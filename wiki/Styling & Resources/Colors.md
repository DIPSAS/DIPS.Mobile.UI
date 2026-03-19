DIPS deliveries a set of colors that are used by the components. All the colors available are exported from our Design System and transformed for you to use from [DIPS.Mobile.DesignTokens](https://github.com/DIPSAS/DIPS.Mobile.DesignTokens) repository. The colors are available for you to use in .NET Standard, but also in the platform code.

> By default, all the components from this component library will have the default color from our design system, but if you need to override them there should be a way for you to do that for each component.

# .NET Standard
## XAML
Colors are available in `Colors.xaml` by using `Colors` markup extension for your UI elements:
```xaml
<Label BackgroundColor="{dui:Colors <designsystem-color-name>}" /> 
```
## Code
If you need to access them by code, you can do the following:
```csharp
Colors.GetColor(ColorName.<designsystem-color-name>)
```

# Android
## XML
To use the colors in a Android xml file:
```xml
<TextView
        android:background="@color/color_<designsystem_color_name>)
        android:text="Hello World!" />
```
> This will look broken for you in the IDE, but once the app is built it will work.
## Code
To transform the color to a `AColor` you can use the following method:
```csharp
Colors.GetColor((ColorName.<designsystem-color-name>).ToPlatform();
```
# iOS
## Code
To transform the color to a `UIColor` you can use the following method:
```csharp
Colors.GetColor(ColorName.<designsystem-color-name>).ToPlatform();
```

---

# XAML ResourceDictionary API (IDE color preview + DynamicResource)

DIPS.Mobile.UI ships three compiled XAML `ResourceDictionary` classes that you can merge in your `App.xaml` to get:

- 🎨 **IDE color previews** — your IDE will show the actual color swatch next to each `StaticResource`/`DynamicResource` reference.
- 🌗 **Automatic dark/light theme switching** — use `DynamicResource` and swap the semantic dictionary at runtime.
- ✅ **Design-system consistency** — all color keys are the same as those used inside the library.

## The three dictionaries

| Class | Content | Theme |
|-------|---------|-------|
| `ColorsPalette` | All raw palette colors (greens, blues, greys, etc.) | Theme-independent |
| `ColorsLight` | All semantic colors for **light** mode | Light |
| `ColorsDark` | All semantic colors for **dark** mode (same keys as `ColorsLight`) | Dark |

> The semantic colors (`ColorsLight` / `ColorsDark`) share the same resource keys. You merge exactly **one** of them at a time. When you swap which one is merged, all views using `DynamicResource` update automatically.

## Setup in App.xaml

```xaml
<Application xmlns="http://schemas.microsoft.com/dotnet/2021/maui"
             xmlns:x="http://schemas.microsoft.com/winfx/2009/xaml"
             xmlns:dui="clr-namespace:DIPS.Mobile.UI.Resources.Colors;assembly=DIPS.Mobile.UI"
             ...>
    <Application.Resources>
        <ResourceDictionary>
            <ResourceDictionary.MergedDictionaries>
                <!-- Palette colors — always included -->
                <dui:ColorsPalette />
                <!-- Semantic colors — start with light, swap at runtime for dark -->
                <dui:ColorsLight />
            </ResourceDictionary.MergedDictionaries>
        </ResourceDictionary>
    </Application.Resources>
</Application>
```

## Using colors in XAML

Use `DynamicResource` for semantic colors so they update automatically when the theme switches:
```xaml
<Label TextColor="{DynamicResource color_text_default}"
       BackgroundColor="{DynamicResource color_surface_default}" />
```

Use `StaticResource` for palette colors that never change between themes:
```xaml
<BoxView Color="{StaticResource color_palette_blue_500}" />
```

## Handling dark / light theme switching

Subscribe to `Application.RequestedThemeChanged` and swap the semantic dictionary.
The index of the semantic dictionary in `MergedDictionaries` is `1` (after `ColorsPalette` at index `0`).

### App.xaml.cs
```csharp
public partial class App : Application
{
    public App()
    {
        InitializeComponent();
        RequestedThemeChanged += OnRequestedThemeChanged;
        ApplyTheme(RequestedTheme);
    }

    private void OnRequestedThemeChanged(object sender, AppThemeChangedEventArgs e)
        => ApplyTheme(e.RequestedTheme);

    private void ApplyTheme(AppTheme theme)
    {
        var mergedDicts = Resources.MergedDictionaries;
        // Index 0 = ColorsPalette (keep), index 1 = semantic colors (swap)
        if (mergedDicts.Count > 1)
            mergedDicts.RemoveAt(1);

        mergedDicts.Add(theme == AppTheme.Dark
            ? new DIPS.Mobile.UI.Resources.Colors.ColorsDark()
            : new DIPS.Mobile.UI.Resources.Colors.ColorsLight());
    }
}
```

Any view using `{DynamicResource color_surface_default}` will automatically re-render with the new color.

---

## Performance considerations

### Current C# static-dictionary approach

The existing `Colors.GetColor(ColorName.x)` API uses a `Dictionary<string, Color>` loaded at startup:

| Aspect | Detail |
|--------|--------|
| Lookup | O(1) string hash lookup per call |
| Memory | All colors loaded at startup (static initializer) |
| IDE preview | ❌ Not supported |
| Theme switching | Manual — requires subscribing to theme change events and calling update methods explicitly |
| XAML designer | ❌ No design-time preview |

### XAML ResourceDictionary approach

Compiled XAML `ResourceDictionary` objects are also hash-table backed:

| Aspect | Detail |
|--------|--------|
| `StaticResource` | Resolved once at XAML parse time — effectively free at runtime |
| `DynamicResource` | O(1) lookup + weak-reference subscription per binding; slight overhead vs. `StaticResource` |
| Memory | Dictionary loaded on first `InitializeComponent()` call — same order of magnitude as C# approach |
| IDE preview | ✅ Full color swatch previews |
| Theme switching | Automatic — swap the dictionary and `DynamicResource` bindings update all views |
| XAML designer | ✅ Design-time preview supported |

**Summary:** The XAML ResourceDictionary approach is not slower in practice. `StaticResource` is faster than any runtime dictionary lookup. `DynamicResource` has a small overhead per binding due to change subscriptions, but this is negligible compared to rendering. The main benefits are IDE integration and automatic theme switching.