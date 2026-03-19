# Nuget
DIPS.Mobile.UI is available to download from [nuget.org](https://www.nuget.org/packages/DIPS.Mobile.UI). The easiest way to add the library to your .NET MAUI project is to use a terminal and run:

```
> dotnet add package DIPS.Mobile.UI
```

# Builder
In your `MauiProgram.cs`:
```csharp

public static Microsoft.Maui.Hosting.MauiApp CreateMauiApp()
{
    var builder = Microsoft.Maui.Hosting.MauiApp.CreateBuilder();
    builder
        .UseMauiApp<App>()
        .UseDIPSUI()
        ...

#if DEBUG
      ...
      DUI.IsDebug = true;
#endif
}
```

> `DUI.IsDebug = true;` is there to benefit from debugging possibilities from the library.

# Label Styles
Go to [Labels](https://github.com/DIPSAS/DIPS.Mobile.UI/wiki/Labels#implementation) for implementation of styles for `Label`.

# Platforms
## Android

### Styles
To get the DIPS Android style (colors and splash screen etc), you will have to modify your `Style` annotation in `MainActivity.cs`:
```csharp
[Activity(Theme = "@style/DIPS.Mobile.UI.Style", MainLauncher = true,
    ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation | ConfigChanges.UiMode |
                           ConfigChanges.ScreenLayout | ConfigChanges.SmallestScreenSize | ConfigChanges.Density)]
public class MainActivity : MauiAppCompatActivity
{
}
```

### HideSoftInputOnTapped
`HideSoftInputOnTapped` not working as expected? Follow this [link](https://github.com/DIPSAS/DIPS.Mobile.UI/wiki/HideSoftInputOnTapped) to make use of our custom implementation.