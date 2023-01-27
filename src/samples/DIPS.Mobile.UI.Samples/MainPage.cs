using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Xamarin.Forms;
using Xamarin.Forms.Internals;
using Button = DIPS.Mobile.UI.Components.Buttons.Button;
using Enum = DIPS.Mobile.UI.Extensions.Enum;

namespace DIPS.Mobile.UI.Samples
{
    public class MainPage : DIPS.Mobile.UI.Components.Pages.ContentPage
    {
        public MainPage()
        {
            Title = "Mobile UI Samples";
            var sampleTypes = Enum.ToList<SampleType>();
            Content = new DIPS.Mobile.UI.Components.Lists.ListView()
            {
                ItemsSource = sampleTypes,
                ItemTemplate = new DataTemplate(() => new ViewCell() {View = new NavigateToSamplesButton()})
            };
        }
    }

    public enum SampleType
    {
        Resources,
        Components,
    }

    public class ResourceSample : Sample
    {
        public ResourceSample(string name) : base(name)
        {
        }
    }

    public class ComponentSample : Sample
    {
        public ComponentSample(string name) : base(name)
        {
        }
    }

    public abstract class Sample : Attribute
    {
        protected Sample(string name)
        {
            Name = name;
        }

        public string Name { get; }
    }

    public class NavigateToSamplesButton : Button
    {
        private SampleType m_sampleType;

        public NavigateToSamplesButton()
        {
            SetBinding(TextProperty, new Binding() {Path = ""});
            Command = new Command(TryNavigateToSamplesPage);
        }

        private void TryNavigateToSamplesPage() {
            var samples = new Dictionary<Func<Page>, Sample>();
            switch (m_sampleType)
            {
                case SampleType.Resources:
                    samples = GetSamples<ResourceSample>();
                    break;
                case SampleType.Components:
                    samples = GetSamples<ComponentSample>();
                    break;
            }

            if (samples.Any())
            {
                App.Current.MainPage.Navigation.PushAsync(new SamplesPage(m_sampleType, samples));
            }
            else
            {
                App.Current.MainPage.DisplayAlert("No samples",
                    $"Theres no samples for {m_sampleType.ToString()} yet.", "Ok");
            }
        }

        protected override void OnBindingContextChanged()
        {
            base.OnBindingContextChanged();
            if (BindingContext is SampleType sampleType)
            {
                m_sampleType = sampleType;
            }
        }

        public Dictionary<Func<Page>, Sample> GetSamples<TSample>() where TSample : Sample
        {
            var samples = new Dictionary<Func<Page>, Sample>();
            foreach (var type in Assembly.GetExecutingAssembly().GetTypes())
            {
                if (type.GetCustomAttributes(typeof(TSample), true).Length > 0)
                {
                    var sample = type.GetCustomAttributes(typeof(TSample), true).First() as TSample;
                    samples.Add(() =>
                    {
                        if (Activator.CreateInstance(type) is Page page)
                        {
                            return page;
                        }

                        return null;
                    }, sample);
                }
            }
            return samples;
        }
    }
}