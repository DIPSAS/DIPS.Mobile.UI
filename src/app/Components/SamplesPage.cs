using Components.Resources.LocalizedStrings;
using DIPS.Mobile.UI.Components.ListItems;
using DIPS.Mobile.UI.Components.ListItems.Extensions;
using DIPS.Mobile.UI.Resources.Sizes;

namespace Components
{
    public class SamplesPage : DIPS.Mobile.UI.Components.Pages.ContentPage
    {
        public SamplesPage(SampleType sampleType, IEnumerable<Sample> samplePages)
        {
            Title = sampleType switch
            {
                SampleType.Components => LocalizedStrings.Components,
                SampleType.Resources => LocalizedStrings.Resources,
                _ => "Unknown"
            };
            Content = new DIPS.Mobile.UI.Components.Lists.CollectionView()
            {
                ItemSpacing = 0,
                ItemsSource = samplePages,
                ItemTemplate = new DataTemplate(() => new NavigateToSingleSampleItem()),
            };
        }

        public class NavigateToSingleSampleItem : NavigationListItem
        {
            private Sample m_sample;
            

            public NavigateToSingleSampleItem()
            {
                Command = new Command(TryNavigateToSingleSamplePage);
                AutoDivider = true;
            }

            private void TryNavigateToSingleSamplePage()
            {
                var contentPage = m_sample.PageCreator.Invoke();
                contentPage.Title = m_sample.Name;
                if (m_sample.IsModal)
                {
                    Shell.Current.Navigation.PushModalAsync(new NavigationPage(contentPage));                    
                }
                else
                {
                    Shell.Current.Navigation.PushAsync(contentPage);
                }
            }

            protected override void OnBindingContextChanged()
            {
                base.OnBindingContextChanged();
                if (BindingContext is Sample sample)
                {
                    m_sample = sample;
                    Title = m_sample.Name;
                }
            }
        }
    }
}