using CoreGraphics;
using Foundation;
using Microsoft.Maui.Controls.Internals;
using Microsoft.Maui.Graphics.Platform;
using Microsoft.Maui.Platform;
using UIKit;

namespace DIPS.Mobile.UI.Components.Labels;

public class MauiLabel : Microsoft.Maui.Platform.MauiLabel
{
    private readonly Label m_label;

    public MauiLabel(Label label)
    {
        m_label = label;
    }
    
    public override void LayoutSubviews()
    {
        base.LayoutSubviews();

        CheckIfEllipsized();
    }

    public void CheckIfEllipsized()
    {
        var nssString = new NSString(m_label.Text);

        var labelSize = nssString.GetBoundingRect(new CGSize(Bounds.Width, nfloat.PositiveInfinity),
            NSStringDrawingOptions.UsesLineFragmentOrigin, new UIStringAttributes { Font = Font },null);

        var numberOfLines = (int)Math.Ceiling(new nfloat(labelSize.Height) / Font.LineHeight);

        if(m_label.MaxLines == -1)
            return;
        
        if (numberOfLines > m_label.MaxLines)
        {
            m_label.IsEllipsized = true;
        }
        else
        {
            m_label.IsEllipsized = false;
        }
    }
}