using CoreGraphics;
using DIPS.Mobile.UI.Internal.Logging;
using Foundation;
using Microsoft.Maui.Controls.Platform;
using UIKit;

namespace DIPS.Mobile.UI.Components.Labels;

public class MauiLabel : Microsoft.Maui.Platform.MauiLabel
{
    private readonly CustomTruncationLabel m_label;

    private string m_originalText;
    private FormattedString? m_originalFormattedText;
    
    private string m_textAfterCustomTruncation;
    private int m_maxLinesAfterCustomTruncation;

    private bool m_firstDraw = true;

    public MauiLabel(CustomTruncationLabel label)
    {
        m_label = label;
    }
    
    public override void LayoutSubviews()
    {
        base.LayoutSubviews();

        // If text has not been set yet
        if (string.IsNullOrEmpty(GetTextFromLabel()))
            return;

        if (m_firstDraw)
        {
            // Only capture original values if we haven't applied custom truncation yet
            // Check if the current FormattedText is already truncated by looking for custom truncation text
            var isAlreadyTruncated = false;
            if (m_label.FormattedText != null && !string.IsNullOrEmpty(m_label.TruncatedText))
            {
                var formattedText = m_label.FormattedText.ToString();
                if (formattedText.EndsWith(m_label.TruncatedText))
                {
                    isAlreadyTruncated = true;
                }
            }
            
            if (!isAlreadyTruncated)
            {
                m_originalText = GetTextFromLabel();
                m_originalFormattedText = m_label.FormattedText;
            }
            else
            {
                // If already truncated, we need to reconstruct the original
                // For now, use the Text property as original if available
                if (!string.IsNullOrEmpty(m_label.Text))
                {
                    m_originalText = m_label.Text;
                    m_originalFormattedText = null; // Clear FormattedText since Text takes priority
                }
            }
            
            m_firstDraw = false;
        }
        
        // Because SetTruncatedText() fires this function we have to check if the text is any different from after setting the custom truncation text or if MaxLines is set to something else, if not, we know that the consumer has not done any changes to the Label
        if (m_textAfterCustomTruncation == GetTextFromLabel() && m_maxLinesAfterCustomTruncation == m_label.MaxLines)
            return;

        // If MaxLines changed, we need to restore original content before re-evaluating truncation
        if (m_maxLinesAfterCustomTruncation != m_label.MaxLines)
        {
            // Restore original text and formatted text
            if (m_originalFormattedText != null)
            {
                // Force clear Text first to prioritize FormattedText
                m_label.Text = string.Empty;
                m_label.InvalidateMeasure();
                
                // Now set the FormattedText
                m_label.FormattedText = m_originalFormattedText;
            }
            else
            {
                // Force clear FormattedText first
                m_label.FormattedText = null;
                
                // Force a layout update to ensure FormattedText is cleared
                m_label.InvalidateMeasure();
                
                // Trigger property change notification by setting to empty first, then to actual value
                m_label.Text = string.Empty;
                m_label.InvalidateMeasure();
                
                // Now set the actual original text
                m_label.Text = m_originalText;
            }
            
            // Force layout invalidation after restoring original content
            m_label.InvalidateMeasure();
        }
        // If the consumer only changed MaxLines, the Text will still be the custom truncated text, so we have to check if they are equal, if so, set the Text to the original
        else if (GetTextFromLabel() == m_textAfterCustomTruncation)
        {
            // Restore original text and formatted text
            if (m_originalFormattedText != null)
            {
                // Force clear Text first to prioritize FormattedText
                m_label.Text = string.Empty;
                m_label.InvalidateMeasure();
                
                // Now set the FormattedText
                m_label.FormattedText = m_originalFormattedText;
            }
            else
            {
                m_label.FormattedText = null; // Clear FormattedText to prioritize Text
                m_label.Text = m_originalText;
            }
            
            // Force layout invalidation after restoring original content
            m_label.InvalidateMeasure();
        }
        
        var wasTruncated = m_label.IsTruncated;
        m_label.IsTruncated = CheckIfTruncated();
        
        SetTruncatedText();
        
        // Always update tracking variables to ensure proper state management
        m_textAfterCustomTruncation = GetTextFromLabel();
        m_maxLinesAfterCustomTruncation = m_label.MaxLines;
    }

    private bool CheckIfTruncated(string? stringToCheck = null)
    {
        var text = stringToCheck ?? GetTextFromLabel();

        if (string.IsNullOrEmpty(text))
            return false;
        
        if (Lines == -1)
            return false;

        CGRect labelSize;

        try
        {
            if (m_label.FormattedText != null && m_label.FormattedText.Spans.Count > 0)
            {
                // Create NSAttributedString to properly handle different fonts per span
                var attributedString = new NSMutableAttributedString();
                var currentLength = 0;
                var targetText = stringToCheck ?? string.Empty;

                foreach (var span in m_label.FormattedText.Spans)
                {
                    var spanText = span.Text ?? string.Empty;

                    // If we're checking a specific string, only include the portion that fits within that string
                    if (stringToCheck != null)
                    {
                        var remainingLength = targetText.Length - currentLength;
                        if (remainingLength <= 0)
                            break;

                        if (spanText.Length > remainingLength)
                            spanText = spanText.Substring(0, remainingLength);
                    }

                    if (!string.IsNullOrEmpty(spanText))
                    {
                        var spanString = new NSString(spanText);

                        var spanAttributes = new UIStringAttributes { Font = span.ToUIFont() };
                        var spanAttributedString = new NSAttributedString(spanString, spanAttributes);
                        attributedString.Append(spanAttributedString);
                    }

                    currentLength += spanText.Length;

                    if (stringToCheck != null && currentLength >= targetText.Length)
                        break;
                }

                // Use the attributed string for accurate bounds calculation
                labelSize = attributedString.GetBoundingRect(
                    new CGSize(Bounds.Width, nfloat.PositiveInfinity),
                    NSStringDrawingOptions.UsesLineFragmentOrigin,
                    null);
            }
            else
            {
                // Fallback to simple text handling when no formatted spans exist
                var nssString = new NSString(text);
                labelSize = nssString.GetBoundingRect(new CGSize(Bounds.Width, nfloat.PositiveInfinity),
                    NSStringDrawingOptions.UsesLineFragmentOrigin, new UIStringAttributes { Font = Font }, null);
            }
        }
        catch (Exception e)
        {
            // If something goes wrong, we don't want to crash the app
            DUILogService.LogError<MauiLabel>(e.Message);
            return false;
        }
        
        var numberOfLines = (int)Math.Ceiling(new nfloat(labelSize.Height) / Font.LineHeight);
          
        return numberOfLines > Lines;
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
        var truncatedText = m_label.TruncatedText ?? "";
        
        if (m_label.FormattedText != null && m_label.FormattedText.Spans.Count > 0)
        {
            // Handle FormattedText by working with the actual spans
            var totalOriginalLength = m_label.FormattedText.ToString().Length;
            
            if (totalOriginalLength > 0)
            {
                var maxAttempts = Math.Min(50, totalOriginalLength); // Limit attempts to prevent infinite loops
                var attempts = 0;
                var targetLength = totalOriginalLength;
                
                while (targetLength > 0 && attempts < maxAttempts)
                {
                    // Create a test FormattedText with the current target length + truncation text
                    var testFormattedText = CreateTruncatedFormattedText(targetLength);
                    testFormattedText.Spans.Add(m_label.CreateTruncatedTextSpan(truncatedText));
                    
                    // Test by temporarily setting the FormattedText
                    var originalFormattedText = m_label.FormattedText;
                    m_label.FormattedText = testFormattedText;
                    
                    var isTruncated = CheckIfTruncated();
                    
                    // Restore original
                    m_label.FormattedText = originalFormattedText;
                    
                    if (!isTruncated)
                        break;
                        
                    // Remove characters more conservatively - start with 1 character and increase gradually
                    var charsToRemove = Math.Max(1, attempts < 5 ? 1 : Math.Min(5, targetLength / 20));
                    targetLength = Math.Max(0, targetLength - charsToRemove);
                    attempts++;
                }
                
                // Apply the final truncated length
                ApplyFormattedTextTruncation(targetLength);
            }
        }
        else
        {
            // Handle regular text - but convert to FormattedText to preserve truncation styling
            var originalText = GetTextFromLabel();
            
            if (!string.IsNullOrEmpty(originalText))
            {
                var maxAttempts = Math.Min(50, originalText.Length); // Limit attempts
                var attempts = 0;
                var targetLength = originalText.Length;
                
                while (targetLength > 0 && attempts < maxAttempts)
                {
                    var truncatedOriginalText = originalText.Substring(0, targetLength);
                    
                    // Create FormattedText to preserve truncation text styling
                    var testFormattedText = new FormattedString();
                    testFormattedText.Spans.Add(new Span 
                    { 
                        Text = truncatedOriginalText, 
                        FontSize = m_label.FontSize, 
                        FontFamily = m_label.FontFamily,
                        FontAttributes = m_label.FontAttributes,
                        TextColor = m_label.TextColor
                    });
                    testFormattedText.Spans.Add(m_label.CreateTruncatedTextSpan(truncatedText));
                    
                    // Test by temporarily setting the FormattedText
                    var originalFormattedText = m_label.FormattedText;
                    m_label.Text = string.Empty; // Clear text first
                    m_label.FormattedText = testFormattedText;
                    
                    var isTruncated = CheckIfTruncated();
                    
                    // Restore original state
                    m_label.FormattedText = originalFormattedText;
                    if (originalFormattedText == null)
                        m_label.Text = originalText;
                    
                    if (!isTruncated)
                        break;
                        
                    // Remove characters more conservatively
                    var charsToRemove = Math.Max(1, attempts < 5 ? 1 : Math.Min(5, targetLength / 20));
                    targetLength = Math.Max(0, targetLength - charsToRemove);
                    attempts++;
                }
                
                // Apply the result using FormattedText to preserve styling
                var finalTruncatedText = originalText.Substring(0, targetLength);
                var finalFormattedText = new FormattedString();
                finalFormattedText.Spans.Add(new Span 
                { 
                    Text = finalTruncatedText, 
                    FontSize = m_label.FontSize, 
                    FontFamily = m_label.FontFamily,
                    FontAttributes = m_label.FontAttributes,
                    TextColor = m_label.TextColor
                });
                finalFormattedText.Spans.Add(m_label.CreateTruncatedTextSpan(truncatedText));
                
                m_label.Text = string.Empty;
                m_label.FormattedText = finalFormattedText;
            }
        }
    }

    private string GetTextFromLabel()
    {
        if (!string.IsNullOrEmpty(m_label.Text))
            return m_label.Text;

        return m_label.FormattedText is not null ? m_label.FormattedText.ToString() : m_label.Text;
    }
    
    private FormattedString CreateTruncatedFormattedText(int targetLength)
    {
        var newFormattedString = new FormattedString();
        var currentLength = 0;
        
        foreach (var span in m_label.FormattedText!.Spans)
        {
            var spanText = span.Text ?? string.Empty;
            var remainingLength = targetLength - currentLength;
            
            if (remainingLength <= 0)
                break;
                
            if (spanText.Length <= remainingLength)
            {
                // Include the entire span
                newFormattedString.Spans.Add(new Span
                {
                    Text = spanText,
                    FontSize = span.FontSize,
                    FontFamily = span.FontFamily,
                    TextColor = span.TextColor,
                    FontAttributes = span.FontAttributes,
                    TextDecorations = span.TextDecorations,
                    CharacterSpacing = span.CharacterSpacing,
                    LineHeight = span.LineHeight
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
                    FontSize = span.FontSize,
                    FontFamily = span.FontFamily,
                    TextColor = span.TextColor,
                    FontAttributes = span.FontAttributes,
                    TextDecorations = span.TextDecorations,
                    CharacterSpacing = span.CharacterSpacing,
                    LineHeight = span.LineHeight
                });
                break;
            }
        }
        
        return newFormattedString;
    }
    
    private void ApplyFormattedTextTruncation(int targetLength)
    {
        var newFormattedString = CreateTruncatedFormattedText(targetLength);
        newFormattedString.Spans.Add(m_label.CreateTruncatedTextSpan(m_label.TruncatedText ?? ""));
        
        m_label.FormattedText = null;
        m_label.FormattedText = newFormattedString;
    }
    
}