using Components.Resources.LocalizedStrings;
using DIPS.Mobile.UI.API.Library;
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

        if (Current != null)
        {
            //Support dark mode if its enabled in the OS
            if (Current.RequestedTheme == AppTheme.Dark)
            {
                DUI.EnableExperimentalFeature(DUI.ExperimentalFeatures.ForceDarkMode);
            }
        
            Current.RequestedThemeChanged += CurrentOnRequestedThemeChanged;    
        }

        void CurrentOnRequestedThemeChanged(object? sender, AppThemeChangedEventArgs e)
        {
            if (Current?.RequestedTheme == AppTheme.Dark)
            {
                DUI.EnableExperimentalFeature(DUI.ExperimentalFeatures.ForceDarkMode);
            }
            else
            {
                DUI.DisableExperimentalFeature(DUI.ExperimentalFeatures.ForceDarkMode);
            }
        }

        return new Window(shell);
    }
}