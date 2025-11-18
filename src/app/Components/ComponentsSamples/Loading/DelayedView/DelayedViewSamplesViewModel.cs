using System.Windows.Input;
using DIPS.Mobile.UI.MVVM;
using Microsoft.Maui.Controls;
using Shell = DIPS.Mobile.UI.Components.Shell.Shell;

namespace Components.ComponentsSamples.Loading.DelayedView;

public class DelayedViewSamplesViewModel : ViewModel
{
    private float m_renderingDelay = DIPS.Mobile.UI.Components.Loading.DelayedView.DelayedView.SecondsUntilRenderProperty.DefaultValue is float defaultValue ? defaultValue : .2f;

    public DelayedViewSamplesViewModel()
    {
        NavigateToSimpleCommand = new Command(async () =>
        {
            var vm = new SimpleContentTestPage(RenderingDelay);
            await Shell.Current.Navigation.PushAsync(vm);
        });

        NavigateToComplexCommand = new Command(async () =>
        {
            var vm = new ComplexLayoutTestPage(RenderingDelay);
            await Shell.Current.Navigation.PushAsync(vm);
        });

        NavigateToCollectionViewCommand = new Command(async () =>
        {
            var vm = new VeryComplexTestPage(RenderingDelay);
            await Shell.Current.Navigation.PushAsync(vm);
        });

        NavigateToSimpleModalCommand = new Command(async () =>
        {
            var page = new SimpleContentTestPage(RenderingDelay);
            var navigationPage = new NavigationPage(page);
            page.ToolbarItems.Add(new ToolbarItem
            {
                Text = "Close",
                Command = new Command(async () => await Shell.Current.Navigation.PopModalAsync())
            });
            await Shell.Current.Navigation.PushModalAsync(navigationPage);
        });

        NavigateToComplexModalCommand = new Command(async () =>
        {
            var page = new ComplexLayoutTestPage(RenderingDelay);
            var navigationPage = new NavigationPage(page);
            page.ToolbarItems.Add(new ToolbarItem
            {
                Text = "Close",
                Command = new Command(async () => await Shell.Current.Navigation.PopModalAsync())
            });
            await Shell.Current.Navigation.PushModalAsync(navigationPage);
        });

        NavigateToCollectionViewModalCommand = new Command(async () =>
        {
            var page = new VeryComplexTestPage(RenderingDelay);
            var navigationPage = new NavigationPage(page);
            page.ToolbarItems.Add(new ToolbarItem
            {
                Text = "Close",
                Command = new Command(async () => await Shell.Current.Navigation.PopModalAsync())
            });
            await Shell.Current.Navigation.PushModalAsync(navigationPage);
        });
    }

    public float RenderingDelay
    {
        get => m_renderingDelay;
        set => RaiseWhenSet(ref m_renderingDelay, value);
    }

    public ICommand NavigateToSimpleCommand { get; }
    public ICommand NavigateToComplexCommand { get; }
    public ICommand NavigateToCollectionViewCommand { get; }
    
    public ICommand NavigateToSimpleModalCommand { get; }
    public ICommand NavigateToComplexModalCommand { get; }
    public ICommand NavigateToCollectionViewModalCommand { get; }
}
