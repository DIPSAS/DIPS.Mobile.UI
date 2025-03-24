using System.ComponentModel;

namespace DIPS.Mobile.UI.Components.ListItems.Options.Subtitle;

public partial class SubtitleOptions : ListItemOptions
{
    public override void SetupDefaults(ListItem listItem)
    {
        if (listItem.SubtitleLabel is null)
            return;
        
        listItem.SubtitleLabel.Text = listItem.Subtitle;
        listItem.SubtitleLabel.Style = this.Style;
        listItem.SubtitleLabel.FontAttributes = this.FontAttributes;
        listItem.SubtitleLabel.TextColor = this.TextColor;
        listItem.SubtitleLabel.HorizontalTextAlignment = this.HorizontalTextAlignment;
        listItem.SubtitleLabel.VerticalTextAlignment = this.VerticalTextAlignment;
        listItem.SubtitleLabel.LineBreakMode = this.LineBreakMode;
    }

    protected override void DoBind(ListItem listItem)
    {
        if (listItem.SubtitleLabel is null)
            return;
        
        listItem.SubtitleLabel.SetBinding(Label.TextProperty, static (ListItem listItem) => listItem.Subtitle, source: listItem);
        listItem.SubtitleLabel.SetBinding(Label.FontAttributesProperty, static (SubtitleOptions options) => options.FontAttributes, source: this);
        listItem.SubtitleLabel.SetBinding(Label.HorizontalTextAlignmentProperty, static (SubtitleOptions options) => options.HorizontalTextAlignment, source: this);
        listItem.SubtitleLabel.SetBinding(Label.VerticalTextAlignmentProperty, static (SubtitleOptions options) => options.VerticalTextAlignment, source: this);
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
}