using System;
using System.Threading.Tasks;
using DIPS.Mobile.UI.Components.BottomSheet;
using DIPS.Mobile.UI.Components.Images;
using DIPS.Mobile.UI.Components.Labels;
using DIPS.Mobile.UI.Extensions;
using DIPS.Mobile.UI.Resources.Colors;
using Xamarin.Forms;
using Image = DIPS.Mobile.UI.Components.Images.Image;
using Label = DIPS.Mobile.UI.Components.Labels.Label;
using ListView = DIPS.Mobile.UI.Components.Lists.ListView;

namespace DIPS.Mobile.UI.Components.Pickers
{
    public partial class Picker
    {
        private IBottomSheet? m_currentBottomSheet;

        private void AttachBottomSheet()
        {
            GestureRecognizers.Add(new TapGestureRecognizer() {Command = (new Command(() => _ = OpenBottomSheet()))});
        }

        private async Task OpenBottomSheet()
        {
            m_currentBottomSheet = await Application.Current.PushBottomSheet(CreateSheetContent());
        }

        private BottomSheetView CreateSheetContent()
        {
            var listView = new ListView()
            {
                ItemsSource = ItemsSource, ItemTemplate = new PickerListViewDataTemplateSelector(this)
            };
            return new BottomSheetView() {Content = listView};
        }

        private class PickerListViewDataTemplateSelector : DataTemplateSelector
        {
            private readonly Picker m_picker;

            public PickerListViewDataTemplateSelector(Picker picker)
            {
                m_picker = picker;
            }

            protected override DataTemplate OnSelectTemplate(object item, BindableObject container)
            {
                return new DataTemplate(() =>
                {
                    var isSelected = m_picker.SelectedItem == item;
                    var grid = new Grid();
                    grid.ColumnDefinitions = new ColumnDefinitionCollection()
                    {
                        new() {Width = 25}, new() {Width = GridLength.Star}
                    };
                    grid.GestureRecognizers.Add(new TapGestureRecognizer()
                    {
                        Command = new Command(() =>
                        {
                            m_picker.SelectedItem = item;
                            m_picker.m_currentBottomSheet?.Close();
                            m_picker.m_currentBottomSheet = null;
                        })
                    });

                    var selectedImage = new Image() {VerticalOptions = LayoutOptions.Center};
                    selectedImage.iOSProperties.SystemIconName = "checkmark";
                    selectedImage.AndroidProperties.IconResourceName = "abc_btn_check_material";
                    selectedImage.Color = Colors.GetColor(ColorName.color_system_black);
                    selectedImage.Margin = new Thickness(5); //TODO: Use DesignSystem
                    selectedImage.IsVisible = isSelected;

                    var itemLabel = new Label()
                    {
                        Text = item.GetPropertyValue(m_picker.ItemDisplayProperty),
                        VerticalTextAlignment = TextAlignment.Center
                    };

                    grid.Children.Add(selectedImage, 0, 0);
                    grid.Children.Add(itemLabel, 1, 0);

                    return new ViewCell() {View = grid};
                });
            }
        }
    }
}