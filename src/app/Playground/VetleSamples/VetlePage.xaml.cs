using System.Windows.Input;
using DIPS.Mobile.UI.Resources.Icons;

namespace Playground.VetleSamples;

public partial class VetlePage
{
    public VetlePage()
    {
        InitializeComponent();
        
        ChangeColor();
    }

    async void ChangeColor()
    {
        await Task.Delay(5000);
        ImageButton.Source = Icons.GetIcon(IconName.patient_info_fill);
    }

    private void Switch_OnToggled(object sender, ToggledEventArgs e)
    {
        if (e.Value)
        {
            
        }
    }
}