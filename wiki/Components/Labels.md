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

## SectionHeader and accessibility

Use `LabelStyle.SectionHeader` for labels that visually introduce a section, group, or list of content. A `dui:Label` with this style is exposed as a semantic heading with `SemanticProperties.HeadingLevel=Level2` by default, so screen reader users can navigate to it as a heading.

```xml
<dui:Label Text="Recent patients"
           Style="{dui:Styles Label=SectionHeader}" />
```

Override `SemanticProperties.HeadingLevel` on the label when the information hierarchy needs a different level, for example `Level3` for a subsection or `None` when the label is not actually a heading.

For formatted text headings, apply `LabelStyle.SectionHeader` to the parent `dui:Label`. `SpanStyle.SectionHeader` is visual text styling only and does not make the label a semantic heading by itself.

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