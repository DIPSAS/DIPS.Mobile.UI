using DIPS.Mobile.UI.Components.BottomSheets;
using DIPS.Mobile.UI.Components.Chips;
using DIPS.Mobile.UI.Converters.ValueConverters;
using DIPS.Mobile.UI.Effects.Touch;
using Microsoft.Maui.Layouts;
using Colors = DIPS.Mobile.UI.Resources.Colors.Colors;
using Label = DIPS.Mobile.UI.Components.Labels.Label;

namespace DIPS.Mobile.UI.Components.Pickers.MultiItemsPicker;

public partial class MultiItemsPicker : ContentView
{
    private readonly FlexLayout m_flexLayout;
    private readonly Label m_placeHolderLabel;

    public MultiItemsPicker()
    {
        OpenCommand = new Command(() => Open());
        BackgroundColor = Microsoft.Maui.Graphics.Colors.Transparent;
        
        m_flexLayout = new FlexLayout()
        {
            Direction = FlexDirection.Row, //Items wrap vertically
            Wrap = FlexWrap.Wrap, //Items should wrap to new lines when they reach the end
            AlignItems = FlexAlignItems.Start, //The items should start at the top of the layout
            AlignContent = FlexAlignContent.Start, //The items should start at the top of the layout
            JustifyContent = FlexJustify.SpaceEvenly
        };
        m_placeHolderLabel = new Label
        {
            VerticalOptions = LayoutOptions.Center,
            FontSize = Sizes.GetSize(SizeName.size_4),
            TextColor = Colors.GetColor(ColorName.color_neutral_60)
        };
        m_placeHolderLabel.SetBinding(Microsoft.Maui.Controls.Label.TextProperty,
            new Binding() {Path = nameof(Placeholder), Source = this});
        m_placeHolderLabel.SetBinding(IsVisibleProperty,
            new Binding()
            {
                Path = nameof(Placeholder), Source = this, Converter = new IsEmptyConverter() {Inverted = true}
            });
        m_flexLayout.Add(m_placeHolderLabel);

        Touch.SetCommand(this,
            OpenCommand);
        Content = m_flexLayout;
    }

    public Task Open()
    {
        return BottomSheetService.OpenBottomSheet(new MultiItemsPickerBottomSheet(this));
    }

    internal void DeSelectItem(object item)
    {
        if (SelectedItems == null) return;
        if (!SelectedItems.Contains(item)) return;

        var tempList = SelectedItems.ToList();
        tempList.Remove(item);
        SelectedItems = tempList;

        SelectedItemCommand?.Execute(SelectedItems);
        DidDeSelectItem?.Invoke(this, item);
    }

    internal void SelectItem(object item)
    {
        if (SelectedItems == null || !SelectedItems.Any())
        {
            SelectedItems = new List<object> {item};
        }
        else
        {
            if (SelectedItems.Contains(item)) return;

            var tempList = SelectedItems.ToList();
            tempList.Add(item);
            SelectedItems = tempList;
        }

        SelectedItemCommand?.Execute(SelectedItems);
        DidSelectItem?.Invoke(this, item);
    }

    private void OnSelectedItemsChanged()
    {
        m_flexLayout.Clear();

        if (SelectedItems == null || !SelectedItems.Any())
        {
            m_flexLayout.Add(m_placeHolderLabel);
            return;
        }

        foreach (var selectedItem in SelectedItems)
        {
            var title = selectedItem.GetPropertyValue(ItemDisplayProperty);
            if (title == null) continue;
            var chip = new Chip {Title = title, Command = OpenCommand};
#if __IOS__ //Android adds vertical space in a flexlayout, but iOS does not.
            chip.Margin = new Thickness(0, 0, 0, Sizes.GetSize(SizeName.size_1));
#endif
            m_flexLayout.Add(chip);
        }
        
        Content = m_flexLayout;
    }
}