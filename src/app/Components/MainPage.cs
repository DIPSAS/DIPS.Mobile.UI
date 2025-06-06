using Components.Resources.LocalizedStrings;
using DIPS.Mobile.UI.Components.ListItems;
using DIPS.Mobile.UI.Components.ListItems.Extensions;
using DIPS.Mobile.UI.Resources.Sizes;

namespace Components;

public class MainPage : DIPS.Mobile.UI.Components.Pages.ContentPage
{
  
    public MainPage(IEnumerable<SampleType> sampleTypes, List<Sample> samples)
    {
        Title = $"{AppInfo.Current.Name} ({AppInfo.Current.VersionString})";
        var collectionView = new DIPS.Mobile.UI.Components.Lists.CollectionView()
        {
            ItemsSource = sampleTypes, 
            ItemTemplate = new DataTemplate(() => new NavigateToSamplesItem(samples)),
            Header = new Grid // Padding at the top
            {
                Padding = new Thickness(0, Sizes.GetSize(SizeName.page_margin_small))
            }
        };
        
        Content = collectionView;
        
        DIPS.Mobile.UI.Effects.Layout.Layout.SetAutoHideLastDivider(collectionView, true);

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
        HasBottomDivider = true;
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