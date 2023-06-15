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

    /// <summary>
    /// Sets the subtitle text that people will see below <see cref="Title"/>
    /// </summary>
    public string Subtitle
    {
        get => (string)GetValue(SubtitleProperty);
        set => SetValue(SubtitleProperty, value);
    }

    /// <summary>
    /// Sets the <see cref="Microsoft.Maui.CornerRadius"/> of the list item
    /// </summary>
    public CornerRadius CornerRadius
    {
        get => (CornerRadius)GetValue(CornerRadiusProperty);
        set => SetValue(CornerRadiusProperty, value);
    }

    /// <summary>
    /// Sets the item that will be placed on this element
    /// </summary>
    public BindableObject ContentItem
    {
        get => (BindableObject)GetValue(ContentItemProperty);
        set => SetValue(ContentItemProperty, value);
    }

    /// <summary>
    /// By default, the <see cref="ListItem"/> will set the layout options of the <see cref="ContentItem"/> in order for it to be right-aligned. Set this property to override the layoutoptions for your <see cref="ContentItem"/> view.
    /// </summary>
    public bool ShouldOverrideContentItemLayoutOptions
    {
        get => (bool)GetValue(ShouldOverrideContentItemLayoutOptionsProperty);
        set => SetValue(ShouldOverrideContentItemLayoutOptionsProperty, value);
    }

    public bool HasTopDivider
    {
        get => (bool)GetValue(HasTopDividerProperty);
        set => SetValue(HasTopDividerProperty, value);
    }
    
    public bool HasBottomDivider
    {
        get => (bool)GetValue(HasBottomDividerProperty);
        set => SetValue(HasBottomDividerProperty, value);
    }
    
    public static readonly BindableProperty ShouldOverrideContentItemLayoutOptionsProperty = BindableProperty.Create(
        nameof(ShouldOverrideContentItemLayoutOptions),
        typeof(bool),
        typeof(ListItem),
        defaultValue:true);
    
    public static readonly BindableProperty ContentItemProperty = BindableProperty.Create(
        nameof(ContentItem),
        typeof(BindableObject),
        typeof(ListItem),
        propertyChanged:OnContentItemChanged);
    
    public static readonly BindableProperty CornerRadiusProperty = BindableProperty.Create(
        nameof(CornerRadius),
        typeof(CornerRadius),
        typeof(ListItem),
        defaultValue: new CornerRadius(Sizes.GetSize(SizeName.size_4)),
        propertyChanged:CornerRadiusChanged);
    
    public static readonly BindableProperty SubtitleProperty = BindableProperty.Create(
        nameof(Subtitle),
        typeof(string),
        typeof(ListItem));
    
    public static readonly BindableProperty HasTopDividerProperty = BindableProperty.Create(
        nameof(HasTopDivider),
        typeof(bool),
        typeof(ListItem));

    public static readonly BindableProperty HasBottomDividerProperty = BindableProperty.Create(
        nameof(HasBottomDivider),
        typeof(bool),
        typeof(ListItem));
}