namespace DIPS.Mobile.UI.Components.Lists;

public partial class ScrollView : Microsoft.Maui.Controls.ScrollView
{
    private bool m_hasAddedSpaceToBottom;

    public static readonly BindableProperty ScrollEnabledProperty = BindableProperty.Create(
        nameof(ScrollEnabled),
        typeof(bool),
        typeof(ScrollView));
    
    public ScrollView()
    {
#if __ANDROID__ //Not possible to set padding on scroll view after its rendered
        AdjustPadding(AndroidAdditionalSpaceAtEnd);
#endif
    }
    
    public bool ScrollEnabled
    {
        get => (bool)GetValue(ScrollEnabledProperty);
        set => SetValue(ScrollEnabledProperty, value);
    }

#if __IOS__
    protected override void OnSizeAllocated(double width, double height)
    {
        base.OnSizeAllocated(width, height);
        AdjustPadding(height);
    }
#endif

    private void AdjustPadding(double height)
    {
        var oldPadding = Padding;
        if (height > 0 && !m_hasAddedSpaceToBottom && HasAdditionalSpaceAtTheEnd)
        {
            m_hasAddedSpaceToBottom = true;
            var newPadding = new Thickness(oldPadding.Left, oldPadding.Top, oldPadding.Right,
                (int)(oldPadding.Bottom + (height / 2)));
            Padding = newPadding;
        } 
    }
}