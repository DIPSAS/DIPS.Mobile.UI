namespace DIPS.Mobile.UI.Resources.Styles.Label;

internal class LabelStyleResources
{
    public static Dictionary<LabelStyle, Style> Styles { get; } = new()
    {
        [LabelStyle.Body400] = LabelFontFamilyStyle.Body.ConcatenateWithStyle(LabelWeightStyle.FourHundred), 
        [LabelStyle.Body300] = LabelFontFamilyStyle.Body.ConcatenateWithStyle(LabelWeightStyle.ThreeHundred), 
        [LabelStyle.Body200] = LabelFontFamilyStyle.Body.ConcatenateWithStyle(LabelWeightStyle.TwoHundred), 
        [LabelStyle.Body100] = LabelFontFamilyStyle.Body.ConcatenateWithStyle(LabelWeightStyle.OneHundred), 
        
        [LabelStyle.UI400] = LabelFontFamilyStyle.UI.ConcatenateWithStyle(LabelWeightStyle.FourHundred), 
        [LabelStyle.UI300] = LabelFontFamilyStyle.UI.ConcatenateWithStyle(LabelWeightStyle.ThreeHundred), 
        [LabelStyle.UI200] = LabelFontFamilyStyle.UI.ConcatenateWithStyle(LabelWeightStyle.TwoHundred), 
        [LabelStyle.UI100] = LabelFontFamilyStyle.UI.ConcatenateWithStyle(LabelWeightStyle.OneHundred), 

        [LabelStyle.Header1000] = LabelFontFamilyStyle.Header.ConcatenateWithStyle(LabelWeightStyle.OneThousand), 
        [LabelStyle.Header900] = LabelFontFamilyStyle.Header.ConcatenateWithStyle(LabelWeightStyle.NineHundred), 
        [LabelStyle.Header800] = LabelFontFamilyStyle.Header.ConcatenateWithStyle(LabelWeightStyle.EightHundred), 
        [LabelStyle.Header700] = LabelFontFamilyStyle.Header.ConcatenateWithStyle(LabelWeightStyle.SevenHundred), 
        [LabelStyle.Header600] = LabelFontFamilyStyle.Header.ConcatenateWithStyle(LabelWeightStyle.SixHundred), 
        [LabelStyle.Header500] = LabelFontFamilyStyle.Header.ConcatenateWithStyle(LabelWeightStyle.FiveHundred),
        
        [LabelStyle.SectionHeader] = LabelTypeStyle.SectionHeader,
        
        [LabelStyle.KeyOverValue] = LabelTypeStyle.KeyOverValue,
        [LabelStyle.ValueBelowKey] = LabelTypeStyle.ValueBelowKey,
        [LabelStyle.KeyInlineWithValue] = LabelTypeStyle.KeyInlineWithValue,
        [LabelStyle.ValueInlineWithKey] = LabelTypeStyle.ValueInlineWithKey,
        
    };
   
}