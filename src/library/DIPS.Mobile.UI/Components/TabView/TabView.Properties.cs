using DIPS.Mobile.UI.Resources.Styles;
using DIPS.Mobile.UI.Resources.Styles.Label;
using Colors = DIPS.Mobile.UI.Resources.Colors.Colors;

namespace DIPS.Mobile.UI.Components.TabView;

public partial class TabView
{
    /// <summary>
    /// Sets the text color of the tabs
    /// </summary>
    public Color DefaultTextColor
    {
        get => (Color)GetValue(TextColorProperty);
        set => SetValue(TextColorProperty, value);
    }
    
    /// <summary>
    /// Sets the text color of tabs when they selected
    /// </summary>
    public Color SelectedTextColor
    {
        get => (Color)GetValue(SelectedTextColorProperty);
        set => SetValue(SelectedTextColorProperty, value);
    }
                
    /// <summary>
    /// Sets the selected text style of the labels of the tabs
    /// </summary>
    public Style DefaultTextStyle
    {
        get => (Style)GetValue(TextStyleProperty);
        set => SetValue(TextStyleProperty, value);
    }
        
    /// <summary>
    /// Sets the selected text style of the labels of the tabs when they are selected
    /// </summary>
    public Style SelectedTextStyle
    {
        get => (Style)GetValue(SelectedTextStyleProperty);
        set => SetValue(SelectedTextStyleProperty, value);
    }
    
    /// <summary>
    /// Sets the selected tab´s index
    /// </summary>
    public int SelectedTabIndex
    {
        get => (int)GetValue(SelectedTabIndexProperty);
        set => SetValue(SelectedTabIndexProperty, value);
    }

    /// <summary>
    /// Title and possibly counter for all tabs
    /// </summary>
    public List<TabItem>? ItemsSource
    {
        get => (List<TabItem>?)GetValue(ItemsSourceProperty);
        set => SetValue(ItemsSourceProperty, value);
    }
    
    public Func<int, Task<bool>>? CanSwitchTab
    {
        get => (Func<int, Task<bool>>?)GetValue(CanSwitchTabProperty);
        set => SetValue(CanSwitchTabProperty, value);
    }  
    
    public event EventHandler<TabViewEventArgs>? OnSelectedTabIndexChanged;
    
    public class TabViewEventArgs : EventArgs
    {
        public TabViewEventArgs(int selectedTabIndex)
        {
            SelectedTabIndex = selectedTabIndex;
        }
    
        public int SelectedTabIndex { get; }
    }
    
    public static readonly BindableProperty TextColorProperty = BindableProperty.Create(
        nameof(DefaultTextColor),
        typeof(Color),
        typeof(Tab),
        Colors.GetColor(ColorName.color_text_default));
        
    public static readonly BindableProperty TextStyleProperty = BindableProperty.Create(
        nameof(DefaultTextStyle),
        typeof(Style),
        typeof(Tab),
        Styles.GetLabelStyle(LabelStyle.Body200));
        
    public static readonly BindableProperty SelectedTextColorProperty = BindableProperty.Create(
        nameof(SelectedTextColor),
        typeof(Color),
        typeof(Tab),
        Colors.GetColor(ColorName.color_text_action));
        
    public static readonly BindableProperty SelectedTextStyleProperty = BindableProperty.Create(
        nameof(SelectedTextStyle),
        typeof(Style),
        typeof(Tab),
        Styles.GetLabelStyle(LabelStyle.UI300));

    public static readonly BindableProperty SelectedTabIndexProperty = BindableProperty.Create(
        nameof(SelectedTabIndex),
        typeof(int),
        typeof(TabItem),
        defaultValue: 0,
        defaultBindingMode: BindingMode.TwoWay
#if __IOS__
        ,
        propertyChanged: (bindable, _, _) => ((TabView)bindable).SetTabToggled()
#endif
        );
    
    public static readonly BindableProperty ItemsSourceProperty = BindableProperty.Create(
        nameof(ItemsSource),
        typeof(List<TabItem>),
        typeof(TabView),
        defaultValueCreator:(bindable => new List<TabItem>()),
        propertyChanged: (bindable, _, _) => ((TabView)bindable).OnItemsSourceChanged());

    public static readonly BindableProperty CanSwitchTabProperty = BindableProperty.Create(
        nameof(CanSwitchTab),
        typeof(Func<int, Task<bool>>),
        typeof(Tab)
    );
}