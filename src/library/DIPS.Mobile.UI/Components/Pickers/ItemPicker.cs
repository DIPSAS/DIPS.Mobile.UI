using System;
using System.Linq;
using DIPS.Mobile.UI.Components.ContextMenus;
using DIPS.Mobile.UI.Extensions;
using DIPS.Mobile.UI.Resources.Colors;
using Xamarin.Forms;
using Label = DIPS.Mobile.UI.Components.Labels.Label;
using Image = DIPS.Mobile.UI.Components.Images.Image;

namespace DIPS.Mobile.UI.Components.Pickers
{
    //TODO: Make sure its accessable
    public partial class ItemPicker : Frame
    {
        private readonly Label m_selectedItemLabel;
        private bool m_layedOut;
        private readonly ContextMenuControl m_contextMenuControl;

        public ItemPicker()
        {
            HorizontalOptions = LayoutOptions.Fill;
            VerticalOptions = LayoutOptions.Center;
            Padding = 0;
            this.SetAppThemeColor(BackgroundProperty, ColorName.color_system_white);
            HasShadow = false;
            CornerRadius = 10; //TODO: Use DesignSystem

            //Header
            var headerLabel = new Label();
            headerLabel.SetBinding(Xamarin.Forms.Label.TextProperty,
                new Binding() {Source = this, Path = nameof(Title)});

            //Context Menu control 
            m_contextMenuControl = new ContextMenuControl();

            //Selected item
            m_selectedItemLabel = new Label {HorizontalTextAlignment = TextAlignment.End};
            m_selectedItemLabel.SetAppThemeColor(Xamarin.Forms.Label.TextColorProperty, ColorName.color_primary_90);

            var image = new Image();
            image.iOSProperties.SystemIconName = "chevron.up.chevron.down";
            image.AndroidProperties.IconResourceName = "mtrl_dropdown_arrow";
            image.SetAppThemeColor(Image.ColorProperty, Colors.GetColor(ColorName.color_primary_90),
                Colors.GetColor(ColorName.color_primary_90));

            //Arrange the grid
            var pickerContent = new Grid()
            {
                Padding = new Thickness(15, 10), //TODO: Replace with design system margins,
                ColumnSpacing = 10, //TODO: Replace with design system margins,
                VerticalOptions = LayoutOptions.Center,
                ColumnDefinitions = new ColumnDefinitionCollection()
                {
                    new() {Width = GridLength.Auto},
                    new() {Width = GridLength.Star},
                    new() {Width = GridLength.Auto}
                }
            };

            pickerContent.Children.Add(headerLabel, 0, 0);
            pickerContent.Children.Add(m_selectedItemLabel, 1, 0);
            pickerContent.Children.Add(image, 2, 0);

            m_contextMenuControl.TheContent = pickerContent;
            Content = m_contextMenuControl; //Set the context menu control as content to the frame
        }

        private void LayoutContent()
        {
            if (Mode == PickerMode.ContextMenu)
            {
                m_contextMenuControl.ItemClickedCommand =
                    new Command<ContextMenuItem>(SetSelectedItemBasedOnContextMenuItem);
                InputTransparent = true; //To make it touchable for context menu
                CascadeInputTransparent = false;
            }
            else if (Mode == PickerMode.BottomSheet)
            {
                AttachBottomSheet();
                m_contextMenuControl.m_theContentView.InputTransparent = false;
            }

            m_layedOut = true;
        }

        protected override void OnPropertyChanged(string propertyName = null)
        {
            if (propertyName.Equals(nameof(Parent)))
            {
                if (!m_layedOut)
                {
                    LayoutContent();
                }
            }

            base.OnPropertyChanged(propertyName);
        }

        private static void SelectedItemChanged(BindableObject bindable, object oldvalue, object newvalue)
        {
            if (bindable is not ItemPicker picker)
            {
                return;
            }

            if (picker.SelectedItem == null)
            {
                return;
            }

            picker.m_selectedItemLabel.Text =
                picker.SelectedItem.GetPropertyValue(picker.ItemDisplayProperty);
            picker.SelectedItemCommand?.Execute(picker.SelectedItem);
            picker.DidSelectItem?.Invoke(picker, picker.SelectedItem);

            if (picker.Mode == PickerMode.ContextMenu)
            {
                UpdateContextMenuItems(
                    picker); //<-- Needed if the selected item was set programatically, and not by the user    
            }
        }

        private static void ItemsSourceChanged(BindableObject bindable, object oldvalue, object newvalue)
        {
            if (bindable is not ItemPicker picker)
            {
                return;
            }

            if (picker.Mode == PickerMode.ContextMenu)
            {
                AddContextMenuItems(picker);
            }
        }

        private object? GetItemFromDisplayProperty(string toCompare)
        {
            if (ItemsSource == null)
            {
                return null;
            }

            var theItem = ItemsSource.FirstOrDefault(i =>
                toCompare.Equals(i.GetPropertyValue(ItemDisplayProperty), StringComparison.InvariantCulture));
            return theItem ?? null;
        }
    }
}