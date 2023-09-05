using Microsoft.Maui.Controls.Shapes;
using Colors = DIPS.Mobile.UI.Resources.Colors.Colors;
using Image = DIPS.Mobile.UI.Components.Images.Image;

namespace DIPS.Mobile.UI.Components.Sorting;

internal class SortOrderButton : Border
{
    private readonly SortOrder m_sortOrder;
    private readonly SortControl m_sortControl;

    private readonly Label m_sortOrderLabel = new()
    {
        VerticalTextAlignment = TextAlignment.Center,
        FontSize = 14,
        FontAttributes = FontAttributes.Bold,
        Margin = new Thickness(Sizes.GetSize(SizeName.size_0), Sizes.GetSize(SizeName.size_1), Sizes.GetSize(SizeName.size_1), Sizes.GetSize(SizeName.size_1)),
    };

    private readonly Border m_sortOrderImageBorder = new()
    {
        StrokeShape = new Ellipse(),
        Padding = Sizes.GetSize(SizeName.size_1),
        WidthRequest = Sizes.GetSize(SizeName.size_6),
        HeightRequest = Sizes.GetSize(SizeName.size_6),
        Margin = new Thickness(Sizes.GetSize(SizeName.size_1), Sizes.GetSize(SizeName.size_1), Sizes.GetSize(SizeName.size_0), Sizes.GetSize(SizeName.size_1)),
        VerticalOptions = LayoutOptions.Center
    };

    public SortOrderButton(SortOrder sortOrder, SortControl sortControl)
    {
        m_sortControl = sortControl;
        m_sortOrder = sortOrder;
        
        Padding = Sizes.GetSize(SizeName.size_0);

        StrokeShape = new RoundRectangle { CornerRadius = Sizes.GetSize(SizeName.size_8) };
        StrokeThickness = 1;
        Stroke = Colors.GetColor(ColorName.color_primary_90);
        
        m_sortOrderImageBorder.Content = new Image.Image
        {
            Source = sortOrder == SortOrder.Ascending
                ? Icons.GetIcon(IconName.ascending_fill)
                : Icons.GetIcon(IconName.descending_fill),
            TintColor = Colors.GetColor(ColorName.color_primary_90)
        };

        m_sortOrderLabel.Text = sortOrder == SortOrder.Ascending ? "Ascending" : "Descending";

        var innerGrid = new Grid
        {
            ColumnDefinitions = new ColumnDefinitionCollection { new(GridLength.Auto), new(GridLength.Star) },
            ColumnSpacing = Sizes.GetSize(SizeName.size_1),
        };
        
        innerGrid.Add(m_sortOrderImageBorder);
        innerGrid.Add(m_sortOrderLabel, 1);

        Content = innerGrid;
        
        UpdateLayout();
    }

    public void UpdateLayout()
    {
        m_sortOrderImageBorder.BackgroundColor = m_sortControl.CurrentSortOrder == m_sortOrder
            ? Colors.GetColor(ColorName.color_system_white)
            : Colors.GetColor(ColorName.color_secondary_90);
        
        m_sortOrderLabel.TextColor = m_sortOrder == m_sortControl.CurrentSortOrder
            ? Colors.GetColor(ColorName.color_system_white)
            : Colors.GetColor(ColorName.color_primary_90);

        BackgroundColor = m_sortControl.CurrentSortOrder == m_sortOrder ? Colors.GetColor(ColorName.color_primary_90) : Colors.GetColor(ColorName.color_system_white);
    }
}