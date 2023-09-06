using DIPS.Mobile.UI.Components.BottomSheets;
using DIPS.Mobile.UI.Components.Pickers;
using DIPS.Mobile.UI.Effects.Touch;
using Colors = DIPS.Mobile.UI.Resources.Colors.Colors;
using Image = DIPS.Mobile.UI.Components.Images.Image.Image;
using Label = DIPS.Mobile.UI.Components.Labels.Label;

namespace DIPS.Mobile.UI.Components.Sorting;

public partial class SortControl : HorizontalStackLayout
{
    private readonly Label m_selectedItemText;
    private readonly Image m_sortImage;
    
    private object? m_selectedItem;
    private SortOrder m_currentSortOrder;

    private bool m_handlerInitialized;

    public SortControl()
    {
        Spacing = Sizes.GetSize(SizeName.size_1);
        
        Touch.SetCommand(this, new Command(OpenBottomSheet));
        
        m_selectedItemText = new Label
        {
            FontAttributes = FontAttributes.Bold,
            FontSize = 11,
            TextTransform = TextTransform.Uppercase,
            TextColor = Colors.GetColor(ColorName.color_primary_90),
            VerticalTextAlignment = TextAlignment.Center
        };

        m_sortImage = new Image
        {
            TintColor = Colors.GetColor(ColorName.color_primary_90),
            VerticalOptions = LayoutOptions.Center,
            WidthRequest = Sizes.GetSize(SizeName.size_5),
            HeightRequest = Sizes.GetSize(SizeName.size_5)
        };
        
        OnSortOrderChanged();
        
        Add(m_selectedItemText);
        Add(m_sortImage);
    }

    private void OpenBottomSheet()
    {
        BottomSheetService.OpenBottomSheet(new SortControlBottomSheet(this));
    }
    
    public void ItemSelected(SelectableItemViewModel item)
    {
        // Change sort order if tapped on already selected item
        if (item.Item == SelectedItem)
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

        BottomSheetService.CloseCurrentBottomSheet();
        
        SelectedItemCommand.Execute((SelectedItem, CurrentSortOrder));
    }

    protected override void OnHandlerChanged()
    {
        base.OnHandlerChanged();

        m_handlerInitialized = true;

        if (InitialSelectedItem is not null)
        {
            SelectedItem = InitialSelectedItem;
        }
        else
        {
            SelectedItem = ItemsSource.FirstOrDefault();
        }

        if (InitialSortOrder is not null)
        {
            CurrentSortOrder = (SortOrder)InitialSortOrder;
        }
    }

    private void OnItemsSourceChanged()
    {
        if(InitialSelectedItem is not null || !m_handlerInitialized)
            return;

        SelectedItem = ItemsSource.FirstOrDefault();
    }

    private void OnSelectedItemChanged()
    {
        m_selectedItemText.Text = SelectedItem.GetPropertyValue(ItemDisplayProperty);
    }

    private void OnSortOrderChanged()
    {
        m_sortImage.Source = CurrentSortOrder == SortOrder.Ascending
            ? Icons.GetIcon(IconName.ascending_fill)
            : Icons.GetIcon(IconName.descending_fill);
    }
}