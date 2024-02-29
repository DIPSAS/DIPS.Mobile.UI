using DIPS.Mobile.UI.Components.Chips;
using Microsoft.Extensions.Logging.Abstractions;
using Microsoft.Maui.Layouts;
using Colors = Microsoft.Maui.Graphics.Colors;
using HorizontalStackLayout = DIPS.Mobile.UI.Components.Lists.HorizontalStackLayout;
using VerticalStackLayout = DIPS.Mobile.UI.Components.Lists.VerticalStackLayout;

namespace DIPS.Mobile.UI.Components.ChipGroup;

public partial class ChipGroup : ContentView
{
    private readonly List<ChipGroupItem> m_selectedItems = [];
    private readonly FlexLayout m_flexLayout = new () { Wrap = FlexWrap.Wrap, Direction = FlexDirection.Row, AlignItems = FlexAlignItems.Start};
    private readonly List<ChipGroupItem> m_chipItems = [];


    public ChipGroup()
    { 
        Content = m_flexLayout;
    }

    protected override void OnHandlerChanged()
    {
        base.OnHandlerChanged();

        if (SelectedItems is not null)
        {
            SetChipsToggledBasedOnSelectedItems();
        }
    }

    private void SetChipsToggledBasedOnSelectedItems()
    {
        var selectedItemList = SelectedItems?.Cast<object>().ToList();
        
        if(selectedItemList is null)
            return;

        if (SelectionMode == ChipGroupSelectionMode.Single)
        {
            var selectedItem = selectedItemList.FirstOrDefault();
            if (selectedItem is not null)
            {
                SetChipToggledBasedOnSelectedItem(selectedItem);
            }
        }
        else
        {
            selectedItemList.ForEach(SetChipToggledBasedOnSelectedItem);
        }
    }

    private void SetChipToggledBasedOnSelectedItem(object selectedItem)
    {
        var chipItem = m_chipItems.FirstOrDefault(chipItem => chipItem.Obj.GetPropertyValue(ItemDisplayProperty)!.Equals(selectedItem.GetPropertyValue(ItemDisplayProperty)));
        if (chipItem is not null)
        {
            chipItem.Chip.IsToggled = true;
            m_selectedItems.Add(chipItem);
        }
    }

    private void OnItemsSourceChanged()
    {
        var list = ItemsSource?.Cast<object?>().ToList();
        if (list is null)
        {
            return;
        }

        var count = list.Count;
        if (count is 0)
        {
            return;
        }

        list.ForEach(obj =>
        {
            var chip = new Chip
            {
                Title = obj.GetPropertyValue(ItemDisplayProperty) ?? string.Empty, 
                IsToggleable = true, 
                Margin = 2
            };
            var item = new ChipGroupItem(chip, obj!);
            chip.Command = new Command(_ => ChipToggled(item));
            
            m_chipItems.Add(item);
            m_flexLayout.Add(chip);
        });
    }

    // private void ChipOnSizeChanged(object? sender, EventArgs e)
    // {
    //     if (sender is not Chip chip)
    //     {
    //         return;
    //     }
    //
    //     m_currentRowWidth += chip.DesiredSize.Width;
    //     m_firstHorizontalStackLayout.Remove(chip);
    //     
    //     if (m_firstHorizontalStackLayout.Count is 0)
    //         m_flexLayout.Remove(m_firstHorizontalStackLayout);
    //
    //     if (m_currentRowWidth > Width)
    //     {
    //         m_flexLayout.Add(m_currentHorizontalStackLayout = []);
    //         m_currentRowWidth = 0;
    //     }
    //
    //     m_currentHorizontalStackLayout.Add(chip);
    //     chip.SizeChanged -= ChipOnSizeChanged;
    // }


    private void ChipToggled(ChipGroupItem chipGroupItem)
    {
        if (SelectionMode is ChipGroupSelectionMode.Single)
        {
            if (m_selectedItems.FirstOrDefault(item => item.Chip.Equals(chipGroupItem.Chip)) != null)
            {
                chipGroupItem.Chip.IsToggled = true;
                return;
            }

            m_selectedItems.ForEach(item => item.Chip.IsToggled = false);
            m_selectedItems.Clear();
        }

        if (chipGroupItem.Chip.IsToggled)
        {
            m_selectedItems.Add(chipGroupItem);
        }
        else
        {
            m_selectedItems.Remove(chipGroupItem);
        }

        SelectedItems = m_selectedItems.Select(item => item.Obj).ToList();
    }
}

public class ChipGroupItem
{
    public ChipGroupItem(Chip chip, object obj)
    {
        Chip = chip;
        Obj = obj;
    }

    public Chip Chip { get; }
    public object Obj { get; }
}