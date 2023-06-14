using System.Windows.Input;

namespace Components.Helpers;

[ContentProperty(nameof(ContentPageType))]
public class NavigationCommandExtension : IMarkupExtension<ICommand>
{
    public Type? ContentPageType { get; set; }

    public ICommand ProvideValue(IServiceProvider serviceProvider)
    {
        return new Command(() =>
        {
            if (ContentPageType == null)
            {
                return;
            }

            var activatedObject = Activator.CreateInstance(ContentPageType);
            if (activatedObject is not Page page) return;
            if (Shell.Current != null)
            {
                Shell.Current.Navigation.PushAsync(page);
            }
            else if(Application.Current != null && Application.Current.MainPage != null)
            {
                Application.Current.MainPage.Navigation.PushAsync(page);
            }
        });
    }

    object IMarkupExtension.ProvideValue(IServiceProvider serviceProvider)
    {
        return ProvideValue(serviceProvider);
    }
}