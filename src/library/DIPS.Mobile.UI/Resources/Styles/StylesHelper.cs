namespace DIPS.Mobile.UI.Resources.Styles;

public static class StylesHelper
{
    public static Style ConcatenateWithStyle(this Style style, Style style2)
    {
        style.BasedOn = style2;
        return style;
    }
}