using DIPS.Mobile.UI.Components.BottomSheets;
using DIPS.Mobile.UI.Components.Chips;
using DIPS.Mobile.UI.Converters.ValueConverters;
using DIPS.Mobile.UI.Effects.Touch;
using HorizontalStackLayout = DIPS.Mobile.UI.Components.Lists.HorizontalStackLayout;

namespace DIPS.Mobile.UI.Components.Pickers.MultiItemsPicker;

public partial class MultiItemsPicker : ContentView
{
    private HorizontalStackLayout m_hStackLayout;

    public MultiItemsPicker()
    {
        OpenCommand = new Command(() => Open(this));
        BackgroundColor = Microsoft.Maui.Graphics.Colors.Transparent;

        m_hStackLayout = new HorizontalStackLayout();
        CreatePlaceHolder();
        m_hStackLayout.Add(CreatePlaceHolder());

        Touch.SetCommand(this,
            OpenCommand);
        m_hStackLayout.ChildOutOfBounds += IsChildOutOfBounds;
        Unloaded += Dispose;
        Content = m_hStackLayout;
    }

    private void IsChildOutOfBounds(object? sender, object e)
    {
        MergeChips();
    }

    private Chip CreatePlaceHolder()
    {
        //The reason this is a chip and are flipping the opacity is to make sure the horizontal list item always stays the same height to make sure the UI does not bounce up and down when you add items.
        var placeHolder = new Chip() {Command = OpenCommand};
        placeHolder.SetBinding(Chip.TitleProperty,
            new Binding() {Path = nameof(Placeholder), Source = this});
        placeHolder.SetBinding(OpacityProperty,
            new Binding()
            {
                Path = nameof(Placeholder),
                Source = this,
                Converter = new IsEmptyToObjectConverter() {TrueObject = 0, FalseObject = 1}
            });
        placeHolder.SetBinding(Chip.HasCloseButtonProperty,
            new Binding()
            {
                Path = nameof(Placeholder),
                Source = this,
                Converter = new IsEmptyToObjectConverter() {TrueObject = true, FalseObject = false}
            });

        return placeHolder;
    }

    private void Dispose(object? sender, EventArgs e)
    {
        Unloaded -= Dispose;
        m_hStackLayout.ChildOutOfBounds -= IsChildOutOfBounds;
    }

    public void Open(MultiItemsPicker multiItemsPicker)
    {
        if (!BottomSheetService.IsBottomSheetOpen())
        {
            BottomSheetService.OpenBottomSheet(new MultiItemsPickerBottomSheet(multiItemsPicker));    
        }
    }

    public void DeSelectItem(object item)
    {
        if (SelectedItems == null) return;
        if (!SelectedItems.Contains(item)) return;

        var tempList = SelectedItems.ToList();
        tempList.Remove(item);
        SelectedItems = tempList;

        SelectedItemCommand?.Execute(SelectedItems);
        DidDeSelectItem?.Invoke(this, item);
    }

    public void SelectItem(object item)
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
        m_hStackLayout.Clear();

        if (SelectedItems == null || !SelectedItems.Any())
        {
            m_hStackLayout.Add(CreatePlaceHolder());
        }
        else
        {
            foreach (var selectedItem in SelectedItems)
            {
                var title = selectedItem.GetPropertyValue(ItemDisplayProperty);
                if (title == null) continue;
                var chip = new Chip
                {
                    Title = title,
                    Command = OpenCommand,
                    Margin = new Thickness(0, 0, Sizes.GetSize(SizeName.size_1), 0),
                    HasCloseButton = true,
                    CloseCommand = new Command(() => { DeSelectItem(selectedItem); })
                };

                m_hStackLayout.Add(chip);
            }
        }
    }

    private async void MergeChips()
    {
        if (SelectedItems == null)
        {
            return;
        }

        m_hStackLayout.Clear();
#if __ANDROID__
        await Task.Delay(5); //Because we are in a event that might ruin the stacklayout we have to wait for the stacklayout to be completely drawn.
#endif
        m_hStackLayout.Add(new Chip()
        {
            Title = SelectedItems.Count().ToString(),
            Command = OpenCommand,
            HasCloseButton = true,
            CloseCommand = new Command(() =>
            {
                if (SelectedItems == null)
                {
                    return;
                }

                foreach (var selectedItem in SelectedItems)
                {
                    DeSelectItem(selectedItem);
                }
            })
        });
    }
}