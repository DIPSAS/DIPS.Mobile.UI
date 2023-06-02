namespace DIPS.Mobile.UI.Components.Loading.Skeleton;

/// <summary>
/// Defines the shape of a skeleton view
/// </summary>
public class SkeletonShape : BindableObject
{
    /// <summary>
    /// Which Row to place the skeleton part inn
    /// </summary>
    public int Row { get; set; }

    /// <summary>
    /// Which Column to place the skeleton inn
    /// </summary>
    public int Column { get; set; }

    /// <summary>
    /// Rowspan of the skeleton
    /// </summary>
    public int RowSpan { get; set; } = 1;

    /// <summary>
    /// ColumnSpan of the skeleton
    /// </summary>
    public int ColumnSpan { get; set; } = 1;

    /// <summary>
    /// Corner Radius of the Skeleton
    /// </summary>
    public double CornerRadius
    {
        get => (double)GetValue(CornerRadiusProperty);
        set => SetValue(CornerRadiusProperty, value);
    }

    /// <summary>
    /// Width of the skeleton. Default is -1 and row/column will be used
    /// </summary>
    public double Width
    {
        get => (double)GetValue(WidthProperty);
        set => SetValue(WidthProperty, value);
        
    }

    /// <summary>
    /// Height of the skeleton. Default is -1 and row/column will be used
    /// </summary>
    public double Height
    {
        get => (double)GetValue(HeightProperty);
        set => SetValue(HeightProperty, value);
    }

    /// <summary>
    /// Horizontal alignment of the skeleton
    /// </summary>
    public LayoutOptions HorizontalAlignment { get; set; } = LayoutOptions.Fill;

    /// <summary>
    /// Vertical alignment of the skeleton
    /// </summary>
    public LayoutOptions VerticalAlignment { get; set; } = LayoutOptions.Fill;

    /// <summary>
    /// Margin of the skeleton
    /// </summary>
    public int Margin { get; set; }

    public static readonly BindableProperty CornerRadiusProperty = BindableProperty.Create(
        nameof(CornerRadius),
        typeof(double),
        typeof(SkeletonShape));

    public static readonly BindableProperty HeightProperty = BindableProperty.Create(
        nameof(Height),
        typeof(double),
        typeof(SkeletonShape), defaultValue:(double)-1);

    public static readonly BindableProperty WidthProperty = BindableProperty.Create(
        nameof(Width),
        typeof(double),
        typeof(SkeletonShape), defaultValue:(double)-1);
}