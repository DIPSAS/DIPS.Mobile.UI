using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using NavigationPage = DIPS.Mobile.UI.Components.Pages.NavigationPage;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]

namespace DIPS.Mobile.UI.Samples
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
            NavigationPage = new NavigationPage(new MainPage());
            MainPage = NavigationPage;
        }
        
        public static NavigationPage NavigationPage { get; private set; }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}