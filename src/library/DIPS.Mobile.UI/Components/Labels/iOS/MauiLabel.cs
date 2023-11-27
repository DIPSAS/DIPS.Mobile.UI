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

        CheckIfTruncated();
    }

    private void CheckIfTruncated()
    {
        var text = "";
        if (m_label.Text is null)
        {
            if (m_label.FormattedText is not null)
                text = m_label.FormattedText.ToString();
        }
        else
        {
            text = m_label.Text;
        }
        
        var nssString = new NSString(text);

        var labelSize = nssString.GetBoundingRect(new CGSize(Bounds.Width, nfloat.PositiveInfinity),
            NSStringDrawingOptions.UsesLineFragmentOrigin, new UIStringAttributes { Font = Font },null);

        var numberOfLines = (int)Math.Ceiling(new nfloat(labelSize.Height) / Font.LineHeight);

        if(m_label.MaxLines == -1)
            return;
        
        if (numberOfLines > m_label.MaxLines)
        {
            m_label.IsTruncated = true;
        }
        else
        {
            m_label.IsTruncated = false;
        }
    }
}