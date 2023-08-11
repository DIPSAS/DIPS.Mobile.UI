namespace DIPS.Mobile.UI.Components.ListItems;

public partial class ListItem
{
    [Obsolete("Use InLineContent instead", true)]
    public IView HorizontalContentItem { get; set; }
    
    [Obsolete("Use UnderlyingContent instead", true)]
    public IView VerticalContentItem { get; set; }
    
    [Obsolete("Use HorizontalOptions and VerticalOptions on the Options object. Example: <dui:ListItem.InLineContentOptions><inLineContent:Options HorizontalOptions='End'/></dui:ListItem.InLineContentOptions>", true)]
    public bool ShouldOverrideContentItemLayoutOptions { get; set; }
    
    [Obsolete("Use FontSize on Title Options object instead", true)]
    public int TitleFontSize { get; set; }
    
    [Obsolete("Use FontAttributes on Title Options object instead", true)]
    public FontAttributes TitleFontAttributes { get; set; }
    
    [Obsolete("Use TextColor on Title Options object instead", true)]
    public Color TitleTextColor { get; set; }
    
    [Obsolete("Use FontAttributes on Subtitle Options object instead", true)]
    public FontAttributes SubtitleFontAttributes { get; set; }
    
    [Obsolete("Use Color on Icon Options object instead")]
    public Color? IconColor { get; set; }
    
    [Obsolete("Use IsVisible on Icon Options object instead")]
    public bool IsIconVisible { get; set; }
    
    [Obsolete("Use Width on InLineContent Options object instead")]
    public GridLength HorizontalContentItemColumnWidth { get; set; }
    
    [Obsolete("Use Width on Title Options object instead")]
    public GridLength TitleColumnWidth { get; set; }
 
    [Obsolete("Remove, set the height on the content itself")]
    public GridLength VerticalContentItemRowHeight { get; set; }
    
}