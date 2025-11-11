using Components.Resources.LocalizedStrings;
using Enum = System.Enum;

namespace Components;

public partial class App
{
    public App()
    {
        InitializeComponent();
    }

    protected override Window CreateWindow(IActivationState? activationState)
    {
        var shell = new DIPS.Mobile.UI.Components.Shell.Shell();
        
        var allSamples = REGISTER_YOUR_SAMPLES_HERE.RegisterSamples();
        var tabBar = new TabBar();
        
        foreach (var sampleType in Enum.GetValues<SampleType>())
        {
            var samples = allSamples.Where(s => s.Type == sampleType).OrderBy(s => s.Name);
            var title = sampleType switch
            {
                SampleType.Resources => LocalizedStrings.Resources,
                SampleType.Components => LocalizedStrings.Components,
                SampleType.Accessibility => LocalizedStrings.Accessibility,
                _ => sampleType.ToString()
            };
            
            var tab = new Tab
            {
                Title = title,
                Items =
                {
                    new ShellContent
                    {
                        ContentTemplate = new DataTemplate(() => new SamplesPage(sampleType, samples))
                    }
                }
            };
            
            tabBar.Items.Add(tab);
        }
        
        shell.Items.Add(tabBar);

        return new Window(shell);
    }
}