using DIPS.Mobile.UI.Resources.Sizes;

namespace DIPS.Mobile.UI.Components.ListItems;

public partial class ListItem
{
    
    public static readonly BindableProperty TitleProperty = BindableProperty.Create(
        nameof(Title),
        typeof(string),
        typeof(ListItem));

    /// <summary>
    /// Sets the title text of the list item that people will see 
    /// </summary>
    public string Title
    {
        get => (string)GetValue(TitleProperty);
        set => SetValue(TitleProperty, value);
    }

    public static readonly BindableProperty SubTitleProperty = BindableProperty.Create(
        nameof(SubTitle),
        typeof(string),
        typeof(ListItem));

    /// <summary>
    /// Sets the subtitle text that people will see below <see cref="Title"/>
    /// </summary>
    public string SubTitle
    {
        get => (string)GetValue(SubTitleProperty);
        set => SetValue(SubTitleProperty, value);
    }

    public static readonly BindableProperty CornerRadiusProperty = BindableProperty.Create(
        nameof(CornerRadius),
        typeof(CornerRadius),
        typeof(ListItem),
        defaultValue: new CornerRadius(Sizes.GetSize(SizeName.size_4)),
        propertyChanged: CornerRadiusChanged);

    /// <summary>
    /// Sets the <see cref="Microsoft.Maui.CornerRadius"/> of the list item
    /// </summary>
    public CornerRadius CornerRadius
    {
        get => (CornerRadius)GetValue(CornerRadiusProperty);
        set => SetValue(CornerRadiusProperty, value);
    }

    public static readonly BindableProperty ContentItemProperty = BindableProperty.Create(
        nameof(ContentItem),
        typeof(BindableObject),
        typeof(ListItem),
        propertyChanged:OnContentItemChanged);
    
    /// <summary>
    /// Sets the item that will be placed on this element
    /// </summary>
    public BindableObject ContentItem
    {
        get => (BindableObject)GetValue(ContentItemProperty);
        set => SetValue(ContentItemProperty, value);
    }
}