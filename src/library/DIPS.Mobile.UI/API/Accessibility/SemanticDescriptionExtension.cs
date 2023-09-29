using DIPS.Mobile.UI.Resources.LocalizedStrings.LocalizedStrings;

namespace DIPS.Mobile.UI.API.Accessibility;

public class SemanticDescriptionExtension : IMarkupExtension<string>
{
    /// <summary>
    /// The type of visual control people are focusing on when using TalkBack or VoiceOver.
    /// </summary>
    public ControlType Type { get; set; }
    
    /// <summary>
    /// The description to read to people when TalkBack or VoiceOver is activated.
    /// </summary>
    public string Description { get; set; }

    public string ProvideValue(IServiceProvider serviceProvider) => SemanticDescription.GetDescription(Description, Type);

    object IMarkupExtension.ProvideValue(IServiceProvider serviceProvider)
    {
        return ProvideValue(serviceProvider);
    }
}

public static class SemanticDescription
{
    /// <summary>
    /// Get the description with the <see cref="ControlType"/> included.
    /// </summary>
    /// <param name="description">The description to read to people when TalkBack or VoiceOver is activated.</param>
    /// <param name="type">The type of visual control people are focusing on when using TalkBack or VoiceOver.</param>
    /// <returns></returns>
    /// <exception cref="ArgumentOutOfRangeException"></exception>
    public static string GetDescription(string description, ControlType type)
    {
#if __IOS__
        var controlType = "";
        switch (type)
        {
            case ControlType.None:
            case ControlType.Label:
                break;
            case ControlType.Button:
                controlType = DUILocalizedStrings.Button;
                break;
            case ControlType.Choice:
                controlType = DUILocalizedStrings.Choice;
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }

        return $"{description}, {controlType}";
#endif
        return description;
    }
}