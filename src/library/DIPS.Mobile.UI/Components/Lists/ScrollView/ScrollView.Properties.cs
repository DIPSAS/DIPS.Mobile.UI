namespace DIPS.Mobile.UI.Components.Lists;

public partial class ScrollView
{
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
    /// Determines if input fields should be unfocused when the user scrolls the <see cref="CollectionView"/>. (ScrollBar, Editor etc..) 
    /// </summary>
    public bool RemoveFocusOnScroll { get; init; }

    public static readonly BindableProperty HasAdditionalSpaceAtTheEndProperty = BindableProperty.Create(
        nameof(HasAdditionalSpaceAtTheEnd),
        typeof(bool),
        typeof(ScrollView), defaultValue:true);

    public bool HasAdditionalSpaceAtTheEnd
    {
        get => (bool)GetValue(HasAdditionalSpaceAtTheEndProperty);
        set => SetValue(HasAdditionalSpaceAtTheEndProperty, value);
    }

    public static readonly BindableProperty AndroidAdditionalSpaceAtEndProperty = BindableProperty.Create(
        nameof(AndroidAdditionalSpaceAtEnd),
        typeof(double),
        typeof(ScrollView), defaultValue: (double)Sizes.GetSize(SizeName.size_25) * 4);

    /// <summary>
    /// Additional space at the end for Android is hardcoded, this can be set by adjusting this property.
    /// </summary>
    /// <remarks>iOS calculates its own size. Half the size of the entire scroll view.</remarks>
    public double AndroidAdditionalSpaceAtEnd
    {
        get => (double)GetValue(AndroidAdditionalSpaceAtEndProperty);
        set => SetValue(AndroidAdditionalSpaceAtEndProperty, value);
    }
}