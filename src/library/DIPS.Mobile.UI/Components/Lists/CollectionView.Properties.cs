namespace DIPS.Mobile.UI.Components.Lists;

public partial class CollectionView
{
    public static readonly BindableProperty ItemSpacingProperty = BindableProperty.Create(
        nameof(ItemSpacing),
        typeof(double),
        typeof(CollectionView), propertyChanged: OnItemSpacingPropertyChanged, defaultValue:(double)Sizes.GetSize(SizeName.size_1));
    
    /// <summary>
    /// Adds spacing between the items in the collection view.
    /// </summary>
    /// <remarks>When the <see cref="ItemsLayout"/> of the <see cref="CollectionView"/> is set to <see cref="GridItemsLayout"/> it will set both the horizontal and vertical item spacing.</remarks>
    public double ItemSpacing
    {
        get => (double)GetValue(ItemSpacingProperty);
        set => SetValue(ItemSpacingProperty, value);
    }
}