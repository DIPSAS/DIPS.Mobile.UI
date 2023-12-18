using Components.Resources.LocalizedStrings;
using DIPS.Mobile.UI.Components.ListItems;
using DIPS.Mobile.UI.Components.ListItems.Extensions;
using DIPS.Mobile.UI.Components.ListItems.Options.Dividers;
using DIPS.Mobile.UI.Resources.Sizes;

namespace Components;

public class MainPage : DIPS.Mobile.UI.Components.Pages.ContentPage
{
    public MainPage(IEnumerable<SampleType> sampleTypes, List<Sample> samples)
    {
        Title = $"{AppInfo.Current.Name} ({AppInfo.Current.VersionString})";
        Content = new DIPS.Mobile.UI.Components.Lists.CollectionView()
        {
            ItemsSource = sampleTypes, ItemTemplate = new DataTemplate(() => new NavigateToSamplesItem(samples, sampleTypes)),
        };
    }
}

public class NavigateToSamplesItem : NavigationListItem
{
    private readonly List<Sample> m_samples;
    private readonly IEnumerable<SampleType> m_sampleTypes;
    private SampleType m_sampleType;

    public NavigateToSamplesItem(List<Sample> samples, IEnumerable<SampleType> sampleTypes)
    {
        m_samples = samples;
        m_sampleTypes = sampleTypes;
        
        Command = new Command(TryNavigateToSamplesPage);
        
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

            if (m_sampleTypes.First() == sampleType)
                CornerRadius = new CornerRadius(Sizes.GetSize(SizeName.size_2), Sizes.GetSize(SizeName.size_2), 0, 0);
            else
            {
                HasTopDivider = true;
                DividersOptions = new DividersOptions { TopDividerMargin = new Thickness(Sizes.GetSize(SizeName.size_2), 0, 0, 0) };
                CornerRadius = new CornerRadius(0, 0, Sizes.GetSize(SizeName.size_2), Sizes.GetSize(SizeName.size_2));
            }

        }
    }
}