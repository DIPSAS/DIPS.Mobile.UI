using DIPS.Mobile.UI.Components.BottomSheets;
using DIPS.Mobile.UI.Components.Pickers;
using DIPS.Mobile.UI.Effects.Touch;
using DIPS.Mobile.UI.Internal;
using DIPS.Mobile.UI.Resources.LocalizedStrings.LocalizedStrings;
using DIPS.Mobile.UI.Resources.Styles;
using DIPS.Mobile.UI.Resources.Styles.Label;
using Colors = DIPS.Mobile.UI.Resources.Colors.Colors;
using Image = DIPS.Mobile.UI.Components.Images.Image.Image;
using Label = DIPS.Mobile.UI.Components.Labels.Label;

namespace DIPS.Mobile.UI.Components.Sorting;

public partial class SortControl : Grid
{
    private readonly Label m_selectedItemText;
    private readonly Image m_sortImage;
    
    private object? m_selectedItem;
    private SortOrder m_currentSortOrder;

    public SortControl()
    {
        AddColumnDefinition(new ColumnDefinition(GridLength.Auto));
        AddColumnDefinition(new ColumnDefinition(GridLength.Star));
        
        ColumnSpacing = Sizes.GetSize(SizeName.content_margin_xsmall);
        
        Touch.SetCommand(this, new Command(OpenBottomSheet));
        
        m_sortImage = new Image
        {
            AutomationId = "SortImage".ToDUIAutomationId<SortControl>(),
            TintColor = Colors.GetColor(ColorName.color_icon_action),
            VerticalOptions = LayoutOptions.Center,
            WidthRequest = Sizes.GetSize(SizeName.size_5),
            HeightRequest = Sizes.GetSize(SizeName.size_5)
        };
        
        m_selectedItemText = new Label
        {
            AutomationId = "SelectedItemText".ToDUIAutomationId<SortControl>(),
            Style = Styles.GetLabelStyle(LabelStyle.UI200),
            TextColor = Colors.GetColor(ColorName.color_text_action),
            VerticalTextAlignment = TextAlignment.Center,
            MaxLines = 1,
            LineBreakMode = LineBreakMode.TailTruncation
        };

        
        OnSortOrderChanged();
        
        this.Add(m_sortImage, 0);
        this.Add(m_selectedItemText, 1);
    }

    private void OpenBottomSheet()
    {
        BottomSheetService.Open(new SortControlBottomSheet(this));
    }
    
    public void ItemSelected(SelectableItemViewModel item)
    {
        // Change sort order if tapped on already selected item
        if (item.Item.Equals(SelectedItem))
        {
            CurrentSortOrder = CurrentSortOrder switch
            {
                SortOrder.Ascending => SortOrder.Descending,
                SortOrder.Descending => SortOrder.Ascending
            };
        }
        else
        {
            SelectedItem = item.Item;
        }

        BottomSheetService.CloseAll();
        
        SelectedItemCommand.Execute((SelectedItem, CurrentSortOrder));
    }

    private void OnItemsSourceChanged()
    {
        if(InitialSelectedItem is not null)
            return;

        SelectedItem = ItemsSource?.FirstOrDefault();
    }

    private void OnSelectedItemChanged()
    {
        m_selectedItemText.Text = SelectedItem?.GetPropertyValue(ItemDisplayProperty);
    }

    private void OnSortOrderChanged()
    {
        m_sortImage.Source = CurrentSortOrder == SortOrder.Ascending
            ? Icons.GetIcon(IconName.ascending_fill)
            : Icons.GetIcon(IconName.descending_fill);
    }
    
    public static string GetLocalizedSortOrderDescription(SortOrder sortOrder)
    {
        return sortOrder switch
        {
            SortOrder.Ascending => DUILocalizedStrings.Ascending,
            SortOrder.Descending => DUILocalizedStrings.Descending,
            _ => string.Empty
        };
    }
}