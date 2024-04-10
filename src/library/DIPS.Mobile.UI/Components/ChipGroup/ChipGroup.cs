using DIPS.Mobile.UI.Components.Chips;
using DIPS.Mobile.UI.Resources.Styles;
using DIPS.Mobile.UI.Resources.Styles.Chip;
using Microsoft.Maui.Layouts;

namespace DIPS.Mobile.UI.Components.ChipGroup;

public partial class ChipGroup : ContentView
{
    private readonly List<ChipGroupItem> m_selectedItems = [];
    private readonly FlexLayout m_flexLayout = new () { Wrap = FlexWrap.Wrap, Direction = FlexDirection.Row, AlignItems = FlexAlignItems.Start };
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

        if (selectedItemList is null || selectedItemList.Count is 0)
        {
            m_selectedItems.ForEach(item => item.Chip.IsToggled = false);
            m_selectedItems.Clear();
            return;
        }

        if (SelectionMode == ChipGroupSelectionMode.Single)
        {
            var selectedItem = selectedItemList.FirstOrDefault();
            if (selectedItem is not null)
            {
                var chipItem = m_chipItems.FirstOrDefault(chipItem => chipItem.Obj.GetPropertyValue(ItemDisplayProperty)!.Equals(selectedItem.GetPropertyValue(ItemDisplayProperty)));
                chipItem!.Chip.IsToggled = true;
                ChipToggled(chipItem!, false);
            }
        }
        else
        {
            selectedItemList.ForEach(item =>
            {
                var chipItem = m_chipItems.FirstOrDefault(chipItem => chipItem.Obj.GetPropertyValue(ItemDisplayProperty)!.Equals(item.GetPropertyValue(ItemDisplayProperty)));
                chipItem!.Chip.IsToggled = true;
                ChipToggled(chipItem!, false);
            });
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
                Style = Styles.GetChipStyle(ChipStyle.EmptyInput),
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

    private void ChipToggled(ChipGroupItem chipGroupItem, bool didTap = true)
    {
        if (SelectionMode is ChipGroupSelectionMode.Single)
        {
            if (m_selectedItems.FirstOrDefault(item => item.Chip.Title.Equals(chipGroupItem.Chip.Title)) != null)
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

        if (didTap)
        {
            var selectedItems = m_selectedItems.Select(groupItem => groupItem.Obj).ToList();
            OnSelectedItemsChanged?.Invoke(this, new ChipGroupEventArgs(selectedItems));
            SelectedItems = selectedItems;
        }
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