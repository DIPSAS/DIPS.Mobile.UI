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

    public static readonly BindableProperty HasAdditionalSpaceAtTheEndProperty = BindableProperty.Create(
        nameof(HasAdditionalSpaceAtTheEnd),
        typeof(bool),
        typeof(ScrollView), defaultValue:true);

    public bool HasAdditionalSpaceAtTheEnd
    {
        get => (bool)GetValue(HasAdditionalSpaceAtTheEndProperty);
        set => SetValue(HasAdditionalSpaceAtTheEndProperty, value);
    }
}