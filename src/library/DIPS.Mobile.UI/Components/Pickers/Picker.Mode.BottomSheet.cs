using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Input;
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
            var checkboxItems = new List<PickerItem>();
            foreach (var item in ItemsSource)
            {
                checkboxItems.Add(new PickerItem(item.GetPropertyValue(ItemDisplayProperty), item == SelectedItem,
                    new Command(() =>
                    {
                        SelectedItem = item;
                        bottomSheet.Close();
                    })));
            }

            bottomSheet.Content = new ListView()
            {
                HasUnevenRows = true,
                ItemsSource = checkboxItems,
                ItemTemplate = new DataTemplate(LoadTemplate),
                Margin = 10 //TODO: Use DesignSystem
            };
            return bottomSheet;
        }

        private object LoadTemplate()
        {
            var checkBox = new CheckBox();
            checkBox.SetBinding(CheckBox.TextProperty, new Binding() {Path = nameof(PickerItem.DisplayName)});
            checkBox.SetBinding(CheckBox.IsSelectedProperty, new Binding() {Path = nameof(PickerItem.IsSelected)});
            checkBox.SetBinding(CheckBox.CommandProperty,
                new Binding() {Path = nameof(PickerItem.IsSelectedCommand)});
            return new ViewCell() {View = checkBox};
        }
    }
}