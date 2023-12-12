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
    /// Adds additional space at the end of the list so people can more easily see and tap the last items.
    /// </summary>
    /// <remarks>This is achieved by using the <see cref="CollectionView.Footer"/>. If you overwrite the <see cref="CollectionView.Footer"/> it will be removed</remarks>
    public bool HasAdditionalSpaceAtTheEnd
    {
        get => (bool)GetValue(HasAdditionalSizeAtTheEndProperty);
        set => SetValue(HasAdditionalSizeAtTheEndProperty, value);
    }

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

    public double ContentHeight {
        get
        {
            var contentHeight = Height;
#if __IOS__
            if (Handler is not CollectionViewHandler collectionViewHandler)
            {
                return contentHeight;
            }

            if (collectionViewHandler.PlatformView.Subviews [0] is UIKit.UICollectionView uiCollectionView)
            {
                contentHeight= uiCollectionView.CollectionViewLayout.CollectionViewContentSize.Height;
            }
#endif
            return contentHeight;
        }

    }
    
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