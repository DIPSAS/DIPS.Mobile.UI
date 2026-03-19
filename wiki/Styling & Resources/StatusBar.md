# Status Bar

Control the appearance of the Android status bar on a per-page basis.

## Platform Support
**Android only** - iOS status bar is controlled differently through `Info.plist` and native APIs.

## Overview

The status bar feature allows you to customize the status bar appearance (background color and icon style) for each page in your application. This is particularly useful when navigating between pages with different backgrounds, such as a photo capture page with a transparent status bar and a regular page with a colored background.

The `StatusBarHandler` automatically detects whether a page is modal or non-modal and applies the correct implementation:
- **Non-modal pages**: Sets color on the Activity window
- **Modal pages**: Tracks DialogFragment-to-ContentPage mapping and sets color on the dialog window

Status bar updates dynamically when the page appears, ensuring the correct appearance as users navigate through your app.

## Properties

### StatusBarColor
Sets the background color of the status bar for the page.

**Type:** `Color`  
**Default:** `null` (uses system default)

### StatusBarStyle
Controls the color of status bar icons (clock, battery, etc.).

**Type:** `StatusBarStyle` enum  
**Default:** `Auto`

**Values:**
- `Auto` - Automatically determines light or dark icons based on the background color's luminosity (default)
- `Light` - Light icons (use with dark backgrounds)
- `Dark` - Dark icons (use with light backgrounds)

## Usage

### XAML

```xaml
<dui:ContentPage 
    xmlns:dui="http://dips.com/mobile.ui"
    StatusBarColor="{dui:Colors color_primary_90}"
    StatusBarStyle="Dark">
    
    <!-- Page content -->
    
</dui:ContentPage>
```

### Code-behind

```csharp
public partial class MyPage : ContentPage
{
    public MyPage()
    {
        InitializeComponent();
        
        StatusBarColor = Colors.Primary90;
        StatusBarStyle = StatusBarStyle.Dark;
    }
}
```

### Using Auto Style (Recommended)

The `Auto` style automatically determines whether to use light or dark icons based on the background color's luminosity:

```xaml
<dui:ContentPage 
    xmlns:dui="http://dips.com/mobile.ui"
    StatusBarColor="{dui:Colors color_primary_90}"
    StatusBarStyle="Auto">
    
    <!-- StatusBarStyle will automatically choose Dark or Light icons -->
    
</dui:ContentPage>
```

## Common Scenarios

### Transparent Status Bar

For pages where you want the status bar to blend with the content (e.g., image viewers, camera):

```xaml
<dui:ContentPage 
    xmlns:dui="http://dips.com/mobile.ui"
    StatusBarColor="Transparent"
    StatusBarStyle="Light">
    
    <Image Source="photo.jpg" Aspect="AspectFill" />
    
</dui:ContentPage>
```

### Colored Background Pages

For regular pages with your app's theme colors:

```xaml
<dui:ContentPage 
    xmlns:dui="http://dips.com/mobile.ui"
    StatusBarColor="{dui:Colors color_primary_90}"
    StatusBarStyle="Auto">
    
    <!-- Page content -->
    
</dui:ContentPage>
```

### Modal Pages

The status bar handler automatically detects modal pages and applies the settings correctly:

```csharp
// Modal navigation
await Navigation.PushModalAsync(new MyModalPage 
{ 
    StatusBarColor = Colors.White,
    StatusBarStyle = StatusBarStyle.Dark
});
```

## Best Practices

1. **Use Auto style when possible** - It automatically adapts to your chosen background color
2. **Set status bar properties on every page** - Ensures consistent appearance when navigating
3. **Match your page background** - The status bar should complement your page design
4. **Test on different Android versions** - Status bar behavior can vary across Android versions

## Notes

- Status bar updates happen in the page's `OnAppearing()` lifecycle method
- Changes are page-specific and revert when navigating back
- The feature works with both standard and modal navigation
- On iOS, status bar behavior is controlled through different mechanisms

## Related

- [Pages](Pages.md) - Information about ContentPage
- [Colors](Colors.md) - Available color resources
