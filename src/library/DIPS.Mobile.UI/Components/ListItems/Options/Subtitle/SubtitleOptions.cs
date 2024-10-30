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
        
        listItem.SubtitleLabel.SetBinding(Label.TextProperty, new Binding(nameof(ListItem.Subtitle), source: listItem));
        listItem.SubtitleLabel.SetBinding(Label.FontAttributesProperty, new Binding(nameof(FontAttributes), source: this));
        listItem.SubtitleLabel.SetBinding(Label.HorizontalTextAlignmentProperty, new Binding(nameof(HorizontalTextAlignment), source: this));
        listItem.SubtitleLabel.SetBinding(Label.VerticalTextAlignmentProperty, new Binding(nameof(VerticalTextAlignment), source: this));
        listItem.SubtitleLabel.SetBinding(VisualElement.StyleProperty, new Binding(nameof(Style), source: this));
        listItem.SubtitleLabel.SetBinding(Label.TextColorProperty, new Binding(nameof(TextColor), source: this));
        listItem.SubtitleLabel.SetBinding(Label.LineBreakModeProperty, new Binding(nameof(LineBreakMode), source: this));
        listItem.SubtitleLabel.SetBinding(Label.FormattedTextProperty, new Binding(nameof(FormattedText), source: this));

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