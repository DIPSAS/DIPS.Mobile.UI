using System.ComponentModel;
using System.Windows.Input;
using DIPS.Mobile.UI.Components.ListItems.Options;
using DIPS.Mobile.UI.Components.ListItems.Options.Dividers;
using DIPS.Mobile.UI.Components.ListItems.Options.Icon;
using DIPS.Mobile.UI.Components.ListItems.Options.InLineContent;
using DIPS.Mobile.UI.Components.ListItems.Options.Subtitle;
using DIPS.Mobile.UI.Components.ListItems.Options.Title;
using DIPS.Mobile.UI.Converters.ValueConverters;
using DebuggingOptions = DIPS.Mobile.UI.Components.ListItems.Options.Debugging.DebuggingOptions;

namespace DIPS.Mobile.UI.Components.ListItems;

public partial class ListItem
{
    public event EventHandler? Tapped;
    
    /// <summary>
    /// The parameter of <see cref="Command"/>
    /// </summary>
    public object? CommandParameter
    {
        get => (object)GetValue(CommandParameterProperty);
        set => SetValue(CommandParameterProperty, value);
    }
    
    /// <summary>
    /// The Command to be executed when the <see cref="ListItem"/> is tapped
    /// </summary>
    /// <remarks>Will automatically add touch effect on <see cref="ListItem"/></remarks>
    public ICommand? Command
    {
        get => (ICommand)GetValue(CommandProperty);
        set => SetValue(CommandProperty, value);
    }

    /// <summary>
    /// Sets the Title of the <see cref="ListItem"/>
    /// </summary>
    public string Title
    {
        get => (string)GetValue(TitleProperty);
        set => SetValue(TitleProperty, value);
    }

    /// <summary>
    /// Sets the Icon of the <see cref="ListItem"/>, which will be displayed to the left of <see cref="Title"/>
    /// </summary>
    [TypeConverter(nameof(ImageSourceConverter))]
    public ImageSource? Icon
    {
        get => (ImageSource?)GetValue(IconProperty);
        set => SetValue(IconProperty, value);
    }
    
    /// <summary>
    /// Sets the subtitle of the <see cref="ListItem"/>, which will be displayed below the <see cref="Title"/>
    /// </summary>
    public string Subtitle
    {
        get => (string)GetValue(SubtitleProperty);
        set => SetValue(SubtitleProperty, value);
    }
        
    /// <summary>
    /// The item to be placed in-line to the title and subtitle
    /// </summary>
    [TypeConverter(typeof(InLineContentTypeConverter))]
    public IView? InLineContent
    {
        get => (IView)GetValue(InLineContentProperty);
        set => SetValue(InLineContentProperty, value);
    }
    
    /// <summary>
    /// The item to be placed under <see cref="Title"/> <see cref="Subtitle"/> <see cref="Icon"/> and <see cref="InLineContent"/>
    /// </summary>
    [TypeConverter(typeof(UnderlyingContentTypeConverter))]
    public IView? UnderlyingContent
    {
        get => (IView)GetValue(UnderlyingContentProperty);
        set => SetValue(UnderlyingContentProperty, value);
    }
    
    /// <summary>
    /// Sets the title of the list item that people will see 
    /// </summary>
    public TitleOptions? TitleOptions
    {
        get => (TitleOptions?)GetValue(TitleOptionsProperty);
        set => SetValue(TitleOptionsProperty, value);
    }

    /// <summary>
    /// Sets the subtitle that people will see below <see cref="InLineContentOptions"/>
    /// </summary>
    public SubtitleOptions? SubtitleOptions
    {
        get => (SubtitleOptions?)GetValue(SubtitleOptionsProperty);
        set => SetValue(SubtitleOptionsProperty, value);
    }

    /// <summary>
    /// Sets the icon that will be displayed to the left of <see cref="InLineContentOptions"/>
    /// </summary>
    public IconOptions? IconOptions
    {
        get => (IconOptions?)GetValue(IconOptionsProperty);
        set => SetValue(IconOptionsProperty, value);
    }

    /// <summary>
    /// Sets options for debugging purposes
    /// </summary>
    public DebuggingOptions? DebuggingOptions
    {
        get => (DebuggingOptions?)GetValue(DebuggingOptionsProperty);
        set => SetValue(DebuggingOptionsProperty, value);
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
    /// Configures the <see cref="InLineContent"/>
    /// </summary>
    public InLineContentOptions? InLineContentOptions
    {
        get => (InLineContentOptions?)GetValue(InLineContentOptionsProperty);
        set => SetValue(InLineContentOptionsProperty, value);
    }
        
    /// <summary>
    /// Configures the top and bottom divider of the <see cref="ListItem"/>
    /// </summary>
    public DividersOptions? DividersOptions
    {
        get => (DividersOptions?)GetValue(DividersOptionsProperty);
        set => SetValue(DividersOptionsProperty, value);
    }
   
    /// <summary>
    /// Determines whether the <see cref="ListItem"/> should display a Divider on the top
    /// </summary>
    public bool HasTopDivider
    {
        get => (bool)GetValue(HasTopDividerProperty);
        set => SetValue(HasTopDividerProperty, value);
    }
    
    /// <summary>
    /// Determines whether the <see cref="ListItem"/> should display a Divider on the bottom
    /// </summary>
    public bool HasBottomDivider
    {
        get => (bool)GetValue(HasBottomDividerProperty);
        set => SetValue(HasBottomDividerProperty, value);
    }
    
    /// <summary>
    /// Sets the <see cref="ListItem"/> in debug mode
    /// </summary>
    public bool IsDebugMode
    {
        get => (bool)GetValue(IsDebugModeProperty);
        set => SetValue(IsDebugModeProperty, value);
    }
        
    /// <summary>
    /// Automatically sets divider based on where it is placed in a <see cref="VerticalStackLayout"/> or <see cref="CollectionView"/>
    /// </summary>
    /// <remarks>Must be a child of <see cref="VerticalStackLayout"/> or <see cref="CollectionView"/></remarks>
    [Obsolete("Use Layout.AutoHideLastDivider instead")]
    public bool AutoDivider
    {
        get => (bool)GetValue(AutoDividerProperty);
        set => SetValue(AutoDividerProperty, value);
    }
        
    public static readonly BindableProperty AutoDividerProperty = BindableProperty.Create(
        nameof(AutoDivider),
        typeof(bool),
        typeof(ListItem),
        propertyChanged: OnAutoDividerChanged);
        
    public static readonly BindableProperty IsDebugModeProperty = BindableProperty.Create(
        nameof(IsDebugMode),
        typeof(bool),
        typeof(ListItem));
        
    public static readonly BindableProperty CornerRadiusProperty = BindableProperty.Create(
        nameof(CornerRadius),
        typeof(CornerRadius),
        typeof(ListItem),
        propertyChanged: ((bindable, _, newValue) =>
        {
            UI.Effects.Layout.Layout.SetCornerRadius(bindable, newValue as CornerRadius? ?? default);
        }));
    
    public static readonly BindableProperty HasTopDividerProperty = BindableProperty.Create(
        nameof(HasTopDivider),
        typeof(bool),
        typeof(ListItem),
        propertyChanged: ((bindable, _, _) => ((ListItem)bindable).AddDivider(true)));

    public static readonly BindableProperty HasBottomDividerProperty = BindableProperty.Create(
        nameof(HasBottomDivider),
        typeof(bool),
        typeof(ListItem),
        propertyChanged: ((bindable, _, _) => ((ListItem)bindable).AddDivider(false)));

    public static readonly BindableProperty CommandParameterProperty = BindableProperty.Create(
        nameof(CommandParameter),
        typeof(object),
        typeof(ListItem));
    
    public static readonly BindableProperty CommandProperty = BindableProperty.Create(
        nameof(Command),
        typeof(ICommand),
        typeof(ListItem),
        defaultValue:null,
        propertyChanged: (bindable, _, _) => ((ListItem)bindable).AddTouch());
    
    public static readonly BindableProperty UnderlyingContentProperty = BindableProperty.Create(
        nameof(UnderlyingContent),
        typeof(IView),
        typeof(ListItem),
        propertyChanged: (bindable, _, _) => ((ListItem)bindable).AddUnderlyingContent());
    
    public static readonly BindableProperty TitleProperty = BindableProperty.Create(
        nameof(Title),
        typeof(string),
        typeof(ListItem),
        propertyChanged: (bindable, _, _) => ((ListItem)bindable).AddTitle());
    
    public static readonly BindableProperty SubtitleProperty = BindableProperty.Create(
        nameof(Subtitle),
        typeof(string),
        typeof(ListItem),
        propertyChanged: (bindable, _, _) => ((ListItem)bindable).AddSubtitle());

    public static readonly BindableProperty IconProperty = BindableProperty.Create(
        nameof(Icon),
        typeof(ImageSource),
        typeof(ListItem),
        propertyChanged: (bindable, _, _) => ((ListItem)bindable).AddIcon());

    public static readonly BindableProperty InLineContentProperty = BindableProperty.Create(
        nameof(InLineContent),
        typeof(IView),
        typeof(ListItem),
        propertyChanged: (bindable, _, _) => ((ListItem)bindable).AddInLineContent());

    #region OptionBindableProperties

    public static readonly BindableProperty TitleOptionsProperty = BindableProperty.Create(
        nameof(TitleOptions),
        typeof(TitleOptions),
        typeof(ListItem),
        propertyChanged: (bindable, _, newValue) =>
        {
            if(bindable is not ListItem listItem)
                return;

            if (listItem.TitleLabel is null)
            {
                // We need to create Title here, because FormattedText should be able to set the Text
                listItem.AddTitle();
            }
            else
            {
                Bind<TitleOptions>(newValue, listItem);
            }
        });

    public static readonly BindableProperty SubtitleOptionsProperty = BindableProperty.Create(
        nameof(SubtitleOptions),
        typeof(SubtitleOptions),
        typeof(ListItem),
        propertyChanged: (bindable, _, newValue) =>
        {
            if(bindable is not ListItem listItem)
                return;

            if (listItem.SubtitleLabel is null)
            {
                // We need to create Subtitle here, because FormattedText should be able to set the Text
                listItem.AddSubtitle();
            }
            else
            {
                Bind<SubtitleOptions>(newValue, listItem);
            }
        });
    
    public static readonly BindableProperty IconOptionsProperty = BindableProperty.Create(
        nameof(IconOptions),
        typeof(IconOptions),
        typeof(ListItem),
        propertyChanged: (bindable, _, newValue) =>
        {
            if (bindable is ListItem { Icon: not null } listItem)
            {
                Bind<IconOptions>(newValue, listItem);
            }
        });

    public static readonly BindableProperty InLineContentOptionsProperty = BindableProperty.Create(
        nameof(InLineContentOptions),
        typeof(InLineContentOptions),
        typeof(ListItem),
        propertyChanged: (bindable, _, newValue) =>
        {
            if (bindable is ListItem { InLineContent: not null } listItem)
            {
                Bind<InLineContentOptions>(newValue, listItem);
            }
        });

    public static readonly BindableProperty DividersOptionsProperty = BindableProperty.Create(
        nameof(DividersOptions),
        typeof(DividersOptions),
        typeof(ListItem),
        propertyChanged: (bindable, _, newValue) =>
        {
            if (bindable is not ListItem listItem)
                return;

            if (listItem.TopDivider is not null || listItem.BottomDivider is not null)
            {
                Bind<DividersOptions>(newValue, listItem);
            }
        });
        
    public static readonly BindableProperty DebuggingOptionsProperty = BindableProperty.Create(
        nameof(DebuggingOptions),
        typeof(DebuggingOptions),
        typeof(ListItem));
    
    private static void Bind<T>(object newValue, ListItem listItem) where T : ListItemOptions
    {
        ((T)newValue).Bind(listItem);
    }
        
    #endregion
    
}