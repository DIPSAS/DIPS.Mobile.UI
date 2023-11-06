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
        
        listItem.SubtitleLabel.SetBinding(Label.FontAttributesProperty, new Binding(nameof(FontAttributes), source: this));
        listItem.SubtitleLabel.SetBinding(Label.HorizontalTextAlignmentProperty, new Binding(nameof(HorizontalTextAlignment), source: this));
        listItem.SubtitleLabel.SetBinding(Label.VerticalTextAlignmentProperty, new Binding(nameof(VerticalTextAlignment), source: this));
        listItem.SubtitleLabel.Style = Styles.GetLabelStyle(LabelStyle.Body200);
        listItem.SubtitleLabel.TextColor = Colors.GetColor(ColorName.color_neutral_60);

    }
}