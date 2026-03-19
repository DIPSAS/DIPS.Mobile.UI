DIPS delivers a set of icons that you can use in your app. These icons are used by the components delivered from the library as well. [The icons are located in the mobile design tokens repository.](https://github.com/DIPSAS/DIPS.Mobile.DesignTokens/tree/main/src/tokens/icons)

#### Usage
The icons are available for you to use in different scenarios for you:

XAML Shared:
```xml
<Image Source="{dui:Icons <icon_name>" />
```

C# Shared:

```csharp
image.Source = Icons.GetIcon(<icon_name);
```

C# iOS:

```csharp
var uiImage = UIImage.FromFile(IconName.<icon_name>.ToString());
myUIImageView = uiImage;
```

C# Android:

```csharp
var androidResource = Android.DUI.GetResourceId(Icons.GetIconName(IconName.<icon_name>), "drawable");
myImageView.SetImageResource((int)androidResource);
```


### Technical note
The icons are imported as `<MauiImage>`. [`<MauiImage> `trigger resizetizer](https://learn.microsoft.com/en-us/dotnet/maui/user-interface/controls/image#load-a-local-image) build time, which takes a `svg` and generated native images as `png` and generating all the sizes needed for the different devices . These are reusable both in the platform and virtually in MAUI.

### Turn it off
If you do not use our icons and are worried your app including icons that are not needed, you can turn it off by setting the following property in your `.csproj`:

```xml
<PropertyGroup>
    <DIPSIncludeIcons>False</DIPSIncludeIcons>
</PropertyGroup>
```
