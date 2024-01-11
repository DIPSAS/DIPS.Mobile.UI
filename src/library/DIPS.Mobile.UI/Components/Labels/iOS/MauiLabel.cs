using CoreGraphics;
using Foundation;
using UIKit;
using Colors = DIPS.Mobile.UI.Resources.Colors.Colors;

namespace DIPS.Mobile.UI.Components.Labels;

public class MauiLabel : Microsoft.Maui.Platform.MauiLabel
{
    private readonly Label m_label;

    private readonly string m_originalText;
    
    private string m_textAfterCustomTruncation;
    private int m_maxLinesAfterCustomTruncation;

    public MauiLabel(Label label)
    {
        m_label = label;

        m_originalText = GetTextFromLabel();
    }
    
    public override void LayoutSubviews()
    {
        base.LayoutSubviews();

        // Because SetTruncatedText() fires this function we have to check if the text is any different from after setting the custom truncation text or if MaxLines is set to something else, if not, we know that the consumer has not done any changes to the Label
        if (m_textAfterCustomTruncation == GetTextFromLabel() && m_maxLinesAfterCustomTruncation == m_label.MaxLines)
            return;

        // If the consumer only changed MaxLines, the Text will still be the custom truncated text, so we have to check if they are equal, if so, set the Text to the original
        if (GetTextFromLabel() == m_textAfterCustomTruncation)
        {
            m_label.Text = m_originalText;
        }
        
        m_label.IsTruncated = CheckIfTruncated();
        SetTruncatedText();
    }

    private bool CheckIfTruncated()
    {
        var text = GetTextFromLabel();
        
        var nssString = new NSString(text);

        var labelSize = nssString.GetBoundingRect(new CGSize(Bounds.Width, nfloat.PositiveInfinity),
            NSStringDrawingOptions.UsesLineFragmentOrigin, new UIStringAttributes { Font = Font },null);

        var numberOfLines = (int)Math.Ceiling(new nfloat(labelSize.Height) / Font.LineHeight);

        if (m_label.MaxLines == -1)
            return false;
            
        return numberOfLines > m_label.MaxLines;
    }

    public void SetTruncatedText()
    {
        if(!m_label.IsTruncated || string.IsNullOrEmpty(m_label.TruncatedText) || string.IsNullOrEmpty(Text))
            return;

        m_label.FormattedText = new FormattedString { Spans =
        {
            new Span { Text = Text, FontSize = m_label.FontSize, FontFamily = m_label.FontFamily },
            new Span { Text = m_label.TruncatedText, FontSize = m_label.FontSize, FontFamily = m_label.FontFamily, TextColor = Colors.GetColor(ColorName.color_primary_90) } 
        } };
        
        RemoveTextUntilNotTruncated();

        m_textAfterCustomTruncation = GetTextFromLabel();
        m_maxLinesAfterCustomTruncation = m_label.MaxLines;
    }

    private void RemoveTextUntilNotTruncated()
    {
        while (CheckIfTruncated())
        {
            var modifiedString = m_label.FormattedText.Spans[0].Text
                .Substring(0, m_label.FormattedText.Spans[0].Text.Length - 1);

            m_label.FormattedText.Spans[0].Text = modifiedString;
        }
    }

    private string GetTextFromLabel()
    {
        if (!string.IsNullOrEmpty(m_label.Text))
            return m_label.Text;

        return m_label.FormattedText is not null ? m_label.FormattedText.ToString() : m_label.Text;
    }
    
}