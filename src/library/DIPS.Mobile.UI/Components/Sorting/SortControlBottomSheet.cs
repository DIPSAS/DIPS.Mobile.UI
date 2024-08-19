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
        
        Padding = new Thickness(Sizes.GetSize(SizeName.size_2), Sizes.GetSize(SizeName.size_4), Sizes.GetSize(SizeName.size_2), Sizes.GetSize(SizeName.size_4));
        
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
            TintColor = Colors.GetColor(ColorName.color_neutral_80)
        };

        var imageBorder = new Border
        {
            AutomationId = "ImageBorder".ToDUIAutomationId<SortControlBottomSheet>(),
            StrokeShape = new Ellipse(), 
            BackgroundColor = Colors.GetColor(ColorName.color_secondary_20),
            Content = inLineImage,
            Padding = Sizes.GetSize(SizeName.size_1)
        };

        var contentView = new ContentView
        {
            AutomationId = "ContentView".ToDUIAutomationId<SortControlBottomSheet>(),
            WidthRequest = Sizes.GetSize(SizeName.size_8), 
            HeightRequest = Sizes.GetSize(SizeName.size_8),
            Content = imageBorder
        };
        
        imageBorder.SetBinding(IsVisibleProperty, new Binding(nameof(SelectableItemViewModel.IsSelected)));

        radioButtonListItem.TitleOptions = new TitleOptions { Width = GridLength.Star };
        
        radioButtonListItem.InLineContent = contentView;
        radioButtonListItem.InLineContentOptions = new InLineContentOptions { Width = GridLength.Auto };

        radioButtonListItem.IconOptions = new IconOptions { Color = Colors.GetColor(ColorName.color_neutral_80) };
        radioButtonListItem.DividersOptions = new DividersOptions
        {
            BottomDividerMargin = new Thickness(Sizes.GetSize(SizeName.size_13), Sizes.GetSize(SizeName.size_0),
                Sizes.GetSize(SizeName.size_0), Sizes.GetSize(SizeName.size_0))
        };
        
        radioButtonListItem.SetBinding(ListItem.CommandParameterProperty, new Binding(nameof(BindingContext), source: new RelativeBindingSource(RelativeBindingSourceMode.Self)));
        radioButtonListItem.SetBinding(ListItem.TitleProperty, new Binding(nameof(SelectableItemViewModel.DisplayName)));
        radioButtonListItem.SetBinding(RadioButtonListItem.IsSelectedProperty, new Binding(nameof(SelectableItemViewModel.IsSelected)));
        radioButtonListItem.SetBinding(ListItem.HasBottomDividerProperty, new Binding(nameof(SelectableSortOptionViewModel.IsLastItem), converter: new InvertedBoolConverter()));
        
        return radioButtonListItem;
    }

    
}