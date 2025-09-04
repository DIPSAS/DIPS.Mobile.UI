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
        var truncatedText = m_label.TruncatedText ?? "";

        if (!string.IsNullOrEmpty(modifiedOriginalText))
        {
            // Start with a more aggressive initial reduction to ensure we have space for truncation text
            var maxAttempts = modifiedOriginalText.Length;
            var attempts = 0;
            
            while (modifiedOriginalText.Length > 0 && attempts < maxAttempts)
            {
                var testText = modifiedOriginalText + truncatedText;
                
                if (!CheckIfTruncated(testText))
                    break;
                    
                // Remove more characters at once initially, then fine-tune
                var charsToRemove = Math.Max(1, modifiedOriginalText.Length / 10);
                if (attempts > maxAttempts * 0.8) // Fine-tune in last 20% of attempts
                    charsToRemove = 1;
                    
                var newLength = Math.Max(0, modifiedOriginalText.Length - charsToRemove);
                modifiedOriginalText = modifiedOriginalText.Substring(0, newLength);
                attempts++;
            }
            
            // Final safety check - if still truncated, remove more aggressively
            while (modifiedOriginalText.Length > 0 && CheckIfTruncated(modifiedOriginalText + truncatedText))
            {
                modifiedOriginalText = modifiedOriginalText.Substring(0, modifiedOriginalText.Length - 1);
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
                        LineHeight = originalSpan.LineHeight,
                        Style = originalSpan.Style // Preserve style
                    });
                    currentLength += spanText.Length;
                }
                else if (remainingLength > 0)
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
                        LineHeight = originalSpan.LineHeight,
                        Style = originalSpan.Style // Preserve style
                    });
                    break;
                }
            }
            
            // Add the truncation text span only if we have content
            if (newFormattedString.Spans.Count > 0 && !string.IsNullOrEmpty(truncatedText))
            {
                newFormattedString.Spans.Add(m_label.CreateTruncatedTextSpan(truncatedText));
            }
            
            m_label.FormattedText = newFormattedString;
        }
        else
        {
            // Fallback to simple text handling - only if we have content
            if (!string.IsNullOrEmpty(modifiedOriginalText) || !string.IsNullOrEmpty(truncatedText))
            {
                var newFormattedString = new FormattedString();
                
                if (!string.IsNullOrEmpty(modifiedOriginalText))
                {
                    newFormattedString.Spans.Add(new Span 
                    { 
                        Text = modifiedOriginalText, 
                        FontSize = m_label.FontSize, 
                        FontFamily = m_label.FontFamily,
                        TextColor = m_label.TextColor
                    });
                }
                
                if (!string.IsNullOrEmpty(truncatedText))
                {
                    newFormattedString.Spans.Add(m_label.CreateTruncatedTextSpan(truncatedText));
                }
                
                m_label.FormattedText = newFormattedString;
            }
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