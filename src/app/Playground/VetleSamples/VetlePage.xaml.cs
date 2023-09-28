using System.Windows.Input;
using DIPS.Mobile.UI.Resources.Icons;

namespace Playground.VetleSamples;

public partial class VetlePage
{
    public VetlePage()
    {
        InitializeComponent();
    }

    protected override void OnSizeAllocated(double width, double height)
    {
        base.OnSizeAllocated(width, height);
        
        
    }

    private void Switch_OnToggled(object sender, ToggledEventArgs e)
    {
        if (e.Value)
        {
            
        }
    }

    
}