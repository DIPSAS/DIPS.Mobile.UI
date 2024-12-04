using CoreGraphics;
using Foundation;
using UIKit;

namespace DIPS.Mobile.UI.Components.Labels;

public class MauiLabel : Microsoft.Maui.Platform.MauiLabel
{
    private string m_originalText;
    
    private string m_textAfterCustomTruncation;
    private int m_maxLinesAfterCustomTruncation;

    private bool m_firstDraw = true;
    
    public Label Label { get; set; }
    
    public override void LayoutSubviews()
    {
        base.LayoutSubviews();

        // If text has not been set yet
        if (string.IsNullOrEmpty(GetTextFromLabel()))
            return;

        if (m_firstDraw)
        {
            m_originalText = GetTextFromLabel();
            m_firstDraw = false;
        }
        
        // Because SetTruncatedText() fires this function we have to check if the text is any different from after setting the custom truncation text or if MaxLines is set to something else, if not, we know that the consumer has not done any changes to the Label
        if (m_textAfterCustomTruncation == GetTextFromLabel() && m_maxLinesAfterCustomTruncation == Label.MaxLines)
            return;

        // If the consumer only changed MaxLines, the Text will still be the custom truncated text, so we have to check if they are equal, if so, set the Text to the original
        if (GetTextFromLabel() == m_textAfterCustomTruncation)
        {
            Label.Text = m_originalText;
        }
        
        Label.IsTruncated = CheckIfTruncated();
        SetTruncatedText();
    }

    private bool CheckIfTruncated(string? stringToCheck = null)
    {
        var text = stringToCheck ?? GetTextFromLabel();

        if (string.IsNullOrEmpty(text))
            return false;
        
        var nssString = new NSString(text);

        var labelSize = nssString.GetBoundingRect(new CGSize(Bounds.Width, nfloat.PositiveInfinity),
            NSStringDrawingOptions.UsesLineFragmentOrigin, new UIStringAttributes { Font = Font },null);

        var numberOfLines = (int)Math.Ceiling(new nfloat(labelSize.Height) / Font.LineHeight);

        if (Lines == -1)
            return false;
            
        return numberOfLines > Lines;
    }

    public void SetTruncatedText()
    {
        if(!Label.IsTruncated || string.IsNullOrEmpty(Label.TruncatedText) || string.IsNullOrEmpty(Text))
            return;

        RemoveTextUntilNotTruncated();

        m_textAfterCustomTruncation = GetTextFromLabel();
        m_maxLinesAfterCustomTruncation = Label.MaxLines;
    }

    private void RemoveTextUntilNotTruncated()
    {
        var modifiedOriginalText = GetTextFromLabel();
        while (true)
        {
            modifiedOriginalText = modifiedOriginalText.Substring(0, modifiedOriginalText.Length - 1);

            if (!CheckIfTruncated(modifiedOriginalText + Label.TruncatedText))
                break;
        }
        
        Label.FormattedText = new FormattedString { Spans =
        {
            new Span { Text = modifiedOriginalText, FontSize = Label.FontSize, FontFamily = Label.FontFamily },
            new Span { Text = Label.TruncatedText, FontSize = Label.FontSize, FontFamily = Label.FontFamily, TextColor = Label.TruncatedTextColor } 
        } };
    }

    private string GetTextFromLabel()
    {
        if (!string.IsNullOrEmpty(Label.Text))
            return Label.Text;

        return Label.FormattedText is not null ? Label.FormattedText.ToString() : Label.Text;
    }
    
}