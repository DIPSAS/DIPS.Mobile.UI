using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Shell = DIPS.Mobile.UI.Components.Shell.Shell;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]

namespace DIPS.Mobile.UI.Samples
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            var shell = new Shell();
            var tabBar = new TabBar();
            var tab = new Tab();
            tab.Items.Add(new ShellContent(){ContentTemplate = new DataTemplate(() => new MainPage())});
            tabBar.Items.Add(tab);
            shell.Items.Add(tabBar);
            MainPage = shell;
        }
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
            DUI.Library.RemoveViewsLocatedOnTopOfPage();
        }
    }
}