using Colors = DIPS.Mobile.UI.Resources.Colors.Colors;

namespace DIPS.Mobile.UI.Components.SyntaxHighlighting;

public partial class CodeViewer
{
    public static readonly BindableProperty CodeProperty = BindableProperty.Create(
        nameof(Code),
        typeof(string),
        typeof(CodeViewer),propertyChanged: (bindable, _, _) => ((CodeViewer)bindable).OnCodeChanged());
    
    public static readonly BindableProperty LanguageProperty = BindableProperty.Create(
        nameof(Language),
        typeof(string),
        typeof(CodeViewer),defaultValue:string.Empty);

    /// <summary>
    /// The code to display for people in the viewer.
    /// </summary>
    public string Code
    {
        get => (string)GetValue(CodeProperty);
        set => SetValue(CodeProperty, value);
    }

    /// <summary>
    /// Setting language will provide better performance of the loading of the web view, and it will include prettyfi (formatted) code. 
    /// </summary>
    /// <remarks>Supported languages can be found <a href="https://github.com/highlightjs/highlight.js/blob/main/SUPPORTED_LANGUAGES.md"/>. The languages that requires an additional <c>package</c> is not supported. The component will autodetect the language of the code if Language is left empty.  Remember to always bind <c>Language</c> property before <c>Code</c> or else it will not work.</remarks>
    public string Language
    {
        get => (string)GetValue(LanguageProperty);
        set => SetValue(LanguageProperty, value);
    }
    
}