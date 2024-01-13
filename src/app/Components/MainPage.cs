using Components.Resources.LocalizedStrings;
using DIPS.Mobile.UI.Components.ListItems;
using DIPS.Mobile.UI.Components.ListItems.Extensions;
using DIPS.Mobile.UI.Resources.Sizes;

namespace Components;

public class MainPage : DIPS.Mobile.UI.Components.Pages.ContentPage
{

    private CancellationTokenSource? cts;
    public MainPage(IEnumerable<SampleType> sampleTypes, List<Sample> samples)
    {
        Title = $"{AppInfo.Current.Name} ({AppInfo.Current.VersionString})";
        Content = new DIPS.Mobile.UI.Components.Lists.CollectionView()
        {
            ItemsSource = sampleTypes, ItemTemplate = new DataTemplate(() => new NavigateToSamplesItem(samples)),
        };
    }
    
    protected override void OnNavigatedTo(NavigatedToEventArgs args)
    {
        base.OnNavigatedTo(args);
        cts = new CancellationTokenSource();
#if DEBUG
        Task.Run(async () =>
        {
            try
            {
                while (true)
                {
                    cts.Token.ThrowIfCancellationRequested();
                    await Task.Delay(2000);
                    await MainThread.InvokeOnMainThreadAsync(() =>
                    {
                        Console.WriteLine("Force GC");
                        GC.Collect();
                        GC.WaitForPendingFinalizers();
                        Console.WriteLine("Full collection total memory: "+GC.GetTotalMemory(true));
                    });
                }
            }
            catch
            {
            }
        });
#endif
    }
    
    protected override void OnNavigatingFrom(NavigatingFromEventArgs args)
    {
        cts?.Dispose();
        cts = null;
        base.OnNavigatingFrom(args);
    }
}

public class NavigateToSamplesItem : NavigationListItem
{
    private readonly List<Sample> m_samples;
    private SampleType m_sampleType;

    public NavigateToSamplesItem(List<Sample> samples)
    {
        m_samples = samples;
        Command = new Command(TryNavigateToSamplesPage);
        AutoDivider = true;
    }

    private void TryNavigateToSamplesPage()
    {
        var samples = m_samples.Where(sample => sample.Type == m_sampleType).ToList().OrderBy(sample => sample.Name);
        if (!samples.Any())
        {
            Shell.Current.DisplayAlert("No samples",
                $"Theres no samples for {m_sampleType} yet.", "Ok");
        }

        Shell.Current.Navigation.PushAsync((new SamplesPage(m_sampleType, samples)));
    }

    protected override void OnBindingContextChanged()
    {
        base.OnBindingContextChanged();
        if (BindingContext is SampleType sampleType)
        {
            m_sampleType = sampleType;
            Title = sampleType switch
            {
                SampleType.Components => LocalizedStrings.Components,
                SampleType.Resources => LocalizedStrings.Resources,
                _ => "Unknown"
            };
        }
    }
}