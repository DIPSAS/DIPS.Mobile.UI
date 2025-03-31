using System.Globalization;
using DIPS.Mobile.UI.Resources.Colors;
using Colors = Microsoft.Maui.Graphics.Colors;

namespace Arena.Mobile.Domains.MunicipalCare.Shared.Converters;

[AcceptEmptyServiceProvider]
public class TextHighlighter : IMultiValueConverter, IMarkupExtension
{
    public string FontFamily { get; set; } = "UI";
    public Color TextColor { get; set; } = DIPS.Mobile.UI.Resources.Colors.Colors.GetColor(ColorName.color_neutral_90);
    public double FontSize { get; set; } = 16;
    public bool ShouldAddEllipsisAtStart { get; set; } = true;
    
    public object? Convert(object[]? values, Type targetType, object? parameter, CultureInfo culture)
    {
        if (values![0] is not string text)
            return new FormattedString();

        if (values[1] is not string searchText)
            return new FormattedString();

        var formattedString = new FormattedString();

        var firstHighlightedTextIsAdded = false;
        
        var words = text.Replace("\n", " ").Split(" ");
        foreach (var word in words)
        {
            if (string.IsNullOrWhiteSpace(word))
                continue;
            
            if (word.Contains(searchText, StringComparison.CurrentCultureIgnoreCase))
            {
                // The first highlighted text should be at the start of the text
                if (formattedString.Spans.Count > 0 && !firstHighlightedTextIsAdded && ShouldAddEllipsisAtStart)
                {
                    formattedString.Spans.Clear();
                    firstHighlightedTextIsAdded = true;
                    
                    formattedString.Spans.Add(AddSpan("... "));
                }
                
                var startIndexOfMatchedWord = word.ToLower().IndexOf(searchText.ToLower(), StringComparison.Ordinal);
                if(startIndexOfMatchedWord == -1)
                    continue;
                
                var charactersBeforeMatch = word[..startIndexOfMatchedWord];
                var charactersAfterMatch = word[(startIndexOfMatchedWord + searchText.Length)..] + " ";
                var matchedCharacters = word.Substring(startIndexOfMatchedWord, searchText.Length);
                
                if(charactersBeforeMatch.Length > 0)
                    formattedString.Spans.Add(AddSpan(charactersBeforeMatch));
                formattedString.Spans.Add(AddSpan(matchedCharacters, true));
                formattedString.Spans.Add(AddSpan(charactersAfterMatch));
                
            }
            else
            {
                formattedString.Spans.Add(AddSpan(word + " "));
            }
        }

        return formattedString;
    }

    public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }

    private Span AddSpan(string text, bool match = false)
    {
        return new Span { Text = text, BackgroundColor = match ? Colors.Yellow : Colors.Transparent, TextColor = TextColor, FontSize = FontSize, FontFamily = FontFamily };
    }

    public object ProvideValue(IServiceProvider serviceProvider) => this;
}