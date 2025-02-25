using DIPS.Mobile.UI.API.Vibration;
using DIPS.Mobile.UI.Components.BottomSheets;
using DIPS.Mobile.UI.Components.ListItems;
using DIPS.Mobile.UI.Components.ListItems.Extensions;
using DIPS.Mobile.UI.Components.ListItems.Options.Dividers;
using DIPS.Mobile.UI.Components.ListItems.Options.Icon;
using DIPS.Mobile.UI.Components.ListItems.Options.InLineContent;
using DIPS.Mobile.UI.Components.ListItems.Options.Title;
using DIPS.Mobile.UI.Components.Pickers;
using DIPS.Mobile.UI.Converters.ValueConverters;
using DIPS.Mobile.UI.Internal;
using DIPS.Mobile.UI.Resources.LocalizedStrings.LocalizedStrings;
using Microsoft.Maui.Controls.Shapes;
using CollectionView = DIPS.Mobile.UI.Components.Lists.CollectionView;
using Colors = DIPS.Mobile.UI.Resources.Colors.Colors;
using Image = DIPS.Mobile.UI.Components.Images.Image.Image;

namespace DIPS.Mobile.UI.Components.Sorting;

internal class SortControlBottomSheet : BottomSheet
{
    private readonly SortControl m_sortControl;

    private readonly CollectionView m_collectionView = new() { ItemSpacing = Sizes.GetSize(SizeName.size_0)};

    public SortControlBottomSheet(SortControl sortControl)
    {
        m_sortControl = sortControl;
        
        if(m_sortControl.ItemsSource == null || !m_sortControl.ItemsSource.Any())
            return;

        Title = DUILocalizedStrings.Sort;
        
        Padding = new Thickness(Sizes.GetSize(SizeName.content_margin_small), Sizes.GetSize(SizeName.content_margin_large), Sizes.GetSize(SizeName.content_margin_small), Sizes.GetSize(SizeName.content_margin_large));
        
        List<SelectableItemViewModel> selectableViewModels = new();
        foreach (var item in m_sortControl.ItemsSource)
        {
            selectableViewModels.Add(new SelectableSortOptionViewModel(
                item.GetPropertyValue(m_sortControl.ItemDisplayProperty)!,
                item.Equals(m_sortControl.SelectedItem), item, m_sortControl.ItemsSource.LastOrDefault()!.Equals(item)));
        }

        m_collectionView.ItemsSource = selectableViewModels;
        m_collectionView.ItemTemplate = new DataTemplate(LoadTemplate);
        
        Content = m_collectionView;
    }

    private object LoadTemplate()
    {
        var radioButtonListItem = new RadioButtonListItem
        {
            AutomationId = "RadioButtonListItem".ToDUIAutomationId<SortControlBottomSheet>(),
            VerticalOptions = LayoutOptions.Center,
            Command = new Command<SelectableItemViewModel>( selectableItemViewModel  =>
            {
                if (m_sortControl.HasHaptics)
                {
                    VibrationService.SelectionChanged();    
                }
                
                m_sortControl.ItemSelected(selectableItemViewModel);
            })
        };

        var inLineImage = new Image
        {
            AutomationId = "InLineImage".ToDUIAutomationId<SortControlBottomSheet>(),
            Source = m_sortControl.CurrentSortOrder == SortOrder.Ascending
                ? Icons.GetIcon(IconName.ascending_fill)
                : Icons.GetIcon(IconName.descending_fill),
            TintColor = Colors.GetColor(ColorName.color_icon_action)
        };

        var imageBorder = new Border
        {
            AutomationId = "ImageBorder".ToDUIAutomationId<SortControlBottomSheet>(),
            StrokeShape = new Ellipse(), 
            BackgroundColor = Colors.GetColor(ColorName.color_surface_active),
            Content = inLineImage,
            Padding = Sizes.GetSize(SizeName.content_margin_xsmall)
        };

        var contentView = new ContentView
        {
            AutomationId = "ContentView".ToDUIAutomationId<SortControlBottomSheet>(),
            WidthRequest = Sizes.GetSize(SizeName.size_8), 
            HeightRequest = Sizes.GetSize(SizeName.size_8),
            Content = imageBorder
        };
        
        imageBorder.SetBinding(IsVisibleProperty, static (SelectableItemViewModel selectableItemViewModel) => selectableItemViewModel.IsSelected);

        radioButtonListItem.TitleOptions = new TitleOptions { Width = GridLength.Star };
        
        radioButtonListItem.InLineContent = contentView;
        radioButtonListItem.InLineContentOptions = new InLineContentOptions { Width = GridLength.Auto };

        radioButtonListItem.IconOptions = new IconOptions { Color = Colors.GetColor(ColorName.color_icon_default) };
        radioButtonListItem.DividersOptions = new DividersOptions
        {
            BottomDividerMargin = new Thickness(Sizes.GetSize(SizeName.size_13), Sizes.GetSize(SizeName.size_0),
                Sizes.GetSize(SizeName.size_0), Sizes.GetSize(SizeName.size_0))
        };
        
        radioButtonListItem.SetBinding(ListItem.CommandParameterProperty, static (RadioButtonListItem radioButtonListItem) => radioButtonListItem.BindingContext, source: new RelativeBindingSource(RelativeBindingSourceMode.Self));
        radioButtonListItem.SetBinding(ListItem.TitleProperty, static (SelectableItemViewModel selectableItemViewModel) => selectableItemViewModel.DisplayName);
        radioButtonListItem.SetBinding(RadioButtonListItem.IsSelectedProperty, static (SelectableItemViewModel selectableItemViewModel) => selectableItemViewModel.IsSelected);
        radioButtonListItem.SetBinding(ListItem.HasBottomDividerProperty, static (SelectableSortOptionViewModel selectableSortOptionViewModel) => selectableSortOptionViewModel.IsLastItem, converter: new InvertedBoolConverter());
        
        return radioButtonListItem;
    }

    
}