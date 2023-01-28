using System.Threading.Tasks;
using DIPS.Mobile.UI.Components.BottomSheets;
using DIPS.Mobile.UI.Extensions;
using DIPS.Mobile.UI.Resources.Colors;
using Xamarin.Forms;
using Image = DIPS.Mobile.UI.Components.Images.Image;
using Label = DIPS.Mobile.UI.Components.Labels.Label;
using ListView = DIPS.Mobile.UI.Components.Lists.ListView;
using CheckBox = DIPS.Mobile.UI.Components.CheckBoxes.CheckBox;

namespace DIPS.Mobile.UI.Components.Pickers
{
    public partial class Picker
    {
        private void AttachBottomSheet()
        {
            GestureRecognizers.Add(new TapGestureRecognizer() {Command = (new Command(() => _ = OpenBottomSheet()))});
        }

        private async Task OpenBottomSheet()
        {
            await Application.Current.PushBottomSheet(CreateSheetContent());
        }

        private BottomSheet CreateSheetContent()
        {
            var bottomSheet = new BottomSheet();
            bottomSheet.Content = new ListView()
            {
                HasUnevenRows = true,
                ItemsSource = ItemsSource,
                ItemTemplate = new PickerListViewDataTemplateSelector(bottomSheet, this),
                Margin = 10 //TODO: Use DesignSystem
            };
            return bottomSheet;
        }

        private class PickerListViewDataTemplateSelector : DataTemplateSelector
        {
            private readonly BottomSheet m_bottomSheet;
            private readonly Picker m_picker;

            public PickerListViewDataTemplateSelector(BottomSheet bottomSheet, Picker picker)
            {
                m_bottomSheet = bottomSheet;
                m_picker = picker;
            }

            protected override DataTemplate OnSelectTemplate(object item, BindableObject container)
            {
                return new DataTemplate(() =>
                {
                    return new ViewCell() {View = new CheckBox()
                        {
                            Text = item.GetPropertyValue(m_picker.ItemDisplayProperty),
                            IsSelected = m_picker.SelectedItem == item,
                            Command = new Command(() =>
                            {
                                m_picker.SelectedItem = item;
                                m_bottomSheet.Close();
                            })
                        }};
                });
            }
        }
    }
}