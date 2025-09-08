#if IOS
using CoreFoundation;
using CoreGraphics;
using DIPS.Mobile.UI.Internal.Logging;
using Foundation;
using Microsoft.Maui.Platform;
using UIKit;

namespace DIPS.Mobile.UI.Components.Labels;

public class MauiLabel : Microsoft.Maui.Platform.MauiLabel
{
    private readonly CheckTruncatedLabel m_label;
    private bool m_needsTruncationCheck = true;

    public MauiLabel(CheckTruncatedLabel label)
    {
        m_label = label;
        
        // Listen for property changes that might affect truncation
        m_label.PropertyChanged += OnLabelPropertyChanged;
    }

    private void OnLabelPropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
    {
        if (e.PropertyName == nameof(CheckTruncatedLabel.Text) || 
            e.PropertyName == nameof(CheckTruncatedLabel.FormattedText) ||
            e.PropertyName == nameof(CheckTruncatedLabel.MaxLines))
        {
            DUILogService.LogDebug<MauiLabel>($"Property changed: {e.PropertyName}");
            m_needsTruncationCheck = true;
            SetNeedsLayout();
        }
    }

    public override void LayoutSubviews()
    {
        base.LayoutSubviews();
        
        if (!m_needsTruncationCheck)
            return;
            
        DUILogService.LogDebug<MauiLabel>($"LayoutSubviews - Starting truncation check. Lines: {Lines}, MaxLines: {m_label.MaxLines}");
        
        // Schedule a delayed check to ensure layout is complete
        DispatchQueue.MainQueue.DispatchAsync(CheckTruncationAfterLayout);
        
        m_needsTruncationCheck = false;
    }

    private void CheckTruncationAfterLayout()
    {
        DUILogService.LogDebug<MauiLabel>($"CheckTruncationAfterLayout - Lines: {Lines}, MaxLines: {m_label.MaxLines}, NumberOfLines: {Lines}");
        
        // Check if text would need more lines if unconstrained
        var isTruncated = WouldTextExceedMaxLines();
        
        DUILogService.LogDebug<MauiLabel>($"CheckTruncationAfterLayout - IsTruncated: {isTruncated}");
        
        // Update the IsTruncated property
        m_label.IsTruncated = isTruncated;
        
        DUILogService.LogDebug<MauiLabel>($"CheckTruncationAfterLayout - Final IsTruncated set to: {isTruncated}");
    }

    private bool WouldTextExceedMaxLines()
    {
        if (Bounds.Width <= 0 || m_label.MaxLines <= 0)
        {
            return false;
        }

        try
        {
            // Create two temporary labels: one unconstrained, one constrained to MaxLines
            using var unconstrainedLabel = new UILabel();
            using var constrainedLabel = new UILabel();
            
            // Configure both labels identically to this label
            if (base.AttributedText != null && base.AttributedText.Length > 0)
            {
                unconstrainedLabel.AttributedText = base.AttributedText;
                constrainedLabel.AttributedText = base.AttributedText;
            }
            else if (!string.IsNullOrEmpty(base.Text))
            {
                unconstrainedLabel.Text = base.Text;
                unconstrainedLabel.Font = base.Font;
                constrainedLabel.Text = base.Text;
                constrainedLabel.Font = base.Font;
            }
            else
            {
                return false;
            }
            
            // Configure line breaking
            unconstrainedLabel.LineBreakMode = base.LineBreakMode;
            constrainedLabel.LineBreakMode = base.LineBreakMode;
            
            // Unconstrained: no line limit
            unconstrainedLabel.Lines = 0;
            
            // Constrained: limited to MaxLines
            constrainedLabel.Lines = m_label.MaxLines;
            
            // Measure both
            var unconstrainedSize = unconstrainedLabel.SizeThatFits(new CGSize(Bounds.Width, nfloat.PositiveInfinity));
            var constrainedSize = constrainedLabel.SizeThatFits(new CGSize(Bounds.Width, nfloat.PositiveInfinity));
            
            DUILogService.LogDebug<MauiLabel>($"Height comparison - Unconstrained: {unconstrainedSize.Height}, Constrained ({m_label.MaxLines} lines): {constrainedSize.Height}");
            
            // If unconstrained height > constrained height, then text is truncated
            var isTruncated = unconstrainedSize.Height > constrainedSize.Height + 1; // +1 for rounding tolerance
            
            DUILogService.LogDebug<MauiLabel>($"Text would exceed MaxLines: {isTruncated}");
            
            return isTruncated;
        }
        catch (Exception ex)
        {
            DUILogService.LogError<MauiLabel>($"Error checking truncation: {ex.Message}");
            return false;
        }
    }

    private bool IsTextTruncated()
    {
        DUILogService.LogDebug<MauiLabel>($"IsTextTruncated - Starting check. Bounds: {Bounds}, MaxLines: {m_label.MaxLines}");
        
        if (Bounds.Width <= 0 || m_label.MaxLines <= 0)
        {
            DUILogService.LogDebug<MauiLabel>($"Invalid bounds or MaxLines - Width: {Bounds.Width}, MaxLines: {m_label.MaxLines}");
            return false;
        }

        var maxLines = m_label.MaxLines;

        try
        {
            if (base.AttributedText != null && base.AttributedText.Length > 0)
            {
                DUILogService.LogDebug<MauiLabel>($"Checking AttributedText: '{base.AttributedText.Value}', Length: {base.AttributedText.Length}");
                
                var textBounds = base.AttributedText.GetBoundingRect(
                    new CGSize(Bounds.Width, nfloat.PositiveInfinity),
                    NSStringDrawingOptions.UsesLineFragmentOrigin,
                    null);
                var numberOfLines = (int)Math.Ceiling(textBounds.Height / base.Font.LineHeight);
                
                DUILogService.LogDebug<MauiLabel>($"AttributedText - Lines: {numberOfLines}, MaxLines: {maxLines}, Height: {textBounds.Height}, LineHeight: {base.Font.LineHeight}, Result: {numberOfLines > maxLines}");
                
                return numberOfLines > maxLines;
            }
            else if (!string.IsNullOrEmpty(base.Text))
            {
                DUILogService.LogDebug<MauiLabel>($"Checking PlainText: '{base.Text}', Length: {base.Text.Length}");
                
                var nsString = new NSString(base.Text);
                var textBounds = nsString.GetBoundingRect(
                    new CGSize(Bounds.Width, nfloat.PositiveInfinity),
                    NSStringDrawingOptions.UsesLineFragmentOrigin,
                    new UIStringAttributes { Font = base.Font },
                    null);
                var numberOfLines = (int)Math.Ceiling(textBounds.Height / base.Font.LineHeight);
                
                DUILogService.LogDebug<MauiLabel>($"PlainText - Lines: {numberOfLines}, MaxLines: {maxLines}, Height: {textBounds.Height}, LineHeight: {base.Font.LineHeight}, Result: {numberOfLines > maxLines}");
                
                return numberOfLines > maxLines;
            }
            else
            {
                DUILogService.LogDebug<MauiLabel>("No text content to check");
            }
        }
        catch (Exception ex)
        {
            DUILogService.LogError<MauiLabel>($"Error checking truncation: {ex.Message}");
        }

        DUILogService.LogDebug<MauiLabel>("IsTextTruncated - Returning false");
        return false;
    }

    protected override void Dispose(bool disposing)
    {
        if (disposing)
        {
            m_label.PropertyChanged -= OnLabelPropertyChanged;
        }
        base.Dispose(disposing);
    }
}
#endif
