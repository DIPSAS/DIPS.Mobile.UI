using System.Windows.Input;
using DIPS.Mobile.UI.Resources.Sizes;
using Colors = DIPS.Mobile.UI.Resources.Colors.Colors;

namespace DIPS.Mobile.UI.Components.ListItems;

public partial class ListItem
{
    public event EventHandler? Tapped;
    public object? CommandParameter
    {
        get => (object)GetValue(CommandParameterProperty);
        set => SetValue(CommandParameterProperty, value);
    }
    public ICommand? Command
    {
        get => (ICommand)GetValue(CommandProperty);
        set => SetValue(CommandProperty, value);
    }

    /// <summary>
    /// Sets the title text of the list item that people will see 
    /// </summary>
    public string Title
    {
        get => (string)GetValue(TitleProperty);
        set => SetValue(TitleProperty, value);
    }
    
    /// <summary>
    /// Sets the font attributes of the <see cref="Title"/>
    /// </summary>
    public FontAttributes TitleFontAttributes
    {
        get => (FontAttributes)GetValue(TitleFontAttributesProperty);
        set => SetValue(TitleFontAttributesProperty, value);
    }
    
    public Color TitleTextColor
    {
        get => (Color)GetValue(TitleTextColorProperty);
        set => SetValue(TitleTextColorProperty, value);
    }

    public double TitleFontSize
    {
        get => (double)GetValue(TitleFontSizeProperty);
        set => SetValue(TitleFontSizeProperty, value);
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
    /// Sets the font attributes of the <see cref="Subtitle"/>
    /// </summary>
    public FontAttributes SubtitleFontAttributes
    {
        get => (FontAttributes)GetValue(SubtitleFontAttributesProperty);
        set => SetValue(SubtitleFontAttributesProperty, value);
    }
    
    public ImageSource? Icon
    {
        get => (ImageSource)GetValue(IconProperty);
        set => SetValue(IconProperty, value);
    }

    public Color IconColor
    {
        get => (Color)GetValue(IconColorProperty);
        set => SetValue(IconColorProperty, value);
    }

    /// <summary>
    /// Sets the <see cref="Microsoft.Maui.CornerRadius"/> of the list item
    /// </summary>
    public CornerRadius CornerRadius
    {
        get => (CornerRadius)GetValue(CornerRadiusProperty);
        set => SetValue(CornerRadiusProperty, value);
    }

    public BindableObject VerticalContentItem
    {
        get => (BindableObject)GetValue(VerticalContentItemProperty);
        set => SetValue(VerticalContentItemProperty, value);
    }
    
    /// <summary>
    /// Sets the item that will be placed on this element
    /// </summary>
    public BindableObject HorizontalContentItem
    {
        get => (BindableObject)GetValue(ContentItemProperty);
        set => SetValue(ContentItemProperty, value);
    }

    /// <summary>
    /// By default, the <see cref="ListItem"/> will set the layout options of the <see cref="HorizontalContentItem"/> in order for it to be right-aligned. Set this property to override the layoutoptions for your <see cref="HorizontalContentItem"/> view.
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
        nameof(HorizontalContentItem),
        typeof(BindableObject),
        typeof(ListItem),
        propertyChanged:OnHorizontalContentItemChanged);
    
    public static readonly BindableProperty CornerRadiusProperty = BindableProperty.Create(
        nameof(CornerRadius),
        typeof(CornerRadius),
        typeof(ListItem),
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

    public static readonly BindableProperty TitleFontAttributesProperty = BindableProperty.Create(
        nameof(TitleFontAttributes),
        typeof(FontAttributes),
        typeof(ListItem));
    
    public static readonly BindableProperty SubtitleFontAttributesProperty = BindableProperty.Create(
        nameof(SubtitleFontAttributes),
        typeof(FontAttributes),
        typeof(ListItem));
    
    public static readonly BindableProperty VerticalContentItemProperty = BindableProperty.Create(
        nameof(VerticalContentItem),
        typeof(BindableObject),
        typeof(ListItem),
        propertyChanged:OnVerticalContentItemChanged);

    public static readonly BindableProperty CommandParameterProperty = BindableProperty.Create(
        nameof(CommandParameter),
        typeof(object),
        typeof(ListItem));
    
    public static readonly BindableProperty CommandProperty = BindableProperty.Create(
        nameof(Command),
        typeof(ICommand),
        typeof(ListItem),
        propertyChanged:OnCommandChanged);
    
    public static readonly BindableProperty TitleProperty = BindableProperty.Create(
        nameof(Title),
        typeof(string),
        typeof(ListItem));
    
    public static readonly BindableProperty IconColorProperty = BindableProperty.Create(
        nameof(IconColor),
        typeof(Color),
        typeof(ListItem),
        defaultValue: Colors.GetColor(ColorName.color_system_black));

    public static readonly BindableProperty TitleTextColorProperty = BindableProperty.Create(
        nameof(TitleTextColor),
        typeof(Color),
        typeof(ListItem),
        defaultValue:Colors.GetColor(ColorName.color_neutral_90));
    
    public static readonly BindableProperty TitleFontSizeProperty = BindableProperty.Create(
        nameof(TitleFontSize),
        typeof(double),
        typeof(ListItem),
        defaultValue:(double)Sizes.GetSize(SizeName.size_4));
    
    public static readonly BindableProperty IconProperty = BindableProperty.Create(
        nameof(Icon),
        typeof(ImageSource),
        typeof(ListItem),
        propertyChanged: (obj, ov, nv) =>
        {
            if(obj is ListItem listItem)
                listItem.AddIcon();
        });
}