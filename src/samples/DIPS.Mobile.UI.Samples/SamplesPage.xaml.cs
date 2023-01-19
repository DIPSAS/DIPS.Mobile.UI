using System;
using System.Collections.Generic;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Button = DIPS.Mobile.UI.Components.Buttons.Button;

namespace DIPS.Mobile.UI.Samples
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SamplesPage : ContentPage
    {
        public SamplesPage(SampleType sampleType, Dictionary<Page, Sample> samples)
        {
            Title = sampleType.ToString();
            Content = new ListView()
            {
                ItemsSource = samples,
                ItemTemplate = new DataTemplate(() =>
                {
                    return new ViewCell() {View = new NavigateToSingleSampleButton()};
                })
            };
        }
        
        public class NavigateToSingleSampleButton : Button
        {
            private Sample m_sample;
            private Page m_contentPage;


            public NavigateToSingleSampleButton()
            {
                Command = new Command(TryNavigateToSingleSamplePage);
            }

            private void TryNavigateToSingleSamplePage()
            {
                m_contentPage.Title = m_sample.Name;
                Application.Current.MainPage.Navigation.PushAsync(m_contentPage);
            }

            protected override void OnBindingContextChanged()
            {
                base.OnBindingContextChanged();
                if (BindingContext is KeyValuePair<Page, Sample> samplePair)
                {
                    m_sample = samplePair.Value;
                    m_contentPage = samplePair.Key;

                    Text = m_sample.Name;
                }
            }
        }
    }
}