`Label` displays single-line and multi-line text. Text displayed by a `Label` can be colored, spaced, and can have text decorations.

# Styles
We have defined three different categories of styles for `Label`:
- **Header**
    - Bold font
- **UI**
    - Medium-bold font
- **Body**
    - Normal font

Each of these style categories has a different weight, such as: `UI400`, to control the font-size.

> The default style is set to `Body300`

Inspect [LabelStyle.cs](https://github.com/DIPSAS/DIPS.Mobile.UI/blob/main/src/library/DIPS.Mobile.UI/Resources/Styles/Label/LabelStyle.cs) to see all of the different styles.

# Usage
Here we place a `Label` with a style of `Body` with a weight of `400`.


```xml
<dui:Label Style="{dui:Styles Label=Body400}" />
```

# Implementation
To make use of these styles you have to implement the font of your choice. This is done in `MauiAppBuilder.cs`.

```cs
builder.ConfigureFonts(fonts =>
{
    fonts.AddFont("OpenSans-Bold.ttf", "Header");
    fonts.AddFont("OpenSans-Medium.ttf", "UI");
    fonts.AddFont("OpenSans-Regular.ttf", "Body");
});
```
>**NB:** It is important that the aliases are exactly `Header`, `UI` and `Body`. 

# Custom Truncation Text

You can set custom truncation text by using `CustomTruncationLabel`. Use the `TruncatedText` property to display custom text, such as "... more", at the end of the label when the content is truncated.

```xml
<dui:CustomTruncationLabel 
    Text="This is a long text that will be truncated."
    TruncatedText="... more" />
```

## Checking Truncation State

The `CustomTruncationLabel` provides the ability to check if the text is currently truncated, allowing you to implement conditional logic based on the truncation state.

```xml
<dui:CustomTruncationLabel 
    x:Name="MyLabel"
    Text="This is a long text that will be truncated."
    TruncatedText="... more" />
```

```csharp
// Check if the label text is truncated
if (MyLabel.IsTruncated)
{
    // Implement logic when text is truncated
    // e.g., show expand button, change styling, etc.
}
```