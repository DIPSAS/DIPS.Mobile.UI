using Android.Content;
using Android.Graphics;
using Android.Text;
using AndroidX.AppCompat.Widget;

namespace DIPS.Mobile.UI.Components.Labels.Android;

public class MauiTextView : Microsoft.Maui.Platform.MauiTextView
{
    private readonly Label m_label;
    
    private string m_originalText;
    private string m_textAfterCustomTruncation;
    
    private int m_maxLinesAfterCustomTruncation;

    private bool m_firstDraw = true;

    public MauiTextView(Context context, Label label) : base(context)
    {
        m_label = label;
    }

    protected override void OnDraw(Canvas? canvas)
    {
        base.OnDraw(canvas);

        // If text has not been set yet
        if (string.IsNullOrEmpty(GetTextFromLabel()))
            return;

        if (m_firstDraw)
        {
            m_originalText = GetTextFromLabel();
            m_firstDraw = false;
        }
        
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

    public void SetTruncatedText()
    {
        if(!m_label.IsTruncated || string.IsNullOrEmpty(m_label.TruncatedText) || string.IsNullOrEmpty(GetTextFromLabel()))
            return;

        RemoveTextUntilNotTruncated();

        m_textAfterCustomTruncation = GetTextFromLabel();
        m_maxLinesAfterCustomTruncation = m_label.MaxLines;
    }

    private void RemoveTextUntilNotTruncated()
    {
        var modifiedOriginalText = GetTextFromLabel();

        if (!string.IsNullOrEmpty(modifiedOriginalText))
        {
            while (modifiedOriginalText.Length > 0)
            {
                modifiedOriginalText = modifiedOriginalText.Substring(0, modifiedOriginalText.Length - 1);

                if (!CheckIfTruncated(modifiedOriginalText + m_label.TruncatedText))
                    break;
            }
        }
        
        // Preserve original formatting if FormattedText exists
        if (m_label.FormattedText != null && m_label.FormattedText.Spans.Count > 0)
        {
            var newFormattedString = new FormattedString();
            var currentLength = 0;
            var targetLength = modifiedOriginalText?.Length ?? 0;
            
            // Recreate spans up to the truncation point, preserving original formatting
            foreach (var originalSpan in m_label.FormattedText.Spans)
            {
                if (currentLength >= targetLength)
                    break;
                    
                var spanText = originalSpan.Text ?? string.Empty;
                var remainingLength = targetLength - currentLength;
                
                if (spanText.Length <= remainingLength)
                {
                    // Include the entire span
                    newFormattedString.Spans.Add(new Span
                    {
                        Text = spanText,
                        FontSize = originalSpan.FontSize,
                        FontFamily = originalSpan.FontFamily,
                        TextColor = originalSpan.TextColor,
                        FontAttributes = originalSpan.FontAttributes,
                        TextDecorations = originalSpan.TextDecorations,
                        CharacterSpacing = originalSpan.CharacterSpacing,
                        LineHeight = originalSpan.LineHeight
                    });
                    currentLength += spanText.Length;
                }
                else
                {
                    // Include partial span
                    var truncatedSpanText = spanText.Substring(0, remainingLength);
                    newFormattedString.Spans.Add(new Span
                    {
                        Text = truncatedSpanText,
                        FontSize = originalSpan.FontSize,
                        FontFamily = originalSpan.FontFamily,
                        TextColor = originalSpan.TextColor,
                        FontAttributes = originalSpan.FontAttributes,
                        TextDecorations = originalSpan.TextDecorations,
                        CharacterSpacing = originalSpan.CharacterSpacing,
                        LineHeight = originalSpan.LineHeight
                    });
                    break;
                }
            }
            
            // Add the truncation text span
            newFormattedString.Spans.Add(m_label.CreateTruncatedTextSpan(m_label.TruncatedText));
            
            m_label.FormattedText = newFormattedString;
        }
        else
        {
            // Fallback to simple text handling
            m_label.FormattedText = new FormattedString { Spans =
            {
                new Span { Text = modifiedOriginalText, FontSize = m_label.FontSize, FontFamily = m_label.FontFamily },
                m_label.CreateTruncatedTextSpan(m_label.TruncatedText) 
            } };
        }
    }

    public bool CheckIfTruncated(string? stringToCheck = null)
    {
        var text = stringToCheck ?? GetTextFromLabel();
        if (string.IsNullOrEmpty(text))
        {
            return false;
        }
        
        var tempPaint = new TextPaint(Paint)
        {
            TextSize = TextSize,
        };
        tempPaint.SetTypeface(Typeface);
        
        var width = Width - PaddingLeft - PaddingRight;
        
#pragma warning disable CA1422
        var staticLayout = new StaticLayout(text, tempPaint, width,
            global::Android.Text.Layout.Alignment.AlignNormal, LineSpacingMultiplier, LineSpacingExtra, false);
#pragma warning restore CA1422
        
        if (m_label.MaxLines == -1)
            return false;

        return staticLayout.LineCount > m_label.MaxLines;
    }
    
    private string? GetTextFromLabel()
    {
        if (!string.IsNullOrEmpty(m_label.Text))
            return m_label.Text;

        return m_label.FormattedText is not null ? m_label.FormattedText.ToString() : m_label.Text;
    }
}