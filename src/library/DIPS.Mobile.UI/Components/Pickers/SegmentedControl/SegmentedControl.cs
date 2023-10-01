using DIPS.Mobile.UI.API.Vibration;
using DIPS.Mobile.UI.Converters.ValueConverters;
using Microsoft.Maui.Controls.Shapes;
using Label = DIPS.Mobile.UI.Components.Labels.Label;
using Image = DIPS.Mobile.UI.Components.Images.Image.Image;
using CollectionView = DIPS.Mobile.UI.Components.Lists.CollectionView;
using Touch = DIPS.Mobile.UI.Effects.Touch.Touch;

namespace DIPS.Mobile.UI.Components.Pickers.SegmentedControl;

[ContentProperty(nameof(ItemsSource))]
public partial class SegmentedControl : ContentView
{
    private readonly CollectionView m_collectionView;
    private List<SelectableItemViewModel> m_allSelectableItems = new();


    public SegmentedControl()
    {
        m_collectionView = new CollectionView()
        {
            VerticalOptions = LayoutOptions.Start,
            HorizontalOptions = LayoutOptions.Start,
            ItemsLayout = new LinearItemsLayout(ItemsLayoutOrientation.Horizontal) {ItemSpacing = 0},
            HeightRequest = Sizes.GetSize(SizeName.size_12),
            HorizontalScrollBarVisibility = ScrollBarVisibility.Never,
            VerticalScrollBarVisibility = ScrollBarVisibility.Never,
            ItemSpacing = 0,
            ShouldBounce = false
        };
        m_collectionView.ItemTemplate = new DataTemplate(CreateSegment);

        Content = m_collectionView;
    }

    private View CreateSegment()
    {
        var border = new Border()
        {
// #if __ANDROID__ //TODO: On Android, each item has spacing in between them, so we move them a tiny bit to the right
//             Margin = new Thickness(0, 0, -2, 0),
// #endif
            VerticalOptions = LayoutOptions.Center,
            HeightRequest = Sizes.GetSize(SizeName.size_10),
            StrokeThickness = 1,
            Stroke = SegmentBorderColor,
            StrokeShape = new RoundRectangle()
            {
                CornerRadius = new CornerRadius(Sizes.GetSize(SizeName.size_8), 0,
                    Sizes.GetSize(SizeName.size_8), 0),
                StrokeThickness = 0

            }
        };
        
        Touch.SetCommand(border, new Command(() => OnItemTouched((SelectableItemViewModel)border.BindingContext)));
        border.SetBinding(BackgroundProperty,
            new Binding()
            {
                Path = nameof(SelectableItemViewModel.IsSelected),
                Converter = new BoolToObjectConverter()
                {
                    TrueObject = SelectedColor,
                    FalseObject = DeSelectedColor
                }
            });
        var grid = new Grid()
        {
            VerticalOptions = LayoutOptions.Center,
            ColumnDefinitions = new ColumnDefinitionCollection() {new(GridLength.Auto), new(GridLength.Auto)},
            ColumnSpacing = 0,
        };
        grid.SetBinding(PaddingProperty,
            new Binding()
            {
                Path = nameof(SelectableItemViewModel.IsSelected),
                Converter = new BoolToObjectConverter()
                {
                    TrueObject =
                        new Thickness(Sizes.GetSize(SizeName.size_2), Sizes.GetSize(SizeName.size_2),
                            Sizes.GetSize(SizeName.size_2), Sizes.GetSize(SizeName.size_2)),
                    FalseObject = new Thickness(Sizes.GetSize(SizeName.size_4), Sizes.GetSize(SizeName.size_2)),
                }
            });
        var checkedImage = new Image()
        {
            Source = Icons.GetIcon(IconName.check_line),
            WidthRequest = Sizes.GetSize(SizeName.size_3),
            HeightRequest = Sizes.GetSize(SizeName.size_3),
            Margin = new Thickness(0, 0, Sizes.GetSize(SizeName.size_1)-0.2, 0) //Small 0.2 hack here to make sure the entire border is not larger when the image is visible, The total size from the label to the border should be size_8
        };
        checkedImage.SetBinding(IsVisibleProperty, new Binding() {Path = nameof(SelectableItemViewModel.IsSelected)});
        var label = new Label() {VerticalTextAlignment = TextAlignment.Center};
        label.SetBinding(Microsoft.Maui.Controls.Label.TextProperty,
            new Binding() {Path = nameof(SelectableItemViewModel.DisplayName)});

        grid.Add(checkedImage, 0);
        grid.Add(label, 1);
        border.Content = grid;
        border.SizeChanged += ((sender, _) =>
        {
            if (sender is not View view) return;
            if (view.BindingContext is not SelectableItemViewModel selectableListItem) return;

            var radius = (double)Sizes.GetSize(SizeName.size_8);
            var roundRectangle = new RoundRectangle() { StrokeThickness = 0};
            if (m_allSelectableItems.Last() == selectableListItem)
            {
                roundRectangle.CornerRadius = new CornerRadius(0, radius, 0, radius);
            }
            else if (m_allSelectableItems.First() == selectableListItem)
            {
                roundRectangle.CornerRadius = new CornerRadius(radius, 0, radius, 0);
            }

            border.StrokeShape = roundRectangle;
        });

        return border;
    }

    private void OnItemTouched(SelectableItemViewModel selectableItemViewModel)
    {
        if (SelectionMode == SelectionMode.Single)
        {
            SelectItem(selectableItemViewModel);
        }

        if (SelectionMode == SelectionMode.Multi)
        {
            ToggleItem(selectableItemViewModel);
        }
        if (HasHaptics)
        {
            VibrationService.SelectionChanged();   
        }
    }

    private void SendDidDeSelect(object item)
    {
        DidDeSelectItem?.Invoke(this, item);
        DeSelectedItemCommand?.Execute(item);
    }

    private void SendDidSelect(object item)
    {
        DidSelectItem?.Invoke(this, item);
        SelectedItemCommand?.Execute(item);
    }

    private void ItemsSourceChanged()
    {
        if (ItemsSource == null) return;
        var listOfSelectableItems = new List<SelectableItemViewModel>();

        foreach (var item in ItemsSource.Cast<object>().ToList())
        {
            switch (SelectionMode)
            {
                case SelectionMode.Single:
                    AddItemToSelectableItemsToPickFromSingleMode(listOfSelectableItems, item);
                    break;
                case SelectionMode.Multi:
                    {
                        AddItemToSelectableItemsToPickFromMultiMode(listOfSelectableItems, item);
                        break;
                    }
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }


        m_allSelectableItems = listOfSelectableItems;
        m_collectionView.ItemsSource = m_allSelectableItems;
    }
}