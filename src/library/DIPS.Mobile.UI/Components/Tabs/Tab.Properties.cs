using System.Windows.Input;
using DIPS.Mobile.UI.Resources.Styles;
using DIPS.Mobile.UI.Resources.Styles.Label;
using Colors = DIPS.Mobile.UI.Resources.Colors.Colors;

namespace DIPS.Mobile.UI.Components.Tabs
{
    public partial class Tab
    {
        /// <summary>
        /// Sets the text color of the tab
        /// </summary>
        public Color DefaultTextColor
        {
            get => (Color)GetValue(TextColorProperty);
            set => SetValue(TextColorProperty, value);
        }
        
                
        /// <summary>
        /// Sets the selected text style of the label of the tab
        /// </summary>
        public Style DefaultTextStyle
        {
            get => (Style)GetValue(TextStyleProperty);
            set => SetValue(TextStyleProperty, value);
        }
        
        /// <summary>
        /// Sets the selected text style of the label of the tab when it is selected
        /// </summary>
        public Style SelectedTextStyle
        {
            get => (Style)GetValue(SelectedTextStyleProperty);
            set => SetValue(SelectedTextStyleProperty, value);
        }
        
        /// <summary>
        /// Sets the text color of the tab when it is selected
        /// </summary>
        public Color SelectedTextColor
        {
            get => (Color)GetValue(SelectedTextColorProperty);
            set => SetValue(SelectedTextColorProperty, value);
        }
        
        /// <summary>
        /// Defines if the tab is selected or not
        /// </summary>
        public bool IsSelected
        {
            get => (bool)GetValue(IsSelectedProperty);
            set => SetValue(IsSelectedProperty, value);
        }
        
        /// <summary>
        /// Label of the tab
        /// </summary>
        public string Title
        {
            get => (string)GetValue(TitleProperty);
            set => SetValue(TitleProperty, value);
        }
        
        /// <summary>
        /// Part of the label, shows how many items are in the tab
        /// </summary>
        public string Counter
        {
            get => (string)GetValue(CounterProperty);
            set => SetValue(CounterProperty, value);
        }
        
        /// <summary>
        /// The command to execute when people tap the chip.
        /// </summary>
        public ICommand? Command
        {
            get => (ICommand)GetValue(CommandProperty);
            set => SetValue(CommandProperty, value);
        }
        
        /// <summary>
        /// The command parameter to pass to the command when it executes when people tap the tab.
        /// </summary>
        public object? CommandParameter
        {
            get => (object)GetValue(CommandParameterProperty);
            set => SetValue(CommandParameterProperty, value);
        }
        
        /// <summary>
        /// The event to be invoked when the tab is tapped.
        /// </summary>
        public event EventHandler? Tapped;
        
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
        
        public static readonly BindableProperty IsSelectedProperty = BindableProperty.Create(
            nameof(IsSelected),
            typeof(bool),
            typeof(Tab),
            defaultValue: false,
            defaultBindingMode: BindingMode.TwoWay,
            propertyChanged: (bindable, _, _) => ((Tab)bindable).OnIsSelectedChanged());
        
        public static readonly BindableProperty TitleProperty = BindableProperty.Create(
            nameof(Title),
            typeof(string),
            typeof(Tab));
        
        public static readonly BindableProperty CounterProperty = BindableProperty.Create(
            nameof(Counter),
            typeof(string),
            typeof(Tab));
        
        public static readonly BindableProperty CommandProperty = BindableProperty.Create(
            nameof(Command),
            typeof(ICommand),
            typeof(Tab));
        
        public static readonly BindableProperty CommandParameterProperty = BindableProperty.Create(
            nameof(CommandParameter),
            typeof(object),
            typeof(Tab));
    }   
}