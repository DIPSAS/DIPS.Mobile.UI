namespace DIPS.Mobile.UI.Resources.Styles.Shared;

public static class SharedStyleResources
{
    public static Dictionary<TextStyle, Style> GetStylesForType(Type targetType)
    {
        return new Dictionary<TextStyle, Style>
        {
            [TextStyle.None] = new Style(targetType),
            [TextStyle.Body400] = SharedFontFamilyStyle.GetBodyStyle(targetType).ConcatenateWithStyle(SharedWeightStyle.GetFourHundredStyle(targetType)), 
            [TextStyle.Body300] = SharedFontFamilyStyle.GetBodyStyle(targetType).ConcatenateWithStyle(SharedWeightStyle.GetThreeHundredStyle(targetType)), 
            [TextStyle.Body200] = SharedFontFamilyStyle.GetBodyStyle(targetType).ConcatenateWithStyle(SharedWeightStyle.GetTwoHundredStyle(targetType)), 
            [TextStyle.Body100] = SharedFontFamilyStyle.GetBodyStyle(targetType).ConcatenateWithStyle(SharedWeightStyle.GetOneHundredStyle(targetType)), 
            
            [TextStyle.UI400] = SharedFontFamilyStyle.GetUIStyle(targetType).ConcatenateWithStyle(SharedWeightStyle.GetFourHundredStyle(targetType)), 
            [TextStyle.UI300] = SharedFontFamilyStyle.GetUIStyle(targetType).ConcatenateWithStyle(SharedWeightStyle.GetThreeHundredStyle(targetType)), 
            [TextStyle.UI200] = SharedFontFamilyStyle.GetUIStyle(targetType).ConcatenateWithStyle(SharedWeightStyle.GetTwoHundredStyle(targetType)), 
            [TextStyle.UI100] = SharedFontFamilyStyle.GetUIStyle(targetType).ConcatenateWithStyle(SharedWeightStyle.GetOneHundredStyle(targetType)), 

            [TextStyle.Header1000] = SharedFontFamilyStyle.GetHeaderStyle(targetType).ConcatenateWithStyle(SharedWeightStyle.GetOneThousandStyle(targetType)), 
            [TextStyle.Header900] = SharedFontFamilyStyle.GetHeaderStyle(targetType).ConcatenateWithStyle(SharedWeightStyle.GetNineHundredStyle(targetType)), 
            [TextStyle.Header800] = SharedFontFamilyStyle.GetHeaderStyle(targetType).ConcatenateWithStyle(SharedWeightStyle.GetEightHundredStyle(targetType)), 
            [TextStyle.Header700] = SharedFontFamilyStyle.GetHeaderStyle(targetType).ConcatenateWithStyle(SharedWeightStyle.GetSevenHundredStyle(targetType)), 
            [TextStyle.Header600] = SharedFontFamilyStyle.GetHeaderStyle(targetType).ConcatenateWithStyle(SharedWeightStyle.GetSixHundredStyle(targetType)), 
            [TextStyle.Header500] = SharedFontFamilyStyle.GetHeaderStyle(targetType).ConcatenateWithStyle(SharedWeightStyle.GetFiveHundredStyle(targetType)),
            
            [TextStyle.SectionHeader] = SharedFontFamilyStyle.GetUIStyle(targetType).ConcatenateWithStyle(SharedWeightStyle.GetFourHundredStyle(targetType))
        };
    }
}
