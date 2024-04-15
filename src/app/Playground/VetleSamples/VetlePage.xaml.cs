using System.Windows.Input;
using DIPS.Mobile.UI.Components.Navigation.FloatingNavigationButton;
using DIPS.Mobile.UI.Resources.Icons;
using Microsoft.Maui.Controls.Internals;
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

    protected override async void OnAppearing()
    {
        base.OnAppearing();

        await Task.Delay(1000);
        
        ToolbarItems.Add(new ToolbarItem()
        {
            IconImageSource = Icons.GetIcon(IconName.alert_fill)
        });
    }

    public ICommand TestCommand { get; }

    protected override void OnSizeAllocated(double width, double height)
    {
        base.OnSizeAllocated(width, height);
        
        
    }




    private void Switch_OnToggled(object sender, ToggledEventArgs e)
    {
        ShouldHideFloatingNavigationMenuButton = e.Value;
    }


    private void VisualElement_OnSizeChanged(object? sender, EventArgs e)
    {
        
    }

    private void VisualElement_OnMeasureInvalidated(object? sender, EventArgs e)
    {
    }
}