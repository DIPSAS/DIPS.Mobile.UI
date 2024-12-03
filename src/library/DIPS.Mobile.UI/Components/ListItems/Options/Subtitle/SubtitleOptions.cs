using DIPS.Mobile.UI.Resources.Styles;
using DIPS.Mobile.UI.Resources.Styles.Label;
using Colors = DIPS.Mobile.UI.Resources.Colors.Colors;

namespace DIPS.Mobile.UI.Components.ListItems.Options.Subtitle;

public partial class SubtitleOptions : ListItemOptions
{
    public override void DoBind(ListItem listItem)
    {
        if(listItem.SubtitleLabel is null)
            return;            
        
        listItem.SubtitleLabel.SetBinding(Label.TextProperty, static (ListItem listItem) => listItem.Subtitle, source: listItem);
        listItem.SubtitleLabel.SetBinding(Label.FontAttributesProperty, static (SubtitleOptions options) => options.FontAttributes, source: this);
        listItem.SubtitleLabel.SetBinding(Label.HorizontalTextAlignmentProperty, static (SubtitleOptions options) => options.HorizontalTextAlignment, source: this);
        listItem.SubtitleLabel.SetBinding(Label.VerticalTextAlignmentProperty, static (SubtitleOptions options) => options.VerticalTextAlignment, source: this);
        listItem.SubtitleLabel.SetBinding(VisualElement.StyleProperty, static (SubtitleOptions options) => options.Style, source: this);
        listItem.SubtitleLabel.SetBinding(Label.TextColorProperty, static (SubtitleOptions options) => options.TextColor, source: this);
        listItem.SubtitleLabel.SetBinding(Label.LineBreakModeProperty, static (SubtitleOptions options) => options.LineBreakMode, source: this);
        listItem.SubtitleLabel.SetBinding(Label.FormattedTextProperty, static (SubtitleOptions options) => options.FormattedText, source: this);

        UpdateVisibility(listItem);
        
        if (MaxLines > -1) //We can not trigger property changed for this if its -1 because it causes bugs on Android.
        {
            listItem.SubtitleLabel.MaxLines = MaxLines;
        }
    }

    private bool IsSubtitleEmptyOrNull(ListItem listItem)
    {
        if (FormattedText is not null)
        {
            return string.IsNullOrEmpty(FormattedText.ToString());
        }

        return string.IsNullOrEmpty(listItem.Subtitle);
    }

    public void UpdateVisibility(ListItem listItem)
    {
        listItem.SubtitleLabel.IsVisible = !IsSubtitleEmptyOrNull(listItem);
    }
}