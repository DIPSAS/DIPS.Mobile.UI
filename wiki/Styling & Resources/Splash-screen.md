Apps that use the library will automatically get a DIPS splash screen when the application launches.

> For iOS it will display [this splash screen.](https://github.com/DIPSAS/DIPS.Mobile.UI/blob/main/src/library/DIPS.Mobile.UI/Resources/Splash/splash.svg)

> From Android 12 and beyond it will animate using [this splash screen.](https://github.com/DIPSAS/DIPS.Mobile.UI/blob/main/src/library/DIPS.Mobile.UI/Platforms/Android/Resources/drawable-v31/splash_logo.xml)

### Getting started
Remove `<MauiSplashScreen ...>` from your `.csproj`.

### Turn it off
If you do not want to use the integrated splash screen, turn it off by setting the following property in your `.csproj`:

```xml
<PropertyGroup>
    <DIPSIncludeSplashScreen>False</DIPSIncludeSplashScreen >
</PropertyGroup>
```
