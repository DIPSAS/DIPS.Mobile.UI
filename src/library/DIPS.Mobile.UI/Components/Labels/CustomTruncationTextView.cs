namespace DIPS.Mobile.UI.Components.Labels;

public partial class CustomTruncationTextView : Grid
{
    private readonly Label m_customTruncationTextLabel;
    private readonly CheckTruncatedLabel.CheckTruncatedLabel m_checkTruncatedLabel;

    public CustomTruncationTextView()
    {
        ColumnDefinitions = 
        [
            new ColumnDefinition { Width = GridLength.Star },
            new ColumnDefinition { Width = GridLength.Auto }
        ];

        m_checkTruncatedLabel = new CheckTruncatedLabel.CheckTruncatedLabel
        {
            LineBreakMode = LineBreakMode.TailTruncation,
            VerticalTextAlignment = TextAlignment.Center
        };
        m_customTruncationTextLabel = new Label
        {
            HorizontalTextAlignment = TextAlignment.End,
            VerticalTextAlignment = TextAlignment.End,
            TextTransform = TextTransform.Lowercase
        };
        
        this.SetBinding(IsTruncatedProperty, static (CheckTruncatedLabel.CheckTruncatedLabel customTruncationTextView) => customTruncationTextView.IsTruncated, source: m_checkTruncatedLabel);
        
        m_checkTruncatedLabel.SetBinding(Microsoft.Maui.Controls.Label.TextProperty, static (CustomTruncationTextView customTruncationTextView) => customTruncationTextView.Text, source: this);
        m_checkTruncatedLabel.SetBinding(Microsoft.Maui.Controls.Label.TextColorProperty, static (CustomTruncationTextView customTruncationTextView) => customTruncationTextView.TextColor, source: this);
        m_checkTruncatedLabel.SetBinding(Microsoft.Maui.Controls.Label.MaxLinesProperty, static (CustomTruncationTextView customTruncationTextView) => customTruncationTextView.MaxLines, source: this);
        m_checkTruncatedLabel.SetBinding(Microsoft.Maui.Controls.Label.FormattedTextProperty, static (CustomTruncationTextView customTruncationTextView) => customTruncationTextView.FormattedText, source: this);
        
        m_customTruncationTextLabel.SetBinding(Microsoft.Maui.Controls.Label.TextProperty, static (CustomTruncationTextView customTruncationTextView) => customTruncationTextView.TruncatedText, source: this);
        m_customTruncationTextLabel.SetBinding(Microsoft.Maui.Controls.Label.TextColorProperty, static (CustomTruncationTextView customTruncationTextView) => customTruncationTextView.TruncatedTextColor, source: this);
        m_customTruncationTextLabel.SetBinding(IsVisibleProperty, static (CheckTruncatedLabel.CheckTruncatedLabel checkTruncatedLabel) => checkTruncatedLabel.IsTruncated, source: m_checkTruncatedLabel);
        
        this.Add(m_checkTruncatedLabel);
        this.Add(m_customTruncationTextLabel, 1);
        
        OnTruncatedTextStyleChanged();
        OnStyleChanged();
    }

    private void OnTruncatedTextStyleChanged()
    {
        m_customTruncationTextLabel.Style = TruncatedTextStyle;
    }

    private void OnStyleChanged()
    {
        m_checkTruncatedLabel.Style = Style;
    }
}