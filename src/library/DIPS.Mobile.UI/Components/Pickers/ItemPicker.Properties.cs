using System.Windows.Input;

namespace DIPS.Mobile.UI.Components.Pickers
{
    public partial class ItemPicker
    {
        public static readonly BindableProperty SelectedItemProperty = BindableProperty.Create(
            nameof(SelectedItem),
            typeof(object),
            typeof(ItemPicker), propertyChanged: SelectedItemChanged, defaultBindingMode: BindingMode.TwoWay);

        /// <summary>
        /// The item that was selected by people when using the picker.
        /// </summary>
        public object? SelectedItem
        {
            get => GetValue(SelectedItemProperty);
            set => SetValue(SelectedItemProperty, value);
        }

        public static readonly BindableProperty ItemsSourceProperty = BindableProperty.Create(
            nameof(ItemsSource),
            typeof(IEnumerable<object>),
            typeof(ItemPicker),
            propertyChanged: ItemsSourceChanged);

        /// <summary>
        /// The items people can select from when opening the picker.
        /// </summary>
        public IEnumerable<object>? ItemsSource
        {
            get => (IEnumerable<object>)GetValue(ItemsSourceProperty);
            set => SetValue(ItemsSourceProperty, value);
        }

        /// <summary>
        /// The name of the property of <see cref="SelectedItem"/> to use when displaying the  item for people in the picker.
        /// </summary>
        /// <remarks>When this is not set, it fall back to <code>.ToString()</code> of the <see cref="SelectedItem"/>.</remarks>
        public string? ItemDisplayProperty { get; set; }

        public static readonly BindableProperty SelectedItemCommandProperty = BindableProperty.Create(
            nameof(SelectedItemCommand),
            typeof(ICommand),
            typeof(ItemPicker));

        /// <summary>
        /// The command to be executed when people select an item from the picker.
        /// </summary>
        public ICommand SelectedItemCommand
        {
            get => (ICommand)GetValue(SelectedItemCommandProperty);
            set => SetValue(SelectedItemCommandProperty, value);
        }

        public PickerMode Mode { get; set; }

        /// <summary>
        /// The event to be raised when people select an item from the picker.
        /// </summary>
        public event EventHandler<object>? DidSelectItem;

        public static readonly BindableProperty HasSearchBarProperty = BindableProperty.Create(
            nameof(HasSearchBar),
            typeof(bool),
            typeof(ItemPicker), defaultValue:true);

        /// <summary>
        /// Determines if a search bar should be visible when the picker is visible for people when in bottom sheet mode.
        /// </summary>
        public bool HasSearchBar
        {
            get => (bool)GetValue(HasSearchBarProperty);
            set => SetValue(HasSearchBarProperty, value);
        }
    }

    public enum PickerMode
    {
        /// <summary>
        /// Display the picker as a context menu.
        /// </summary>
        /// <remarks>Best suited when the list of items are short or the names of the items are short.</remarks>
        ContextMenu,
        /// <summary>
        /// Display the picker in a sheet.
        /// </summary>
        /// <remarks>Best suited when the list of items are long or the the names of the items are long.</remarks>
        BottomSheet,
    }
}