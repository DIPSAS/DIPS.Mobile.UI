namespace DIPS.Mobile.UI.Resources.Styles.InputField;

public class InputFieldStyleResources
{
    public static Dictionary<InputFieldStyle, Style> Styles => new()
    {
        [InputFieldStyle.Default] = InputFieldDefaultStyle.Current,
        [InputFieldStyle.Focused] = InputFieldTypeStyle.Focused,
        [InputFieldStyle.Disabled] = InputFieldTypeStyle.Disabled
    };
}