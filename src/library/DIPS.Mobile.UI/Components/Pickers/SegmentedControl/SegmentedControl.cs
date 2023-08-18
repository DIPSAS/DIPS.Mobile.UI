using DIPS.Mobile.UI.Components.Pickers.ItemPicker;
using DIPS.Mobile.UI.Converters.ValueConverters;
using DIPS.Mobile.UI.Effects.Touch;
using Microsoft.Maui.Controls.Shapes;
using Label = DIPS.Mobile.UI.Components.Labels.Label;
using Colors = DIPS.Mobile.UI.Resources.Colors.Colors;
using Image = DIPS.Mobile.UI.Components.Images.Image.Image;

namespace DIPS.Mobile.UI.Components.Pickers.SegmentedControl;

[ContentProperty(nameof(ItemsSource))]
public partial class SegmentedControl : ContentView
{
    private readonly CollectionView m_collectionView;
    private List<SelectableListItem> m_allSelectableItems;
    private readonly Border m_outerBorder;

    public SegmentedControl()
    {
        m_outerBorder = new Border()
        {
            BackgroundColor = Colors.GetColor(ColorName.color_neutral_30),
            StrokeThickness = 1,
            Stroke = Colors.GetColor(ColorName.color_neutral_30),
            StrokeShape = new RoundRectangle()
            {
                CornerRadius = new CornerRadius(Sizes.GetSize(SizeName.size_2), 0,
                    Sizes.GetSize(SizeName.size_2), 0)
            }
        };
        m_collectionView = new CollectionView()
        {
            VerticalOptions = LayoutOptions.Start,
            HorizontalOptions = LayoutOptions.Start,
            ItemsLayout = new LinearItemsLayout(ItemsLayoutOrientation.Horizontal){ ItemSpacing = 0.5},
        };
        m_collectionView.ItemTemplate = new DataTemplate(CreateSegment);

        m_outerBorder.Content = m_collectionView;
        Content = m_outerBorder;
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
            VerticalOptions = LayoutOptions.Start,
            Padding = new Thickness(Sizes.GetSize(SizeName.size_4), Sizes.GetSize(SizeName.size_2)),
            ColumnDefinitions = new ColumnDefinitionCollection() {new(GridLength.Auto), new(GridLength.Star)}
        };
        Touch.SetCommand(grid, new Command(() => SelectItem((SelectableListItem)grid.BindingContext)));
        grid.SetBinding(BackgroundProperty,
            new Binding()
            {
                Path = nameof(SelectableListItem.IsSelected),
                Converter = new BoolToObjectConverter()
                {
                    TrueObject = Colors.GetColor(ColorName.color_neutral_30),
                    FalseObject = Colors.GetColor(ColorName.color_system_white)
                }
            });

        
        var checkedImage = new Image()
        {
            Source = Icons.GetIcon(IconName.check_line),
            WidthRequest = Sizes.GetSize(SizeName.size_3),
            HeightRequest = Sizes.GetSize(SizeName.size_3)
        };
        checkedImage.SetBinding(IsVisibleProperty, new Binding() {Path = nameof(SelectableListItem.IsSelected)});
        var label = new Label();
        label.SetBinding(Label.TextProperty, new Binding() {Path = nameof(SelectableListItem.DisplayName)});

        grid.Add(checkedImage, 0);
        grid.Add(label, 1);
        return grid;
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
        m_collectionView.Scrolled += (sender, args) =>
        {
            if (m_outerBorder.StrokeShape is not RoundRectangle roundRectangle) return;
            var firstItemVisible = GetFirstItemVisible();
            var lastVisibleItem = GetLastItemVisible();
            var radius = (double)Sizes.GetSize(SizeName.size_8);
            var cornerRadius = roundRectangle.CornerRadius;
            var isLastItemVisible = lastVisibleItem == m_allSelectableItems.Last();
            var isFirstItemVisible = m_allSelectableItems.First() == firstItemVisible;

            if (isLastItemVisible && isFirstItemVisible)
            {
                cornerRadius = new CornerRadius(radius, radius, radius, radius);
            }else if (isFirstItemVisible && !isLastItemVisible)
            {
                cornerRadius = new CornerRadius(radius,0,radius,0);
            }else if (isLastItemVisible && !isFirstItemVisible)
            {
                cornerRadius = new CornerRadius(0, radius, 0, radius);
            }
            else
            {
                cornerRadius = new CornerRadius(0,0,0,0);
            }

            m_outerBorder.StrokeShape = new RoundRectangle(){CornerRadius = cornerRadius};
            // if (args.Element.BindingContext is not SelectableListItem selectableListItem) return;
            // if (m_outerBorder.StrokeShape is not RoundRectangle roundRectangle) return;
            //
            // var radius = (double)Sizes.GetSize(SizeName.size_8);
            // if (m_allSelectableItems.Last() == selectableListItem)
            // {
            //     roundRectangle.CornerRadius = new CornerRadius(0, radius, 0, radius);
            // }else if (m_allSelectableItems.First() == selectableListItem)
            // {
            //     roundRectangle.CornerRadius = new CornerRadius(radius,0,radius,0);
            // }
            //
            // m_outerBorder.StrokeShape = roundRectangle;
        };
    }

    internal partial SelectableListItem? GetFirstItemVisible();
    internal partial SelectableListItem? GetLastItemVisible();
    private void SelectedItemChanged()
    {
    }
}