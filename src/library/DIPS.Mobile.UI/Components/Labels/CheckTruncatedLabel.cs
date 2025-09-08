namespace DIPS.Mobile.UI.Components.Labels;

/// <summary>
/// Use this to check if the text is truncated
/// <remarks>This is only possible on this Label to avoid additional overhead</remarks>
/// </summary>
public partial class CheckTruncatedLabel : Label
{
    public CheckTruncatedLabel()
    {
        this.SetAppThemeColor(TextColorProperty, ColorName.color_text_default);
        MaxLines = int.MaxValue;
        Style = DefaultLabelStyle;
    }
}