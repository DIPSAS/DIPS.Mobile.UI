using System.Windows.Input;
using DIPS.Mobile.UI.Resources.Icons;
using Playground.HåvardSamples;

namespace Playground.VetleSamples;

public partial class VetlePage
{
    public VetlePage()
    {
        InitializeComponent();
        TestCommand = new Command(Test123);
    }

    private void Test123()
    {
    }

    public ICommand TestCommand { get; }

    protected override void OnSizeAllocated(double width, double height)
    {
        base.OnSizeAllocated(width, height);
        
        
    }



    private void Switch_OnToggled(object sender, ToggledEventArgs e)
    {
        if (e.Value)
        {
            Grid.Padding = new Thickness(12, 8, 12, 8);

        }
        else
        {
            Grid.Padding = new Thickness(20, 8, 20, 8);


        }
    }
}