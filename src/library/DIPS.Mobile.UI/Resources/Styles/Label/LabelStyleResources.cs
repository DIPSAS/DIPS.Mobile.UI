namespace DIPS.Mobile.UI.Resources.Styles.Label;

internal class LabelStyleResources
{
    public static Dictionary<LabelStyle, Style> Styles { get; } = new()
    {
        [LabelStyle.Body400] = LabelTypeStyle.Body.ConcatenateWithStyle(LabelWeightStyle.FourHundred), 
        [LabelStyle.Body300] = LabelTypeStyle.Body.ConcatenateWithStyle(LabelWeightStyle.ThreeHundred), 
        [LabelStyle.Body200] = LabelTypeStyle.Body.ConcatenateWithStyle(LabelWeightStyle.TwoHundred), 
        [LabelStyle.Body100] = LabelTypeStyle.Body.ConcatenateWithStyle(LabelWeightStyle.OneHundred), 
        
        [LabelStyle.UI400] = LabelTypeStyle.UI.ConcatenateWithStyle(LabelWeightStyle.FourHundred), 
        [LabelStyle.UI300] = LabelTypeStyle.UI.ConcatenateWithStyle(LabelWeightStyle.ThreeHundred), 
        [LabelStyle.UI200] = LabelTypeStyle.UI.ConcatenateWithStyle(LabelWeightStyle.TwoHundred), 
        [LabelStyle.UI100] = LabelTypeStyle.UI.ConcatenateWithStyle(LabelWeightStyle.OneHundred), 

        [LabelStyle.Header1000] = LabelTypeStyle.Header.ConcatenateWithStyle(LabelWeightStyle.OneThousand), 
        [LabelStyle.Header900] = LabelTypeStyle.Header.ConcatenateWithStyle(LabelWeightStyle.NineHundred), 
        [LabelStyle.Header800] = LabelTypeStyle.Header.ConcatenateWithStyle(LabelWeightStyle.EightHundred), 
        [LabelStyle.Header700] = LabelTypeStyle.Header.ConcatenateWithStyle(LabelWeightStyle.SevenHundred), 
        [LabelStyle.Header600] = LabelTypeStyle.Header.ConcatenateWithStyle(LabelWeightStyle.SixHundred), 
        [LabelStyle.Header500] = LabelTypeStyle.Header.ConcatenateWithStyle(LabelWeightStyle.FiveHundred)
    };
   
}