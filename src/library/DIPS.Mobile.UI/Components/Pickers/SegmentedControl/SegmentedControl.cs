using DIPS.Mobile.UI.API.Vibration;
using DIPS.Mobile.UI.Converters.ValueConverters;
using DIPS.Mobile.UI.Internal;
using DIPS.Mobile.UI.Resources.Styles;
using DIPS.Mobile.UI.Resources.Styles.Label;
using Microsoft.Maui.Controls.Shapes;
using CollectionView = Microsoft.Maui.Controls.CollectionView;
using Colors = Microsoft.Maui.Graphics.Colors;
using Label = DIPS.Mobile.UI.Components.Labels.Label;
using Image = DIPS.Mobile.UI.Components.Images.Image.Image;
using HorizontalStackLayout = DIPS.Mobile.UI.Components.Lists.HorizontalStackLayout;
using Touch = DIPS.Mobile.UI.Effects.Touch.Touch;

namespace DIPS.Mobile.UI.Components.Pickers.SegmentedControl;

[ContentProperty(nameof(ItemsSource))]
public partial class SegmentedControl : ContentView
{
    private readonly HorizontalStackLayout m_horizontalStackLayout;
    private List<SelectableItemViewModel> m_allSelectableItems = new();

    public SegmentedControl()
    {
        m_horizontalStackLayout = new HorizontalStackLayout
        {
            AutomationId = "HorizontalStackLayout".ToDUIAutomationId<SegmentedControl>(),
            VerticalOptions = LayoutOptions.Start,
            HorizontalOptions = LayoutOptions.Start,
        };
        
        BindableLayout.SetItemTemplate(m_horizontalStackLayout, new DataTemplate(CreateSegment));

        Content = new ScrollView
        {
            Content = m_horizontalStackLayout,
            HorizontalScrollBarVisibility = ScrollBarVisibility.Never,
            VerticalScrollBarVisibility = ScrollBarVisibility.Never,
            Orientation = ScrollOrientation.Horizontal
        };
    }

    private View CreateSegment()
    {
        var border = new Border
        {
            VerticalOptions = LayoutOptions.Center,
            StrokeThickness = Sizes.GetSize(SizeName.stroke_medium),
            Stroke = SegmentBorderColor,
            StrokeShape = new RoundRectangle
            {
                CornerRadius = new CornerRadius(Sizes.GetSize(SizeName.radius_xlarge), 0,
                    Sizes.GetSize(SizeName.radius_xlarge), 0),
                StrokeThickness = 0

            }
        };
        
        Touch.SetCommand(border, new Command(() => OnItemTouched((SelectableItemViewModel)border.BindingContext)));
        border.SetBinding(BackgroundProperty, static (SelectableItemViewModel selectableItemViewModel) => selectableItemViewModel.IsSelected, converter: new BoolToObjectConverter
        {
            TrueObject = SelectedColor,
            FalseObject = DeSelectedColor
        });
        var horizontalStackLayout = new HorizontalStackLayout()
        {
            VerticalOptions = LayoutOptions.Center,
            Spacing = Sizes.GetSize(SizeName.content_margin_xsmall)
        };
        horizontalStackLayout.SetBinding(PaddingProperty, static (SelectableItemViewModel selectableItemViewModel) => selectableItemViewModel.IsSelected, converter: new BoolToObjectConverter
        {
            TrueObject = new Thickness(Sizes.GetSize(SizeName.content_margin_medium), Sizes.GetSize(SizeName.content_margin_small)),
            FalseObject = new Thickness(Sizes.GetSize(SizeName.content_margin_large), Sizes.GetSize(SizeName.content_margin_small))
        });
        var checkedImage = new Image()
        {
            Source = Icons.GetIcon(IconName.check_line),
            WidthRequest = Sizes.GetSize(SizeName.size_3),
            HeightRequest = Sizes.GetSize(SizeName.size_3),
        };
        checkedImage.SetBinding(IsVisibleProperty, static (SelectableItemViewModel selectableItemViewModel) => selectableItemViewModel.IsSelected);
        var label = new Label() {VerticalTextAlignment = TextAlignment.Center, Style = Styles.GetLabelStyle(LabelStyle.Body200)};
        label.SetBinding(Microsoft.Maui.Controls.Label.TextProperty, static (SelectableItemViewModel selectableItemViewModel) => selectableItemViewModel.DisplayName);

        horizontalStackLayout.Add(checkedImage);
        horizontalStackLayout.Add(label);
        border.Content = horizontalStackLayout;
        border.SizeChanged += ((sender, _) =>
        {
            if (sender is not View view) return;

            if (view.BindingContext is not SelectableItemViewModel selectableListItem) return;

            var radius = Sizes.GetSize(SizeName.radius_xlarge);
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
        BindableLayout.SetItemsSource(m_horizontalStackLayout, m_allSelectableItems);
    }
}