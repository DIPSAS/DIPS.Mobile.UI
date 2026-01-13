using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;

namespace DIPS.Mobile.UI.Formatters;

public static partial class StringFormatter
{
    public static string ReplaceAllEmojisWithPlaceholder(string input)
    {
        try
        {
            var output = new StringBuilder();
            var enumerator = StringInfo.GetTextElementEnumerator(input);

            // Regex to detect if the grapheme is an emoji or emoji component
            // Includes all surrogate pairs and symbol characters likely to be emojis
            var emojiRegex = MyRegex();

            while (enumerator.MoveNext())
            {
                var element = enumerator.GetTextElement();

                // A more complete check for emoji characters (surrogate pairs, symbols, modifier letters)
                // You can customize this regex if needed to be more strict or broad
                if (emojiRegex.IsMatch(element))
                {
                    output.Append("[Emoji]");
                }
                else
                {
                    output.Append(element);
                }
            }

            return output.ToString();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
    
    [GeneratedRegex("^\\p{Cs}|\\p{So}|\\p{Sk}$", RegexOptions.Compiled)]
    private static partial Regex MyRegex();
}