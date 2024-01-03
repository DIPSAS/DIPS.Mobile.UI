using DIPS.Mobile.UI.Components.Slidable;

namespace Playground.EirikSamples;

public partial class EirikPage
{
    public EirikPage()
    {
        InitializeComponent();
    }

    private void SlidableContentLayout_OnContentTapped(object sender, ContentTappedEventArgs e)
    {
        Console.WriteLine(e.Index);
        SlidableContentLayout.ScrollTo(e.Index);
    }
}