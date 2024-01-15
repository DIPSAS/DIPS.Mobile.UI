using Microsoft.Maui.Controls.Shapes;

namespace DIPS.Mobile.UI.Components.Dividers;

public class Divider : ContentView
{
    public Divider()
    {
        this.SetAppThemeColor(BackgroundColorProperty, ColorName.color_neutral_20);
        var divider = new Border() {HeightRequest = 1.2};
        divider.SetBinding(BackgroundProperty, new Binding(nameof(BackgroundColor), source: this));
        Content = divider;
    }

}