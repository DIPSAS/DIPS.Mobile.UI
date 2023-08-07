using System.Windows.Input;
using DIPS.Mobile.UI.Components.BottomSheets;
using DIPS.Mobile.UI.Components.Chips;
using DIPS.Mobile.UI.Effects.Touch;
using Microsoft.Maui.Controls.Shapes;
using Microsoft.Maui.Layouts;
using Label = DIPS.Mobile.UI.Components.Labels.Label;

namespace DIPS.Mobile.UI.Components.Pickers.MultiItemsPicker;

public partial class MultiItemsPicker : ContentView
{
    private readonly FlexLayout m_flexLayout;
    private readonly Label m_placeHolderLabel;

    public MultiItemsPicker()
    {
        OpenCommand = new Command(() => Open());
        var borderRectangle = new RoundRectangle() {StrokeThickness = 0};
        borderRectangle.SetBinding(RoundRectangle.CornerRadiusProperty,
            new Binding {Path = nameof(ContainerCornerRadius), Source = this});
        var border = new Border() {StrokeShape = borderRectangle};
        m_flexLayout = new FlexLayout()
        {
            Direction = FlexDirection.Row, //Items wrap vertically
            Wrap = FlexWrap.Wrap, //Items should wrap to new lines when they reach the end
            AlignItems = FlexAlignItems.Start, //The items should start at the top of the layout
            AlignContent = FlexAlignContent.Start, //The items should start at the top of the layout
            JustifyContent = FlexJustify.SpaceEvenly //Adds space between each item
        };
        m_placeHolderLabel = new Label {VerticalOptions = LayoutOptions.Center};
        m_placeHolderLabel.SetBinding(Microsoft.Maui.Controls.Label.TextProperty,
            new Binding() {Path = nameof(Placeholder), Source = this});
        m_flexLayout.Add(m_placeHolderLabel);

        border.Content = m_flexLayout;
        Touch.SetCommand(border,
            OpenCommand);
        Content = border;
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
            m_flexLayout.Add(new Chip {Title = title, Command = OpenCommand});
        }
    }
}