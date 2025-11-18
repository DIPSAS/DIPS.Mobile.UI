using Android.Content;
using Android.Graphics;
using Android.Util;
using Android.Views;
using DIPS.Mobile.UI.Internal.Logging;

namespace DIPS.Mobile.UI.Components.Labels.CheckTruncatedLabel.Android;

public class MauiTextView : Microsoft.Maui.Platform.MauiTextView
{
    private readonly CheckTruncatedLabel m_label;
    private bool m_needsTruncationCheck = true;

    public MauiTextView(Context context, CheckTruncatedLabel label) : base(context)
    {
        m_label = label;
        
        // Listen for property changes that might affect truncation
        m_label.PropertyChanged += OnLabelPropertyChanged;
    }

    private void OnLabelPropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
    {
        if (e.PropertyName is nameof(CheckTruncatedLabel.Text) or nameof(CheckTruncatedLabel.FormattedText) or nameof(CheckTruncatedLabel.MaxLines))
        {
            m_needsTruncationCheck = true;
            RequestLayout();
        }
    }

    protected override void OnDraw(Canvas? canvas)
    {
        base.OnDraw(canvas);
        
        if (!m_needsTruncationCheck)
            return;
            
        // Schedule truncation check after layout is complete
        Post(CheckTruncationAfterLayout);
        
        m_needsTruncationCheck = false;
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
        if (Width <= 0 || m_label.MaxLines <= 0)
        {
            return false;
        }

        try
        {
            // Create two temporary TextViews: one unconstrained, one constrained to MaxLines
            using var unconstrainedTextView = new AndroidX.AppCompat.Widget.AppCompatTextView(Context);
            using var constrainedTextView = new AndroidX.AppCompat.Widget.AppCompatTextView(Context);
            
            // Configure both TextViews identically to this TextView
            if (TextFormatted != null)
            {
                unconstrainedTextView.TextFormatted = TextFormatted;
                constrainedTextView.TextFormatted = TextFormatted;
            }
            else if (!string.IsNullOrEmpty(Text))
            {
                unconstrainedTextView.Text = Text;
                unconstrainedTextView.Typeface = Typeface;
                unconstrainedTextView.SetTextSize(ComplexUnitType.Px, TextSize);
                constrainedTextView.Text = Text;
                constrainedTextView.Typeface = Typeface;
                constrainedTextView.SetTextSize(ComplexUnitType.Px, TextSize);
            }
            else
            {
                return false;
            }
            
            // Configure line breaking and ellipsize
            unconstrainedTextView.Ellipsize = Ellipsize;
            constrainedTextView.Ellipsize = Ellipsize;
            
            // Unconstrained: no line limit
            unconstrainedTextView.SetMaxLines(int.MaxValue);
            
            // Constrained: limited to MaxLines
            constrainedTextView.SetMaxLines(m_label.MaxLines);
            
            // Measure both with the same width
            var widthSpec = MeasureSpec.MakeMeasureSpec(Width, MeasureSpecMode.Exactly);
            var heightSpec = MeasureSpec.MakeMeasureSpec(0, MeasureSpecMode.Unspecified);
            
            unconstrainedTextView.Measure(widthSpec, heightSpec);
            constrainedTextView.Measure(widthSpec, heightSpec);
            
            var unconstrainedHeight = unconstrainedTextView.MeasuredHeight;
            var constrainedHeight = constrainedTextView.MeasuredHeight;
            
            // If unconstrained height > constrained height, then text is truncated
            var isTruncated = unconstrainedHeight > constrainedHeight + 5; // +5 for rounding tolerance
            
            return isTruncated;
        }
        catch (System.Exception ex)
        {
            DUILogService.LogError<MauiTextView>($"Error checking truncation: {ex.Message}");
            return false;
        }
    }

    public void Cleanup()
    {
        m_label.PropertyChanged -= OnLabelPropertyChanged;
    }
}
