using DIPS.Mobile.UI.Components.Chips;
using Microsoft.Maui.Layouts;
using Colors = Microsoft.Maui.Graphics.Colors;
using HorizontalStackLayout = DIPS.Mobile.UI.Components.Lists.HorizontalStackLayout;
using VerticalStackLayout = DIPS.Mobile.UI.Components.Lists.VerticalStackLayout;

namespace DIPS.Mobile.UI.Components.ChipGroup;

public partial class ChipGroup : ContentView
{
    private readonly List<ChipGroupItem> m_selectedItems = new();
    private readonly FlexLayout m_flexLayout = new () { Wrap = FlexWrap.Wrap, Direction = FlexDirection.Column};
    // private IEnumerable<Chip>? m_chips;
    // private int m_column;
    // private double m_currentRowWidth;
    // private int m_row;

    public ChipGroup()
    { 
        Content = m_flexLayout;
        BackgroundColor = Colors.Red;
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
                Title = obj.GetPropertyValue(ItemDisplayProperty) ?? string.Empty, IsToggleable = true
            };
            chip.Command = new Command(_ => ChipToggled(new ChipGroupItem(chip, obj!)));
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

        SelectedItems = m_selectedItems.Select(item => item.Obj);
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