using Components.Resources.LocalizedStrings;
using DIPS.Mobile.UI.Components.ListItems;
using DIPS.Mobile.UI.Components.ListItems.Extensions;
using DIPS.Mobile.UI.Components.ListItems.Options.Dividers;
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
                ItemTemplate = new DataTemplate(() => new NavigateToSingleSampleItem(samplePages)),
            };
        }

        public class NavigateToSingleSampleItem : NavigationListItem
        {
            private readonly IEnumerable<Sample> m_samplePages;
            private Sample m_sample;
            

            public NavigateToSingleSampleItem(IEnumerable<Sample> samplePages)
            {
                m_samplePages = samplePages;
                Command = new Command(TryNavigateToSingleSamplePage);
            }

            private void TryNavigateToSingleSamplePage()
            {
                var contentPage = m_sample.PageCreator.Invoke();
                contentPage.Title = m_sample.Name;
                Shell.Current.Navigation.PushAsync(contentPage);
            }

            protected override void OnBindingContextChanged()
            {
                base.OnBindingContextChanged();
                if (BindingContext is Sample sample)
                {
                    m_sample = sample;
                    Title = m_sample.Name;

                    if (m_samplePages.First() == sample)
                    {
                        CornerRadius = new CornerRadius(Sizes.GetSize(SizeName.size_2), Sizes.GetSize(SizeName.size_2), 0, 0);
                    }
                    else if (m_samplePages.Last() == sample)
                    {
                        HasTopDivider = true;
                        DividersOptions = new DividersOptions { TopDividerMargin = new Thickness(Sizes.GetSize(SizeName.size_2), 0, 0, 0) };
                        CornerRadius = new CornerRadius(0, 0, Sizes.GetSize(SizeName.size_2), Sizes.GetSize(SizeName.size_2));
                    }
                    else
                    {
                        HasTopDivider = true;
                        DividersOptions = new DividersOptions { TopDividerMargin = new Thickness(Sizes.GetSize(SizeName.size_2), 0, 0, 0) };
                    }
                }
            }
        }
    }
}