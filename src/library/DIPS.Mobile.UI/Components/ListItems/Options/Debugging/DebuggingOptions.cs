using Colors = Microsoft.Maui.Graphics.Colors;

namespace DIPS.Mobile.UI.Components.ListItems.Options.Debugging;

public partial class DebuggingOptions : ListItemOptions
{
    public override void DoBind(ListItem listItem)
    {
        if(!listItem.IsDebugMode)
            return;

        if (ShouldColorEverything)
        {
            listItem.TitleAndLabelGrid.BackgroundColor = Colors.Red;

            if (listItem.InLineContent is not null)
            {
                ((listItem.InLineContent as View)!).BackgroundColor = Colors.Green;
            }
            
            if (listItem.UnderlyingContent is not null)
            {
                ((listItem.UnderlyingContent as View)!).BackgroundColor = Colors.Blue;
            }

            if (listItem.ImageIcon is not null)
            {
                listItem.ImageIcon.BackgroundColor = Colors.Yellow;
            }
        }
    }
}