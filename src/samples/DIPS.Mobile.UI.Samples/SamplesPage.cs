using System;
using System.Collections.Generic;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Button = DIPS.Mobile.UI.Components.Buttons.Button;

namespace DIPS.Mobile.UI.Samples
{
    public class SamplesPage : DIPS.Mobile.UI.Components.Pages.ContentPage
    {
        public SamplesPage(SampleType sampleType, Dictionary<Func<Page>, Sample> samples)
        {
            Title = sampleType.ToString();
            Content = new DIPS.Mobile.UI.Components.Lists.ListView()
            {
                ItemsSource = samples,
                ItemTemplate = new DataTemplate(() => new ViewCell() {View = new NavigateToSingleSampleButton()})
            };
        }
        
        public class NavigateToSingleSampleButton : Button
        {
            private Sample m_sample;
            private Func<Page> m_contentPageFunc;


            public NavigateToSingleSampleButton()
            {
                Command = new Command(TryNavigateToSingleSamplePage);
            }

            private void TryNavigateToSingleSamplePage()
            {
                var contentPage = m_contentPageFunc.Invoke();
                contentPage.Title = m_sample.Name;
                Application.Current.MainPage.Navigation.PushAsync(contentPage);
            }

            protected override void OnBindingContextChanged()
            {
                base.OnBindingContextChanged();
                if (BindingContext is KeyValuePair<Func<Page>, Sample> samplePair)
                {
                    m_sample = samplePair.Value;
                    m_contentPageFunc = samplePair.Key;

                    Text = m_sample.Name;
                }
            }
        }
    }
}