public static class StringHelper
{

    public static string RemoveWhitespace(string input)
    {
        return new string(input.ToCharArray()
            .Where(c => !Char.IsWhiteSpace(c))
            .ToArray());
    }

    public static string Between(string input , string firstString, string lastString)
    {       
        int firstStringIndex = input.IndexOf(firstString) + firstString.Length;
        int lastStringIndex = input.IndexOf(lastString);
        return input[firstStringIndex..lastStringIndex];
    }

}