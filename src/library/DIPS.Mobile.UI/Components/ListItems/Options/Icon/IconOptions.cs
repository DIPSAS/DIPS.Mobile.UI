using Image = DIPS.Mobile.UI.Components.Images.Image.Image;

namespace DIPS.Mobile.UI.Components.ListItems.Options.Icon;

public partial class IconOptions : ListItemOptions
{
    
    public override void DoBind(ListItem listItem)
    {
        base.Bind(listItem);
        
        if(listItem.ImageIcon is null)
            return;

        listItem.ImageIcon.SetBinding(View.MarginProperty, new Binding(nameof(Margin), source: this));
        listItem.ImageIcon.SetBinding(Image.TintColorProperty, new Binding(nameof(Color), source: this));
        listItem.ImageIcon.SetBinding(VisualElement.IsVisibleProperty, new Binding(nameof(IsVisible), source: this));
        listItem.ImageIcon.SetBinding(View.VerticalOptionsProperty, new Binding(nameof(VerticalOptions), source: this));
    }

}