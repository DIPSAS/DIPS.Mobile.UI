using DIPS.Mobile.UI.API.Camera.Preview;
using DIPS.Mobile.UI.API.Library;
using DIPS.Mobile.UI.Components.BottomSheets;
using DIPS.Mobile.UI.Components.Chips;
using DIPS.Mobile.UI.Components.ContextMenus;
using DIPS.Mobile.UI.Components.Images.NativeIcon;
using DIPS.Mobile.UI.Components.Navigation.FloatingNavigationButton;
using DIPS.Mobile.UI.Components.Pickers.DateAndTimePicker;
using DIPS.Mobile.UI.Components.Pickers.DatePicker;
using DIPS.Mobile.UI.Components.Pickers.ScrollPicker;
using DIPS.Mobile.UI.Components.TextFields.Editor;
using DIPS.Mobile.UI.Effects.Touch;
using MemoryToolkit.Maui;
using Microsoft.Maui.LifecycleEvents;
using SkiaSharp.Views.Maui.Controls.Hosting;
using ButtonHandler = DIPS.Mobile.UI.Components.Buttons.ButtonHandler;
using Button = DIPS.Mobile.UI.Components.Buttons.Button;
using CameraPreviewHandler = DIPS.Mobile.UI.API.Camera.Preview.CameraPreviewHandler;
using CollectionView = DIPS.Mobile.UI.Components.Lists.CollectionView;
using CollectionViewHandler = DIPS.Mobile.UI.Components.Lists.CollectionViewHandler;
using ContextMenuPlatformEffect = DIPS.Mobile.UI.Components.ContextMenus.ContextMenuPlatformEffect;
using Editor = DIPS.Mobile.UI.Components.TextFields.Editor.Editor;
using Entry = DIPS.Mobile.UI.Components.TextFields.Entry.Entry;
using EntryHandler = DIPS.Mobile.UI.Components.TextFields.Entry.EntryHandler;
using Image = DIPS.Mobile.UI.Components.Images.Image.Image;
using ImageButton = DIPS.Mobile.UI.Components.Images.ImageButton;
using ImageHandler = DIPS.Mobile.UI.Components.Images.Image.ImageHandler;
using Label = DIPS.Mobile.UI.Components.Labels.Label;
using LabelHandler = DIPS.Mobile.UI.Components.Labels.LabelHandler;
using Layout = DIPS.Mobile.UI.Effects.Layout.Layout;
using LayoutPlatformEffect = DIPS.Mobile.UI.Effects.Layout.LayoutPlatformEffect;
using ScrollView = DIPS.Mobile.UI.Components.Lists.ScrollView;
using ScrollViewHandler = DIPS.Mobile.UI.Components.Lists.ScrollViewHandler;
using SearchBar = DIPS.Mobile.UI.Components.Searching.SearchBar;
using SearchBarHandler = DIPS.Mobile.UI.Components.Searching.SearchBarHandler;
using ShellRenderer = DIPS.Mobile.UI.Components.Shell.ShellRenderer;
using TimePickerHandler = DIPS.Mobile.UI.Components.Pickers.TimePicker.TimePickerHandler;

namespace DIPS.Mobile.UI.API.Builder;

public static partial class AppHostBuilderExtensions
{
    // ReSharper disable once IdentifierTypo
    // ReSharper disable once InconsistentNaming
    public static MauiAppBuilder UseDIPSUI(
        this MauiAppBuilder builder, Action<IDIPSUIOptions>? configure = null)
    {
        //Initializers
        DUI.Init();
        
        builder.ConfigureLifecycleEvents(ConfigurePlatformLifecycleEvents);
        
        builder.UseLeakDetection(collectionTarget =>
        {
            // This callback will run any time a leak is detected.
            Console.WriteLine($"❗🧟❗{collectionTarget.Name} is a zombie!");
        });
        
        
        //Handlers
        builder.ConfigureMauiHandlers(handlers =>
        {
            handlers.AddHandler(typeof(Chip), typeof(ChipHandler));
            handlers.AddHandler(typeof(Components.Pickers.DatePicker.DatePicker), typeof(DatePickerHandler));
            handlers.AddHandler(typeof(DateAndTimePicker), typeof(DateAndTimePickerHandler));
            handlers.AddHandler(typeof(Components.Pickers.TimePicker.TimePicker), typeof(TimePickerHandler));
            handlers.AddHandler(typeof(NativeIcon), typeof(NativeIconHandler));
            handlers.AddHandler(typeof(SearchBar), typeof(SearchBarHandler));
            handlers.AddHandler<ImageButton.ImageButton, ImageButton.ImageButtonHandler>();
            handlers.AddHandler<Image, ImageHandler>();
            handlers.AddHandler<Button, ButtonHandler>();
            handlers.AddHandler<Label, LabelHandler>();
            handlers.AddHandler<CollectionView, CollectionViewHandler>();
            handlers.AddHandler<ScrollView, ScrollViewHandler>();
            handlers.AddHandler<FloatingNavigationButton, FloatingNavigationButtonHandler>();
            handlers.AddHandler<Entry, EntryHandler>();
            handlers.AddHandler<Editor, EditorHandler>();
            handlers.AddHandler<BottomSheet, BottomSheetHandler>();
            handlers.AddHandler<CameraPreview, CameraPreviewHandler>();
            handlers.AddHandler<ScrollPicker, ScrollPickerHandler>();
            handlers.AddHandler<Components.Shell.Shell, ShellRenderer>();
            
            AddPlatformHandlers(handlers);
        });
        
        builder.ConfigureEffects(effects =>
        {
            effects.Add(typeof(ContextMenuEffect), typeof(ContextMenuPlatformEffect));
            effects.Add(typeof(Touch), typeof(TouchPlatformEffect));
            effects.Add(typeof(Layout), typeof(LayoutPlatformEffect));
        });

        builder.UseSkiaSharp();

        var options = new DIPSUIOptions();
        configure?.Invoke(options);
        
        return builder;
    }

    static partial void AddPlatformHandlers(IMauiHandlersCollection handlers);
    static partial void ConfigurePlatformLifecycleEvents(ILifecycleBuilder events);

}

