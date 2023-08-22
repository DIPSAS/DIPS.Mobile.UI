namespace DIPS.Mobile.UI.Components.Labels;

public partial class LabelHandler : Microsoft.Maui.Handlers.LabelHandler
{
    public LabelHandler() : base(LabelPropertyMapper)
    {
    }
    
    public static readonly IPropertyMapper<Label, LabelHandler> LabelPropertyMapper =
        new PropertyMapper<Label, LabelHandler>(Mapper)
        {
            [nameof(Label.MaxLines)] = MapOverrideMaxLinesAndLineBreakMode,
            [nameof(Label.LineBreakMode)] = MapOverrideMaxLinesAndLineBreakMode
        };

    private static partial void MapOverrideMaxLinesAndLineBreakMode(LabelHandler handler, Label label);
}