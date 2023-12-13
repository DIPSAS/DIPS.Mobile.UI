namespace DIPS.Mobile.UI.Components.Lists;

public partial class ScrollView : Microsoft.Maui.Controls.ScrollView
{
    private bool m_hasAddedSpaceToBottom;

    protected override void OnSizeAllocated(double width, double height)
    {
        base.OnSizeAllocated(width, height);
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