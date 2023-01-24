using System;
using System.Collections.Generic;
using System.Windows.Input;
using Xamarin.Forms;

namespace DIPS.Mobile.UI.Components.Pickers
{
    public partial class Picker
    {
        public static readonly BindableProperty HeaderProperty = BindableProperty.Create(
            nameof(Title),
            typeof(string),
            typeof(Picker));

        /// <summary>
        /// The title displayed for people in the picker.
        /// </summary>
        public string Title
        {
            get => (string)GetValue(HeaderProperty);
            set => SetValue(HeaderProperty, value);
        }

        public static readonly BindableProperty SelectedItemProperty = BindableProperty.Create(
            nameof(SelectedItem),
            typeof(object),
            typeof(Picker), propertyChanged: SelectedItemChanged, defaultBindingMode: BindingMode.TwoWay);

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
            typeof(Picker),
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
            typeof(Picker));

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
        public event EventHandler<object>? ItemSelected;
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