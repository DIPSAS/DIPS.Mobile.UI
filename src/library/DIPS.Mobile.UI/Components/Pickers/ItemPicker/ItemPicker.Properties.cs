using System.Collections;
using System.Windows.Input;
using DIPS.Mobile.UI.Resources.LocalizedStrings.LocalizedStrings;

namespace DIPS.Mobile.UI.Components.Pickers.ItemPicker
{
    public partial class ItemPicker
    {
        public static readonly BindableProperty HasHapticsProperty = BindableProperty.Create(
            nameof(HasHaptics),
            typeof(bool),
            typeof(ItemPicker), defaultValue:true);

        /// <summary>
        /// Determines if the phone should stimulate the sense of touch and motion by the use of vibration when people tap the item.
        /// </summary>
        public bool HasHaptics
        {
            get => (bool)GetValue(HasHapticsProperty);
            set => SetValue(HasHapticsProperty, value);
        }
        
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
            typeof(IEnumerable),
            typeof(ItemPicker),
            propertyChanged: ItemsSourceChanged);

        /// <summary>
        /// The items people can select from when opening the picker.
        /// </summary>
        public IEnumerable? ItemsSource
        {
            get => (IEnumerable)GetValue(ItemsSourceProperty);
            set => SetValue(ItemsSourceProperty, value);
        }

        /// <summary>
        /// The name of the property of <see cref="SelectedItem"/> to use when displaying the  item for people in the picker.
        /// </summary>
        /// <remarks>
        /// When this is not set, it fall back to <code>.ToString()</code> of the <see cref="SelectedItem"/>.
        /// This is used when people search if <see cref="Mode"/> is set to <see cref="PickerMode.BottomSheet"/>
        /// This is not used if you set a <see cref="BottomSheetPickerConfiguration.SelectableItemTemplate"/>
        /// </remarks>
        public string? ItemDisplayProperty { get; set; }

        public static readonly BindableProperty SelectedItemCommandProperty = BindableProperty.Create(
            nameof(SelectedItemCommand),
            typeof(ICommand),
            typeof(ItemPicker));

        /// <summary>
        /// The command to be executed when people select/de-select an item from the picker.
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

        /// <summary>
        /// The place holder for people to see when they have not selected a item.
        /// </summary>
        public string Placeholder
        {
            get => (string)GetValue(PlaceholderProperty);
            set => SetValue(PlaceholderProperty, value);
        }

        /// <summary>
        /// Opens the picker.
        /// </summary>
        /// <remarks>This will not work if <see cref="Mode"/> is <see cref="PickerMode.ContextMenu"/></remarks>
        public ICommand OpenCommand => new Command(Open);

        /// <summary>
        /// Opens the picker.
        /// </summary>
        /// <remarks>This will not work if <see cref="Mode"/> is <see cref="PickerMode.ContextMenu"/></remarks>
        public partial void Open();

        public static readonly BindableProperty PlaceholderProperty = BindableProperty.Create(
            nameof(Placeholder),
            typeof(string),
            typeof(ItemPicker), defaultValue: DUILocalizedStrings.Choose);

        public static readonly BindableProperty SelectedItemProperty = BindableProperty.Create(
            nameof(SelectedItem),
            typeof(object),
            typeof(ItemPicker),
            propertyChanged: (bindable, _, _) => ((ItemPicker)bindable).SelectedItemChanged(),
            defaultBindingMode: BindingMode.TwoWay);
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