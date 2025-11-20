using AndroidX.AppCompat.Graphics.Drawable;
using DIPS.Mobile.UI.API.Camera.Preview;
using DIPS.Mobile.UI.API.Camera.Preview.Android.Slider;
using DIPS.Mobile.UI.API.Library;
using DIPS.Mobile.UI.Components.Chips;
using Microsoft.Maui.Handlers;
using Microsoft.Maui.LifecycleEvents;
using Microsoft.Maui.Platform;
using DIPS.Mobile.UI.Components.TabView;
using DIPS.Mobile.UI.Components.TabView.Android;
using ActivityIndicatorHandler = DIPS.Mobile.UI.Components.Loading.Android.ActivityIndicatorHandler;
using Colors = DIPS.Mobile.UI.Resources.Colors.Colors;
using Shell = DIPS.Mobile.UI.Components.Shell.Shell;
using SwitchHandler = DIPS.Mobile.UI.Components.Selection.Android.SwitchHandler;

namespace DIPS.Mobile.UI.API.Builder;

public static partial class AppHostBuilderExtensions
{
    static partial void AddPlatformHandlers(IMauiHandlersCollection handlers)
    {
        handlers
            .AddHandler(typeof(DIPS.Mobile.UI.Components.Searching.Android.IndeterminateProgressBar),
                typeof(DIPS.Mobile.UI.Components.Searching.Android.IndeterminateProgressBarHandler))
            .AddHandler(typeof(Chip), typeof(ChipHandler))
            .AddHandler<CameraZoomSlider, CameraZoomSliderHandler>()
            .AddHandler<CameraPreview, CameraPreviewHandler>()
            .AddHandler<TabView, TabViewHandler>()
            .AddHandler<Switch, SwitchHandler>()
            .AddHandler<ActivityIndicator, ActivityIndicatorHandler>();
        
        ToolbarHandler.Mapper.AppendToMapping<Toolbar, IToolbarHandler>(nameof(Toolbar.ToolbarItems), (h, t) =>
        {
            // TODO: Workaround: .NET MAUI does not set the color on Text toolbar items on Shell pages, so we need to set it manually.
            // TODO: Workaround: .NET MAUI does not set the color on Text and icon toolbar items on Modal pages, so we need to set it manually.
            for (var i = 0; i < h.PlatformView.Menu?.Size(); i++)
            {
                var item = h.PlatformView.Menu.GetItem(i);
                var span = new Android.Text.SpannableString(item?.TitleFormatted);
                span.SetSpan(new Android.Text.Style.ForegroundColorSpan(Colors.GetColor(Shell.ForegroundColorName).ToPlatform()), 0, span.Length(), 0);
                item?.SetTitle(span);
            }
        });
        
        ToolbarHandler.Mapper.AppendToMapping<Toolbar, IToolbarHandler>(nameof(Toolbar.BackButtonVisible), (h, t) =>
        {
            // TODO: Modal Workaround: .NET MAUI does not inherit the color from the Shell, so we need to set it manually.
            if (h.PlatformView.NavigationIcon is DrawerArrowDrawable icon)
            {
                icon.Color = Colors.GetColor(Shell.ForegroundColorName).ToPlatform();
            }
        });
    }

    static partial void ConfigurePlatformLifecycleEvents(ILifecycleBuilder events)
    {
        events.AddAndroid(android => android
            .OnCreate((activity, _) =>
            {
                DUI.Init(activity);
                DUI.EnsureSkLottieResourcesAdded();
                
                // Set status bar and navigation bar colors to match Shell background
                // This ensures proper appearance with edge-to-edge content in .NET 10
                if (activity.Window != null)
                {
                    var shellBackgroundColor = Colors.GetColor(Shell.BackgroundColorName);
                    activity.Window.SetStatusBarColor(shellBackgroundColor.ToPlatform());
                    activity.Window.SetNavigationBarColor(shellBackgroundColor.ToPlatform());
                }
            }));
    }
}

