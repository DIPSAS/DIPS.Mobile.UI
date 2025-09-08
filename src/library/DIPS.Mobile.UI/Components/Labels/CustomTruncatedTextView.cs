namespace DIPS.Mobile.UI.Components.Labels;

public partial class CustomTruncatedTextView : Grid
{
    public CustomTruncatedTextView()
    {
        ColumnDefinitions = 
        [
            new ColumnDefinition { Width = GridLength.Star },
            new ColumnDefinition { Width = GridLength.Auto }
        ];

        var checkTruncatedLabel = new CheckTruncatedLabel { LineBreakMode = LineBreakMode.TailTruncation };
        var customTruncatedTextLabel = new Label
        {
            HorizontalTextAlignment = TextAlignment.End,
            VerticalTextAlignment = TextAlignment.End
        };
        
        checkTruncatedLabel.SetBinding(Microsoft.Maui.Controls.Label.TextProperty, static (CustomTruncatedTextView customTruncatedTextView) => customTruncatedTextView.Text, source: this);
        checkTruncatedLabel.SetBinding(Microsoft.Maui.Controls.Label.TextColorProperty, static (CustomTruncatedTextView customTruncatedTextView) => customTruncatedTextView.TextColor, source: this);
        checkTruncatedLabel.SetBinding(Microsoft.Maui.Controls.Label.MaxLinesProperty, static (CustomTruncatedTextView customTruncatedTextView) => customTruncatedTextView.MaxLines, source: this);
        checkTruncatedLabel.SetBinding(FormattedTextProperty, static (CustomTruncatedTextView customTruncatedTextView) => customTruncatedTextView.FormattedText, source: this);
        checkTruncatedLabel.SetBinding(StyleProperty, static (CustomTruncatedTextView customTruncatedTextView) => customTruncatedTextView.Style, source: this);
        
        customTruncatedTextLabel.SetBinding(Microsoft.Maui.Controls.Label.TextProperty, static (CustomTruncatedTextView customTruncatedTextView) => customTruncatedTextView.TruncatedText, source: this);
        customTruncatedTextLabel.SetBinding(Microsoft.Maui.Controls.Label.TextColorProperty, static (CustomTruncatedTextView customTruncatedTextView) => customTruncatedTextView.TruncatedTextColor, source: this);
        customTruncatedTextLabel.SetBinding(IsVisibleProperty, static (CheckTruncatedLabel checkTruncatedLabel) => checkTruncatedLabel.IsTruncated, source: checkTruncatedLabel);
        customTruncatedTextLabel.SetBinding(StyleProperty, static (CustomTruncatedTextView customTruncatedTextView) => customTruncatedTextView.TruncatedTextStyle, source: this);
        
        this.Add(checkTruncatedLabel);
        this.Add(customTruncatedTextLabel, 1);
    }
}