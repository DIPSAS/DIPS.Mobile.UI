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
        
        public static readonly BindableProperty IsSelectedProperty = BindableProperty.Create(
            nameof(IsSelected),
            typeof(bool),
            typeof(Tab),
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
    }   
}