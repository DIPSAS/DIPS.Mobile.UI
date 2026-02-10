using System.ComponentModel;

namespace DIPS.Mobile.UI.Components.ListItems.Options.Subtitle;

public partial class SubtitleOptions : ListItemOptions
{
    private Label? m_subscribedLabel;

    public static void SetupDefaults(ListItem listItem)
    {
        if (listItem.SubtitleLabel is null)
            return;
        
        listItem.SubtitleLabel.Style = (Style?)StyleProperty.DefaultValue;
        listItem.SubtitleLabel.FontAttributes = (FontAttributes)FontAttributesProperty.DefaultValue;
        listItem.SubtitleLabel.TextColor = (Color)TextColorProperty.DefaultValue;
        listItem.SubtitleLabel.LineBreakMode = (LineBreakMode)LineBreakModeProperty.DefaultValue;
    }

    protected override void DoBind(ListItem listItem)
    {
        if (listItem.SubtitleLabel is null)
            return;
        
        m_subscribedLabel = listItem.SubtitleLabel;
        
        listItem.SubtitleLabel.SetBinding(Label.FontAttributesProperty, static (SubtitleOptions options) => options.FontAttributes, source: this);
        listItem.SubtitleLabel.SetBinding(VisualElement.StyleProperty, static (SubtitleOptions options) => options.Style, source: this);
        listItem.SubtitleLabel.SetBinding(Label.TextColorProperty, static (SubtitleOptions options) => options.TextColor, source: this);
        listItem.SubtitleLabel.SetBinding(Label.LineBreakModeProperty, static (SubtitleOptions options) => options.LineBreakMode, source: this);
        listItem.SubtitleLabel.SetBinding(Label.FormattedTextProperty, static (SubtitleOptions options) => options.FormattedText, source: this);
        
        listItem.SubtitleLabel.PropertyChanged += SubtitleLabelOnPropertyChanged;
        
        if (MaxLines > -1) //We can not trigger property changed for this if its -1 because it causes bugs on Android.
        {
            listItem.SubtitleLabel.MaxLines = MaxLines;
        }
    }

    private void SubtitleLabelOnPropertyChanged(object? sender, PropertyChangedEventArgs e)
    {
        if (e.PropertyName == Label.TextProperty.PropertyName)
        {
            if (sender is Label label)
            {
                UpdateVisibility(label);
            }
        }
        else if (e.PropertyName == Label.FormattedTextProperty.PropertyName)
        {
            if (sender is Label label)
            {
                UpdateVisibility(label);
            }
        }
    }

    private bool IsSubtitleEmptyOrNull(Label label)
    {
        return string.IsNullOrEmpty(FormattedText is not null ? FormattedText.ToString() : label.Text);
    }

    private void UpdateVisibility(Label label)
    {
        label.IsVisible = !IsSubtitleEmptyOrNull(label);
    }

    protected override void DoUnbind()
    {
        if (m_subscribedLabel is not null)
        {
            m_subscribedLabel.PropertyChanged -= SubtitleLabelOnPropertyChanged;
            m_subscribedLabel = null;
        }
    }
}