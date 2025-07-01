using System.Windows.Input;

namespace DIPS.Mobile.UI.Components.Tabs
{
    public partial class Tab
    {
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
        
        public static readonly BindableProperty IsSelectedProperty = BindableProperty.Create(
            nameof(IsSelected),
            typeof(bool),
            typeof(Tab),
            defaultValue: false,
            defaultBindingMode: BindingMode.TwoWay);
        
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