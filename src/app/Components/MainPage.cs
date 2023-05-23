using System.Net.Mime;
using System.Reflection;
using Components.ComponentsSamples;
using Components.Resources.LocalizedStrings;
using Components.ResourcesSamples;
using DIPS.Mobile.UI.Components.ListItems;
using DIPS.Mobile.UI.Resources.Icons;
using DIPS.Mobile.UI.Resources.Sizes;
using DIPS.Mobile.UI.Sizes.Sizes;
using Button = DIPS.Mobile.UI.Components.Buttons.Button;

namespace Components;

public class MainPage : DIPS.Mobile.UI.Components.Pages.ContentPage
{
  
    public MainPage(IEnumerable<SampleType> sampleTypes, List<Sample> samples)
    {
        Padding = Sizes.GetSize(SizeName.size_4);
        Title = LocalizedStrings.Components;
        Content = new DIPS.Mobile.UI.Components.Lists.CollectionView()
        {
            ItemsSource = sampleTypes, ItemTemplate = new DataTemplate(() => new NavigateToSamplesItem(samples)),
        };
    }
}

public class NavigateToSamplesItem : NavigationListItem
{
    private readonly List<Sample> m_samples;
    private SampleType m_sampleType;

    public NavigateToSamplesItem(List<Sample> samples)
    {
        m_samples = samples;
        this.SetBinding(TitleProperty, new Binding(){Path = ""});
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
        }
    }
}