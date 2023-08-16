using DIPS.Mobile.UI.Components.BottomSheets;
using DIPS.Mobile.UI.Components.Chips;
using DIPS.Mobile.UI.Converters.ValueConverters;
using DIPS.Mobile.UI.Effects.Touch;
using HorizontalStackLayout = DIPS.Mobile.UI.Components.Lists.HorizontalStackLayout;

namespace DIPS.Mobile.UI.Components.Pickers.MultiItemsPicker;

public partial class MultiItemsPicker : ContentView
{
    private HorizontalStackLayout m_hStackLayout;
    private DisplayOrientation m_currentDeviceOrientation;

    public MultiItemsPicker()
    {
        OpenCommand = new Command(Open);
        BackgroundColor = Microsoft.Maui.Graphics.Colors.Transparent;

        m_hStackLayout = new HorizontalStackLayout()
        {
            VerticalOptions = LayoutOptions.Start
        };
        CreatePlaceHolder();
        m_hStackLayout.Add(CreatePlaceHolder());

        Touch.SetCommand(this,
            OpenCommand);
        m_hStackLayout.ChildOutOfBounds += IsChildOutOfBounds;
        Unloaded += Dispose;

        Content = m_hStackLayout;

        DeviceDisplay.Current.MainDisplayInfoChanged += MainDisplayInfoChanged;
        m_currentDeviceOrientation = DeviceDisplay.Current.MainDisplayInfo.Orientation;
    }


    private void MainDisplayInfoChanged(object? sender, DisplayInfoChangedEventArgs e)
    {
        if (e.DisplayInfo.Orientation == m_currentDeviceOrientation)
        {
            return;
        }

        OnSelectedItemsChanged(); //This triggers a redraw and it will make the UI look good for people when they change orientation
        m_currentDeviceOrientation = e.DisplayInfo.Orientation;
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
        DeviceDisplay.Current.MainDisplayInfoChanged -= MainDisplayInfoChanged;
    }

    public void Open()
    {
        if (!BottomSheetService.IsBottomSheetOpen())
        {
            BottomSheetService.OpenBottomSheet(new MultiItemsPickerBottomSheet(this));
        }
    }

    public void DeSelectItem(object item)
    {
        if (SelectedItems == null) return;
        var selectedItems = SelectedItems.Cast<object?>().ToList();
        if (!selectedItems.Contains(item)) return;

        var tempList = selectedItems.ToList();
        tempList.Remove(item);
        SelectedItems = tempList;

        SelectedItemsCommand?.Execute(SelectedItems);
        DidDeSelectItem?.Invoke(this, item);
    }

    public void SelectItem(object item)
    {
        if (SelectedItems == null)
        {
            SelectedItems = new List<object> {item};
        }
        else
        {
            var selectedItems = SelectedItems.Cast<object?>().ToList();
            if (!selectedItems.Any())
            {
                SelectedItems = new List<object> {item};
                return;
            }
            
            if (selectedItems.Contains(item)) return;

            var tempList = selectedItems.ToList();
            tempList.Add(item);
            SelectedItems = tempList;
        }

        SelectedItemsCommand?.Execute(SelectedItems);
        DidSelectItem?.Invoke(this, item);
    }

    private void OnSelectedItemsChanged()
    {
        m_hStackLayout.Clear();

        if (SelectedItems == null)
        {
            m_hStackLayout.Add(CreatePlaceHolder());
        }
        else
        {
            var selectedItems = SelectedItems.Cast<object?>().ToList();
            if (!selectedItems.Any())
            {
                m_hStackLayout.Add(CreatePlaceHolder());
                return;
            }
            
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
        var chipIdentifier = "MergedCounterChip";
        if (SelectedItems == null)
        {
            return;
        }

        Chip? mergedChip;
        var selectedItems = SelectedItems.Cast<object?>().ToList();
        var numberOfSelectedItems = selectedItems.Count().ToString();
        var view = m_hStackLayout.FirstOrDefault(v => v.AutomationId == chipIdentifier); //Check if it already exists.
        
        if (view is Chip chip) //No need to redraw if it already exists. Redrawing leads to a small visual glitch, especially for Android.
        {
            mergedChip = chip;
            mergedChip.Title = numberOfSelectedItems;
            return;
        }

        mergedChip = new Chip() {AutomationId = chipIdentifier, Title = numberOfSelectedItems, Command = OpenCommand,};
        m_hStackLayout.Clear();
#if __ANDROID__
        await Task.Delay(5); //Because we are in a event that might ruin the stacklayout we have to wait for the stacklayout to be completely drawn.
#endif
        m_hStackLayout.Add(mergedChip);
    }
}