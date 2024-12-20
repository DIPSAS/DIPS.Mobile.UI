using System.Text.Json;
using System.Web;
using DIPS.Mobile.UI.Internal.Logging;
using Label = DIPS.Mobile.UI.Components.Labels.Label;

namespace DIPS.Mobile.UI.Components.SyntaxHighlighting;

/// <summary>
/// This is a webview that uses https://highlightjs.org. The javascript and css is loaded in the app as a raw resource, so no need for internet connection.
/// See Arena.Mobile/Resources/Raw/syntax-highlight for the source code.
/// </summary>
public partial class CodeViewer : ContentView
{
    private readonly WebView m_webView;
    private readonly JsonSerializerOptions m_jsonPrettifyOptions;

    public CodeViewer()
    {
        m_webView = new WebView();
        m_jsonPrettifyOptions = new JsonSerializerOptions {WriteIndented = true};
        Content = m_webView;
    }
    
    private void OnCodeChanged()
    {
        try
        {
            var cssPath = "default.min.css";
            var javascriptPath = "highlight.min.js";

            Code = TryPrettify(Code);
            
            var html = $@"
<!DOCTYPE html>
<html lang=""en"">
<head>
    <meta charset=""UTF-8"">
    <meta name=""viewport"" content=""width=device-width, initial-scale=1.0"">

    <!-- Make sure the background is transparent -->
    <style>
        .code-container pre, .code-container code {{
            background-color: transparent;
        }}
    </style>
    <!-- Local Highlight.js CSS -->
    <link rel=""stylesheet"" href=""{cssPath}"">

    <!-- Local Highlight.js JavaScript -->
    <script src=""{javascriptPath}""></script>

    <!-- Activate Highlight.js -->
    <script>
        document.addEventListener(""DOMContentLoaded"", () => {{
            hljs.highlightAll();
        }});
    </script>
</head>
<body>
<div class='code-container'>
    <pre><code class=""{Language}"">
{HttpUtility.HtmlEncode(Code)}
    </code></pre>
</div>
</body>
</html>
";
            m_webView.Source = new HtmlWebViewSource() {Html = html};
        }
        catch (Exception e)
        {
            Content = new Label() {Text = e.Message};
            DUILogService.LogError<CodeViewer>(e.Message);
        }
    }

    private string TryPrettify(string code)
    {
        if (Language.Equals("json", StringComparison.InvariantCultureIgnoreCase))
        {
            return JsonSerializer.Serialize(JsonSerializer.Deserialize<object>(code),m_jsonPrettifyOptions);
        }

        return code;
    }
}
