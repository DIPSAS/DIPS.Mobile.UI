We have written a custom implementation of `HideSoftInputOnTapped`, as it does not work properly for custom controls, and only removes the soft keyboard on Android, not actually removing focus.

# Enabling
```csharp
...
var builder = MauiApp.CreateBuilder();
builder
    .UseMauiApp<App>()
    .UseDIPSUI(options =>
    {
       options.EnableCustomHideSoftInputOnTapped();
    });
...