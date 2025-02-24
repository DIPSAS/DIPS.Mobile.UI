namespace DIPS.Mobile.UI.Components.Lists;

public class VerticalStackLayout : Microsoft.Maui.Controls.VerticalStackLayout
{
    public VerticalStackLayout()
    {
       Spacing = Sizes.GetSize(SizeName.content_margin_xsmall);
    }
  
}