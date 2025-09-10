namespace DIPS.Mobile.UI.Components.Labels.CheckTruncatedLabel;

public partial class CheckTruncatedLabelHandler : Microsoft.Maui.Handlers.LabelHandler
{
    public CheckTruncatedLabelHandler() : base(PropertyMapper)
    {
    }

    public static readonly IPropertyMapper<CheckTruncatedLabel, CheckTruncatedLabelHandler> PropertyMapper =
        new PropertyMapper<CheckTruncatedLabel, CheckTruncatedLabelHandler>(Mapper)
        {
#if __IOS__ || __ANDROID__
            [nameof(CheckTruncatedLabel.MaxLines)] = MapOverrideMaxLinesAndLineBreakMode,
            [nameof(CheckTruncatedLabel.LineBreakMode)] = MapOverrideMaxLinesAndLineBreakMode,
#endif
        };
}