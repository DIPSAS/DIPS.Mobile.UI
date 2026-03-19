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