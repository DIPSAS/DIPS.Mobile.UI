To make sure the applications have the same look and feel, DIPS Mobile UI provides a set of predefined sizes that should be used in the application. These colors are defined as design tokens and generated from the design tokens repository. 

[The mapping of the sizes can be found here.](https://github.com/DIPSAS/DIPS.Mobile.UI/blob/main/src/library/DIPS.Mobile.UI/Resources/Sizes/Sizes.xaml)


> The actual number of the size is not important for the developers, but reusing the same resources when setting sizes is. If the designers feels like the application needs more air, they can simply change the underlying value of the sizes without the developers having to do anything as long as they reuse the same resources.

# How to use it
## XAML sizing

## Using size as a number
A lot of properties of a view element has a number as an input (like font size, height request, width request etc). Use `Sizes` markup extension to use the sizes from the library:

```xml
<dui:Label FontSize="{dui:Sizes size_2}"
           HeightRequest="{dui:Sizes size_3}" />
```

### Thickness
Thickness is primary used when setting margins and paddings on the view elements that people see. Use `Thickness` markup extension to use the sizes from the library:

```xml
<!-- Thickness for each size-->
<dui:Label Margin="{dui:Thickness Left=size_2, Top=size_2, Right=size_2, Bottom=size_2}" />
<dui:Label Padding="{dui:Thickness Left=size_2, Top=size_2, Right=size_2, Bottom=size_2}" />
<!-- Uniform thickness -->
<dui:Label Margin="{dui:Thickness size_2}" />
<dui:Label Padding="{dui:Thickness size_2}" />
```

### Corner radius
Corner radius is used to modify the corners of a view element that people see. TUse the `CornerRadius` markup extension to use the sizes from the library:
```xml
<!-- Corner radius for each size -->
<Border>
    <Border.StrokeShape>
        <RoundRectangle
            CornerRadius="{dui:CornerRadius TopLeft=size_2, TopRight=2, BottomLeft=size_2, BottomRight=size_2}" />
    </Border.StrokeShape>
</Border>
<!-- Uniform corner radius -->
<Border>
    <Border.StrokeShape>
        <RoundRectangle CornerRadius="{dui:CornerRadius size_2}" />
    </Border.StrokeShape>
</Border>
```

## C# sizing
If you need to use a size when writing C#, use the `Sizes` API:
```csharp
var number = Sizes.GetSize(SizeName.size_2);
```