namespace DIPS.Mobile.UI.Components.Lists;

public partial class ScrollView : Microsoft.Maui.Controls.ScrollView
{
    private bool m_hasAddedSpaceToBottom;

#if __IOS__
    protected override void OnSizeAllocated(double width, double height)
    {
        base.OnSizeAllocated(width, height);
        AdjustPadding(height);
    }
#endif

#if __ANDROID__
    protected override void OnHandlerChanged()
    {
        base.OnHandlerChanged();

        if (Handler is not null)
            AdjustPadding(AndroidAdditionalSpaceAtEnd);
    }
#endif

    protected override void OnHandlerChanging(HandlerChangingEventArgs args)
    {
        base.OnHandlerChanging(args);
    }

    private void AdjustPadding(double height)
    {
        var oldPadding = Padding;
        if (height > 0 && !m_hasAddedSpaceToBottom && HasAdditionalSpaceAtTheEnd)
        {
            m_hasAddedSpaceToBottom = true;
            var newPadding = new Thickness(oldPadding.Left, oldPadding.Top, oldPadding.Right,
                oldPadding.Bottom + (height / 2));
            Padding = newPadding;
        }
    }
}
