namespace DIPS.Mobile.UI.Components.Lists;

public partial class CollectionView
{
    /// <summary>
    /// Adds spacing between the items in the collection view.
    /// </summary>
    /// <remarks>When the <see cref="ItemsLayout"/> of the <see cref="CollectionView"/> is set to <see cref="GridItemsLayout"/> it will set both the horizontal and vertical item spacing.</remarks>
    public double ItemSpacing
    {
        get => (double)GetValue(ItemSpacingProperty);
        set => SetValue(ItemSpacingProperty, value);
    }

    /// <summary>
    /// Will automatically set the corner radius of the first and last item in the <see cref="CollectionView"/>.
    /// </summary>
    internal bool AutoCornerRadius { get; set; } = true;
    
    /// <summary>
    /// Adds additional space at the end of the list so people can more easily see and tap the last items.
    /// </summary>
    /// <remarks>Default value is true</remarks>
    public bool HasAdditionalSpaceAtTheEnd
    {
        get => (bool)GetValue(HasAdditionalSizeAtTheEndProperty);
        set => SetValue(HasAdditionalSizeAtTheEndProperty, value);
    }
    
    /// <summary>
    /// Determines if input fields should be unfocused when the user scrolls the <see cref="CollectionView"/>. (ScrollBar, Editor etc..) 
    /// </summary>
    public bool RemoveFocusOnScroll { get; init; }

    public static readonly BindableProperty ShouldBounceProperty = BindableProperty.Create(
        nameof(ShouldBounce),
        typeof(bool),
        typeof(CollectionView),
        defaultValue: true);

    /// <summary>
    /// Determines if the collection view should bounce.
    /// </summary>
    public bool ShouldBounce
    {
        get => (bool)GetValue(ShouldBounceProperty);
        set => SetValue(ShouldBounceProperty, value);
    }

    /// <summary>
    /// Margin for the content inside the CollectionView, using this will not affect for example scroll bar, like default margin does
    /// </summary>
    /// <remarks>Default value is (Left: size_3, Right: size_3)</remarks>
    public Thickness Padding { get; set; } = new(Sizes.GetSize(SizeName.size_3), 0);
    
    public CornerRadius FirstItemCornerRadius { get; set; }
    public CornerRadius LastItemCornerRadius { get; set; }
    
    public static readonly BindableProperty HasAdditionalSizeAtTheEndProperty = BindableProperty.Create(
        nameof(HasAdditionalSpaceAtTheEnd),
        typeof(bool),
        typeof(CollectionView), defaultValue:true);
    
    public static readonly BindableProperty ItemSpacingProperty = BindableProperty.Create(
        nameof(ItemSpacing),
        typeof(double),
        typeof(CollectionView), propertyChanged: (bindable, value, newValue) => ((CollectionView)bindable).TrySetItemSpacing(), defaultValue:(double)Sizes.GetSize(SizeName.size_1));

    /// <summary>
    /// Reloads all the data in the <see cref="CollectionView"/>
    /// </summary>
    /// <remarks>Use this if you need to re draw the items.</remarks>
    public void ReloadData()
    {
        if (Handler is CollectionViewHandler handler)
        {
            handler.ReloadData(handler);
        }
    }
}