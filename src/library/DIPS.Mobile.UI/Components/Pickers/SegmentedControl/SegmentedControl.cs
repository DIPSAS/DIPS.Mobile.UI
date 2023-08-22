using DIPS.Mobile.UI.Components.Pickers.ItemPicker;
using DIPS.Mobile.UI.Converters.ValueConverters;
using DIPS.Mobile.UI.Effects.Touch;
using Microsoft.Maui.Controls.Shapes;
using Label = DIPS.Mobile.UI.Components.Labels.Label;
using Colors = DIPS.Mobile.UI.Resources.Colors.Colors;
using Image = DIPS.Mobile.UI.Components.Images.Image.Image;
using CollectionView = DIPS.Mobile.UI.Components.Lists.CollectionView;

namespace DIPS.Mobile.UI.Components.Pickers.SegmentedControl;

[ContentProperty(nameof(ItemsSource))]
public partial class SegmentedControl : ContentView
{
    private readonly CollectionView m_collectionView;
    private List<SelectableListItem> m_allSelectableItems = new();
    private List<View> m_allVisualElements = new();

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
        };
        m_collectionView.ItemTemplate = new DataTemplate(CreateSegment);

        Content = m_collectionView;
    }

    /**
     *  <Border VerticalOptions="Start"
                            StrokeThickness="1"
                            Stroke="{dui:Colors color_neutral_30}"
                            BackgroundColor="{dui:Colors color_neutral_30}"
                            Padding="{dui:Thickness Left=size_4, Top=size_2, Right=size_4, Bottom=size_2}"
                            dui:Touch.Command="{Binding Source={RelativeSource AncestorType={x:Type CollectionView}}, Path=BindingContext.SelectedItemCommand}">
                        <!-- If item is first or last -->
                        <Border.StrokeShape>
                            <RoundRectangle CornerRadius="8" />
                        </Border.StrokeShape>
                        <Grid ColumnDefinitions="Auto, *"
                              ColumnSpacing="{dui:Sizes size_2}">
                            <dui:Image Source="{dui:Icons check_line}"
                                       HeightRequest="{dui:Sizes size_3}"
                                       WidthRequest="{dui:Sizes size_3}" />
                            <dui:Label Grid.Column="1"
                                       Text="{Binding Title}" />
                        </Grid>
                    </Border>
     */
    private View CreateSegment()
    {
        var border = new Border()
        {
            VerticalOptions = LayoutOptions.Center,
            HeightRequest = Sizes.GetSize(SizeName.size_10),
            StrokeThickness = 1,
#if __ANDROID__
            Margin =
                new Thickness(-2,
                    0), //TODO: Fix Dotnet 8. https://github.com/dotnet/maui/issues/7764, due to bug with MAUI when setting StrokeThickness it has extra margins on the horizontal plane
#endif
            Stroke = Colors.GetColor(ColorName.color_neutral_30),
            StrokeShape = new RoundRectangle()
            {
                CornerRadius = new CornerRadius((double)Sizes.GetSize(SizeName.size_8), 0,
                    (double)Sizes.GetSize(SizeName.size_8), 0),
                //TODO: Fix Dotnet 8. https://github.com/dotnet/maui/issues/7764, this makes sure theres no extra space between each segment
#if __ANDROID__
                StrokeThickness = 1
#elif __IOS__
                StrokeThickness = 0
#endif
            }
        };
        Touch.SetCommand(border, new Command(() => SelectItem((SelectableListItem)border.BindingContext)));
        border.SetBinding(BackgroundProperty,
            new Binding()
            {
                Path = nameof(SelectableListItem.IsSelected),
                Converter = new BoolToObjectConverter()
                {
                    TrueObject = Colors.GetColor(ColorName.color_neutral_30),
                    FalseObject = Colors.GetColor(ColorName.color_system_white)
                }
            });

        /* *  <Grid ColumnDefinitions="Auto, *"
                              ColumnSpacing="{dui:Sizes size_2}">
                            <dui:Image Source="{dui:Icons check_line}"
                                       HeightRequest="{dui:Sizes size_3}"
                                       WidthRequest="{dui:Sizes size_3}" />
                            <dui:Label Grid.Column="1"
                                       Text="{Binding Title}" />
                        </Grid>
         */
        var grid = new Grid()
        {
            VerticalOptions = LayoutOptions.Center,
            ColumnDefinitions = new ColumnDefinitionCollection() {new(GridLength.Auto), new(GridLength.Auto)},
            ColumnSpacing = 0,
        };
        grid.SetBinding(PaddingProperty,
            new Binding()
            {
                Path = nameof(SelectableListItem.IsSelected),
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
            Margin = new Thickness(0, 0, Sizes.GetSize(SizeName.size_1), 0)
        };
        checkedImage.SetBinding(IsVisibleProperty, new Binding() {Path = nameof(SelectableListItem.IsSelected)});
        var label = new Label() {VerticalTextAlignment = TextAlignment.Center};
        label.SetBinding(Microsoft.Maui.Controls.Label.TextProperty,
            new Binding() {Path = nameof(SelectableListItem.DisplayName)});

        grid.Add(checkedImage, 0);
        grid.Add(label, 1);
        border.Content = grid;
        m_allVisualElements.Add(border);
        border.SizeChanged += ((sender, args) =>
        {
            if (sender is not View view) return;
            if (view.BindingContext is not SelectableListItem selectableListItem) return;

            var radius = (double)Sizes.GetSize(SizeName.size_8);
            var roundRectangle = new RoundRectangle()
            {
                //TODO: Fix Dotnet 8. https://github.com/dotnet/maui/issues/7764, this makes sure theres no extra space between each segment
#if __ANDROID__
                StrokeThickness = 1
#elif __IOS__
                StrokeThickness = 0
#endif
            };
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

    private void SelectItem(SelectableListItem selectableListItem)
    {
        foreach (var selectableItem in m_allSelectableItems)
        {
            selectableItem.IsSelected = false;
        }

        selectableListItem.IsSelected = true;
        SelectedItem = selectableListItem.Item;
    }

    /**
     *  <CollectionView VerticalOptions="Start"
                        HorizontalOptions="Start">
            <CollectionView.ItemsLayout>
                <LinearItemsLayout Orientation="Horizontal" />
            </CollectionView.ItemsLayout>
            <CollectionView.ItemsSource>
                <x:Array Type="{x:Type dui:SegmentControl}">
                    <dui:SegmentControl Title="Ikke utført" IsSelected="True" />
                    <dui:SegmentControl Title="Siste 9 dager" />
                    <dui:SegmentControl Title="Siste 30 dager" />
                    <dui:SegmentControl Title="Siste 90 dager" />
                </x:Array>
            </CollectionView.ItemsSource>

            <CollectionView.ItemTemplate>
                <DataTemplate x:DataType="{x:Type dui:SegmentControl}">
                    <Border VerticalOptions="Start"
                            StrokeThickness="1"
                            Stroke="{dui:Colors color_neutral_30}"
                            BackgroundColor="{dui:Colors color_neutral_30}"
                            Padding="{dui:Thickness Left=size_4, Top=size_2, Right=size_4, Bottom=size_2}"
                            dui:Touch.Command="{Binding Source={RelativeSource AncestorType={x:Type CollectionView}}, Path=BindingContext.SelectedItemCommand}">
                        <!-- If item is first or last -->
                        <Border.StrokeShape>
                            <RoundRectangle CornerRadius="8" />
                        </Border.StrokeShape>
                        <Grid ColumnDefinitions="Auto, *"
                              ColumnSpacing="{dui:Sizes size_2}">
                            <dui:Image Source="{dui:Icons check_line}"
                                       HeightRequest="{dui:Sizes size_3}"
                                       WidthRequest="{dui:Sizes size_3}" />
                            <dui:Label Grid.Column="1"
                                       Text="{Binding Title}" />
                        </Grid>
                    </Border>
                </DataTemplate>
            </CollectionView.ItemTemplate>
        </CollectionView>
     */
    private void ItemsSourceChanged()
    {
        if (ItemsSource == null) return;
        var selectableListItem = new List<SelectableListItem>();
        foreach (var item in ItemsSource.Cast<object>().ToList())
        {
            selectableListItem.Add(new SelectableListItem(item.GetPropertyValue(ItemDisplayProperty) ?? string.Empty,
                item.Equals(SelectedItem), item));
        }

        m_allSelectableItems = selectableListItem;
        m_collectionView.ItemsSource = m_allSelectableItems;
    }

    private void SelectedItemChanged()
    {
    }
}