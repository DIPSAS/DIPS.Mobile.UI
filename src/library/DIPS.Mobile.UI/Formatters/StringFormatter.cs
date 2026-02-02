using System.Globalization;
using System.Text;

namespace DIPS.Mobile.UI.Formatters;

public static class StringFormatter
{
    public static string ReplaceAllEmojisWithPlaceholder(this string input, string placeholder = "[Emoji]")
    {
        if (string.IsNullOrEmpty(input))
            return input;

        var output = new StringBuilder();
        var enumerator = StringInfo.GetTextElementEnumerator(input);

        while (enumerator.MoveNext())
        {
            var element = enumerator.GetTextElement();
            if (IsEmojiTextElement(element))
                output.Append(placeholder);
            else
                output.Append(element);
        }

        return output.ToString();
    }

    // Treat a text element as emoji if it contains ZWJ / VS16 or any rune in known emoji ranges.
    private static bool IsEmojiTextElement(string element)
    {
        if (string.IsNullOrEmpty(element))
            return false;

        // Variation Selector-16 or Zero Width Joiner indicate emoji sequences
        if (element.IndexOf('\uFE0F') >= 0 || element.IndexOf('\u200D') >= 0)
            return true;

        var span = element.AsSpan();
        var i = 0;
        while (i < span.Length)
        {
            var ch = span[i];
            int codePoint;
            int consumed;

            if (char.IsHighSurrogate(ch) && i + 1 < span.Length && char.IsLowSurrogate(span[i + 1]))
            {
                codePoint = char.ConvertToUtf32(ch, span[i + 1]);
                consumed = 2;
            }
            else
            {
                codePoint = ch;
                consumed = 1;
            }

            var rune = new Rune(codePoint);

            if (IsEmojiRune(rune))
                return true;

            i += consumed;
        }

        return false;
    }

    private static bool IsEmojiRune(Rune rune)
    {
        var v = rune.Value;

        return
            (v >= 0x1F600 && v <= 0x1F64F) ||
            (v >= 0x1F300 && v <= 0x1F5FF) ||
            (v >= 0x1F680 && v <= 0x1F6FF) ||
            (v >= 0x1F900 && v <= 0x1F9FF) ||
            (v >= 0x1FA70 && v <= 0x1FAFF) ||
            (v >= 0x2600 && v <= 0x26FF)     ||
            (v >= 0x2700 && v <= 0x27BF)     ||
            (v >= 0x1F1E6 && v <= 0x1F1FF)   ||
            (v >= 0x2300 && v <= 0x23FF)     ||
            v == 0x00A9 || v == 0x00AE ||
            v == 0x203C || v == 0x2049 ||
            v == 0x3030 || v == 0x303D ||
            v == 0x2B50 || v == 0x2B55 ||
            (v >= 0x1F004 && v <= 0x1F0CF);
    }
}
