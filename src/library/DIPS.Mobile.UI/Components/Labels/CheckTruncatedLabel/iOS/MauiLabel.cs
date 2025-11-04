using CoreFoundation;
using CoreGraphics;
using DIPS.Mobile.UI.Internal.Logging;
using UIKit;

namespace DIPS.Mobile.UI.Components.Labels.CheckTruncatedLabel.iOS;

public class MauiLabel : Microsoft.Maui.Platform.MauiLabel
{
    private readonly CheckTruncatedLabel m_label;
    private bool m_needsTruncationCheck = true;
    private CGRect m_previousBounds;

    public MauiLabel(CheckTruncatedLabel label)
    {
        m_label = label;
        
        // Listen for property changes that might affect truncation
        m_label.PropertyChanged += OnLabelPropertyChanged;
    }

    private void OnLabelPropertyChanged(object? sender, System.ComponentModel.PropertyChangedEventArgs e)
    {
        if (e.PropertyName is nameof(CheckTruncatedLabel.Text) or nameof(CheckTruncatedLabel.FormattedText) or nameof(CheckTruncatedLabel.MaxLines))
        {
            m_needsTruncationCheck = true;
            SetNeedsLayout();
        }
    }
    
    public override void LayoutSubviews()
    {
        base.LayoutSubviews();

        // If bounds are changed, we need to recheck truncation
        if (!Bounds.Equals(m_previousBounds))
        {
            m_needsTruncationCheck = true;
        }
        
        if (!m_needsTruncationCheck || Bounds.IsEmpty)
            return;
            
        // Schedule a delayed check to ensure layout is complete
        DispatchQueue.MainQueue.DispatchAsync(CheckTruncationAfterLayout);
        
        m_needsTruncationCheck = false;
        m_previousBounds = Bounds;
    }

    private void CheckTruncationAfterLayout()
    {
        // Check if text would need more lines if unconstrained
        var isTruncated = WouldTextExceedMaxLines();
        
        // Update the IsTruncated property
        m_label.IsTruncated = isTruncated;
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
            
            // If unconstrained height > constrained height, then text is truncated
            var isTruncated = unconstrainedSize.Height > constrainedSize.Height + 1; // +1 for rounding tolerance
            
            return isTruncated;
        }
        catch (Exception ex)
        {
            DUILogService.LogError<MauiLabel>($"Error checking truncation: {ex.Message}");
            return false;
        }
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